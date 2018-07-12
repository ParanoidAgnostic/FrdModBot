using Newtonsoft.Json.Linq;
using RedditWrapper.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using RedditWrapper.Helpers;

namespace RedditWrapper
{
    public class Reddit
    {
        RedditUser user;
        DateTime tokenExpiry = DateTime.MinValue;

        Api redditLoginApi;
        Api redditApi;
        
        public Reddit(RedditApplication application, RedditUser user)
        {
            if (application == null) throw new ArgumentNullException("application");
            RedditClient client = application.Client;
            if (client == null) throw new ArgumentNullException("application.Client");
            this.user = user ?? throw new ArgumentNullException("user");

            string userAgentHeader = String.Format("DotNet:{0}:{1} (by /u/{2})", application.ApplicationName, application.ApplicationVersion, application.AuthorUsername);

            redditLoginApi = new Api("https://www.reddit.com/api/v1/", userAgentHeader);
            redditLoginApi.AuthorizeHeader = String.Format("basic {0}", Convert.ToBase64String(Encoding.UTF8.GetBytes(application.Client.ClientId + ":" + application.Client.ClientSecret)));

            redditApi = new Api("https://oauth.reddit.com/", userAgentHeader);
        }

        public async Task<bool> UpdateToken()
        {
            DateTime requestTime = DateTime.Now;

            AccessTokenResponse accessTokenResponse = await redditLoginApi.Post<AccessTokenResponse>("access_token", new Dictionary<string, string>() {
                {"grant_type", "password"},
                {"username", user.Username },
                {"password", user.Password }
            });
                        
            if(accessTokenResponse!=null)
            {
                tokenExpiry = requestTime.AddSeconds(accessTokenResponse.ExpiresIn - 300);
                redditApi.AuthorizeHeader = String.Format("bearer {0}", accessTokenResponse.AccessToken);
                return true;
            }

            return false;
        }

        public async Task CheckToken()
        {
            if (DateTime.Now > tokenExpiry) await UpdateToken();
        }

        private async Task<JToken> Get(string path)
        {
            await CheckToken();
            return await redditApi.Get(path);
        }

        private async Task<T> Get<T>(string path)
        {
            await CheckToken();
            return await redditApi.Get<T>(path);
        }

        private async Task<Listing> GetListing(string path, ListingParameters parameters)
        {
            StringBuilder pathBuilder = new StringBuilder(path);

            pathBuilder.Append("?show=all");

            if (parameters != null)
            {
                if(!String.IsNullOrEmpty(parameters.After))
                {
                    pathBuilder.AppendFormat("&after={0}", parameters.After);
                }

                if(parameters.Count.HasValue)
                {
                    pathBuilder.AppendFormat("&count={0}", parameters.Count.Value);
                }

                if (parameters.Limit.HasValue)
                {
                    pathBuilder.AppendFormat("&limit={0}", parameters.Limit.Value);
                }
            }

            string pathWithParameters = pathBuilder.ToString();

            await CheckToken();

            Item response = await Get<Item>(pathWithParameters);
                        
            if (response.Kind != ItemKind.Listing)
            {
                throw new Exception(String.Format("Expected Listing. Recieved '{0}' : {1}", response.KindString, response.Kind));
            }

            Listing listing = response.Data.ToObject<Listing>();
            listing.Reddit = this;
            listing.NextPath = path;
            listing.Parameters = parameters;
            return listing;
        }

        public async Task<Listing> ModQueue(string subreddit, ListingParameters parameters=null)
        {
            string path = String.IsNullOrEmpty(subreddit) ? "about/modqueue" : String.Format("r/{0}/about/modqueue", subreddit);

            return await GetListing(path, parameters);
        }

        public async Task<Listing> Next(Listing listing)
        {
            ListingParameters parameters = new ListingParameters
            {
                After = listing.After,
                Count = (listing.Parameters?.Count ?? 0) + listing.Children.Count,
                Limit = listing.Parameters?.Limit
            };
            return await GetListing(listing.NextPath, parameters);
        }

        public async Task<Link> GetLink(string linkId)
        {
            string path = String.Format("/api/info.json?id={0}", linkId);

            await CheckToken();

            Item response = await Get<Item>(path);

            if (response.Kind != ItemKind.Listing)
            {
                throw new Exception(String.Format("Expected Listing. Recieved '{0}' : {1}", response.KindString, response.Kind));
            }

            Listing listing = response.GetListing();

            if (listing.Children.Count != 1)
            {
                throw new Exception(String.Format("Expected Single Child. Recieved {0}", listing.Children.Count));
            }

            Item child = listing.Children.First();

            if (child.Kind != ItemKind.Link)
            {
                throw new Exception(String.Format("Expected Link. Recieved '{0}' : {1}", child.KindString, child.Kind));
            }

            return child.GetLink();
        }

        public async Task<Comment> GetComment(string commentId)
        {
            string path = String.Format("/api/info.json?id={0}", commentId);

            await CheckToken();

            Item response = await Get<Item>(path);

            if (response.Kind != ItemKind.Listing)
            {
                throw new Exception(String.Format("Expected Listing. Recieved '{0}' : {1}", response.KindString, response.Kind));
            }

            Listing listing = response.GetListing();

            if (listing.Children.Count != 1)
            {
                throw new Exception(String.Format("Expected Single Child. Recieved {0}", listing.Children.Count));
            }

            Item child = listing.Children.First();

            if (child.Kind != ItemKind.Comment)
            {
                throw new Exception(String.Format("Expected Comment. Recieved '{0}' : {1}", child.KindString, child.Kind));
            }

            return child.GetComment();
        }

        public async Task<Comment> GetComment(string linkId, string commentId)
        {
            string path = String.Format("/comments/{0}.json?comment={1}", ItemHelpers.GetShortId(linkId), ItemHelpers.GetShortId(commentId));

            await CheckToken();

            Item[] response = await Get<Item[]>(path);
            
            if(response.Length!=2)
            {
                throw new Exception(String.Format("Expected 2 Items. Recieved '{0}'", response.Length));
            }
                        
            if (response[1].Kind != ItemKind.Listing)
            {
                throw new Exception(String.Format("Expected Listing. Recieved '{0}' : {1}", response[1].KindString, response[1].Kind));
            }

            Listing listing = response[1].GetListing();

            if (listing.Children.Count != 1)
            {
                throw new Exception(String.Format("Expected Single Child. Recieved {0}", listing.Children.Count));
            }

            Item child = listing.Children.First();

            if (child.Kind != ItemKind.Comment)
            {
                throw new Exception(String.Format("Expected Comment. Recieved '{0}' : {1}", child.KindString, child.Kind));
            }

            return child.GetComment();
        }
    }
}
