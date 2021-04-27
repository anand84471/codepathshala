using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpResponse
{
    public class StudentJoinedCoursesResponseModal
    {
        [JsonProperty("course_id")]
        public long m_llCourseId;
        [JsonProperty("course_name")]
        public string m_strCourseName;
        [JsonProperty("course_description")]
        public string m_strCourseDescription;
        [JsonProperty("join_date")]
        public string m_strCourseJoiningDate;
        [JsonProperty("finish_date")]
        public string m_strCourseFinishDate;
        [JsonProperty("course_progress")]
        public int m_iCourseProgress;
        public StudentJoinedCoursesResponseModal()
        {

        }
        public StudentJoinedCoursesResponseModal(long CourseId,string CourseName,string CourseDescription,string CourseJoinDate,int CourseProgress)
        {
            this.m_llCourseId = CourseId;
            this.m_strCourseName = CourseName;
            this.m_strCourseDescription = CourseDescription;
            this.m_strCourseJoiningDate = CourseJoinDate;
            this.m_iCourseProgress = CourseProgress;
        }
    }
}