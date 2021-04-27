using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StudentDashboard.Models;
using Newtonsoft.Json;
namespace StudentDashboard.HttpRequest
{
    public class ClassroomFeedbackRequest: MasterFeedback
    {
        [JsonProperty("classroom_id")]
        public long m_llClassroomId;
        [JsonIgnore]
        public long m_llStudentId;

    }
}