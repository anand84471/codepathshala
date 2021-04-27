using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpResponse
{
    public class SubmitAssignmentResponse:APIDefaultResponse
    {
        [JsonProperty("submission_id")]
        public long m_llSubmissionId;
    }
}