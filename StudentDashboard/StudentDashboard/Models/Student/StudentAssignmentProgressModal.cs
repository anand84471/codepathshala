using Newtonsoft.Json;
using StudentDashboard.HttpResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.Models.Student
{
    public class StudentAssignmentProgressModal:APIDefaultResponse
    {
        [JsonProperty("assignment_id")]
        public long m_llAssignmentId;
        [JsonProperty("assignment_name")]
        public string m_strAssignmentName;
        [JsonProperty("assignment_type")]
        public string m_strAssignmentType;
        [JsonProperty("assignment_description")]
        public string m_strAssignmentDescription;
        [JsonProperty("total_of_question")]
        public int m_iNoOfQuestions;
        [JsonProperty("marks")]
        public int? m_iMarks;
        [JsonProperty("time_taken")]
        public string m_strTimeTaken;
        [JsonProperty("percentage_score")]
        public string m_strPercentageScore;
        [JsonProperty("submission_id")]
        public long? m_llSubmissionId;
        public StudentAssignmentProgressModal(long AssignmentId,string AssignmentName,string Assignmentdescription,
            string AssignmentType,int TotalNoOQuestions,int? Marks, long? SubmissionId,int? TimeTakenInSeconds,int? PercentageScore )
        {
            this.m_llAssignmentId = AssignmentId;
            this.m_strAssignmentName = AssignmentName;
            this.m_strAssignmentDescription = Assignmentdescription;
            this.m_iNoOfQuestions = TotalNoOQuestions;
            this.m_iMarks = Marks;
            this.m_llSubmissionId = SubmissionId;
            this.m_strAssignmentType = AssignmentType;
            if(TimeTakenInSeconds!=null)
            {
                TimeSpan ts = TimeSpan.FromSeconds((double)TimeTakenInSeconds);
                this.m_strTimeTaken = "" + ts.TotalHours + "hh" +"" + ts.TotalMinutes + "mm" +"" + ts.TotalSeconds + "ss";
            }
        }

    }
}