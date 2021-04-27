using Newtonsoft.Json;
using StudentDashboard.Views.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpResponse
{
    public class TestSubmissionOfStudentResponse:APIDefaultResponse
    {
        [JsonProperty("test_submissions")]
        public List<TestSubmissionModal> m_lsTestSubmissionModal;
    }
}