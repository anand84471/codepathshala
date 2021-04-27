using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.Models.Student
{
    public class StudentTestSearchResultModal
    {
        [JsonProperty("test_name")]
        public string m_strTestName;
        [JsonProperty("test_id")]
        public long m_llTestId;
        [JsonProperty("test_access_code")]
        public string m_strTestAccessCode;
        [JsonProperty("test_description")]
        public string m_strTestDescription;
        [JsonProperty("is_submitted")]
        public bool m_bIsSubmitted;
        [JsonProperty("test_creation_date")]
        public string m_strTestCreationDate;
        [JsonProperty("test_submission_date")]
        public string m_strTestSubmissionDate;
        [JsonProperty("test_submission_id")]
        public long? m_iSubmissionId;
        [JsonProperty("no_of_questions")]
        public int m_iNoOfQuestions;
        [JsonProperty("total_time_for_test")]
        public int? m_iTotalTimeFoTest;
        public StudentTestSearchResultModal(long TestId,string TestName,string TestAccessCode,string TestDescription,
            string TestCreationDate,
            long? TestSubmissionId,string TestSubmissionDate,int NoOfQuestions,int? TotalTimeForTest)
        {
            this.m_strTestName = TestName;
            this.m_strTestDescription = TestDescription;
            this.m_strTestAccessCode = TestAccessCode;
            this.m_strTestCreationDate = TestCreationDate;
            this.m_iSubmissionId = TestSubmissionId;
            this.m_strTestSubmissionDate = TestSubmissionDate;
            if (this.m_iSubmissionId != null)
            {
                this.m_bIsSubmitted = true;
            }
            this.m_iNoOfQuestions = NoOfQuestions;
            this.m_iTotalTimeFoTest = TotalTimeForTest;
            this.m_llTestId = TestId;
        }
    }
}