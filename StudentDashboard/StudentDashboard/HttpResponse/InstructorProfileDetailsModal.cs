using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpResponse
{
    public class InstructorProfileDetailsModal:APIDefaultResponse
    {
        [JsonProperty("is_following")]
        public bool m_bIsFollowing;
        [JsonProperty("profile_url")]
        public string m_strProfileUrl;
        [JsonProperty("instructor_id")]
        public long m_iInstructorId;
        [JsonProperty("instructor_name")]
        public string m_strInstructorName { get; set; }
        [JsonProperty("date_of_joining")]
        public string m_strDateOfJoining { get; set; }
        [JsonProperty("no_of_courses_created")]
        public int m_iNoOfCoursesCreated { get; set; }
        [JsonProperty("no_of_assignments_created")]
        public int m_iNoOfAssignmentsCreated { get; set; }
        [JsonProperty("no_of_tests_created")]
        public int m_iNoOfTestsCreated { get; set; }
        [JsonProperty("no_of_course_join")]
        public int m_iNoOfCourseJoin { get; set; }
        [JsonProperty("no_of_followers")]
        public int m_iNoOfFollowers { get; set; }
        [JsonProperty("courses")]
        public List<CourseDetailsModel> m_lsCourses { get; set; }
        [JsonProperty("date_of_following")]
        public string m_strDateOfFollowing;
        public InstructorProfileDetailsModal(string InstructorName,string InstructorJoinDate,int NoOfCoursesCreated,int NoOfAssignmentsCreted,
            int NoOfTestsCreated,int StudentsJoined,int StudnetsJoinedToCourse,string FollowingDate,string ProfileUrl)
        {
            this.m_strInstructorName = InstructorName;
            this.m_strDateOfJoining = InstructorJoinDate;
            this.m_iNoOfCoursesCreated = NoOfCoursesCreated;
            this.m_iNoOfAssignmentsCreated = NoOfAssignmentsCreted;
            this.m_iNoOfTestsCreated = NoOfTestsCreated;
            this.m_iNoOfFollowers = StudentsJoined;
            this.m_iNoOfCourseJoin = StudnetsJoinedToCourse;
            this.m_strDateOfFollowing = FollowingDate;
            if (FollowingDate!=null)
            {
                this.m_bIsFollowing = true;
            }
            this.m_strProfileUrl = ProfileUrl;
        }
    }
}