using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using StudentDashboard.Models.Classroom;

namespace StudentDashboard.HttpResponse
{
    public class GetClassroomSyllabusDetailsResponse : APIDefaultResponse
    {
        [JsonProperty("classroom_syllabus_response")]
        public ClassroomSyllabusDetailsModal classroomSyllabusDetailsModal;
    }
}