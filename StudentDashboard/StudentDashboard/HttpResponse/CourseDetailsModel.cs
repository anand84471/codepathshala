using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpResponse
{
    public class CourseDetailsModel
    {
        [JsonProperty("course_name")]
        public string m_strCourseName { get; set; }
        [JsonProperty("course_description")]
        public string m_strCourseDescription { get; set; }
        [JsonProperty("insertion_date")]
        public string m_dtCourseCreationDate { get; set; }
        [JsonProperty("course_id")]
        public long m_llCourseid { get; set; }
        [JsonProperty("no_of_indexes")]
        public int m_iNoOfIndexses { get; set; }
        [JsonProperty("course_activation_date")]
        public string m_strCourseActivationDate { get; set; }
        [JsonIgnore]
        public byte m_iCourseStatusId { get; set; }
        [JsonProperty("no_of_students_joined")]
        public int m_iTotalNoOfStudentsJoined;
        [JsonProperty("course_status")]
        public string m_strCourseStatus { get; set; }
        [JsonProperty("instructor_name")]
        public string m_strInstructorName;
        
        public CourseDetailsModel()
        {

        }
        public CourseDetailsModel(long Id, string Name, string CreationDate, int NoOfindexes,string CourseStatus,int NoOfStudentsJoined)
        {
            this.m_llCourseid = Id;
            this.m_dtCourseCreationDate = CreationDate;
            this.m_strCourseName = Name;
            this.m_iNoOfIndexses = NoOfindexes;
            this.m_strCourseStatus = CourseStatus;
            this.m_iTotalNoOfStudentsJoined = NoOfStudentsJoined;
        }
        public CourseDetailsModel(long CourseId,string CourseName,string CourseDescription,string ActivationDate)
        {
            this.m_llCourseid = CourseId;
            this.m_strCourseName = CourseName;
            this.m_strCourseDescription = CourseDescription;
            this.m_strCourseActivationDate = ActivationDate;
        }
        public CourseDetailsModel(long CourseId, string CourseName, string CourseDescription, string ActivationDate,int NoOfStudentsJoined,string InstructorName)
        {
            this.m_llCourseid = CourseId;
            this.m_strCourseName = CourseName;
            this.m_strCourseDescription = CourseDescription;
            this.m_strCourseActivationDate = ActivationDate;
            this.m_strInstructorName = InstructorName;
            this.m_iTotalNoOfStudentsJoined = NoOfStudentsJoined;
        }

    }
}