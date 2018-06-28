using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedditWrapper.Models
{
    public class AccessTokenResponse
    {
        [JsonProperty("access_token")]
        public String AccessToken { get; set; }

        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

        [JsonProperty("scope")]
        public String Scope { get; set; }

        [JsonProperty("token_type")]
        public String TokenType { get; set; }
    }
}
