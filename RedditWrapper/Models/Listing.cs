using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RedditWrapper.Models
{
    public class Listing
    {
        [JsonProperty("after")]
        public string After { get; set; }

        [JsonProperty("before")]
        public string Before { get; set; }

        [JsonProperty("children")]
        public List<Item> Children { get; set; }

        public ListingParameters Parameters { get; set; }
        public string NextPath { get; set; }
        public Reddit Reddit { get; set; }

        public bool HasNext
        {
            get
            {
                return !String.IsNullOrEmpty(After);
            }
        }

        public async Task<Listing> Next()
        {
            return await Reddit.Next(this);
        }
    }
}
