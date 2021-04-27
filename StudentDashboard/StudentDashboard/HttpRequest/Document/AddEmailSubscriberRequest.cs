using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpRequest.Document
{
    public class AddEmailSubscriberRequest
    {
        [JsonProperty("email_address")]
        public string m_strEmailAddress;
    }
}