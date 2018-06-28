using RedditWrapper.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedditWrapper.Models
{
    public enum ItemKind
    {
        [StringValue("Listing")]
        Listing,

        [StringValue("t1")]
        Comment,

        [StringValue("t2")]
        Account,

        [StringValue("t3")]
        Link,

        [StringValue("t4")]
        Message,

        [StringValue("t5")]
        Subreddit,

        [StringValue("t6")]
        Award
    }
}
