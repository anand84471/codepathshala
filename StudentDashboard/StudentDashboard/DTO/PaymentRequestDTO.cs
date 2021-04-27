using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.DTO
{
    public class PaymentRequestDTO
    {
        public string m_strCustomerName;
        public string m_strCutomerEmail;
        public string m_strCustomerPhoneNo;
        public int m_iClassroomPayment;
        public string m_strCurrency;
        public PaymentRequestDTO()
        {

        }
        public PaymentRequestDTO(string CustomerName,string CustomerEmail,string CustomerPhoneNo,
            int ClassroomAcceesPaymentAmount)
        {
            this.m_strCustomerName = CustomerName;
            this.m_strCustomerPhoneNo = CustomerPhoneNo;
            this.m_strCutomerEmail = CustomerEmail;
            this.m_iClassroomPayment = ClassroomAcceesPaymentAmount;
        }
    }
}