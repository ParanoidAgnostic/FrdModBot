using System;
using System.Collections.Generic;
using System.Text;

namespace RedditWrapper
{
    public class ListingParameters
    {
        public string After { get; set; }
        public int? Limit { get; set; }
        public int? Count { get; set; }
    }
}
