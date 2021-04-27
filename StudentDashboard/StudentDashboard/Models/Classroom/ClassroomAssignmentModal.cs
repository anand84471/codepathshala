using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.Models.Classroom
{
    public class ClassroomAssignmentModal
    {
        [JsonProperty("assignment_name")]
        public string m_strAssignmentName;
        [JsonProperty("no_of_submissions")]
        public int m_iNoOfSubmissions;
        [JsonProperty("assignment_id")]
        public long m_llAssignmentId;
        [JsonProperty("creation_date")]
        public string m_strCreationDate;
        public ClassroomAssignmentModal(string AssignmentName,int NoOfSubmissions,long AssignmentId,string CreatiionDate)
        {
            
            this.m_llAssignmentId = AssignmentId;
            this.m_strCreationDate = CreatiionDate;
            this.m_strAssignmentName = AssignmentName; ;
            this.m_iNoOfSubmissions = NoOfSubmissions;
        }
    }
}