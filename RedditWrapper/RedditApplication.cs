using System;
using System.Collections.Generic;
using System.Text;

namespace RedditWrapper
{
    public class RedditApplication
    {
        public string ApplicationName { get; set; }
        public string ApplicationVersion { get; set; }
        public string AuthorUsername { get; set; }
        public RedditClient Client { get; set; }
    }
}
