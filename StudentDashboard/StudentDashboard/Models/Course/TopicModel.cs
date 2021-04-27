using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.Models.Course
{
    public class TopicModel
    {
        [JsonProperty("topic_id")]
        public long m_llTopicId { get; set; }
        [JsonProperty("topic_name")]
        public string m_strTopicName { get; set; }
        [JsonProperty("creation_date")]
        public string m_strRowInsertionDateTime { get; set; }
        [JsonProperty("updation_date")]
        public string m_strRowUpdationDateTime { get; set; }
        [JsonProperty("topic_description")]
        public string m_strTopicDescription { get; set; }
        [JsonProperty("index_id")]
        public long m_llIndexId { get; set; }
        [JsonProperty("file_path")]
        public string m_strFilePath { get; set; }
        public short m_iFileUploadTypeId { get; set; }
        [JsonIgnore]
        public byte? m_iFileUploadTypeIdNew { get; set; }
        [JsonProperty("file_attachment_path")]
        public string m_strFileAttachmentPath { get; set; }
        public TopicModel()
        {

        }
        public TopicModel(long Id,string TopicName,string TopicDesciption,string FilePath,string FileAttchmentPath, string CreationDate,string UpdationDate)
        {
            this.m_strRowInsertionDateTime = CreationDate;
            this.m_strRowUpdationDateTime = UpdationDate;
            this.m_llTopicId = Id;
            this.m_strTopicName = TopicName;
            this.m_strTopicDescription = TopicDesciption;
            this.m_strFilePath = FilePath;
            this.m_strFileAttachmentPath = FileAttchmentPath;
        }
    }
}