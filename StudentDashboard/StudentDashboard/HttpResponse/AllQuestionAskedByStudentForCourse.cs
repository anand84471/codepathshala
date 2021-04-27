using Newtonsoft.Json;
using StudentDashboard.Models.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpResponse
{
    public class AllQuestionAskedByStudentForCourse:APIDefaultResponse
    {
        [JsonProperty("questions")]
        public List<StudentCourseQuestionModal> m_lsStudentCourseQuestionModal;
        public AllQuestionAskedByStudentForCourse():base()
        {

        }
    }
}