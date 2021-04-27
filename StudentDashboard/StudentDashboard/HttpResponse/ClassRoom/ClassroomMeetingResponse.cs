using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpResponse.ClassRoom
{
    public class ClassroomMeetingResponse:APIDefaultResponse
    {
        [JsonProperty("meeting_name")]
        public string m_strMeetingName;
        [JsonProperty("meeting_password")]
        public string m_strMeetingPassword;
        [JsonProperty("meeting_id")]
        public long m_llMeetingId;
        public ClassroomMeetingResponse() : base()
        {


        }
        public ClassroomMeetingResponse(long MeetingId,string MeetingName,string MeetingPassword) : base()
        {
            this.m_llMeetingId = MeetingId;
            this.m_strMeetingName = MeetingName;
            this.m_strMeetingPassword = MeetingPassword;
        }
    }
}