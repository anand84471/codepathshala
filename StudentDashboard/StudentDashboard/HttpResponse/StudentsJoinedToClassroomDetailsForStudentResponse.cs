using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpResponse
{
    public class StudentsJoinedToClassroomDetailsForStudentResponse:APIDefaultResponse
    {
        [JsonProperty("students_joined")]
        public List<StudentsJoinedToClassroomDetailsForStudent> m_lsStudentsJoinedToClassroomDetailsForStudent;

    }
}