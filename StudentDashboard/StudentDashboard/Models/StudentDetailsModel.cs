using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpRequest
{
    public class StudentDetailsModel
    {
        [JsonProperty("student_name")]
        public string m_strStudentName { get; set; }
        [JsonProperty("email")]
        public string m_strEmail { get; set; }
        [JsonProperty("id")]
        public string m_iStudentId { get; set; }
        [JsonProperty("courses")]
        public List<string> m_lsAllCourseName{get;set;}
        [JsonProperty("tests")]
        public List<string> m_lsAllTestNames { get; set; }
        [JsonProperty("assignments")]
        public List<string> m_lsAllAssignmentNames { get; set; }
        [JsonProperty("total_no_of_courses")]
        public int m_iNoOfAvailableCourse { get; set; }
        [JsonProperty("no_of_completed_course")]
        public int m_NoOfCourseComplted { get; set; } 
    }
}