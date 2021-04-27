using Newtonsoft.Json;

namespace StudentDashboard.HttpResponse
{
    public class BasicIndexDetails
    {
        [JsonProperty("index_name")]
        public string m_strIndexName { get; set; }
        [JsonProperty("index_id")]
        public long m_llIndexId { get; set; }
    }
}