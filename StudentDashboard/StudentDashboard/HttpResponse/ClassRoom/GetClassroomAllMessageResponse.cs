using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpResponse.ClassRoom
{
    public class GetClassroomAllMessageResponse:APIDefaultResponse
    {
        [JsonProperty("message_details")]
        public List<ClassroomInstructorMessageModal> m_lsClassroomInstructorMessageModal;
    }
}