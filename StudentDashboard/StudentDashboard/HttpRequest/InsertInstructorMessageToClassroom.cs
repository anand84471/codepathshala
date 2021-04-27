using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpRequest
{
    public class InsertInstructorMessageToClassroom
    {
        [JsonProperty("message")]
        public string m_strMessage;
        [JsonIgnore]
        public int m_InstructorId;
        [JsonProperty("classroom_id")]
        public long m_llClassroomId;
        [JsonProperty("message_id")]
        public long m_llMessageId;
    }
}