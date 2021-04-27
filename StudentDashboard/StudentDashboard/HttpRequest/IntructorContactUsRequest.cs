using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpRequest
{
    public class IntructorContactUsRequest: ContactUsApiRequest
    {
        [JsonProperty("instructor_id")]
        public int m_iInstructorId;
    }
}