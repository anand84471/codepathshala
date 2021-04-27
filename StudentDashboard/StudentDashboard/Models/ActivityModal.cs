using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.Models
{
    public class ActivityModal
    {
        [JsonProperty("message")]
        public string m_strActivityMessage { get; set; }
      
        [JsonProperty("creation_date")]
        public string m_strActivityDate { get; set; }
        [JsonIgnore]
        public DateTime m_dtDateTime { get; set; }
        public ActivityModal(DateTime ActualDate,string Date,string Message)
        {
            this.m_strActivityDate = Date;
            this.m_strActivityMessage = Message;
            this.m_dtDateTime = ActualDate;
        }
    }
}