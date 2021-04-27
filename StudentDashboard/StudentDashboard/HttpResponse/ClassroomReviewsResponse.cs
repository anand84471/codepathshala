using Newtonsoft.Json;
using StudentDashboard.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpResponse
{
    public class ClassroomReviewsResponse
    {
        [JsonProperty("avg_rating")]
        public AvgReviewModel avgReviewModel;
        [JsonProperty("reviews")]
        public List<ReviewModel> lsReviews;
    }
}