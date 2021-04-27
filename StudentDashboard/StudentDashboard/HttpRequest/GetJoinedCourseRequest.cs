using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpRequest
{
    public class GetJoinedCourseRequest
    {
        [JsonProperty("key")]
        public string m_strSearchString;
        [JsonIgnore]
        [JsonProperty("id")]
        public long m_llStudentGid;
        [JsonProperty("last_fetched_id")]
        public long m_llLastFetchedId;
        [JsonProperty("no_of_rows_fetched")]
        public int m_iNoOfRowsFetched;
    }
}