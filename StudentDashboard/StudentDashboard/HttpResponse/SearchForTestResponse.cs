using Newtonsoft.Json;
using StudentDashboard.Models.Course;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpResponse
{
    public class SearchForTestResponse:APIDefaultResponse
    {
        [JsonProperty("test_details")]
        public List<TestDetailsModel> m_lsTestDetailsModel;
    }
}