using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.Models.Instructor
{
    public class InstructorConferencesAttendsModal
    {
        [JsonProperty("conference_name")]
        public string m_strConferenceName;
        [JsonProperty("attend_date")]
        public string m_strAttendDate;
    }
}