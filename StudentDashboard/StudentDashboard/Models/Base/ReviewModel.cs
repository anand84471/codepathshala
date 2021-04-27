using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.Models.Base
{
    public class ReviewModel
    {
        [JsonProperty("user_id")]
        public long m_llUserId;
        [JsonProperty("name")]
        public string m_strName;
        [JsonProperty("feedback")]
        public string m_strFeedback;
        [JsonProperty("date")]
        public string m_strReviewDate;
        [JsonProperty("profile_url")]
        public string m_strProfileUrl;
        [JsonProperty("no_of_ratings")]
        public int m_iNoOfRatings;
        public ReviewModel(int NoOfRatings,string ProfileUrl, long UserId, string Name,string Feedback,DateTime? date)
        {
            this.m_iNoOfRatings = NoOfRatings;
            this.m_strProfileUrl = ProfileUrl;
            this.m_llUserId = UserId;
            this.m_strName = Name;
            this.m_strFeedback = Feedback;
            if (date != null)
            {
                this.m_strReviewDate = ((DateTime)date).ToString("dd MMM yyy");
            }
        }
    }
}