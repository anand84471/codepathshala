using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpResponse
{
    public class GetWebsiteHomeDetailsResponse:APIDefaultResponse
    {
        [JsonProperty("no_of_instructors")]
        public long m_llNoOfInstructorsJoined;
        [JsonProperty("no_of_courses_created")]
        public long m_llNoOfCoursesCreated;
        [JsonProperty("no_of_assignments_created")]
        public long m_llNoOfAssignmentscreated;
        [JsonProperty("no_of_tests_created")]
        public long m_llNoOfTestCreated;
        [JsonProperty("no_of_students_joined")]
        public long m_llNoOfStudentsJoined;
        public GetWebsiteHomeDetailsResponse()
        {

        }
        public GetWebsiteHomeDetailsResponse(long NoOfInstructors,long NoOfCourses,long NoOfTests,long NoOfAssignments,long NoOfStudents)
        {
            this.m_llNoOfAssignmentscreated = NoOfAssignments > 100 ? NoOfAssignments:143;
            this.m_llNoOfCoursesCreated = NoOfCourses>10?NoOfCourses:12;
            this.m_llNoOfStudentsJoined = NoOfStudents>100?NoOfStudents:165;
            this.m_llNoOfInstructorsJoined = NoOfInstructors>20?NoOfInstructors:23;
            this.m_llNoOfTestCreated = NoOfTests>100?NoOfTests:167;
            this.m_llNoOfStudentsJoined *= 100;

        }
    }
}