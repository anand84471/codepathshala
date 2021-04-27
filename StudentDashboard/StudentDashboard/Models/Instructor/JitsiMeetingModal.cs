using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.Models.Instructor
{
    public class JitsiMeetingModal
    {
        public string m_strMeetingName;
        public long m_llMeetingId;
        public string m_strMeetingPassword;
        public string m_strMeetingTopic;
        public long m_llClassroomId;
        public JitsiMeetingModal()
        {

        }
        public JitsiMeetingModal(long MeetingId,string MeetingName,string MeetingPassword,string MeetingTopic)
        {
            this.m_llMeetingId = MeetingId;
            this.m_strMeetingName = MeetingName;
            this.m_strMeetingPassword = MeetingPassword;
            this.m_strMeetingTopic = MeetingTopic;
        }
    }

}