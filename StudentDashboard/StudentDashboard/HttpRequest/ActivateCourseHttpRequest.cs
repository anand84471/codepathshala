using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpRequest
{
    public class ActivateCourseHttpRequest
    {
        [JsonProperty("course_id")]
        public long m_llCourseId;
        [JsonProperty("course_fee")]
        public int m_iCourseFee;
        [JsonIgnore]
        public int m_iInstructorId;
    }
}