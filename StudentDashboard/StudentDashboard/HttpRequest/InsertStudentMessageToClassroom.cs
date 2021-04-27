using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpRequest
{
    public class InsertStudentMessageToClassroom: InsertInstructorMessageToClassroom
    {
        [JsonProperty("student_id")]
        public long m_llStudentId;
    }
}