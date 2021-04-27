using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.Models.Instructor
{
    public class InstructorProjectsDetailsModal
    {
        [JsonProperty("project_name")]
        public string m_strProjectName;
        [JsonProperty("team_size")]
        public string m_strTeamSize;
        [JsonProperty("submission_date")]
        public string m_strSubmissionDate;
    }
}