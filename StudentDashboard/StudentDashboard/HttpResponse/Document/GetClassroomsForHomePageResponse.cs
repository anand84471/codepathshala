using Newtonsoft.Json;
using StudentDashboard.Models.Document;
using System.Collections.Generic;

namespace StudentDashboard.HttpResponse.Document
{
    public class GetClassroomsForHomePageResponse:APIDefaultResponse
    {
        [JsonProperty("classrooms")]
        public List<ClassroomBasicDetailsModalForHome> m_lsClassroomBasicDetailsModal;
    }
}