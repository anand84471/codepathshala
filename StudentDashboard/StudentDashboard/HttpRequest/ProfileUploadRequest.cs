using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpRequest
{
    public class ProfileUploadRequest
    {
        [JsonProperty("url")]
        public string m_strUrl;
       
    }
}