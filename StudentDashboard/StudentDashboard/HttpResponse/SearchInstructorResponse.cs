using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpResponse
{
    public class SearchInstructorResponse:APIDefaultResponse
    {
        [JsonProperty("instructors")]
        public List<SearchInstructorResponseModal> lsSearchInstructorResponseModal;
    }
}