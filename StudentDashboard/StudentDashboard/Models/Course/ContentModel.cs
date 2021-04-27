using Newtonsoft.Json;
using System;

namespace StudentDashboard.Models.Course
{
    public class ContentModel
    {
        [JsonProperty("content_name")]
        public string m_strContentName { get; set; }
        [JsonProperty("content_description")]
        public string m_strContentDescription { get; set; }
        [JsonProperty("content_creation_date")]
        public DateTime m_dtContentCreationDate { get; set; }
        [JsonProperty("content_updation_date")]
        public DateTime m_dtContentUpdationDateTime { get; set; }
        [JsonProperty("content_file_path")]
        public string m_strFilePath { get; set; }        
    }
}