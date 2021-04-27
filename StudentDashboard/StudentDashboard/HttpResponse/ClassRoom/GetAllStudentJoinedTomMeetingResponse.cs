using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpResponse.ClassRoom
{
    public class GetAllStudentJoinedTomMeetingResponse:APIDefaultResponse
    {
        [JsonProperty("student_details")]
        public List<StudentMeetingJoinedResponse> m_lsStudnetJoinedToMeeting;
    }
}