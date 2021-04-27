using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpRequest
{
    public class InstructorBioUpdateHttpRequest
    {
        [JsonProperty("bio")]
        public string m_strInstructorBio;
    }
}