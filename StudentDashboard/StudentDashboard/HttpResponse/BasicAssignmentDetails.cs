using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpResponse
{
    public class BasicAssignmentDetails
    {
        [JsonProperty("assignment_name")]
        public string m_strAssignmentName { get; set; }
        [JsonProperty("assignment_id")]
        public long? m_llAssignmentId { get; set; }
       
        public BasicAssignmentDetails(long AssignmentId,string AssignmentName)
        {
            this.m_strAssignmentName = AssignmentName;
            this.m_llAssignmentId = AssignmentId;
        }
    }
}