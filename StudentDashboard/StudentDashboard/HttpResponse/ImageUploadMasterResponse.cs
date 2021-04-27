using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpResponse
{
    public class ImageUploadMasterResponse:APIDefaultResponse
    {
        [JsonProperty("file_location")]
        public string m_strRawImagePathUrl;
        [JsonProperty("small_size_icon_url")]
        public string m_str50pxThumbnailUrl;
        [JsonProperty("medium_size_icon_url")]
        public string m_str250pxThumb;
    }
}