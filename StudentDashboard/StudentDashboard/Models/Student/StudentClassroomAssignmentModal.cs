using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.Models.Student
{
    public class StudentClassroomAssignmentModal
    {
        [JsonProperty("assignment_id")]
        public long m_llAssignmentId;
        [JsonProperty("student_id")]
        public long m_llStudentId;
        [JsonProperty("submission_id")]
        public long? m_llSubmissionid;
        [JsonProperty("is_submitted")]
        public bool m_bIsSubmitted;
        [JsonProperty("assignment_name")]
        public string m_strAssignmentName;
        [JsonProperty("creation_date")]
        public string m_strCreationDate;
        [JsonProperty("score")]
        public int? m_iScore;
        [JsonProperty("assignment_type")]
        public string m_strAssignmentType;
        [JsonProperty("no_of_questions")]
        public int m_iNoOfQuestions;
        [JsonProperty("access_code")]
        public string m_strAccessCode;
        [JsonProperty("submission_date")]
        public string m_strSubmissionDate;
        public StudentClassroomAssignmentModal(long AssignmentId,string AssignmentName,string CreationDate,long? SubmissionId,int? PercentageScore, string
            AssignmentType,int NoOfQuestions,string AccessCode)
        {
            this.m_llAssignmentId = AssignmentId;
            this.m_strAssignmentName = AssignmentName;
            this.m_iScore = PercentageScore;
            this.m_strCreationDate = CreationDate;
            this.m_llSubmissionid = SubmissionId;
            this.m_bIsSubmitted = SubmissionId == null ? false : true;
            this.m_iNoOfQuestions = NoOfQuestions;
            this.m_strAssignmentType = AssignmentType;
            this.m_iNoOfQuestions = NoOfQuestions;
            this.m_strAccessCode = AccessCode;
        }
        public StudentClassroomAssignmentModal(string AssignmentName,long? SubmissionId,string SubmissionsDate, int? PercentageScore,int TotalQuestions)
        {
            this.m_strAssignmentName = AssignmentName;
            this.m_llSubmissionid = SubmissionId;
            this.m_strSubmissionDate = SubmissionsDate;
            this.m_iScore = PercentageScore;
            this.m_iNoOfQuestions = TotalQuestions;
        }
    }
}