using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpResponse
{
    public class AddAssgnmentResponse:APIDefaultResponse
    {
        [JsonProperty("assignment_id")]
        public long m_llAssignmentId { get; set; }
    }
}