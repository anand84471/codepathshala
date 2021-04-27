using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.Models.Base
{
    public class AvgReviewModel
    {
        [JsonProperty("star_1")]
        public float m_fPercentage1StartRating;
        [JsonProperty("star_2")]
        public float m_fPercentage2StartRating;
        [JsonProperty("star_3")]
        public float m_fPercentage3StartRating;
        [JsonProperty("star_4")]
        public float m_fPercentage4StartRating;
        [JsonProperty("star_5")]
        public float m_fPercentage5StartRating;
        [JsonProperty("total_reviews")]
        public int m_iTotalReviews;
        [JsonProperty("avg_rating")]
        public float m_fAvgRating;
    }
}