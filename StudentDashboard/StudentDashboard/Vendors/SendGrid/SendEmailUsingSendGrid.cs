using SendGrid;
using SendGrid.Helpers.Mail;
using StudentDashboard.Models.SmsModal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace StudentDashboard.Vendors.SendGrid
{
    public class SendEmailUsingSendGrid
    {
        public  static async Task  SendEmail(MasterSmsModal masterSmsModal)
        {
            //var apiKey = Environment.GetEnvironmentVariable(MvcApplication._strSendGridEmailApiKey);
            var apiKey = MvcApplication._strSendGridEmailApiKey;
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(masterSmsModal.m_strSendersAddress, masterSmsModal.m_strSenderName);
            var subject = masterSmsModal.m_strSubject;
            var to = new EmailAddress(masterSmsModal.m_strReceiverAddress, masterSmsModal.m_strReceiverName);
            //var plainTextContent = "and easy to do anywhere, even with C#";
            var htmlContent = masterSmsModal.m_strMessage;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, "", htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
    }
}