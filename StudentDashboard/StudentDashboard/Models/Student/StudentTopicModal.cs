using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.Models.Student
{
    public class StudentTopicModal
    {
        [JsonProperty("topic_id")]
        public long m_llTopicId { get; set; }
        [JsonProperty("topic_name")]
        public string m_strTopicName { get; set; }
        [JsonProperty("topic_description")]
        public string m_strTopicDescription { get; set; }
        [JsonProperty("index_id")]
        public long m_llIndexId { get; set; }
        [JsonProperty("file_path")]
        public string m_strFilePath { get; set; }
        public short m_iFileUploadTypeId { get; set; }
        [JsonProperty("is_compelted")]
        public bool? m_IsCompleted;
        [JsonProperty("file_attachment_path")]
        public string m_strFileAttachmentPath { get; set; }
        [JsonIgnore]
        public byte? m_iFileUploadTypeIdNew { get; set; }
        public StudentTopicModal()
        {

        }
        public StudentTopicModal(long Id, string TopicName, string TopicDesciption, string FilePath,bool? IsCompleted,string FileAttachmentPath)
        {
            this.m_llTopicId = Id;
            this.m_strTopicName = TopicName;
            this.m_strTopicDescription = TopicDesciption;
            this.m_strFilePath = FilePath;
            this.m_IsCompleted = IsCompleted;
            this.m_strFileAttachmentPath = FileAttachmentPath;
        }
    }
}