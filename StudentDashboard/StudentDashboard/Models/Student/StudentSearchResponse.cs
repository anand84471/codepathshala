using StudentDashboard.HttpResponse;
using StudentDashboard.Models.Course;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.Models.Student
{
    public class StudentSearchResponse
    {
        public List<CourseDetailsModel> m_lsCourses;
        public List<GetPublicClassroomsResponse> m_lsLiveClasses;
        public List<TestDetailsModel> m_lsTest;
        public List<SearchInstructorResponseModal> m_lsInstructors;
    }
}