using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.Models.Instructor
{
    public class InstructorTestSeriesModal
    {
        [JsonProperty("instructor_id")]
        public int m_iInstructorId;
        [JsonProperty("test_series_name")]
        public string m_strTestSeriesName;
        [JsonProperty("test_series_description")]
        public string m_strTestSeriesDescription;
        [JsonProperty("creation_date")]
        public string m_strCreationDate;
        [JsonProperty("last_updation_date")]
        public string m_strLastUpdationDate;
        [JsonProperty("test_series_id")]
        public long m_llTestSeriesId;
        [JsonProperty("status")]
        public string m_strTestSeriesStatus;
        [JsonProperty("no_of_tests")]
        public int m_iNoOfTests;
        [JsonProperty("test_series_image")]
        public string m_strTestSeriesImage;
    }
}