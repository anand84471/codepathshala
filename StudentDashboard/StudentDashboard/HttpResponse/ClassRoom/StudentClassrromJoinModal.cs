using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpResponse.ClassRoom
{
    public class StudentClassrromJoinModal
    {
        [JsonProperty("classroom_id")]
        public long m_llClassroomId;
        [JsonProperty("student_id")]
        public long m_llStudentId;
        [JsonProperty("student_name")]
        public string m_strStudentName;
        [JsonProperty("date_of_joining")]
        public string m_strDateOfJoining;
        public StudentClassrromJoinModal()
        {

        }
        public StudentClassrromJoinModal(long StudentId,string StudentName,string DateOfJoining)
        {
            this.m_strDateOfJoining = DateOfJoining;
            this.m_strStudentName = StudentName;
            this.m_llStudentId = StudentId;
        }

    }
}