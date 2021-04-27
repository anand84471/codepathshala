using Newtonsoft.Json;
using StudentDashboard.Models.Instructor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpResponse.ClassRoom
{
    public class GetAllClassroomMeetingResponse:APIDefaultResponse
    {
        [JsonProperty("meeting_details")]
        public List<ClassroomMeetingModal> m_lsClassroomMeetingModal;

    }
}