using StudentDashboard.DTO;
using StudentDashboard.Utilities;
using System.Threading.Tasks;

namespace StudentDashboard.ServiceLayer
{
    public class SMSServiceManager
    {
        SmsManger objSmsManger;
        TinyUrlService objTinyUrlService;
        DocumentService objDocumentService;
        public SMSServiceManager()
        {
            objSmsManger = new SmsManger();
            objTinyUrlService = new TinyUrlService();
            objDocumentService = new DocumentService();
        }
        private string GetAccountVerificationMessage(string VerificationLink)
        {
            return "Hi!, you are successfully registered to ReadMyCourse. To verify your phone no for future uses please click on the link: "+VerificationLink;
        }
        private string GetStudentForgotPasswordOtpMessage(string otp)
        {
            return "ReadMyCourse: Otp to reset your password is: "+otp;
        }
        private string GetStudentVarifyPhoneNoOtp(string otp)
        {
            return "ReadMyCourse: Otp to varify your phone no is: " + otp;
        }
        public void SendSms(string PhoneNo,string SmsBody)
        {
            objSmsManger.SendEmail(PhoneNo, SmsBody);
        }
        public async Task<bool> SendInstructorPhoneNoVarification(string Link,string PhoneNo)
        {
            var tinyLink = await objTinyUrlService.GetTinyUrl(Link);
            return await objDocumentService.InsertSMSNotification(GetAccountVerificationMessage(tinyLink), PhoneNo, 3);
        }
        public async Task<bool> SendStudentPasswordRecoveryOTP(string OTP,string PhoneNo)
        {
            return await objDocumentService.InsertSMSNotification(GetStudentForgotPasswordOtpMessage(OTP), PhoneNo, Constants.SMS_NOTIFICATION_TYPE_STUDENT_FORGOT_PASSWORD);
        }
        public async Task<bool> SendInstructorPasswordRecoveryOTP(string OTP, string PhoneNo)
        {
            return await objDocumentService.InsertSMSNotification(GetStudentForgotPasswordOtpMessage(OTP), PhoneNo, Constants.SMS_NOTIFICATION_TYPE_INSTRUCTOR_FORGOT_PASSWORD);
        }
        public async Task<bool> SendPhoneNoVarificationOtp(string OTP, string PhoneNo)
        {
            return await objDocumentService.InsertSMSNotification(GetStudentVarifyPhoneNoOtp(OTP), PhoneNo, Constants.SMS_NOTIFICATION_TYPE_STUDENT_PHONE_NO_VERIFICATION);
        }


    }
}