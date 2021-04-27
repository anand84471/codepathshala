using Newtonsoft.Json;
using StudentDashboard.HttpResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.Models.Student
{
    public class StudentCourseModal:APIDefaultResponse
    {
        [JsonProperty("course_name")]
        public string m_strCourseName { get; set; }
        [JsonProperty("course_description")]
        public string m_strCourseDescription { get; set; }
       
        [JsonProperty("index_count")]
        public int m_iIndexCount { get; set; }
        [JsonProperty("test_count")]
        public int m_iTestCount { get; set; }
        [JsonProperty("assignment_count")]
        public int m_iAssignmentCount { get; set; }
        [JsonProperty("course_status")]
        public string m_strCourseStatus { get; set; }
        [JsonProperty("course_share_url")]
        public string m_strCourseShareUrl { get; set; }
        [JsonProperty("course_access_code")]
        public string m_strAccessCode { get; set; }
        [JsonProperty("topic_count")]
        public int m_iTopicCount { get; set; }
        [JsonProperty("m_strEstimatedTimeOfCourse")]
        public string m_strEstimatedTimeForCourse { get; set; }
        [JsonProperty("assignment_details")]
        public List<StudentBasicAssignmentDettails> m_lsAssignmentDetails { get; set; }
        [JsonProperty("test_details")]
        public List<StudentBasicTestDetails> m_lsTestDetails { get; set; }
        [JsonProperty("index_details")]
        public List<StudentBasicIndexDetails> m_lsIndexDetails { get; set; }
        public StudentCourseModal():base()
        {

        }
        public StudentCourseModal(string CourseName, string CourseDescription,string CourseStatus, string ShareUrl, string AccessCode)
        {
            this.m_strCourseName = CourseName;
            this.m_strCourseDescription = CourseDescription;
            this.m_strCourseStatus = CourseStatus;
            this.m_strCourseShareUrl = ShareUrl;
            this.m_strAccessCode = AccessCode;
            m_lsIndexDetails = new List<StudentBasicIndexDetails>();
            m_lsAssignmentDetails = new List<StudentBasicAssignmentDettails>();
            m_lsTestDetails = new List<StudentBasicTestDetails>();
        }
        public void SetCounts()
        {
            m_iTestCount = m_lsTestDetails.Count;
            m_iIndexCount = m_lsIndexDetails.Count;
            m_iAssignmentCount = m_lsAssignmentDetails.Count;
        }
        public void AddTest(string TestName, long? TestId)
        {
            StudentBasicTestDetails objBasicTestDetails = new StudentBasicTestDetails();
            objBasicTestDetails.m_llTestId = TestId;
            objBasicTestDetails.m_strTestName = TestName;
            m_lsTestDetails.Add(objBasicTestDetails);
        }
        public void AddIndex(string IndexName, long IndexId)
        {
            StudentBasicIndexDetails objBasicIndexDetails = new StudentBasicIndexDetails();
            objBasicIndexDetails.m_llIndexId = IndexId;
            objBasicIndexDetails.m_strIndexName = IndexName;
            m_lsIndexDetails.Add(objBasicIndexDetails);
        }
        public void AddAssignment(string AssignmentName, long? AssignmentId,bool ? IsCompleted)
        {
            StudentBasicAssignmentDettails objBasicAssignmentDetails = new StudentBasicAssignmentDettails((long)AssignmentId, AssignmentName, IsCompleted);
            m_lsAssignmentDetails.Add(objBasicAssignmentDetails);
        }
        public void IncremetTopicCount(int count)
        {
            this.m_iTopicCount += count;
        }
    }
}