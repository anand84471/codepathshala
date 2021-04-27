using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.Models.Address
{
    public class CityModel
    {
        [JsonProperty("city_id")]
        public int m_iCityId { get; set; }
        [JsonProperty("city_name")]
        public string m_strCityName { get; set; }
        public CityModel(int Id,string Name)
        {
            this.m_iCityId = Id;
            this.m_strCityName = Name;
        }
    }
}