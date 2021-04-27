using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpRequest
{
    public class MasterSearchRequest
    {
        [JsonProperty("key")]
        public string m_strSearchString;
        [JsonProperty("no_of_rows_fetched")]
        public int m_iNoOfRowsFetched;

    }
}