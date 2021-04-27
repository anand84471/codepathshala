using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.Models.Student
{
    public class StudentClassroomTestModal
    {
        [JsonProperty("test_id")]
        public long m_llTestId;
        [JsonProperty("test_name")]
        public string m_strTestName;
        [JsonProperty("test_description")]
        public string m_strTestDescription;
        [JsonProperty("score")]
        public int m_iTestScore;
        [JsonProperty("creation_date")]
        public string m_strTestCreationDate;
        [JsonProperty("submission_date")]
        public string m_strTestSubmissionDate;
        [JsonProperty("no_of_questions")]
        public int m_iNoOfQuestions;
        [JsonProperty("test_type")]
        public string m_strTestType;
        [JsonProperty("is_submitted")]
        public bool m_bIsSubmitted;
        [JsonProperty("submission_id")]
        public long? m_llSubmissionId;
        [JsonProperty("access_code")]
        public string m_strAccessCode;
        public StudentClassroomTestModal(long TestId,long? SubmissionId,string TestName,string TestDescription,string CreationDate,int NoOfQuestions,
            string TestType,string AccessCode)
        {
            this.m_llTestId = TestId;
            this.m_llSubmissionId = SubmissionId;
            this.m_strTestName = TestName;
            this.m_strTestDescription = TestDescription;
            this.m_strTestCreationDate = CreationDate;
            this.m_iNoOfQuestions = NoOfQuestions;
            this.m_bIsSubmitted = this.m_llSubmissionId == null ? false : true;
            this.m_strTestType = TestType;
            this.m_strAccessCode = AccessCode;
        }
        public StudentClassroomTestModal(long SubmissionId,string TestName,string SubmissionDate,int PercentageScore,int TotalNoOfQuestions)
        {
            this.m_llSubmissionId = SubmissionId;
            this.m_strTestName = TestName;
            this.m_strTestSubmissionDate = SubmissionDate;
            this.m_iTestScore = PercentageScore;
            this.m_iNoOfQuestions = TotalNoOfQuestions;
        }
    }
}