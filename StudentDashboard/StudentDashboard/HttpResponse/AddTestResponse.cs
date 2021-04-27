using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpResponse
{
    public class AddTestResponse:APIDefaultResponse
    {
        [JsonProperty("test_id")]
        public long m_llTestId { get; set; }
    }
}