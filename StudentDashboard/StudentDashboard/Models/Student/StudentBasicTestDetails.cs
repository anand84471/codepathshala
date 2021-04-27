using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.Models.Student
{
    public class StudentBasicTestDetails
    {
        [JsonProperty("test_name")]
        public string m_strTestName { get; set; }
        [JsonProperty("test_id")]
        public long? m_llTestId { get; set; }
        [JsonProperty("is_completed")]
        public bool m_IsCompleted { get; set; }
        public StudentBasicTestDetails()
        {

        }
        public StudentBasicTestDetails(long TestId, string TestName,bool IsCompleted)
        {
            this.m_strTestName = TestName;
            this.m_llTestId = TestId;
            this.m_IsCompleted = IsCompleted;
        }
    }
}