using Newtonsoft.Json;
using StudentDashboard.HttpResponse;
using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace StudentDashboard.HttpRequest
{
    public class TestSubmissionRequest
    {
        [JsonIgnore]
        [JsonProperty("student_id")]
        public long m_llStudentId { get; set; }
        [JsonProperty("test_id")]
        public long m_llTestId { get; set; }
        [JsonProperty("start_time")]
        public string m_strAssignmetStartTime { get; set; }
        [JsonProperty("finish_time")]
        public string m_strAssignmetFinishTime { get; set; }
        [JsonProperty("response")]
        public List<TestQuestionResponse> m_lsResponse;
        [JsonProperty("time_taken")]
        public int m_iTimeTakenToCompleteTest;
        [JsonProperty("correct_answers")]
        public int m_iCorrectAnswers;
        [JsonProperty("incorrect_answers")]
        public int m_iIncorrectAnswers;
        [JsonIgnore]
        public DateTime m_dtStartTime;
        [JsonIgnore]
        public DateTime m_dtFinishTime;
        [JsonIgnore]
        public string m_strResponse;
        [JsonProperty("submission_id")]
        public long m_llSubmissionId;
        [JsonIgnore]
        public int m_iPercentageScore;
        [JsonIgnore]
        public int m_iNotAttemptedQuestions;
        [JsonProperty("total_questions")]
        public int m_iTotalNoOfQuestions;

        public void ProcessRequest()
        {
            this.m_dtStartTime = new DateTime();
            this.m_dtStartTime = DateTime.Now;
            this.m_dtStartTime.AddSeconds(-this.m_iTimeTakenToCompleteTest);
            this.m_dtFinishTime = DateTime.Now;
            double TotalNoOfQuestions = 0;
            double CorrectAnswers = 0;
            if (m_lsResponse != null && m_lsResponse.Count > 0)
            {
                m_iTotalNoOfQuestions = m_lsResponse.Count;
                var json = new JavaScriptSerializer().Serialize(m_lsResponse);
                this.m_strResponse = json.ToString();
                foreach (var Question in m_lsResponse)
                {
                    TotalNoOfQuestions++;
                    if (Question.m_iOptionSelected == Question.m_CorrectOption)
                    {
                        CorrectAnswers++;
                    }
                    else if (Question.m_iOptionSelected == -1)
                    {
                        this.m_iNotAttemptedQuestions++;
                    }
                    else
                    {
                        this.m_iIncorrectAnswers++;
                    }
                }
                m_iPercentageScore = (int)((CorrectAnswers / TotalNoOfQuestions) * 100);
            }
            this.m_iCorrectAnswers = (int)CorrectAnswers;
        }
    }
}