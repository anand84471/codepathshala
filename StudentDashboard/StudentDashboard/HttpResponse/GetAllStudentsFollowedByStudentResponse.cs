using Newtonsoft.Json;
using StudentDashboard.Models.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpResponse
{
    public class GetAllStudentsFollowedByStudentResponse:APIDefaultResponse
    {
        [JsonProperty("students")]
        public List<StudentFollowedByStudentDetails> m_lsStudentFollowedByStudentDetails;
    }
}