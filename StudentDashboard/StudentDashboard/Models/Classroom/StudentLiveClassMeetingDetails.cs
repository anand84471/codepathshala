using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.Models.Classroom
{
    public class StudentLiveClassMeetingDetails
    {
        [JsonProperty("meeting_id")]
        public long m_llLiveClassMeetingId;
        [JsonProperty("topic_name")]
        public string m_strTopicName;
        [JsonProperty("student_join_id")]
        public long? m_llStudentJoinId;
        [JsonProperty("topic_description")]
        public string m_strTopicDescription;
        [JsonProperty("video_url")]
        public string m_strVideoUrl;
        [JsonProperty("date")]
        public string m_strDate;
        public StudentLiveClassMeetingDetails(long ClassroomMeetingId,
            string TopicName,long? StudentJoinId,string Date)
        {
            this.m_llLiveClassMeetingId = ClassroomMeetingId;
            this.m_strTopicName = TopicName;
            this.m_llStudentJoinId = StudentJoinId;
            this.m_strDate = Date;
        }
        public StudentLiveClassMeetingDetails(long ClassroomMeetingId,
            string TopicName,long? StudentJoinId,string TopicDescription,string VideoUrl,string Date)
        {
            this.m_llLiveClassMeetingId = ClassroomMeetingId;
            this.m_strTopicName = TopicName;
            this.m_strTopicDescription = TopicDescription;
            this.m_strVideoUrl = VideoUrl;
            this.m_llStudentJoinId = StudentJoinId;
            this.m_strDate = Date;
        }
    }
}