using Newtonsoft.Json;
using StudentDashboard.Models.Instructor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpRequest
{
    public class InstructorAcademicRecordUpdateRequest
    {
        [JsonProperty("instructor_id")]
        public int m_iInstructorId;
        [JsonProperty("lastes_qualification")]
        public string m_strLatestQualification;
        [JsonProperty("linkedin_id")]
        public string m_strLinkedIn;
        [JsonProperty("google_scholar_id")]
        public string m_strGoogleScholarId;
        [JsonProperty("school_details")]
        public InstructorSchoolDetailsModal instructorSchoolDetailsModal;

    }
}