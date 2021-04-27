using Newtonsoft.Json;
using StudentDashboard.HttpResponse;
using System.Collections.Generic;

namespace StudentDashboard.Models.Course
{
    public class IndexModel:APIDefaultResponse
    {
        [JsonProperty("index_name")]
        public string m_strIndexName { get; set; }
        [JsonProperty("index_description")]
        public string m_strIndexDescription { get; set; }
        [JsonProperty("index_contents")]
        public List<ContentModel> m_lsContents { get; set; }       
        [JsonProperty("assignment_for_index")]  
        public AssignmentModel m_objAssignmentModel { get; set; }
        [JsonProperty("test_for_index")]
        public TestModel m_objTestModel { get; set; } 
        [JsonProperty("course_id")]
        public long m_llCourseId { get; set; }
        [JsonProperty("index_id")]
        public long m_llIndexId { get; set; }
        [JsonProperty("topic_details")]
        public List<TopicModel> m_lsTopicModel { get; set; }
        [JsonProperty("creation_date")]
        public string m_strRowInsertionDateTime { get; set; }
        [JsonProperty("updation_date")]
        public string m_strRowUpdationDateTime { get; set; }
        [JsonProperty("assignment_id")]
        public long m_llAssignmentId { get; set; }
        [JsonProperty("assignment_name")]
        public string m_strAssignmentName { get; set; }
        [JsonProperty("test_id")]
        public long? m_llTestId { get; set; }
        [JsonProperty("test_name")]
        public string m_strTestName { get; set; }
        public IndexModel()
        {

        }
        public IndexModel(long IndexId,string IndexName,string IndexDescription,string CreationDate,string UpdationDate)
        {
            this.m_llIndexId = IndexId;
            this.m_strIndexName = IndexName;
            this.m_strIndexDescription = IndexDescription;
            this.m_strRowInsertionDateTime = CreationDate;
            this.m_strRowUpdationDateTime = UpdationDate;
        }
    }
}