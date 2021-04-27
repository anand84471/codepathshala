using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpResponse
{
    public class APIDefaultResponse
    {
        [JsonProperty("response_code")]
        public int m_iResponseCode { get; set; }
        [JsonProperty("response_message")]
        public string m_strResponseMessage { get; set; }
        internal void SetSuccessResponse()
        {
            m_iResponseCode = Constants.API_RESPONSE_CODE_SUCCESS;
            m_strResponseMessage = Constants.API_RESPONSE_MESSAGE_SUCCESS;
        }
        public APIDefaultResponse()
        {
            m_iResponseCode = Constants.API_RESPONSE_CODE_FAIL;
            m_strResponseMessage = Constants.API_RESPONSE_MESSAGE_FAIL;
        }
    }
}