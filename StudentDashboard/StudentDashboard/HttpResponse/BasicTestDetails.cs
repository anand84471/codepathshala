using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpResponse
{
    public class BasicTestDetails
    {
        [JsonProperty("test_name")]
        public string m_strTestName { get; set; }
        [JsonProperty("test_id")]
        public long? m_llTestId { get;set;}
        public BasicTestDetails()
        {

        }
        public BasicTestDetails(long? TestId,string TestName)
        {
            this.m_strTestName = TestName;
            this.m_llTestId = TestId;
        }
    }
}