using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpResponse.ClassRoom
{
    public class ClassroomAttachmentDetailsResponse
    {
        [JsonProperty("classroom_id")]
        public long m_llClassroomId;
        [JsonProperty("attachment_id")]
        public long m_llAttachmentId;
        [JsonProperty("attachment_name")]
        public string m_strAttachmentName;
        [JsonProperty("attachment_description")]
        public string m_strAttachmentDescription;
        [JsonProperty("date_of_addition")]
        public string m_strAttachmentCreationDate;
        [JsonProperty("attachment_url")]
        public string m_strAttachmentUrl;
        public ClassroomAttachmentDetailsResponse(long AttachmentId,string AttachmentName,string AttachmentDescription,string DateOfAddition,string AttachmentUrl)
        {
            this.m_llAttachmentId = AttachmentId;
            this.m_strAttachmentName = AttachmentName;
            this.m_strAttachmentDescription = AttachmentDescription;
            this.m_strAttachmentCreationDate = DateOfAddition;
            this.m_strAttachmentUrl = AttachmentUrl;
        }
    }
}