using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.Models.Instructor
{
    public class InstrucorEarningDetailsModal
    {
        [JsonProperty("instructor_id")]
        public string m_iInstructorId;
        [JsonProperty("classroom_earning")]
        public int m_iClassroomEarning;
        [JsonProperty("course_earning")]
        public int m_iCourseEarning;
        [JsonProperty("total_earning")]
        public int m_iTotalEarning;
        [JsonProperty("classroom_earning_details")]
        public List<InstructorClassroomEarningModal> m_lsInstructorClassroomEarningModal;
        [JsonProperty("course_earning_details")]
        public List<InstructorCourseEarningDetailsModal> m_lsInstructorCourseEarningDetailsModal;
        public InstrucorEarningDetailsModal(int ClassroomEarning,int CourseEarning)
        {
            this.m_iClassroomEarning = ClassroomEarning/100;
            this.m_iCourseEarning = CourseEarning/100;
            this.m_iTotalEarning = m_iClassroomEarning + m_iCourseEarning;
        }
        public InstrucorEarningDetailsModal()
        {

        }

    }
}