using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpResponse.ClassRoom
{
    public class ClassroomStudentMessageModal
    {
        [JsonProperty("message_id")]
        public long m_llMessageId;
        [JsonProperty("message")]
        public string m_strMessage;
        [JsonProperty("message_creation_time")]
        public string m_strMessageCreationTime;
        [JsonProperty("sender_name")]
        public string m_strSenderName;
        [JsonProperty("is_instructor")]
        public bool m_bIsInstructor;
        [JsonProperty("is_me")]
        public bool m_bIsSelf;
        public ClassroomStudentMessageModal(string Message, string StudentName, string CreationTime, long MessageId,
            bool IsInstructor,bool IsMe)
        {
            this.m_llMessageId = MessageId;
            this.m_strMessage = Message;
            this.m_strMessageCreationTime = CreationTime;
            this.m_strSenderName = StudentName;
            this.m_bIsInstructor = IsInstructor;
            this.m_bIsSelf = IsMe;
            if (this.m_bIsInstructor) { this.m_strSenderName = Constants.MESSEGE_SENDER_CLASSROOM; }
        }
    }
}