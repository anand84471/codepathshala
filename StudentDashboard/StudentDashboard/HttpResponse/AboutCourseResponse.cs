using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpResponse
{
    public class AboutCourseResponse:APIDefaultResponse
    {
        [JsonProperty("course_name")]
        public string m_strCourseName { get; set; }
        [JsonProperty("course_description")]
        public string m_strCourseDescription { get; set; }
        [JsonProperty("course_creation_date")]
        public string m_strCourseCreationDate { get; set; }
        [JsonProperty("course_updation_date")]
        public string m_strCourseUpdationDate { get; set; }
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
        public List<BasicAssignmentDetails> m_lsAssignmentDetails { get; set; }
        [JsonProperty("test_details")]
        public List<BasicTestDetails> m_lsTestDetails { get; set; }
        [JsonProperty("index_details")]
        public List<BasicIndexDetails> m_lsIndexDetails { get; set; }
        [JsonProperty("course_joining_fee")]
        public int m_iCourseJoiningFee;
        [JsonProperty("course_joinin_fee_in_rupees")]
        public string m_strCourseJoiningFeeInRupees;
        public AboutCourseResponse():base()
        {
            
        }
        public AboutCourseResponse(string CourseName,string CourseDescription,String CourseCreationDate,string CourseUpdationDateTime,
            string CourseStatus,string ShareUrl,string AccessCode,int CourseJoiningFeeInPaise)
        {
            this.m_strCourseName = CourseName;
            this.m_strCourseDescription = CourseDescription;
            this.m_strCourseCreationDate = CourseCreationDate;
            this.m_strCourseUpdationDate = CourseUpdationDateTime;
            this.m_strCourseStatus = CourseStatus;
            this.m_strCourseShareUrl = ShareUrl;
            this.m_strAccessCode = AccessCode;
            m_lsIndexDetails = new List<BasicIndexDetails>();
            m_lsAssignmentDetails = new List<BasicAssignmentDetails>();
            m_lsTestDetails = new List<BasicTestDetails>();
            this.m_iCourseJoiningFee = CourseJoiningFeeInPaise/100;
            if(this.m_iCourseJoiningFee==0)
            {
                this.m_strCourseJoiningFeeInRupees = "&#x20B9 free";
            }
            else
            {
                this.m_strCourseJoiningFeeInRupees = "&#x20B9 "+this.m_iCourseJoiningFee.ToString();
            }
        } 
        public void SetCounts()
        {
            m_iTestCount = m_lsTestDetails.Count;
            m_iIndexCount = m_lsIndexDetails.Count;
            m_iAssignmentCount = m_lsAssignmentDetails.Count;
        }
        public void AddTest(string TestName,long? TestId)
        {
            BasicTestDetails objBasicTestDetails = new BasicTestDetails();
            objBasicTestDetails.m_llTestId = TestId;
            objBasicTestDetails.m_strTestName = TestName;
            m_lsTestDetails.Add(objBasicTestDetails);
        }
        public void AddIndex(string IndexName,long IndexId)
        {
            BasicIndexDetails objBasicIndexDetails = new BasicIndexDetails();
            objBasicIndexDetails.m_llIndexId = IndexId;
            objBasicIndexDetails.m_strIndexName = IndexName;
            m_lsIndexDetails.Add(objBasicIndexDetails);
        }
        public void AddAssignment(string AssignmentName,long? AssignmentId)
        {
            BasicAssignmentDetails objBasicAssignmentDetails = new BasicAssignmentDetails((long)AssignmentId, AssignmentName);
            m_lsAssignmentDetails.Add(objBasicAssignmentDetails);
        }
        public void IncremetTopicCount(int count)
        {
            this.m_iTopicCount += count;
        }
    }
}