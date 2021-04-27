using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.Models.Course
{
    public class SubjectiveQuestion
    {
        [JsonProperty("question_statement")]
        public string m_strQuestionStatement { get; set; }
        [JsonProperty("hint")]
        public string m_strHint { get; set; }
        [JsonProperty("assignment_id")]
        public long m_llAsssignmentId { get; set; }
        [JsonProperty("question_id")]
        public long m_llQuestionId { get; set; }
        [JsonProperty("creation_date")]
        public string m_strQuestionCreationDate { get; set; }
        [JsonProperty("updation_date")]
        public string m_strUpdationDate { get; set; }
       
        public SubjectiveQuestion()
        {

        }
        public SubjectiveQuestion(long AssignmentId,long QuestionId,string QuestionStatement,string QuestionHint,string CreationDate,string UpdationDate)
        {
            this.m_strQuestionCreationDate = CreationDate;
            this.m_strUpdationDate = UpdationDate;
            this.m_strQuestionStatement = QuestionStatement;
            this.m_strHint = QuestionHint;
            this.m_llAsssignmentId = AssignmentId;
            this.m_llQuestionId = QuestionId;
        }
    }
}