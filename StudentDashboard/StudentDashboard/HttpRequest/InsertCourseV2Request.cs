using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpRequest
{
    public class InsertCourseV2Request
    {
        [JsonProperty("course_name")]
        public string m_strCourseName;
        [JsonProperty("course_description")]
        public string m_strCourseDescription;
        [JsonProperty("about_course")]
        public string m_strAboutCourse;
        [JsonProperty("instructor_id")]
        public int m_iInstructorId;
        [JsonProperty("course_bg_image")]
        public string m_strCourseImagePath;
        [JsonProperty("course_id")]
        public long m_llCourseId;
    }
}