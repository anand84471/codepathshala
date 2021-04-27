using System.Collections.Generic;
using Newtonsoft.Json;
using StudentDashboard.Models.Instructor;
namespace StudentDashboard.HttpResponse
{
    public class SearchInstructorByUserIdResponse:APIDefaultResponse
    {
        [JsonProperty("instructors")]
        public List<MasterInstructorDetails> m_lsMasterInstructorDetails;

    }
}