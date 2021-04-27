using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpRequest
{
    public class ContactUsApiRequest
    {
        [JsonProperty("name")]
        public string m_strName { get; set; }
        [JsonProperty("phone_no")]
        public string m_strPhoneNo { get; set; }
        [JsonProperty("email")]
        public string m_strEmail { get; set; }
        [JsonProperty("subject")]
        public string m_strSubject { get; set; }
        [JsonProperty("message")]
        public string m_strMessage { get; set; }
    }
}