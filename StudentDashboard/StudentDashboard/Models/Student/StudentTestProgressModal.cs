using Newtonsoft.Json;
using StudentDashboard.HttpResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.Models.Student
{
    public class StudentTestProgressModal:APIDefaultResponse
    {
        [JsonProperty("test_id")]
        public long m_llTestId;
        [JsonProperty("test_name")]
        public string m_strTestName;
        [JsonProperty("test_description")]
        public string m_strTestDescription;
        [JsonProperty("total_no_of_questions")]
        public int m_iTotalNoOfQuestions;
        [JsonProperty("marks")]
        public int m_iTotalMarks;
        [JsonProperty("max_time_allowed_for_test")]
        public int m_iTotalTimeForTestInSeconds;
        [JsonProperty("submission_id")]
        public long? m_llSubmissionId;
        public StudentTestProgressModal(long TestId,string TestName,string TestDescription,int TotalNoOfQuestions,
            int TimeAllowedForTest,int Marks,long? SubmissionId)
        {
            this.m_llTestId = TestId;
            this.m_strTestName = TestName;
            this.m_strTestDescription = TestDescription;
            this.m_iTotalNoOfQuestions = TotalNoOfQuestions;
            this.m_iTotalTimeForTestInSeconds = TimeAllowedForTest;
            this.m_llSubmissionId = SubmissionId;
            this.m_iTotalMarks = Marks;
        }
    }
}