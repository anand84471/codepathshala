using Newtonsoft.Json;
using StudentDashboard.Models.Course;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpResponse
{
    public class AllInstructorTestsApiResponse:APIDefaultResponse
    {
        [JsonProperty("tests")]
        public List<TestDetailsModel> m_lsTestDetailsModel { get; set; }

    }
}