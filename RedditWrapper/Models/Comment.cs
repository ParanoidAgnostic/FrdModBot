using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RedditWrapper.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RedditWrapper.Models
{
    public class Comment
    {
        [JsonProperty("subreddit_id")]
        public string SubredditId { get; set; }

        [JsonConverter(typeof(UnixTimestampConverter))]
        [JsonProperty("approved_at_utc")]
        public DateTime? ApprovedAtUtc { get; set; }

        [JsonProperty("ups")]
        public int Ups { get; set; }

        [JsonProperty("link_title")]
        public string LinkTitle { get; set; }

        [JsonProperty("mod_reason_by")]
        public string ModReasonBy { get; set; }

        [JsonProperty("banned_by")]
        public string BannedBy { get; set; }

        [JsonProperty("author_flair_type")]
        public string AuthorFlairType { get; set; }

        [JsonProperty("removal_reason")]
        public string RemovalReason { get; set; }

        [JsonProperty("link_id")]
        public string LinkId { get; set; }

        [JsonProperty("author_flair_template_id")]
        public string AuthorFlairTemplateId { get; set; }

        [JsonProperty("likes")]
        public bool? Likes { get; set; }

        [JsonProperty("replies")]
        public Item Replies { get; set; }

        [JsonProperty("user_reports")]
        public List<JArray> UserReportsRaw { get; set; }
                
        [JsonProperty("saved")]
        public bool Saved { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonConverter(typeof(UnixTimestampConverter))]
        [JsonProperty("banned_at_utc")]
        public DateTime BannedAtUtc { get; set; }

        [JsonProperty("gilded")]
        public int Gilded { get; set; }

        [JsonProperty("archived")]
        public bool Archived { get; set; }

        [JsonProperty("no_follow")]
        public bool NoFollow { get; set; }

        [JsonProperty("author")]
        public string Author { get; set; }

        [JsonProperty("num_comments")]
        public int NumComments { get; set; }

        [JsonProperty("can_mod_post")]
        public bool CanModPost { get; set; }

        [JsonProperty("send_replies")]
        public bool SendReplies { get; set; }

        [JsonProperty("parent_id")]
        public string ParentId { get; set; }

        [JsonProperty("score")]
        public int Score { get; set; }

        [JsonProperty("approved_by")]
        public string ApprovedBy { get; set; }

        [JsonProperty("over_18")]
        public bool Over18 { get; set; }

        [JsonProperty("ignore_reports")]
        public bool IgnoreReports { get; set; }

        [JsonProperty("downs")]
        public int Downs { get; set; }

        [JsonProperty("body")]
        public string Body { get; set; }

        [JsonProperty("edited")]
        public bool Edited { get; set; }

        [JsonProperty("author_flair_css_class")]
        public string AuthorFlairCssClass { get; set; }

        [JsonProperty("collapsed")]
        public bool Collapsed { get; set; }

        [JsonProperty("is_submitter")]
        public bool IsSubmitter { get; set; }

        [JsonProperty("collapsed_reason")]
        public string CollapsedReason { get; set; }

        [JsonProperty("body_html")]
        public string BodyHtml { get; set; }

        [JsonProperty("spam")]
        public bool Spam { get; set; }

        [JsonProperty("stickied")]
        public bool Stickied { get; set; }

        [JsonProperty("subreddit_type")]
        public string SubredditType { get; set; }

        [JsonProperty("can_gild")]
        public bool CanGild { get; set; }

        [JsonProperty("removed")]
        public bool Removed { get; set; }

        [JsonProperty("approved")]
        public bool Approved { get; set; }

        [JsonProperty("score_hidden")]
        public bool ScoreHidden { get; set; }

        [JsonProperty("permalink")]
        public string Permalink { get; set; }

        [JsonProperty("num_reports")]
        public int NumReports { get; set; }

        [JsonProperty("link_permalink")]
        public string LinkPermalink { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("link_author")]
        public string LinkAuthor { get; set; }

        [JsonProperty("subreddit")]
        public string Subreddit { get; set; }

        [JsonProperty("author_flair_text")]
        public object AuthorFlairText { get; set; }

        [JsonProperty("link_url")]
        public string LinkUrl { get; set; }

        [JsonConverter(typeof(UnixTimestampConverter))]
        [JsonProperty("created")]
        public DateTime Created { get; set; }

        [JsonConverter(typeof(UnixTimestampConverter))]
        [JsonProperty("created_utc")]
        public DateTime CreatedUtc { get; set; }

        [JsonProperty("subreddit_name_prefixed")]
        public string SubredditNamePrefixed { get; set; }

        [JsonProperty("controversiality")]
        public int Controversiality { get; set; }

        [JsonProperty("rte_mode")]
        public string RteMode { get; set; }

        [JsonProperty("mod_reports")]
        public List<JArray> ModReportsRaw { get; set; }

        [JsonProperty("quarantine")]
        public bool Quarantine { get; set; }

        [JsonProperty("mod_note")]
        public object ModNote { get; set; }

        [JsonProperty("distinguished")]
        public string Distinguished { get; set; }

        private List<UserReport> userReports = null;
        private List<ModReport> modReports = null;

        public List<UserReport> UserReports
        {
            get
            {
                return userReports ?? (userReports = UserReportsRaw.Select(r => new UserReport()
                {
                    Reason = r[0].ToObject<string>(),
                    Count = r[1].ToObject<int>()
                }).ToList());
            }
        }

        public List<ModReport> ModReports
        {
            get
            {
                return modReports ?? (modReports = ModReportsRaw.Select(r => new ModReport()
                {
                    Reason = r[0].ToObject<string>(),
                    UserName = r[1].ToObject<string>()
                }).ToList());
            }
        }
    }
}
