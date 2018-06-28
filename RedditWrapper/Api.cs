using Newtonsoft.Json.Linq;
using RedditWrapper.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RedditWrapper
{
    public class Api
    {
        private HttpClient client;

        public Api(string baseUrl, string userAgent = null)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            if (!String.IsNullOrEmpty(userAgent))
                client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", userAgent);
        }

        public string AuthorizeHeader
        {
            get
            {
                return client.DefaultRequestHeaders.GetValues("Authorization").FirstOrDefault();
            }
            set
            {
                client.DefaultRequestHeaders.Remove("Authorization");
                if(!String.IsNullOrWhiteSpace(value))
                    client.DefaultRequestHeaders.Add("Authorization", value);
            }
        }

        public async Task<TReceive> Post<TReceive>(string path, Dictionary<string, string> data)
        {
            string contentString = String.Join('&',data.Select(kvp=>String.Format("{0}={1}",kvp.Key,kvp.Value)));
            HttpContent content = new StringContent(contentString, Encoding.UTF8, "application/x-www-form-urlencoded");
            HttpResponseMessage response = await client.PostAsync(path, content);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsJsonAsync<TReceive>();
            }

            string result = await response.Content.ReadAsStringAsync();

            throw new ApiException(response.StatusCode);
        }

        public async Task<JToken> Get(string path)
        {
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsJToken();
            }

            JToken result = await response.Content.ReadAsJToken();

            throw new ApiException(response.StatusCode);
        }

        public async Task<TReceive> Get<TReceive>(string path)
        {
            JToken response = await Get(path);
            TReceive result = response.ToObject<TReceive>();
            return result;
        }

        public async Task<TReceive> Post<TSend, TReceive>(string path, TSend data)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(path, data);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsJsonAsync<TReceive>();
            }
            throw new ApiException(response.StatusCode);
        }


    }
}
