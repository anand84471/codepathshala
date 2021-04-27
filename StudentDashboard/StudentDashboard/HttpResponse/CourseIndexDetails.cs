using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpResponse
{
    public class CourseIndexDetails
    {
        [JsonProperty("index_id")]
        public long m_llIndexId { get; set; }
        [JsonProperty("index_name")]
        public string m_strIndexName { get; set; }
        [JsonProperty("index_description")]
        public string m_strIndexDescription { get; set; }
        [JsonProperty("insertion_date")]
        public string m_strRowInsertionDate { get; set; }
        [JsonProperty("updation_date")]
        public string m_strRowUpdationDate { get; set; }
        [JsonProperty("assignment_name")]
        public string m_strAssignmentName { get; set; }
        [JsonProperty("assignment_type")]
        public string m_strAssignmentType { get; set; }
        public long? m_llAssignmentId { get; set; }
        [JsonProperty("test_name")]
        public string m_strTestName { get; set; }
        public string m_strTestDescription { get; set; }
        [JsonProperty("test_type")]
        public string m_strTestType { get; set; }
        public long? m_llTestId { get; set; }
        [JsonProperty("topic_count")]
        public int m_iTotalNoOfTopic { get; set; }
        public CourseIndexDetails(string IndexName,string IndexDescription,string IndexInDate,string IndexUpDate,
                                  string AssignmentName,byte? AssignmentType,string TestName,
                                  byte? TestType,int TotalNoOfTopic,long IndexId,long? AssignmentId,long? TestId)
        {
            this.m_strIndexName = IndexName;
            this.m_strIndexDescription = IndexDescription;
            this.m_strRowInsertionDate = IndexInDate;
            this.m_strRowUpdationDate = IndexUpDate;
            this.m_strAssignmentName = AssignmentName;
            this.m_iTotalNoOfTopic = TotalNoOfTopic;
            this.m_llAssignmentId = AssignmentId;
            this.m_llTestId = TestId;
            this.m_llIndexId = IndexId;
            if (AssignmentType != null&&AssignmentType != 0)
            {
                switch (AssignmentType)
                {
                    case (int)Constants.AssignmentQuestionType.MCQ:
                        {
                            this.m_strAssignmentType = Constants.ASSIGNMENT_TYPE_MCQ;
                            break;
                        }
                    case (int)Constants.AssignmentQuestionType.SUBJECTIVE:
                        {
                            this.m_strAssignmentType = Constants.ASSIGNMENT_TYPE_SUBJECTIVE;
                            break;
                        }

                }
            }
            this.m_strTestName = TestName;
            if (AssignmentType!=null&&AssignmentType != 0)
            {
                switch (TestType)
                {
                    case (int)Constants.TestQuestionType.MCQ:
                        {
                            this.m_strTestType = Constants.TEST_TYPE_MCQ;
                            break;
                        }
                    case (int)Constants.TestQuestionType.SUBJECTIVE:
                        {
                            this.m_strTestType = Constants.TEST_TYPE_SUBJECTIVE;
                            break;
                        }

                }
            }
        }


    }
}