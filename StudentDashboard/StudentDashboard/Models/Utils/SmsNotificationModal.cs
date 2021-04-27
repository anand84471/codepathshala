using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.Models.Utils
{
    public class SmsNotificationModal
    {
        public string m_strSmsBody;
        public long m_llSmsId;
        public string m_strReceiverPhoneNo;
        public SmsNotificationModal(string SmsBody,string SmsReceiverNo,long SmsId)
        {
            this.m_strSmsBody = SmsBody;
            this.m_strReceiverPhoneNo = SmsReceiverNo;
            this.m_llSmsId = SmsId;
        }
    }
}