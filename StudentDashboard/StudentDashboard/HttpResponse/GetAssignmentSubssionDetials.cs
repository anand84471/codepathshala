using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpResponse
{
    public class GetAssignmentSubssionDetials:APIDefaultResponse
    {
        [JsonIgnore]
        public string m_strResponse;
        [JsonProperty("assignment_name")]
        public string m_strAssignmentName { get; set; }
        [JsonProperty("assignment_submission_date")]
        public string m_strSubmissionDate;
        [JsonIgnore]
        public DateTime m_dtAssignmentStartDate { get; set; }
        [JsonIgnore]
        public DateTime m_dtAssignmentFinishDate { get; set; }
        [JsonProperty("submission_details")]
        public List<HttpRequest.AssignmentQuestionResponse> m_lsAssignmentQuestionResponse;
        [JsonProperty("time_to_solve_in_seconds")]
        public int m_iTimeToSolveAssignmentInSeconds;
        public GetAssignmentSubssionDetials()
        {

        }
        public GetAssignmentSubssionDetials(string AssignmentName,string Response,string SubmissionDate,DateTime AssignmentStartTime,DateTime AssignmentFinishTime)
        {
            this.m_strAssignmentName = AssignmentName;
            this.m_strResponse = Response;
            this.m_dtAssignmentStartDate = AssignmentStartTime;
            this.m_dtAssignmentFinishDate = AssignmentFinishTime;
            this.m_strSubmissionDate = SubmissionDate;
            m_iTimeToSolveAssignmentInSeconds = (this.m_dtAssignmentFinishDate - this.m_dtAssignmentStartDate).Seconds;
        }
    }
}