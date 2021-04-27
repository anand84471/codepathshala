using Newtonsoft.Json;
using StudentDashboard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpResponse
{
    public class GetInstructorActivityResponse:APIDefaultResponse
    {
        [JsonProperty("activity")]
        public List<ActivityModal> m_lsActivityDetails { get; set; } 
    }
}