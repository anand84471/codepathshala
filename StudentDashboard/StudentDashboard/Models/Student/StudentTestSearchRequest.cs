using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.Models.Student
{
    public class StudentTestSearchRequest
    {
        [JsonProperty("search_string")]
        public string m_strSearchString;
        [JsonProperty("last_test_fetched")]
        public long m_llLastTestFetched;
        [JsonIgnore]
        public long m_llStudentId;
        [JsonIgnore]
        public int m_iNoOfRowsToBeFetched;
    }
}