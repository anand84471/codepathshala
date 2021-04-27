using Newtonsoft.Json;
using StudentDashboard.Models.Course;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpResponse
{
    public class SearchForAssignmentsApiResponse:APIDefaultResponse
    {
        [JsonProperty("assignments")]
        public List<AssignmentDetailsModel> m_lsAssignments { get; set; }
    }
}