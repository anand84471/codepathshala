using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpRequest
{
    public class GetLiveClassroomDetailsForInstructorRequest
    {
        [JsonProperty("classroom_id")]
        public long m_llClassroomId;
        [JsonProperty("meeting_id")]
        public long m_llMeetingId;
    }
}