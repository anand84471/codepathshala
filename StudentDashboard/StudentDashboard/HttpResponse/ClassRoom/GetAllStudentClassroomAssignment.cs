using Newtonsoft.Json;
using StudentDashboard.Models.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpResponse.ClassRoom
{
    public class GetAllStudentClassroomAssignment:APIDefaultResponse
    {
        [JsonProperty("assignment_details")]
        public List<StudentClassroomAssignmentModal> m_lsStudentClassroomAssignmentModal;
    }
}