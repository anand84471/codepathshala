using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.Models
{
    public class MasterSearchRequestModel
    {
        [JsonProperty("serach_request")]
        public string m_strSearchRequest;
        [JsonProperty("no_of_record_fetched")]
        public int m_iNoOfRecordsFeteched;
        [JsonProperty("max_rows_to_be_fecthed")]
        public int m_iNoOfRecordsToBeFetched;
        [JsonIgnore]
        public long id;
    }
}