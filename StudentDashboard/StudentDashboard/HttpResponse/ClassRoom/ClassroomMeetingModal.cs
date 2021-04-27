using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpResponse.ClassRoom
{
    public class ClassroomMeetingModal
    {
        [JsonProperty("meeting_start_time")]
        public string m_strMeetingStartTime;
        [JsonProperty("meeting_id")]
        public long m_llMeetingId;
        [JsonProperty("meeting_finish_time")]
        public string m_strMeetingFinishTime;
        [JsonProperty("meeting_status")]
        public string m_strMeetingStatus;
        [JsonProperty("participants_count")]
        public int m_iNoOfPartcipants;
        [JsonProperty("topic_name")]
        public string m_strTopicName;
        [JsonProperty("meeting_notes")]
        public string m_strNotes;
        [JsonProperty("video_url")]
        public string m_strVideoUrl;
        public ClassroomMeetingModal(long MeetingId,string MeetingStartTime,
            string MeetingFinishTime,bool MeetingStatus,int NoOfParticipants,string TopicName,
            string TopicDescription,string VideoUrl)
        {
            this.m_llMeetingId = MeetingId;
            this.m_strMeetingStartTime = MeetingStartTime;
            this.m_strMeetingFinishTime = MeetingFinishTime;
            this.m_strMeetingStatus = MeetingStatus ? Constants.MEETING_STATUS_ACTIVE : Constants.MEETING_STATUS_CLOSED;
            this.m_iNoOfPartcipants = NoOfParticipants;
            this.m_strTopicName = TopicName;
            this.m_strNotes = TopicDescription;
            this.m_strVideoUrl = VideoUrl;
        }
    }
}