using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpResponse
{
    public class SearchCourseHttpResponse:APIDefaultResponse
    {
        [JsonProperty("courses")]
        public List<CourseDetailsModel> lsCourseDetailsModel { get; set; }
    }
}