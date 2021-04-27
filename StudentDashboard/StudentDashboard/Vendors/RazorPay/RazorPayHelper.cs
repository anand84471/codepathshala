using Razorpay.Api;
using StudentDashboard.Models.RazorPay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.Vendors.RazorPay
{
    public class RazorPayHelper
    {
        RazorpayClient client;

        public RazorPayHelper()
        {
            try
            {
               client = new RazorpayClient(MvcApplication._strRazorPayKey,
                    MvcApplication._strRazorPaySecret);
            }
            catch(Exception Ex)
            {

            }

        }
        public string CreateOrder(RazorPayPaymentDataModal razorPayPaymentDataModal)
        {
            Dictionary<string, object> options = new Dictionary<string, object>();
            options.Add("amount", razorPayPaymentDataModal.m_iAmountInPaise.ToString());
            options.Add("currency", razorPayPaymentDataModal.m_strCurrency);
            options.Add("payment_capture", razorPayPaymentDataModal.m_strPaymentCaptureCode);
            Order order = client.Order.Create(options);
            string strOderId = null;
            if(order!=null)
            {
                strOderId = order["id"].ToString();
            }
            return strOderId;
        }
    }
}