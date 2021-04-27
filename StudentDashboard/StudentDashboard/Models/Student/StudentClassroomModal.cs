using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.Models.Student
{
    public class StudentClassroomModal
    {
        [JsonIgnore]
        public long m_llStudentId;
        [JsonProperty("classroom_name")]
        public string m_strClassroomName;
        [JsonProperty("classroom_description")]
        public string m_strClassroomDescription;
        [JsonProperty("date_of_joining")]
        public string m_strDateOfJoining;
        [JsonProperty("classroom_id")]
        public long m_llClassroomId;

        public StudentClassroomModal(long ClassroomId, string ClassroomName,string ClassroomDescription,string DateOfJoining)
        {
            this.m_llClassroomId = ClassroomId;
            this.m_strClassroomName = ClassroomName;
            this.m_strClassroomDescription = ClassroomDescription;
            this.m_strDateOfJoining = DateOfJoining;
        }
    }
}