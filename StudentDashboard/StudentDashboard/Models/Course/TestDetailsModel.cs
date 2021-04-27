using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.Models.Course
{
    public class TestDetailsModel
    {
        [JsonProperty("test_id")]
        public long m_llTestId { get; set; }
        [JsonProperty("no_of_questions")]
        public int m_iNoOfQuestions { get; set; }
        [JsonProperty("test_name")]
        public string m_strTestName { get; set; }
        [JsonProperty("test_description")]
        public string m_strTestDescription { get; set; }
        [JsonProperty("creation_date")]
        public string m_strCreationDate { get; set; }
        [JsonProperty("test_type")]
        public string m_strTestType { get; set; }
        [JsonProperty("no_of_submissions")]
        public int m_iNoOfSubmissions;
        [JsonProperty("test_access_code")]
        public string m_strTestAccessCode;
        public TestDetailsModel()
        {

        }
        public TestDetailsModel(long Id ,string Name,string TestDescription,string CreationDate,int NoOfQuestions,byte TestType)
        {
            this.m_strTestName = Name;
            this.m_strTestDescription = TestDescription;
            this.m_iNoOfQuestions = NoOfQuestions;
            this.m_llTestId = Id;
            this.m_strCreationDate = CreationDate;
            switch(TestType)
            {
                case (byte)Constants.TestQuestionType.MCQ:
                    {
                        this.m_strTestType =Constants.TEST_TYPE_MCQ ;
                        break;
                    }
                case (byte)Constants.TestQuestionType.SUBJECTIVE:
                    {
                        this.m_strTestType = Constants.TEST_TYPE_SUBJECTIVE;
                        break;
                    }
            }
        }
        public TestDetailsModel(long Id, string Name, string TestDescription, string CreationDate, int NoOfQuestions, byte TestType,int NoOfSubmissions)
        {
            this.m_strTestName = Name;
            this.m_strTestDescription = TestDescription;
            this.m_iNoOfQuestions = NoOfQuestions;
            this.m_llTestId = Id;
            this.m_strCreationDate = CreationDate;
            switch (TestType)
            {
                case (byte)Constants.TestQuestionType.MCQ:
                    {
                        this.m_strTestType = Constants.TEST_TYPE_MCQ;
                        break;
                    }
                case (byte)Constants.TestQuestionType.SUBJECTIVE:
                    {
                        this.m_strTestType = Constants.TEST_TYPE_SUBJECTIVE;
                        break;
                    }
            }
            this.m_iNoOfSubmissions = NoOfSubmissions;
        }
        public TestDetailsModel(long Id, string Name, string TestDescription, string CreationDate, int NoOfQuestions, byte TestType,string AccessCode)
        {
            this.m_strTestName = Name;
            this.m_strTestDescription = TestDescription;
            this.m_iNoOfQuestions = NoOfQuestions;
            this.m_llTestId = Id;
            this.m_strCreationDate = CreationDate;
            switch (TestType)
            {
                case (byte)Constants.TestQuestionType.MCQ:
                    {
                        this.m_strTestType = Constants.TEST_TYPE_MCQ;
                        break;
                    }
                case (byte)Constants.TestQuestionType.SUBJECTIVE:
                    {
                        this.m_strTestType = Constants.TEST_TYPE_SUBJECTIVE;
                        break;
                    }
            }
            this.m_strTestAccessCode = AccessCode;
        }
    }
}