using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpResponse
{
    public class FileUploadResponse:APIDefaultResponse
    {
        [JsonProperty("path")]
        public string FilePath { get; set; }
    }
}