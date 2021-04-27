using Newtonsoft.Json;
using StudentDashboard.Models.Classroom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpResponse.ClassRoom
{
    public class GetClassroomSheduleResponse:APIDefaultResponse
    {
        [JsonProperty("classroom_schedule_details")]
        public ClassroomScheduleDetails classroomScheduleDetails;
    }
}