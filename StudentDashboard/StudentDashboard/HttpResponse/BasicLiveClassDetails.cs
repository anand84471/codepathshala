using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpResponse
{
    public class BasicLiveClassDetails
    {
        [JsonProperty("classroom_name")]
        public string m_strClassroomName;
        [JsonProperty("classroom_id")]
        public long m_llClassroomId;
        [JsonProperty("classroom_image")]
        public string m_strClassroomImage;
    }
}