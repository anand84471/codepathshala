using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpRequest
{
    public class UpdateClassroomVideoUrlRequest
    {
        [JsonProperty("video_link")]
        public string m_strVideoLink;
        [JsonProperty("meeting_id")]
        public long m_llMeetingId;
        [JsonProperty("classroom_id")]
        public long m_llClassroomId;
        [JsonProperty("topic_name")]
        public string m_strTopicName;
        [JsonProperty("topic_description")]
        public string m_strTopicDescription;
    }
}