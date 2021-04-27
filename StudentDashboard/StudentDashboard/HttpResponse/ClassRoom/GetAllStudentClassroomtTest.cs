using Newtonsoft.Json;
using StudentDashboard.Models.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpResponse.ClassRoom
{
    public class GetAllStudentClassroomtTest:APIDefaultResponse
    {
        [JsonProperty("test_details")]
        public List<StudentClassroomTestModal> m_lsStudentClassroomTestModal;
    }
}