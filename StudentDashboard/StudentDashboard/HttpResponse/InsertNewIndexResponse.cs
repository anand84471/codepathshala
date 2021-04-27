using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpResponse
{
    public class InsertNewIndexResponse:APIDefaultResponse
    {
        [JsonProperty("index_id")]
        public long m_llIndexId { get; set; }
    }
}