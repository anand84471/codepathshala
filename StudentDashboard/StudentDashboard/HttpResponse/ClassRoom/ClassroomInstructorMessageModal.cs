using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpResponse.ClassRoom
{
    public class ClassroomInstructorMessageModal
    {
        [JsonProperty("message_id")]
        public long m_llMessageId;
        [JsonProperty("message")]
        public string m_strMessage;
        [JsonProperty("message_creation_time")]
        public string m_strMessageCreationTime;
        [JsonProperty("student_name")]
        public string m_strStudentName;
        [JsonProperty("is_instructor")]
        public bool m_bIsInstructor;
        public ClassroomInstructorMessageModal(string Message,string StudentName,string CreationTime,long MessageId,
            bool IsInstructor)
        {
            this.m_llMessageId = MessageId;
            this.m_strMessage = Message;
            this.m_strMessageCreationTime = CreationTime;
            this.m_strStudentName = StudentName;
            this.m_bIsInstructor = IsInstructor;
        }
    }
}