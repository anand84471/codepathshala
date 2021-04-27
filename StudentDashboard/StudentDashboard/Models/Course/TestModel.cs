using Newtonsoft.Json;
using StudentDashboard.HttpResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.Models.Course
{
    public class TestModel:APIDefaultResponse
    {
        [JsonProperty("index_id")]
        public long m_llIndexId { get; set; }   
        [JsonProperty("course_id")]
        public long m_llCourseId { get; set; }
        [JsonProperty("test_id")]
        public long m_llTestId { get; set; }
        [JsonProperty("test_type")]
        public string m_strTestType { get; set; }
        public short m_iTestType { get; set; }
        [JsonProperty("test_creation_datetime")]
        public string m_strCreationDate { get; set; }
        [JsonProperty("test_updation_datetime")]
        public string m_strUpdationDate { get; set; }
        [JsonProperty("max_time_allowed_for_test")]
        public int? m_iTimeForTest { get; set; }
        [JsonProperty("is_test_active")]
        public bool m_bIsTestActive { get; set; }
        [JsonProperty("test_name")]
        public string m_strTestName { get; set; }
        [JsonProperty("test_description")]
        public string m_strTestDescription { get; set; }
        [JsonProperty("file_path")]
        public string m_strFilePath { get; set; }
        [JsonProperty("file_type_id")]
        public short m_iFileTypeId { get; set; }
        [JsonProperty("mcq_questions")]
        public List<McqQuestion> m_lsMcqQuestion { get; set; }
        [JsonProperty("instructor_id")]
        public int m_iInstructorId { get; set; }
        [JsonProperty("total_marks")]
        public int? m_iTotalMarks { get; set; }
        [JsonProperty("total_no_of_questions")]
        public int? m_iTotalNoOfQuestions { get; set; }
        [JsonProperty("status")]
        public string m_strTestSatus;
        [JsonProperty("tiny_url")]
        public string m_strTinyUrl;
        [JsonProperty("access_code")]
        public string m_strAccessCode;
        [JsonProperty("classroom_id")]
        public long m_llClassroomId;
        [JsonProperty("is_classroom_acess")]
        public bool m_bIsClassroomAccess;
        public TestModel()
        {

        }
        public TestModel(long TestId,string TestName,string TestDescription,string CreationDate,string UpdationDate,int? TotalNoOfQuestions,int? TotalMarks,int? TotalTime,
            bool IsActive,string TinyUrl,string AccessCode)
        {
            this.m_llTestId = TestId;
            this.m_strTestName = TestName;
            this.m_strTestDescription = TestDescription;
            this.m_iTotalMarks = TotalMarks;
            this.m_iTimeForTest = TotalTime;
            this.m_strCreationDate = CreationDate;
            this.m_strUpdationDate = UpdationDate;
            this.m_iTotalNoOfQuestions = TotalNoOfQuestions;
            this.m_strAccessCode = AccessCode;
            this.m_strTestSatus = IsActive ?"active":"in active";
            this.m_strTinyUrl = TinyUrl;
        }
        public TestModel(long TestId, string TestName, string TestDescription, string CreationDate, string UpdationDate, int? TotalNoOfQuestions, int? TotalMarks, int? TotalTime
)
        {
            this.m_llTestId = TestId;
            this.m_strTestName = TestName;
            this.m_strTestDescription = TestDescription;
            this.m_iTotalMarks = TotalMarks;
            this.m_iTimeForTest = TotalTime;
            this.m_strCreationDate = CreationDate;
            this.m_strUpdationDate = UpdationDate;
            this.m_iTotalNoOfQuestions = TotalNoOfQuestions;
           
        }
    }
}