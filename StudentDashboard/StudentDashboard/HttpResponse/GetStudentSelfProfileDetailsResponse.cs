using Newtonsoft.Json;
using StudentDashboard.Models.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpResponse
{
    public class GetStudentSelfProfileDetailsResponse:APIDefaultResponse
    {
        [JsonProperty("student_details")]
        public StudentProfileDetails m_objStudentProfileDetails;
    }
}