using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpResponse
{
    public class AssignmentSubmissionOfStudnetResponse:APIDefaultResponse
    {
        [JsonProperty("assignment_submissions")]
        public List<AssignmentsSubmissionModal> m_lsOfStudentResponse;
    }
}