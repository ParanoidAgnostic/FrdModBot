using Newtonsoft.Json.Linq;
using RedditWrapper.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RedditWrapper
{
    public class ListingReader
    {
        SemaphoreSlim mutex = new SemaphoreSlim(1);
        private int index = -1;
        private Listing listing;

        public ListingReader(Listing listing)
        {
            this.listing = listing;
        }

        public Action<Comment> CommentHandler { get; set; }

        public Action<Link> LinkHandler { get; set; }

        public async Task<bool> MoveNext()
        {
            await mutex.WaitAsync().ConfigureAwait(false);
            try {
                return await ThreadUnsafeMoveNext();
            }
            finally
            {
                mutex.Release();
            }       
        }

        public async Task<Item> GetCurrent()
        {
            await mutex.WaitAsync().ConfigureAwait(false);
            try
            {
                return ThreadUnsafeGetCurrent();
            }
            finally
            {
                mutex.Release();
            }
        }

        private async Task<bool> ThreadUnsafeMoveNext()
        {
            index++;
            if (index < listing.Children.Count) return true;
            if (listing.HasNext)
            {
                listing = await listing.Next();
                index = 0;
                return true;
            }
            return false;
        }

        private Item ThreadUnsafeGetCurrent()
        {
            return listing.Children[index];
        } 

        public async Task ForEach(Action<Item> action, Func<bool> condition=null)
        {
            await mutex.WaitAsync().ConfigureAwait(false);
            try
            {
                while((condition==null || condition()) && await ThreadUnsafeMoveNext())
                {
                    Item item = ThreadUnsafeGetCurrent();
                    action(item);
                }
            }
            finally
            {
                mutex.Release();
            }
        }

        public async Task Handle(Func<bool> condition=null)
        {
            await ForEach(i =>
            {
                switch (i.Kind)
                {
                    case ItemKind.Comment:
                        CommentHandler?.Invoke(i.GetComment());
                        break;
                    case ItemKind.Link:
                        LinkHandler?.Invoke(i.GetLink());
                        break;
                }
            }, condition);
        }
    }
}
