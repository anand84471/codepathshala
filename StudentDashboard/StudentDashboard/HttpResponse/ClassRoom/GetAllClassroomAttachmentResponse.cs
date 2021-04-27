using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpResponse.ClassRoom
{
    public class GetAllClassroomAttachmentResponse:APIDefaultResponse
    {
        [JsonProperty("attachments")]
        public List<ClassroomAttachmentDetailsResponse> m_lsAttachments;
    }
}