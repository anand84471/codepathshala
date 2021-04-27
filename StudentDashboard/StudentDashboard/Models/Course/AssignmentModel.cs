using Newtonsoft.Json;
using StudentDashboard.HttpResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.Models.Course
{
    public class AssignmentModel:APIDefaultResponse
    {
        [JsonProperty("index_id")]
        public long m_llIndexId { get; set; }
        [JsonProperty("total_no_of_questions")]
        public int m_iTotalNoOfQuestions { get; set; }
        [JsonProperty("assignment_name")]
        public string m_strAssignmentName { get; set; }
        [JsonProperty("assignment_description")]
        public string m_strAssignmentDescription { get; set; }
        [JsonProperty("creation_date")]
        public string m_strAssignmentCreationDate { get; set; }
        [JsonProperty("updatione_date")]
        public string m_strAssignmentUpdationDate { get; set; }
        [JsonProperty("file_path")]
        public string m_strFileUploadPath { get; set; }
        [JsonProperty("file_type_id")]
        public short m_iFileUploadTypeId { get; set; }
        [JsonProperty("assignment_id")]
        public long m_llAssignemntId { get; set; }
        [JsonProperty("assignment_type")]
        public string m_strAssignmentType { get; set; }
        [JsonProperty("assignment_type_id")]
        public short m_iAssignmentType { get; set; } = 1;
        [JsonProperty("mcq_questions")]
        public List<McqQuestion> m_lsMcqQuestion { get; set; }
        [JsonProperty("subjective_questions")]
        public List<SubjectiveQuestion > m_lsSubjectiveQuestion { get; set; } 
        [JsonIgnore]
        [JsonProperty("instructor_id")]
        public int m_iInstructorId { get; set; }
        [JsonProperty("course_id")]
        public long m_llCourseId { get; set; }
        [JsonProperty("tiny_url")]
        public string m_strTinyUrl;
        [JsonProperty("access_code")]
        public string m_strAccessCode;
        [JsonProperty("status")]
        public string m_strStatus;
        [JsonProperty("classroom_id")]
        public long m_llClassroomId;
        [JsonProperty("is_classroom_assignment")]
        public bool m_bIsClassroomAssignment;
        public AssignmentModel()
        {
        }
        public AssignmentModel(string AssignmentName,string AssignmentDescription,string CreationDate,string UpdationDate,byte AssignmentType)
        {
            this.m_strAssignmentName = AssignmentName;
            this.m_strAssignmentDescription = AssignmentDescription;
            this.m_strAssignmentCreationDate = CreationDate;
            this.m_strAssignmentUpdationDate = UpdationDate;
            this.m_iAssignmentType = AssignmentType;
            if(this.m_iAssignmentType==(int)Constants.AssignmentQuestionType.MCQ)
            {
                this.m_strAssignmentType = Constants.ASSIGNMENT_TYPE_MCQ;
            }
            else if(this.m_iAssignmentType== (int)Constants.AssignmentQuestionType.SUBJECTIVE)
            {
                this.m_strAssignmentType = Constants.ASSIGNMENT_TYPE_SUBJECTIVE;
            }
        }
        public void SetRemaningValues()
        {
            if (this.m_iAssignmentType == (int)Constants.AssignmentQuestionType.MCQ)
            {
                if (this.m_lsMcqQuestion != null)
                {
                    this.m_iTotalNoOfQuestions = this.m_lsMcqQuestion.Count;
                }
            }
            else if (this.m_iAssignmentType == (int)Constants.AssignmentQuestionType.SUBJECTIVE)
            {
                if (m_lsSubjectiveQuestion != null)
                {
                    this.m_iTotalNoOfQuestions = this.m_lsSubjectiveQuestion.Count;
                }
            }
        }
        public AssignmentModel(string AssignmentName, string AssignmentDescription, string CreationDate, string UpdationDate, byte AssignmentType,
            string TinyUrl,string AccessCode,bool IsActive,int NoOfQuestions)
        {
            this.m_strAssignmentName = AssignmentName;
            this.m_strAssignmentDescription = AssignmentDescription;
            this.m_strAssignmentCreationDate = CreationDate;
            this.m_strAssignmentUpdationDate = UpdationDate;
            this.m_iAssignmentType = AssignmentType;
            if (this.m_iAssignmentType == (int)Constants.AssignmentQuestionType.MCQ)
            {
                this.m_strAssignmentType = Constants.ASSIGNMENT_TYPE_MCQ;
            }
            else if (this.m_iAssignmentType == (int)Constants.AssignmentQuestionType.SUBJECTIVE)
            {
                this.m_strAssignmentType = Constants.ASSIGNMENT_TYPE_SUBJECTIVE;
            }
            this.m_strStatus = IsActive ? Constants.CONTENT_STATUS_ACTIVE : Constants.CONTENT_STATUS_INACTIVE;
            this.m_strTinyUrl = TinyUrl;
            this.m_strAccessCode = AccessCode;
            this.m_iTotalNoOfQuestions = NoOfQuestions;
        }
    }
}