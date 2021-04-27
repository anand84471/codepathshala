using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.Models.Address
{
    public class CountryModel
    {
        [JsonProperty("country_id")]
        public int m_Id { get; set; }
        [JsonProperty("country_name")]
        public string m_strName { get; set; }
        [JsonProperty("counrty_short_name")]
        public string m_strShortName { get; set; }
        [JsonProperty("coutry_phone_code")]
        public string m_strPhoneCode { get; set; }


    }
}