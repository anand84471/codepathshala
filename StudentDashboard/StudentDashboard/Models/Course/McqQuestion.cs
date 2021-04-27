using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.Models.Course
{
    public class McqQuestion
    {
        [JsonProperty("test_id")]
        public long m_llTestId { get; set; }
        [JsonProperty("assignment_id")]
        public long m_llAssignmentId { get; set; }
        [JsonProperty("question_id")]
        public long m_llQuestionId { get; set; }
        [JsonProperty("question_statement")]
        public string m_strQuestionStatement { get; set; }
        [JsonProperty("option1")]
        public string m_strOption1 { get; set; }
        [JsonProperty("option2")]
        public string m_strOption2 { get; set; }
        [JsonProperty("option3")]
        public string m_strOption3 { get; set; }
        [JsonProperty("option4")]
        public string m_strOption4 { get; set; }
        [JsonProperty("correct_option")]
        public short m_iCorrectOption { get; set; }
        [JsonProperty("question_time_in_seconds")]
        public int m_iTimeInSeconds { get; set; }
        [JsonProperty("marks")]
        public int m_iMarks { get; set; }
        [JsonProperty("creation_date")]
        public string m_strCreationDate { get; set; }
        [JsonProperty("updation_date")]
        public string m_strUpdationDate { get; set; }
        public McqQuestion()
        {

        }
        public McqQuestion(long QuestionId, string QuestionStatement,string option1,string option2,string option3,string option4,short CorrectOption, string CreationDate,string UpdationDate)
        {
            this.m_llQuestionId = QuestionId;
            this.m_strQuestionStatement = QuestionStatement;
            this.m_strOption1 = option1;
            this.m_strOption2 = option2;
            this.m_strOption3 = option3;
            this.m_iCorrectOption = CorrectOption;
            this.m_strOption4 = option4;
            this.m_strCreationDate = CreationDate;
            this.m_strUpdationDate = UpdationDate;
        }
        public McqQuestion(long QuestionId, string QuestionStatement, string option1, string option2, string option3, string option4, short CorrectOption, string CreationDate, string UpdationDate,
                           int Marks,int TimeInSeconds)
        {
            this.m_llQuestionId = QuestionId;
            this.m_strQuestionStatement = QuestionStatement;
            this.m_strOption1 = option1;
            this.m_strOption2 = option2;
            this.m_strOption3 = option3;
            this.m_iCorrectOption = CorrectOption;
            this.m_strOption4 = option4;
            this.m_strCreationDate = CreationDate;
            this.m_strUpdationDate = UpdationDate;
            this.m_iTimeInSeconds = TimeInSeconds;
            this.m_iMarks = Marks;
        }
    }
}