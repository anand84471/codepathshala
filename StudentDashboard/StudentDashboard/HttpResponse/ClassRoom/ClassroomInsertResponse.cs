using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpResponse.ClassRoom
{
    public class ClassroomInsertResponse:APIDefaultResponse
    {
        [JsonProperty("classroom_id")]
        public long m_llClassroomId;
        public ClassroomInsertResponse():base()
        {

        }
    }
}