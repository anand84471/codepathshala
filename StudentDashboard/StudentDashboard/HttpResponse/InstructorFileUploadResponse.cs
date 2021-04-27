using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpResponse
{
    public class InstructorFileUploadResponse:APIDefaultResponse
    {
        [JsonProperty("file_location")]
        public string m_strAwsLocation;
    }
}