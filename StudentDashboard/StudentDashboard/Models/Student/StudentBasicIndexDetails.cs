using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.Models.Student
{
    public class StudentBasicIndexDetails
    {
        [JsonProperty("index_name")]
        public string m_strIndexName { get; set; }
        [JsonProperty("index_id")]
        public long m_llIndexId { get; set; }
    }
}