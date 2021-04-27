using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpRequest
{
    public class StudentsJoinedToClassroomRequestForStudent
    {
        [JsonProperty("student_id")]
        public long m_llStudentId;
        [JsonProperty("classroom_id")]
        public long m_llClassroomId;
        [JsonProperty("no_of_rows_fetched")]
        public int m_iNoOfRowsFetched;
        [JsonProperty("max_rows_to_be_fetched")]
        public int m_iMaxRowsToBeFetched;
    }
}