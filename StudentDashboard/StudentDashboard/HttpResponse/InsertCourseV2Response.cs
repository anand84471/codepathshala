using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpResponse
{
    public class InsertCourseV2Response:APIDefaultResponse
    {
        [JsonProperty("course_id")]
        public long m_llCourseId;
    }
}