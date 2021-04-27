using Newtonsoft.Json;

namespace StudentDashboard.HttpRequest
{
    public class ContentSearchRequest
    {
        [JsonProperty("last_feched_content_id")]
        public long m_llLastFetchedContentId;
        [JsonProperty("search_string")]
        public string m_strSearchString;
    }
}