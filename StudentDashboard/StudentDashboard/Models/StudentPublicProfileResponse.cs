using Newtonsoft.Json;
using StudentDashboard.HttpResponse;
using StudentDashboard.Models.Social;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.Models
{
    public class StudentPublicProfileResponse:APIDefaultResponse
    {
        [JsonProperty("name")]
        public string m_strStudentName;
        [JsonProperty("student_id")]
        public long m_llStudentId;
        [JsonProperty("joining_date")]
        public string m_strJoiningDate;
        [JsonProperty("profile_url")]
        public string m_strProfileUrl;
        [JsonProperty("no_of_followers")]
        public int m_iNoOfFollowers;
        [JsonProperty("no_of_students_folowing")]
        public int m_iNoOfStudentsFollowing;
        [JsonProperty("no_of_live_courses_joined")]
        public int m_iNoOfLiveCourseJoined;
        [JsonProperty("no_of_live_classes_attended")]
        public int m_iNoOfLiveClassesAttended;
        [JsonProperty("no_of_course_joined")]
        public int m_iNoOfCoursesJoined;
        public List<BasicCourseDetails> m_lsCoursesJoined;
        public List<BasicLiveClassDetails> m_lsLiveClassesJoined;
        public List<StudentDetailToFolllow> m_lsStudentsFollowers;
        public List<StudentDetailToFolllow> m_lsInstructorFollowing;
        public StudentPublicProfileResponse(long StudentId, string StudentName, string JoiningDate, int NoOfFollowers,
            int NoOfCoursesJoined, int NoOfLiveCoursesJoined, string ProfileUrl, int NoOfLiveClassesAttended,
            int NoOfStudentsFollowing)
        {
            this.m_strStudentName = StudentName;
            this.m_llStudentId = StudentId;
            this.m_strJoiningDate = JoiningDate;
            this.m_iNoOfCoursesJoined = NoOfCoursesJoined;
            this.m_strProfileUrl = ProfileUrl;
            this.m_iNoOfFollowers = NoOfFollowers;
            this.m_iNoOfLiveCourseJoined = NoOfLiveCoursesJoined;
            this.m_iNoOfLiveClassesAttended = NoOfLiveClassesAttended;
            this.m_iNoOfStudentsFollowing = NoOfStudentsFollowing;

        }
    }
}