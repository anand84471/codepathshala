using Newtonsoft.Json;
using StudentDashboard.Models.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpResponse
{
    public class GetTestResultsForStudentResponse:APIDefaultResponse
    {
        [JsonProperty("test_details")]
        public List<StudentTestSearchResultModal> m_lsStudentTestSearchResultModal;
    }
}