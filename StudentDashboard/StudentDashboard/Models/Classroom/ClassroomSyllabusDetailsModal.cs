using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.Models.Classroom
{
    public class ClassroomSyllabusDetailsModal
    {
        [JsonProperty("classroom_id")]
        public long m_llClassroomId;
        [JsonProperty("week_wise_schedule")]
        public List<ClassroomWeekWiseSyallabus> m_lsClassroomWeekWiseSyallabus;
        public string m_strSerializedSyllabus;
        public ClassroomSyllabusDetailsModal()
        {

        }
        public ClassroomSyllabusDetailsModal(string SerializedSyallabus)
        {
            this.m_strSerializedSyllabus = SerializedSyallabus;
        }
    }
    public class ClassroomWeekWiseSyallabus
    {
        [JsonProperty("week_name")]
        public string m_strWeekName;
        [JsonProperty("topics_to_be_covered")]
        public List<String> lsTopicsToBeCovered;
    }
    public class ClassroomTopicsDetails
    {
        [JsonProperty("topic_name")]
        public string m_strTopicName;
        [JsonProperty("is_topic_for_preview")]
        public bool bIsTopicForPreview;
        [JsonProperty("topic_preview_url")]
        public string m_strTopicPreviewUrl;
    }
}