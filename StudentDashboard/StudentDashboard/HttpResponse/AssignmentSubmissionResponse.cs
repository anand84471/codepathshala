using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpResponse
{
    public class AssignmentSubmissionResponse:APIDefaultResponse
    {
        [JsonProperty("submissions")]
        public List<AssignmentSubmissionResponseModal> m_lsAssignmentSubmissionResponseModal;
    }
}