using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpResponse.ClassRoom
{
    public class GetClassroomAllMessageResponseForStudent:APIDefaultResponse
    {
        [JsonProperty("message_details")]
        public List<ClassroomStudentMessageModal> m_lsGetClassroomAllMessageResponseForStudent;
    }
}