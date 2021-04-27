using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpRequest
{
    public class GetAllStudentFollowedByStudentRequest
    {
        [JsonProperty("student_id")]
        public long m_llStudentId;
        [JsonProperty("no_of_rows_to_be_fetched")]
        public int m_iNoOfRowsToBeFetched;
        [JsonProperty("no_of_rows_fetched")]
        public int m_iNoOfRowsFetched;
    }
}