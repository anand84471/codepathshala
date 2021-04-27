using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.Models.Student
{
    public class StudentBasicCourseDetails
    {
        public string m_strCourseName;
        public long m_llCourseId;
        public string m_strJoiningDate;
        public int m_iProgress;
        public string m_strCourseDescription;
        public int m_iTotalTopics;
        public StudentBasicCourseDetails(long CourseId,string CourseName,string JoiningDate,
            int Progress,string CourseDescription,int TotalTopics)
        {
            this.m_strCourseName = CourseName;
            this.m_strCourseDescription = CourseDescription;
            this.m_strJoiningDate = JoiningDate;
            this.m_iProgress = Progress;
            this.m_llCourseId = CourseId;
            this.m_iTotalTopics = TotalTopics;
        }
        public StudentBasicCourseDetails(long CourseId, string CourseName, string CourseDescription, string JoiningDate)
        {
            this.m_strCourseName = CourseName;
            this.m_strCourseDescription = CourseDescription;
            this.m_strJoiningDate = JoiningDate;
            this.m_llCourseId = CourseId;
        }
    }
}