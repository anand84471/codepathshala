using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpResponse
{
    public class CourseJoinedResponse:APIDefaultResponse
    {
        [JsonProperty("students")]
        public List<CoursesJoinedResponseModal> m_lsStudentsJoined;

    }
}