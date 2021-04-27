using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpResponse
{
    public class AssignmentSubmissionResponseModal
    {
        [JsonProperty("student_name")]
        public string m_strStudentName;
        [JsonProperty("student_id")]
        public long m_llStudentId;
        [JsonProperty("submission_id")]
        public long m_llSubmissionId;
        [JsonProperty("submission_date")]
        public string m_strSubmissionDate;
        [JsonProperty("percentage_score")]
        public int m_iPercentageScore;
        public AssignmentSubmissionResponseModal()
        {

        }
        public AssignmentSubmissionResponseModal(string StudentName,long SubmissionId,long StudentId,string SubmissionDate,int PercentageScore)
        {
            this.m_strStudentName = StudentName;
            this.m_llStudentId = StudentId;
            this.m_llSubmissionId = SubmissionId;
            this.m_strSubmissionDate = SubmissionDate;
            this.m_iPercentageScore = PercentageScore;
        }
    }
}