using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpResponse
{
    public class AssignmentsSubmissionModal:APIDefaultResponse
    {
        [JsonProperty("submission_id")]
        public long m_lsSubmissionId;
        [JsonProperty("total_no_questions")]
        public int m_iNoOfQuestions;
        [JsonProperty("submission_date")]
        public string m_strSubmissionDate;
        [JsonProperty("percentage_score")]
        public int m_iAssignmentScore;
        [JsonProperty("assignment_name")]
        public string m_strAssignmentName;
        public AssignmentsSubmissionModal(long SubmissionId,int TotalNoOfQuestions,string SubmissionDate,int PercentageScore,string AssignmentName)
        {
            this.m_lsSubmissionId = SubmissionId;
            this.m_iNoOfQuestions = TotalNoOfQuestions;
            this.m_strSubmissionDate = SubmissionDate;
            this.m_iAssignmentScore = PercentageScore;
            this.m_strAssignmentName = AssignmentName;
        }
    }
}