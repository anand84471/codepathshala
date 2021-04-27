using Newtonsoft.Json;
using StudentDashboard.Models.Classroom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpResponse
{
    public class GetClassroomSyllabusResponse:APIDefaultResponse
    {
        [JsonProperty("syllabus")]
        public ClassroomSyllabusDetailsModal ClassroomSyllabusDetailsModal;
    }
}