using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpResponse.ClassRoom
{
    public class ClassroomBasicDetailsModal
    {

        [JsonProperty("classroom_id")]
        public long m_llClassRoomId;
        [JsonProperty("classroom_name")]
        public string m_strClassRoomName;
        [JsonProperty("creation_date")]
        public string m_strCreationDate;
        [JsonProperty("status")]
        public string m_strStatus;
        public ClassroomBasicDetailsModal(long ClassroomId,string ClassroomName,string CreationDate,string Status)
        {
            this.m_llClassRoomId = ClassroomId;
            this.m_strClassRoomName = ClassroomName;
            this.m_strStatus = Status;
            this.m_strCreationDate = CreationDate;
        }
    }
}