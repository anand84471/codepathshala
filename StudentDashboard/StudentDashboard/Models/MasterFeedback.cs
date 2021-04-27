using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
namespace StudentDashboard.Models
{
    public class MasterFeedback
    {
        [JsonProperty("feedback_message")]
        public string m_strFeedbackMessage;
        [JsonProperty("no_of_ratings")]
        public int m_iNoOfRatings;
    }
}