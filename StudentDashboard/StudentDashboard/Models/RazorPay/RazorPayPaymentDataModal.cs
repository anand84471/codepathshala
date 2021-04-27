using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.Models.RazorPay
{
    public class RazorPayPaymentDataModal
    {
        [JsonProperty("amount")]
        public int m_iAmountInPaise;
        [JsonProperty("currency")]
        public string m_strCurrency;
        [JsonProperty("payment_capture")]
        public string m_strPaymentCaptureCode;
        [JsonProperty("order_id")]
        public string m_strOrderId;

    }
}