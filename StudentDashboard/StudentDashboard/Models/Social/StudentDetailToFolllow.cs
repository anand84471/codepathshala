using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.Models.Social
{
    public class StudentDetailToFolllow
    {
        [JsonProperty("student_id")]
        public long m_llStudentId;
        [JsonProperty("joining_date")]
        public string m_strJoiningDate;
        [JsonProperty("followers")]
        public int m_iNoOfFollowers;
        [JsonProperty("name")]
        public string m_strStudentName;
        [JsonProperty("profile_url")]
        public string m_strProfileUrl;
        public StudentDetailToFolllow(long StudentId,string StudentName,string ProfileUrl,
            int NoOfFollowers,string JoiningDate)
        {
            this.m_strStudentName = StudentName;
            this.m_strJoiningDate = JoiningDate;
            this.m_iNoOfFollowers = NoOfFollowers;
            this.m_llStudentId = StudentId;
            this.m_strProfileUrl = ProfileUrl;
        }

    }
}