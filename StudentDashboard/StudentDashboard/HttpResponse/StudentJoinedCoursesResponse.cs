using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpResponse
{
    public class StudentJoinedCoursesResponse:APIDefaultResponse
    {
        [JsonProperty("course_details")]
        public List<StudentJoinedCoursesResponseModal> lsStudentJoinedCoursesResponseModal;
    }
}