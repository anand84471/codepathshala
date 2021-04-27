using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpResponse
{
    public class APIObjectInfoResponse:APIDefaultResponse
    {
       [JsonProperty("details")]
       List<ObjectInfoDetailsResponse> m_lsObjectDetails { get; set; }



    }
}