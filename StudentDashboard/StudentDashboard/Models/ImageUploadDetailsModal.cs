using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.Models
{
    public class ImageUploadDetailsModal
    {
        [JsonProperty("original_url")]
        public string m_strOriginalFileUrl;
        [JsonProperty("medium_size_url")]
        public string m_strMediumSizeUrl;
        [JsonProperty("small_size_url")]
        public string m_strSmallSizeUrl;
    }
}