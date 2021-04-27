using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpResponse
{
    public class GetCourseDetailsApiResponse:APIDefaultResponse
    {
        [JsonProperty("course_name")]
        public string m_strCourseName { get; set; }
        [JsonProperty("course_description")]
        public string m_strCourseDescription { get; set; }
        [JsonProperty("course_creation_date")]
        public string m_strCourseCreationDate { get; set; }
        [JsonProperty("course_updation_date")]
        public string m_strCourseUpdationDate { get; set; }
        [JsonProperty("course_status")]
        public string m_strCourseStatus { get; set; }
        [JsonProperty("course_share_url")]
        public string m_strShareUrl;
        [JsonProperty("course_access_code")]
        public string m_strCourseAccessCode;
        [JsonProperty("indexes")]
        public List<CourseIndexDetails> m_lsIndexes { get; set; }
        [JsonProperty("course_joining_fee")]
        public int m_iCourseJoiningFee;
        public GetCourseDetailsApiResponse(string CourseName, string CourseDescription, string CourseCreationDate,string CourseUpdationDate,
            string CourseStatus,string AccessCode,string TinyUrl ,int CourseJoingFee)
        {
            this.m_strCourseName = CourseName;
            this.m_strCourseDescription = CourseDescription;
            this.m_strCourseCreationDate = CourseCreationDate;
            this.m_strCourseUpdationDate = CourseUpdationDate;
            this.m_strCourseStatus = CourseStatus;
            this.m_strShareUrl = TinyUrl;
            this.m_strCourseAccessCode = AccessCode;
            this.m_iCourseJoiningFee = CourseJoingFee;
        }
        public GetCourseDetailsApiResponse()
        {

        }
    }
}