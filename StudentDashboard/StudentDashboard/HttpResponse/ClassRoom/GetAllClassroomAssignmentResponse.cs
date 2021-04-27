using Newtonsoft.Json;
using StudentDashboard.Models.Classroom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpResponse.ClassRoom
{
    public class GetAllClassroomAssignmentResponse:APIDefaultResponse
    {
        [JsonProperty("assignment_details")]
        public List<ClassroomAssignmentModal> m_lsClassroomAssignmentModal;
    }
}