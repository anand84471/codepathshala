using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpResponse
{
    public class AddeNewTestSeriesResponse:APIDefaultResponse
    {
        [JsonProperty("test_series_id")]
        public long m_llTestSeriesId;
    }
}