using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.Models.Student
{
    public class StudentBasicAssignmentDettails
    {
        [JsonProperty("assignment_name")]
        public string m_strAssignmentName { get; set; }
        [JsonProperty("assignment_id")]
        public long m_llAssignmentId { get; set; }
        [JsonProperty("is_cpmpleted")]
        public bool? m_IsCompleted;

        public StudentBasicAssignmentDettails(long AssignmentId, string AssignmentName,bool? IsCompleted)
        {
            this.m_strAssignmentName = AssignmentName;
            this.m_llAssignmentId = AssignmentId;
            this.m_IsCompleted = IsCompleted;
        }
    }
}