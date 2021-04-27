using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpRequest
{
    public class InsertTestSeriesRequest
    {
        [JsonProperty("test_series_name")]
        public string m_strTestSeriesName;
        [JsonProperty("test_series_description")]
        public string m_strTestSeriesDescription;
        [JsonIgnore]
        public int m_iInstructorId;
        [JsonProperty("test_series_image")]
        public string m_strTestSeriesImage;
    }
}