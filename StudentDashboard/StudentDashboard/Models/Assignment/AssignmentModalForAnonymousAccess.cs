using Newtonsoft.Json;
using StudentDashboard.HttpResponse;
using System;


namespace StudentDashboard.Models.Assignment
{
    public class AssignmentModalForAnonymousAccess:APIDefaultResponse
    {
        [JsonProperty("assignment_id")]
        public long m_llAssignmentId;
        [JsonProperty("assignment_name")]
        public string m_strAssignmentName;
        [JsonProperty("assignment_description")]
        public string m_strAssignmentDescription;
        [JsonProperty("assignment_creation_date")]
        public string m_strAssignmentCreationDate;
        [JsonProperty("assignment_type")]
        public string m_strAssignmentType;
        [JsonProperty("instructor_name")]
        public string m_strInstructorName;
        [JsonProperty("assignment_expiry_time")]
        public DateTime? m_dtAssignmentExpiryTime;
        [JsonProperty("no_of_questions")]
        public int m_iNoOfQuestions;
        [JsonProperty("total_no_of_submissions")]
        public int m_iNoOfSubmissions;
        public AssignmentModalForAnonymousAccess()
        {

        }
        public AssignmentModalForAnonymousAccess(long AssignmentId, string AssignmentName, string AssignmentDescription, string AssignmentCreationDate, int AssignmentType, string InstructorName,
           int NoOfQuestions, int NoOfSubmissions, DateTime? AssignmentExipryTime)
        {
            this.m_llAssignmentId = AssignmentId;
            this.m_strAssignmentName = AssignmentName;
            this.m_strAssignmentDescription = AssignmentDescription;
            this.m_strAssignmentCreationDate = AssignmentCreationDate;
            switch (AssignmentType)
            {
                case (int)Constants.AssignmentQuestionType.MCQ:
                    {
                        m_strAssignmentType = Constants.ASSIGNMENT_TYPE_MCQ;
                        break;
                    }
                case (int)Constants.AssignmentQuestionType.SUBJECTIVE:
                    {
                        m_strAssignmentType = Constants.ASSIGNMENT_TYPE_SUBJECTIVE;
                        break;
                    }
            }
            this.m_strInstructorName = InstructorName;

            this.m_dtAssignmentExpiryTime = AssignmentExipryTime;
            this.m_iNoOfQuestions = NoOfQuestions;
            this.m_iNoOfSubmissions = NoOfSubmissions;
        }
    }
}