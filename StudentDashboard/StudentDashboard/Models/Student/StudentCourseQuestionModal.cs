using Newtonsoft.Json;
using StudentDashboard.HttpResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.Models.Student
{
    public class StudentCourseQuestionModal:APIDefaultResponse
    {
        [JsonProperty("course_id")]
        public long m_llCourseId;
        [JsonProperty("student_id")]
        public long m_llStudentId;
        [JsonProperty("question_statetment")]
        public string m_strQuestionStatement;
        [JsonProperty("asked_date")]
        public string m_strAskedDate;
        public StudentCourseQuestionModal()
        {

        }
        public StudentCourseQuestionModal(long Courseid,long Studentid,string QuestionStatement)
        {
            this.m_llCourseId = Courseid;
            this.m_llStudentId = Studentid;
            this.m_strQuestionStatement = QuestionStatement;
        }
        public StudentCourseQuestionModal(string QuestionStatement,string DateFoAsking)
        {
            this.m_strQuestionStatement = QuestionStatement;
            this.m_strAskedDate = DateFoAsking;
        }
    }
}