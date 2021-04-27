using Newtonsoft.Json;
using StudentDashboard.Models.RazorPay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpResponse
{
    public class ClassroomAcceptPaymentResponse:APIDefaultResponse
    {
        [JsonProperty("payment_data")]
        public RazorPayPaymentRequestModal razorPayPaymentRequestModal;
        [JsonProperty("is_joined")]
        public bool m_bIsJoined;
        [JsonProperty("is_free_course")]
        public bool m_bIsFreeCourse;
    }
}