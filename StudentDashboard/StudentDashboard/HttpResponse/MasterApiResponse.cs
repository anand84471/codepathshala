using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpResponse
{
    public class MasterApiResponse<T>:APIDefaultResponse
    {
        [JsonProperty("data")]
        public T data;
    }
}