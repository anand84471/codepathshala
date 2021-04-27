using Newtonsoft.Json;

namespace StudentDashboard.ServiceLayer
{
    public class ClassroomPaymentRequestDTO
    {
        [JsonProperty("classroom_id")]
        public long m_llClassroomId;
        [JsonProperty("country_code")]
        public int m_iCountryCode;
        [JsonIgnore]
        public long m_llStudentId;
        [JsonProperty("coupon_code")]
        public string m_strCouponCode;

    }
}