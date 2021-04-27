using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.Models.Student
{
    public class StudentFollowedByStudentDetails
    {
        [JsonProperty("student_id")]
        public long m_llStudentId;
        [JsonProperty("student_name")]
        public string m_strStudentName;
        [JsonProperty("following_since")]
        public string m_strFollowingDate;
        [JsonProperty("is_following_back")]
        public bool? m_isFollowingBack;
        [JsonProperty("profile_url")]
        public string m_strProfileUrl;
        public StudentFollowedByStudentDetails(long StudentId,string StudentName,
            string FollowStartDate,bool? IsFoolwoingBack,string ProfileUrl)
        {
            this.m_llStudentId = StudentId;
            this.m_strFollowingDate = FollowStartDate;
            this.m_strStudentName = StudentName;
            this.m_isFollowingBack = IsFoolwoingBack;
            this.m_strProfileUrl = ProfileUrl;
        }
    }
}