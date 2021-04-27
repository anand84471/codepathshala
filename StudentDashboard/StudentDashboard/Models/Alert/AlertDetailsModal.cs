using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.Models.Alert
{
    public class AlertDetailsModal
    {
        [JsonProperty("alert_id")]
        public long m_llAlertId;
        [JsonProperty("alert_message")]
        public string m_strAlertMessage;
        [JsonProperty("alert_time")]
        public string m_strAlertTime;
        [JsonProperty("alert_icon")]
        public string m_strAlertIcon;
        public AlertDetailsModal(long AlertId,string AlertMessage,string AlertTime,string AlertIcon)
        {
            this.m_llAlertId = AlertId;
            this.m_strAlertMessage = AlertMessage;
            this.m_strAlertTime = AlertTime;
            this.m_strAlertIcon = AlertIcon;
        }
    }
}