using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using RedditWrapper.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedditWrapper.Models
{
    public class Me
    {
        [JsonProperty("comment_karma")]
        public int CommentKarma { get; set; }

        [JsonConverter(typeof(UnixTimestampConverter))]
        [JsonProperty("created")]
        public DateTime Created { get; set; }

        [JsonConverter(typeof(UnixTimestampConverter))]
        [JsonProperty("created_utc")]
        public DateTime CreatedUtc { get; set; }

        [JsonProperty("has_mail")]
        public bool HasMail { get; set; }

        [JsonProperty("has_mod_mail")]
        public bool HasModMail { get; set; }

        [JsonProperty("has_verified_email")]
        public bool HasVerifiedEmail { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("is_gold")]
        public bool IsGold { get; set; }

        [JsonProperty("is_mod")]
        public bool IsMod { get; set; }

        [JsonProperty("link_karma")]
        public int LinkKarma { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("over_18")]
        public bool Over18 { get; set; }
    }
}
