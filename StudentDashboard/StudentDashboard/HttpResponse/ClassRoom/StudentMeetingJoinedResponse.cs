using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpResponse.ClassRoom
{
    public class StudentMeetingJoinedResponse
    {
        [JsonProperty("student_id")]
        public long m_llStudentId;
        [JsonProperty("student_name")]
        public string m_strStudentName;
        [JsonProperty("time_of_joining")]
        public string m_strTimeOfJoining;
        public StudentMeetingJoinedResponse(long StudentId,string StudentName,string DateOfJoining)
        {
            this.m_llStudentId = StudentId;
            this.m_strStudentName = StudentName;
            this.m_strTimeOfJoining = DateOfJoining;
        }
    }
}