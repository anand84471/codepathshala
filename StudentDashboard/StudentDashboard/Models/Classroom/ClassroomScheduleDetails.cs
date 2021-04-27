using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.Models.Classroom
{
    public class ClassroomScheduleDetails
    {
        [JsonProperty("classroom_id")]
        public long m_llClassroomId;
        [JsonProperty("schedule_details")]
        public List<ClassroomDayWiseScheduleModal> m_lsDayWiseScheduleDetails;
        [JsonProperty("row_insertion_time")]
        public string m_strClassroomScheduleInsertionTime;
        [JsonProperty("row_updation_time")]
        public string m_strClassroomScheduleUpdationTime;
    }
}