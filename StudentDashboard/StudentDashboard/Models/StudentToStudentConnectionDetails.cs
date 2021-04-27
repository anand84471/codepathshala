using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.Models
{
    public class StudentToStudentConnectionDetails: StudentPublicProfileResponse
    {
        [JsonProperty("follow_start_date")]
        public string m_strFollowStartDate;
        public StudentToStudentConnectionDetails(long StudentId, string StudentName, string JoiningDate, int NoOfFollowers,
            int NoOfCoursesJoined, int NoOfLiveCoursesJoined, string ProfileUrl, int NoOfLiveClassesAttended,
            int NoOfStudentsFollowing,string FollowStartDate):base(StudentId, StudentName, JoiningDate,
                NoOfFollowers, NoOfCoursesJoined, NoOfLiveCoursesJoined, ProfileUrl, NoOfLiveClassesAttended,
                NoOfStudentsFollowing)
        {
            this.m_strFollowStartDate = FollowStartDate;
        }
    }
}