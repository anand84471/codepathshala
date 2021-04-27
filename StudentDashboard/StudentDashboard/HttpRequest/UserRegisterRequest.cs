using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpRequest
{
    public class UserRegisterRequest
    {

        [JsonProperty("student_")]
        public int iMerchantId { get; set; }
        [JsonProperty("max_health_difference_to_be_allowed_by_merchant")]
        public int iMaxHealthDifferenceByMerchant { get; set; }
        [JsonProperty("acquirer_list")]
        public List<int> lsAcquirerDetails { get; set; }
        [JsonProperty("pine_pg_txn_id")]
        public long lTransactionId { get; set; }
    }
}