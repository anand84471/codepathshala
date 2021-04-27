using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpRequest
{
    public class StudentCourseDetailsRequestModel
    {
        [JsonProperty("instructor_id")]
        public int m_iTeacherId { get; set; }
        [JsonProperty("course_id")]
        public long m_llCourseId { get; set; }
    }
}