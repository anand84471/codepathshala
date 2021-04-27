using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.Models.Instructor
{
    public class InstructorAcadmeicsPublicationModal
    {
        [JsonProperty("publication_name")]
        public string m_strPublicationsName;
        [JsonProperty("publication_date")]
        public string m_strPublicationDate;
        [JsonProperty("publication_url")]
        public string m_strPublicationUrl;
    }
}