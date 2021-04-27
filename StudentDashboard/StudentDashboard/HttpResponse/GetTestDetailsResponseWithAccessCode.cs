using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpResponse
{
    public class GetTestDetailsResponseWithAccessCode:APIDefaultResponse
    {
        [JsonProperty("test_id")]
        public long m_llTestId;
        [JsonProperty("test_name")]
        public string m_strTestName;
        [JsonProperty("test_description")]
        public string m_strTestDescription;
        [JsonProperty("test_creation_date")]
        public string m_strTestCreationDate;
        [JsonProperty("test_type")]
        public string m_strTestType;
        [JsonProperty("instructor_name")]
        public string m_strInstructorName;
        [JsonProperty("time_for_test_in_seconds")]
        public int m_iTimeForTestInSeconds;
        [JsonProperty("max_marks")]
        public int m_iMaxMarks;
        [JsonProperty("test_expiry_time")]
        public DateTime? m_dtTestExpiryTime;
        [JsonProperty("no_of_questions")]
        public int m_iNoOfQuestions;
        [JsonProperty("total_no_of_submissions")]
        public int m_iNoOfSubmissions;
        public GetTestDetailsResponseWithAccessCode()
        {

        }
        public GetTestDetailsResponseWithAccessCode(long TestId, string TestName, string TestDescription, string TestCreationDate, int TestType, string InstructorName,
            int TimeForTest, int Marsks, int NoOfQuestions, int NoOfSubmissions, DateTime? TestExipryTime)
        {
            this.m_llTestId = TestId;
            this.m_strTestName = TestName;
            this.m_strTestDescription = TestDescription;
            this.m_strTestCreationDate = TestCreationDate;
            switch (TestType)
            {
                case (int)Constants.TestQuestionType.MCQ:
                    {
                        m_strTestType = Constants.TEST_TYPE_MCQ;
                        break;
                    }
                case (int)Constants.TestQuestionType.SUBJECTIVE:
                    {
                        m_strTestType = Constants.TEST_TYPE_MCQ;
                        break;
                    }
            }
            this.m_strInstructorName = InstructorName;
            this.m_iTimeForTestInSeconds = TimeForTest;
            this.m_iMaxMarks = Marsks;
            this.m_dtTestExpiryTime = TestExipryTime;
            this.m_iNoOfQuestions = NoOfQuestions;
            this.m_iNoOfSubmissions = NoOfSubmissions;
        }
    }
}