using Newtonsoft.Json;

namespace StudentDashboard.Models.RazorPay
{
   
    public class RazorPayPaymentRequestModal
    {
        [JsonProperty("customer_data")]
        public RazorPayCustomerData razorPayCustomerData;
        [JsonProperty("payment_data")]
        public RazorPayPaymentDataModal razorPayPaymentDataModal;
        [JsonProperty("key")]
        public string m_strRazorPayKey;
        [JsonProperty("name")]
        public string m_strSiteName;
        [JsonProperty("description")]
        public string m_strDescription;
        [JsonProperty("image")]
        public string m_strLogoUrl;
        [JsonProperty("order_id")]
        public string m_strOrderId;
        [JsonProperty("is_joined")]
        public bool m_bIsJoined;
        [JsonProperty("is_free_course")]
        public bool m_bIsFreeCourse;

    }
}