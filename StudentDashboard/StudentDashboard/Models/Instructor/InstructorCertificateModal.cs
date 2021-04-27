using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.Models.Instructor
{
    public class InstructorCertificateModal
    {
        [JsonProperty("certificate_name")]
        public string m_strCertificateName;
        [JsonProperty("receiving_date")]
        public string m_strRecevingDate;
    }
}