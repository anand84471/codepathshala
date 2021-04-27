using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpRequest
{
    public class SearchInstructorByUserIdRequest
    {
        [JsonProperty("user_id_search_string")]
        public string m_strInstructorSearchId;
        [JsonIgnore]
        public long m_llInstructorId;
        
    }
}