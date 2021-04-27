using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.Models.Instructor
{
    public class InstructorSchoolDetailsModal
    {
        [JsonProperty("high_school_name")]
        public string m_strHighSchoolName;
        [JsonProperty("high_school_passing_date")]
        public string m_strHighSchoolPassingDate;
        [JsonProperty("intermediate_school_name")]
        public string m_strIntermediateSchoolName;
        [JsonProperty("intermediate_passing_date")]
        public string m_strIntermediatSchoolPassingDate;
        [JsonProperty("graduate_school_name")]
        public string m_strGraduateSchoolName;
        [JsonProperty("graduate_school_comletion_date")]
        public string m_strGrauateSchoolPassingDate;
        [JsonProperty("graduate_branch")]
        public string m_strGraduateBranch;
        [JsonProperty("post_graduate_school_name")]
        public string m_strPostGraduateSchoolName;
        [JsonProperty("post_graduate_school_passing_date")]
        public string m_strPosGrauateSchoolPassingDate;
        [JsonProperty("post_graduate_branch")]
        public string m_strPosGraduateBranch;
        [JsonProperty("phd_school_name")]
        public string m_strPHdSchoolName;
        [JsonProperty("phd_passing_date")]
        public string m_strPhdSchoolPassingDate;
        [JsonProperty("phd_branch")]
        public string m_strPhdBranch;
    }
}