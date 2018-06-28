using Microsoft.Extensions.Configuration;
using RedditWrapper;
using RedditWrapper.Models;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace FrdModBot
{
    class Program
    {
        static void Main(string[] args)
        {
            AsyncMain().Wait();
        }

        public static async Task AsyncMain()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("config.json");

            IConfigurationRoot securityConfiguration = builder.Build();

            RedditApplication application = new RedditApplication()
            {
                ApplicationName = "FrdModBot",
                ApplicationVersion = "0.1",
                AuthorUsername = "ParanoidAgnostic",
                Client = new RedditClient()
                {
                    ClientId = securityConfiguration["client:id"],
                    ClientSecret = securityConfiguration["client:secret"]
                }
            };

            RedditUser user = new RedditUser()
            {
                Username = securityConfiguration["user:username"],
                Password = securityConfiguration["user:password"]
            };

            string subreddit = securityConfiguration["subreddit"];

            Reddit reddit = new Reddit(application, user);

            ListingReader modQueue = new ListingReader(await reddit.ModQueue(subreddit));

            List<Comment> comments = new List<Comment>();

            modQueue.CommentHandler = comment =>
            {
                comments.Add(comment);
            };
                        
            await modQueue.Handle();
            
            return;
        }
    }
}
