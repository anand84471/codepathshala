using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpResponse
{
    public abstract class ObjectInfoDetailsResponse
    {
        [JsonProperty("identity")]
        public string key { get; set; }
        [JsonProperty("value")]
        public string value { get; set; }
    }
}