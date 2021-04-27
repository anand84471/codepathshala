using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpResponse
{
    public class GetTestSubmissionDetailsResponse:APIDefaultResponse
    {
        [JsonIgnore]
        public string m_strResponse;
        [JsonProperty("test_name")]
        public string m_strTestName { get; set; }
        [JsonProperty("test_submission_date")]
        public string m_strSubmissionDate;
        [JsonIgnore]
        public DateTime m_dtTestStartDate { get; set; }
        [JsonIgnore]
        public DateTime m_dtTestFinishDate { get; set; }
        [JsonProperty("submission_details")]
        public List<TestQuestionResponse> m_lsTestQuestionResponse;
        [JsonProperty("time_to_solve_in_seconds")]
        public int m_iTimeToSolveTestInSeconds;
        public GetTestSubmissionDetailsResponse()
        {

        }
        public GetTestSubmissionDetailsResponse(string TestName, string Response, string SubmissionDate, DateTime AssignmentStartTime, DateTime FinishTime)
        {
            this.m_strTestName = TestName;
            this.m_strResponse = Response;
            this.m_dtTestStartDate = AssignmentStartTime;
            this.m_dtTestFinishDate = FinishTime;
            this.m_strSubmissionDate = SubmissionDate;
            m_iTimeToSolveTestInSeconds = (this.m_dtTestFinishDate - this.m_dtTestStartDate).Seconds;
        }
    }
}