using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpRequest
{
    public class SerchCourseRequest
    {
        [JsonProperty("key")]
        public string m_strKey { get; set; }
        [JsonProperty("no_of_rows_fetched")]
        public int m_iNoOfRowsFetched;
        [JsonProperty("sorting_id")]
        public int m_iSortingId;
        [JsonProperty("student_id")]
        public long m_llStudentId;

    }
}