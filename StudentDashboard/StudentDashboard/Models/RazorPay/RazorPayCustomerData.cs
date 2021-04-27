using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.Models.RazorPay
{
    public class RazorPayCustomerData
    {
        [JsonProperty("customer_name")]
        public string m_strName;
        [JsonProperty("customer_contact_no")]
        public string m_strContact;
        [JsonProperty("customer_email")]
        public string m_strEmail;
        [JsonProperty("customer_address")]
        public string m_strAddress;
    }
}