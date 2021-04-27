using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpResponse
{
    public class SearchStudentPublicClassroomResponse:APIDefaultResponse
    {
        [JsonProperty("classrooms")]
       public List<GetPublicClassroomsResponse> m_lsGetPublicClassroomsResponse;
    }
}