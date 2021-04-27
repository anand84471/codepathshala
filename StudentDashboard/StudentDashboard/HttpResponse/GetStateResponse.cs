using Newtonsoft.Json;
using StudentDashboard.Models.Address;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpResponse
{
    public class GetStateResponse:APIDefaultResponse
    {
        [JsonProperty("states")]
        public List<StateModel> m_lsStateDetails { get; set; }
    }
}