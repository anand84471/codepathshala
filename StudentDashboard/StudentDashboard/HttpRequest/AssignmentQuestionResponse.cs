using Newtonsoft.Json;
using StudentDashboard.Models.Course;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpRequest
{
    public class AssignmentQuestionResponse
    {

        [JsonProperty("question_id")]
        public long m_llQuestionId { get; set; }
        [JsonProperty("option_selected")]
        public int m_iOptionSelected { get; set; }
        [JsonProperty("correct_option")]
        public int m_CorrectOption;
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
        
    }
}