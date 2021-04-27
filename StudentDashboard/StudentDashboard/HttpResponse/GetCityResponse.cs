using Newtonsoft.Json;
using StudentDashboard.Models.Address;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpResponse
{
    public class GetCityResponse:APIDefaultResponse
    {
        [JsonProperty("cities")]
        public List<CityModel> m_lsAllCity { get; set; }
    }
}