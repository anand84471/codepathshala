using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.Models.Classroom
{
    public class GetLiveClassDetailsForInstructor
    {
        [JsonProperty("class_topic")]
        public string m_strClassTopic;
        [JsonProperty("topic_description")]
        public string m_strClassTopicDescription;
        [JsonProperty("class_start_time")]
        public string m_strClassStartTime;
        [JsonProperty("no_of_participants")]
        public int m_iNoOfParticipants;
        [JsonProperty("video_url")]
        public string m_strVideoUrl;
        public GetLiveClassDetailsForInstructor(string ClassTopic,string ClassTopicDescription, string VideoUrl,
            string ClassStartTime,int NoOfParticipants
            )
        {
            this.m_strClassStartTime = ClassStartTime;
            this.m_strClassTopic = ClassTopic;
            this.m_strClassTopicDescription = ClassTopicDescription;
            this.m_strVideoUrl = VideoUrl;
            this.m_iNoOfParticipants = NoOfParticipants;

        }
    }
}