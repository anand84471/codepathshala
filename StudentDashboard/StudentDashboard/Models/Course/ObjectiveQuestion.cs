using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.Models.Course
{
    public class ObjectiveQuestion
    {
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
        public string m_strCorrectOption { get; set; }
        [JsonProperty("mark")]
        public int m_iMarks { get; set; }
        [JsonProperty("question_time_in_minute")]
        public short m_iTimeInSeconds { get; set; }
        [JsonProperty("question_time_in_second")]
        public short m_iQuestionTimeInMinute { get; set; }
    }
}