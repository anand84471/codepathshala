using Newtonsoft.Json;
using StudentDashboard.Models.Social;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpResponse
{
    public class GetAllStudentsToJoinRespone:APIDefaultResponse
    {
        [JsonProperty("students")]
        public List<StudentDetailToFolllow> m_lsStudentDetailToFolllow;                                                                                                        
    }
}