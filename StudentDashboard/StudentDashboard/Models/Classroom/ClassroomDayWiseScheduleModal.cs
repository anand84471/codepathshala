using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.Models.Classroom
{
    public class ClassroomDayWiseScheduleModal
    {
        [JsonProperty("start_time")]
        public string m_strClassroomStartTime;
        [JsonProperty("close_time")]
        public string m_strClassroomCloseTime;
        [JsonProperty("is_open")]
        public bool IsOpen;
        [JsonProperty("day_name")]
        public string m_strDayName;
    }
}