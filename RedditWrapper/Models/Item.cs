using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RedditWrapper.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedditWrapper.Models
{
    public class Item
    {
        [JsonProperty("kind")]
        public string KindString { get; set; }

        [JsonProperty("data")]
        public JToken Data { get; set; }

        public ItemKind Kind
        {
            get
            {
                return ItemKindHelpers.FromStringValue(KindString);
            }
        }

        public Listing GetListing()
        {
            return Data.ToObject<Listing>();
        }

        public Comment GetComment()
        {
            return Data.ToObject<Comment>();
        }
    }
}
