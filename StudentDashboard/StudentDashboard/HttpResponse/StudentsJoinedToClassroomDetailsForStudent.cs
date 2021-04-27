using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpResponse
{
    public class StudentsJoinedToClassroomDetailsForStudent
    {
        [JsonProperty("student_id")]
        public long m_llStudentId;
        [JsonProperty("name")]
        public string m_strStudentName;
        [JsonProperty("profile_url")]
        public string m_strProfileUrl;
        [JsonProperty("no_of_followers")]
        public int m_iNoOfFollowers;
        [JsonProperty("no_of_followings")]
        public int m_iNoOfFollowings;
        [JsonProperty("date_of_joining")]
        public string m_strDateOfJoining;
        [JsonProperty("is_following")]
        public bool m_bIsFollowing;
        public StudentsJoinedToClassroomDetailsForStudent(long StudentId,string Name,string DateOfJoining,
            int NoOfFollowers,int NoOfStudentsFollowing,string ProfileUrl,DateTime? DateOfFollowing)
        {
            this.m_strStudentName = Name;
            this.m_llStudentId = StudentId;
            this.m_strDateOfJoining = DateOfJoining;
            this.m_iNoOfFollowers = NoOfFollowers;
            this.m_iNoOfFollowings = NoOfStudentsFollowing;
            this.m_strProfileUrl = ProfileUrl;
            this.m_bIsFollowing = DateOfFollowing==null?false:true;
        }
    }
}