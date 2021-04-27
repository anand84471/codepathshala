using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpRequest
{
    public class ClassroomAttachmentRequest
    {
        [JsonProperty("classroom_id")]
        public long m_llClassroomId;
        [JsonProperty("attachment_url")]
        public string m_strAttachmentUrl;
        [JsonProperty("attachment_name")]
        public string m_strAttachmentName;
        [JsonProperty("attachment_description")]
        public string m_strAttachmentDescription;
        [JsonProperty("classroom_name")]
        public string m_strClassroomName;
    }
}