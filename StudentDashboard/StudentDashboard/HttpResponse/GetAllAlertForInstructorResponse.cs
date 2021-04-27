using Newtonsoft.Json;
using StudentDashboard.Models.Alert;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpResponse
{
    public class GetAllAlertForInstructorResponse:APIDefaultResponse
    {
        [JsonProperty("alerts")]
        public List<AlertDetailsModal> m_lsInstructorAlertModal;
    }
}