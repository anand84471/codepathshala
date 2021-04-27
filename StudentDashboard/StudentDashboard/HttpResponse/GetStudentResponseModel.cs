using Newtonsoft.Json;
using StudentDashboard.HttpRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpResponse
{
    public class GetStudentResponseModel
    {
        [JsonProperty("response_code")]
        public int m_iResponseCode { get; set; }
        [JsonProperty("response_message")]
        public string m_stgrResponseMessage { get; set; }
        [JsonProperty("student_details")]
        public StudentDetailsModel m_objStudentDetails {get;set;}
    }
}