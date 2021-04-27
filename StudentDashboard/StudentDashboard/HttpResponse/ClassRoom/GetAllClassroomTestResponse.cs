using Newtonsoft.Json;
using StudentDashboard.Models.Classroom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpResponse.ClassRoom
{
    public class GetAllClassroomTestResponse:APIDefaultResponse
    {
        [JsonProperty("classroom_test_details")]
        public List<ClassroomTestModal> m_lsClassroomTestDetails;
    }
}