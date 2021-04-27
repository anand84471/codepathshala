using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.Models.Address
{
    public class StateModel
    {
        [JsonProperty("state_id")]
        public int m_iId { get; set; }
        [JsonProperty("state_name")]
        public string m_strName {get;set ;}
       
        public StateModel(int Id,string Name)
        {
            this.m_iId = Id;
            this.m_strName = Name;
        }

    }
}