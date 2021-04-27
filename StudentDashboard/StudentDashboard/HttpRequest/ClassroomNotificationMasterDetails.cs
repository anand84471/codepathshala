using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpRequest
{
    public class ClassroomNotificationMasterDetails
    {
        [JsonProperty("classroom_id")]
        public long m_llClassroomId;
        [JsonProperty("notification")]
        public string m_strNotification;
    }
}