using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.Models.RazorPay
{
    public class RazorPayPaymentResponseModal
    {
        [JsonProperty("razorpay_payment_id")]
        public string m_strRazorPayPaymentId;
        [JsonProperty("razorpay_order_id")]
        public string m_strRazorPayOrderId;
        [JsonProperty("razorpay_signature")]
        public string m_strRazorPaySignature;
        [JsonProperty("order_id")]
        public string m_strOrderId;
    }
}