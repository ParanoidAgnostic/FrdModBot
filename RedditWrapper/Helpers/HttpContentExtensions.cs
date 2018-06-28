using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace RedditWrapper.Helpers
{
    public static class HttpContentExtensions
    {
        public static async Task<T> ReadAsJsonAsync<T>(this HttpContent content)
        {
            string json = await content.ReadAsStringAsync();
            T value = JsonConvert.DeserializeObject<T>(json);
            return value;
        }

        public static async Task<JToken> ReadAsJToken(this HttpContent content)
        {
            string json = await content.ReadAsStringAsync();
            JToken value = JToken.Parse(json);
            return value;
        }
    }
}
