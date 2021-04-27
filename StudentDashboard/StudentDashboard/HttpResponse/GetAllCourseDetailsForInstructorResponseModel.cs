using Newtonsoft.Json;
using StudentDashboard.Models.Course;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpResponse
{
    public class GetAllCourseDetailsForInstructorResponseModel:APIDefaultResponse
    {
        [JsonProperty("courses")]
        public List<CourseDetailsModel> m_lsCourseModel { get; set; }
    }
}