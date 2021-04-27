using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.Models.Course
{
    public class AssignmentDetailsModel
    {
        [JsonProperty("assignment_id")]
        public long m_llAssignmentId { get; set; }
        [JsonProperty("creation_date")]
        public string m_strCreationDate { get; set; }
        [JsonProperty("name")]
        public string m_strAssignmentName { get; set; }
        [JsonProperty("type")]
        public string m_strAssignmentType { get; set; }
        [JsonProperty("description")]
        public string m_strAssignmentDescription { get; set; }
        [JsonProperty("no_of_questions")]
        public int m_iNoOfQuestions { get; set; }
        [JsonProperty("no_of_submissions")]
        public int m_iNoOfSubmissions { get; set; }
        [JsonProperty("access_code")]
        public string m_strAccessCode { get; set; }
        public AssignmentDetailsModel(long Id,string Name,string Description,byte AssignmentType,string CreationDate,int NoOfMcqQuestions,int NoOfSubjectiveQuestions,string AccessCode=null)
        {
            this.m_llAssignmentId = Id;
            this.m_strAssignmentName = Name;
            this.m_strAssignmentDescription = Description;
            switch (AssignmentType)
            {
                case (byte)Constants.AssignmentQuestionType.MCQ:
                    {
                        this.m_strAssignmentType = Constants.TEST_TYPE_MCQ;
                        this.m_iNoOfQuestions = NoOfMcqQuestions;
                        break;
                    }
                case (byte)Constants.AssignmentQuestionType.SUBJECTIVE:
                    {
                        this.m_strAssignmentType = Constants.QUESTION_TYPE_SUBJECTIVE;
                        this.m_iNoOfQuestions = NoOfSubjectiveQuestions;
                        break;
                    }
            }
            this.m_strCreationDate = CreationDate;
            this.m_strAccessCode = AccessCode;
        }
        public AssignmentDetailsModel(long Id, string Name, string Description, byte AssignmentType, string CreationDate, int NoOfMcqQuestions, int NoOfSubjectiveQuestions,
            int NoOfSubmissions)
        {
            this.m_llAssignmentId = Id;
            this.m_strAssignmentName = Name;
            this.m_strAssignmentDescription = Description;
            switch (AssignmentType)
            {
                case (byte)Constants.AssignmentQuestionType.MCQ:
                    {
                        this.m_strAssignmentType = Constants.TEST_TYPE_MCQ;
                        this.m_iNoOfQuestions = NoOfMcqQuestions;
                        break;
                    }
                case (byte)Constants.AssignmentQuestionType.SUBJECTIVE:
                    {
                        this.m_strAssignmentType = Constants.QUESTION_TYPE_SUBJECTIVE;
                        this.m_iNoOfQuestions = NoOfSubjectiveQuestions;
                        break;
                    }
            }
            this.m_strCreationDate = CreationDate;
            this.m_iNoOfSubmissions = NoOfSubmissions;
        }
       

    }
}