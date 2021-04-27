using Newtonsoft.Json;
using StudentDashboard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpRequest
{
    public class UpdateCourseBackgroundImageRequest
    {
        [JsonProperty("course_id")]
        public long m_llCourseId;

        [JsonProperty("image_data")]
        public ImageUploadDetailsModal imageUploadDetailsModal;
    }
}