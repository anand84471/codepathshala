using StudentDashboard.DTO;
using StudentDashboard.Models.Utils;
using StudentDashboard.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace StudentDashboard.Schedulers
{
    public class ForgotPasswordSchedulerService
    {
        DocumentDTO objDocumentDTO;
        SMSServiceManager objSMSServiceManager;
        List<SmsNotificationModal> lsSmsNotificationModal;
        public ForgotPasswordSchedulerService()
        {
            objDocumentDTO = new DocumentDTO();
            objSMSServiceManager = new SMSServiceManager();
        }
        private List<SmsNotificationModal> GetAllSmsNotificationToProcess()
        {
            return  objDocumentDTO.GetAllSmsNotification(MvcApplication._maxRetryCountForSms);
        }
        private void SendNotification(SmsNotificationModal objSmsNotificationModal)
        {
            objSMSServiceManager.SendSms(objSmsNotificationModal.m_strReceiverPhoneNo, objSmsNotificationModal.m_strSmsBody);
            return ;
        }
        private void ChangeSmsNotificationStatus(SmsNotificationModal objSmsNotificationModal)
        {
            objDocumentDTO.ChangeSmsNotificationStatus(objSmsNotificationModal.m_llSmsId,true);
        }
        public void ProcessSmsNotifications()
        {
            lsSmsNotificationModal = GetAllSmsNotificationToProcess();
            if(lsSmsNotificationModal!=null)
            {
                foreach(var message in lsSmsNotificationModal)
                {
                    SendNotification(message);
                    ChangeSmsNotificationStatus(message);
                }
            }
        }
    }
}