using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.Models.Document
{
    public class ClassroomBasicDetailsModalForHome
    {
        [JsonProperty("classroom_id")]
        public long m_llClassroomId;
        [JsonProperty("classroom_name")]
        public string m_strClassroomName;
        [JsonProperty("classroom_description")]
        public string m_strClassroomDescription;
        [JsonProperty("image")]
        public string m_strClassroomImage;
        public ClassroomBasicDetailsModalForHome(long ClassroomId,string ClassroomName,
            string ClassroomDescription,string ClassroomImagePath)
        {
            this.m_llClassroomId = ClassroomId;
            this.m_strClassroomName = ClassroomName;
            this.m_strClassroomDescription = ClassroomDescription;
            this.m_strClassroomImage = ClassroomImagePath;
        }

    }
}