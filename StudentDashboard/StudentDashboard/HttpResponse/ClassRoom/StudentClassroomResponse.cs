using Newtonsoft.Json;
using StudentDashboard.Models.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpResponse.ClassRoom
{
    public class StudentClassroomResponse:APIDefaultResponse
    {
        [JsonProperty("classroom_details")]
        public List<StudentClassroomModal> m_lsStudentClassroomModal;

    }
}