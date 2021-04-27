using Newtonsoft.Json;
using StudentDashboard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpRequest
{
    public class UpdateClassroomBackgroundImageRequest
    {
        [JsonProperty("classroom_id")]
        public long m_llClassroomId;
        [JsonProperty("image_data")]
        public ImageUploadDetailsModal imageUploadDetailsModal;
    }
}