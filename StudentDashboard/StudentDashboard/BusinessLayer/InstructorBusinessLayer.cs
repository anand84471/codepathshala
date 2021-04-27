using StudentDashboard.DTO;
using StudentDashboard.Models;
using StudentDashboard.Models.RazorPay;
using StudentDashboard.Utilities;
using StudentDashboard.Vendors.AWS.Concrete;
using StudentDashboard.Vendors.MagicDotNetDataCompression;
using StudentDashboard.Vendors.RazorPay;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static StudentDashboard.Constants;

namespace StudentDashboard.BusinessLayer
{
    public  class InstructorBusinessLayer
    {
        StringBuilder m_strLogMessage;
        TinyUrlService objTinyUrlService;
        AWSS3ServiceManagerLayer objAWSS3ServiceManagerLayer;
        RazorPayHelper razorPayHelper;
        ImageCompressionUtilities imageCompressionUtilities;
        public InstructorBusinessLayer()
        {
            objTinyUrlService = new TinyUrlService();
            m_strLogMessage = new StringBuilder();
            objAWSS3ServiceManagerLayer = new AWSS3ServiceManagerLayer();
        }
        public async Task<string> GetTinyUrlForAssignment(long id,string AccessCode)
        {
            string result = null;
            try
            {
                string path = Constants.BASE_URL_PATH_FOR_ASSIGNMENT + id + "&access_code=" + AccessCode;
                result = await objTinyUrlService.GetTinyUrl(path);
            }
            catch(Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetTinyUrlForAssignment", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<string> GetTinyUrlForTest(long id, string AccessCode)
        {
            string result = null;
            try
            {
                string path = Constants.BASE_URL_PATH_FOR_TEST + id + "&access_code=" + AccessCode;
                result = await objTinyUrlService.GetTinyUrl(path);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetTinyUrlForTest", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<string> GetTinyUrlForCourse(long id, string AccessCode)
        {
            string result = null;
            try
            {
                string path = Constants.BASE_URL_PATH_FOR_COURSE + id + "&access_code=" + AccessCode;
                result = await objTinyUrlService.GetTinyUrl(path);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetTinyUrlForCourse", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public string GetShareCodeForAssignment()
        {
            string result = null;
            try
            {
                return RandomAccessCode();
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetTinyUrlForAssignment", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        private string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }
        private int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }
        public string RandomAccessCode()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(RandomString(4, true));
            builder.Append(RandomNumber(1000, 9999));
            builder.Append(RandomString(2, false));
            return builder.ToString();
        }
        public string GetSmsVerificationString()
        {
            Guid obj = new Guid();
            return Guid.NewGuid().ToString(); 
        }
        public string GetEmailVerficationString()
        {
            return Guid.NewGuid().ToString();
        }
        public string GenerateOtp()
        {
            return RandomNumber(100000, 999999).ToString();
        }
        public string GetLinkForSmsVarification(string Guid,string Id,int RequestType)
        {
            return Constants.BASE_URL_PATH_FOR_AUTHORIZATION + "rt=" + RequestType + "&&sid=" + Id + "&&guid=" + Guid;
        }
        public string GeneratePasswordVeryficationToken()
        {
            string token;
            using (RandomNumberGenerator rng = new RNGCryptoServiceProvider())
            {
                byte[] tokenData = new byte[32];
                rng.GetBytes(tokenData);
                token = Convert.ToBase64String(tokenData);
            }
            return token;
        }
        public string GetRandomMeetingName()
        {

            Guid id = Guid.NewGuid();
            return id.ToString() + GetRandomMeetingPassword();
        }
        public string GetRandomMeetingPassword()
        {
            return GenerateOtp();
        }
        public async Task<string> GetTinyUrlForClassroom(long id, string AccessCode)
        {
            string result = null;
            try
            {
                string path = Constants.BASE_URL_PATH_FOR_CLASSROOM + id + "&access_code=" + AccessCode;
                result = await objTinyUrlService.GetTinyUrl(path);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetTinyUrlForCourse", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        
        public async Task<string> UploadImageAsync(string FileName, string FilePath)
        {
            string awsFilePath = null;
            try
            {
                awsFilePath= await objAWSS3ServiceManagerLayer.UploadImageFileAsync(FileName, FilePath,(int)Constants.AWSFolderType.INSTRUCTOR);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetTinyUrlForCourse", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return awsFilePath;
            
        }
        public async Task<string> UploadImageFileCompressedAsync(string FileName, string FilePath)
        {
            string awsFilePath = null;
            try
            {
                awsFilePath = await objAWSS3ServiceManagerLayer.UploadImageFileCompressedAsync(FileName, FilePath, (int)Constants.AWSFolderType.INSTRUCTOR);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetTinyUrlForCourse", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return awsFilePath;

        }
        public async Task<string> UploadVideoAsync(string FileName, string FilePath)
        {
            string awsFilePath = null;
            try
            {
                awsFilePath = await objAWSS3ServiceManagerLayer.UploadVideoFileAsync(FileName, FilePath, (int)Constants.AWSFolderType.INSTRUCTOR);
                
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetTinyUrlForCourse", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return awsFilePath;

        }
        public async Task<string> UploadPdfAsync(string FileName, string FilePath)
        {
            string awsFilePath = null;
            try
            {
                awsFilePath = await objAWSS3ServiceManagerLayer.UploaddPdfFileAsync(FileName, FilePath, (int)Constants.AWSFolderType.INSTRUCTOR);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetTinyUrlForCourse", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return awsFilePath;

        }
        public async Task<string> UploadCustomTypeAttachmentAsync(string FileName, string FilePath)
        {
            string awsFilePath = null;
            try
            {

                awsFilePath = await objAWSS3ServiceManagerLayer.UploaddCustomeFileAsync(FileName, FilePath, (int)Constants.AWSFolderType.INSTRUCTOR);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetTinyUrlForCourse", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return awsFilePath;

        }
        public string GetClassroomAttachmentName(string ClassroomName,string FileName)
        {
            return MasterUtilities.RemoveWhitespace(ClassroomName) + "_" + MasterUtilities.RemoveWhitespace(FileName);
        }
        public string GetCurrencyValue(int CurrenyId)
        {
            string currency = null;
            switch (CurrenyId)
            {
                case (int)Constants.RazorPayCountryCodes.INDIA:
                    {
                        currency = Constants.COUNTY_CODE_INDIA;
                        break;
                    }
                case (int)Constants.RazorPayCountryCodes.FOREIGN:
                    {
                        currency = Constants.COUNTRY_CODE_FOREIGN;
                        break;
                    }
            }
            return currency;
        }
        public int GetAmountBasedOnCounty(int CurrenyId,int AmountInPaise)
        {
            int amount = 0;
            switch (CurrenyId)
            {
                case (int)Constants.RazorPayCountryCodes.INDIA:
                    {
                        amount = AmountInPaise;
                        break;
                    }
                case (int)Constants.RazorPayCountryCodes.FOREIGN:
                    {
                        amount = AmountInPaise*Constants.FOREIGN_COURRENCY_CONVERSION_CONSTANT/100;
                        break;
                    }
            }
            return amount;
        }
        public int GetCouponDiscount(string CouponCode, int AmountInPaise,List<CouponDetailsModal> lsCouponDetailsModal)
        {
            int amount = AmountInPaise;
            foreach(var CouponData in lsCouponDetailsModal)
            {
                if(CouponData.m_strCouponCode == CouponCode){
                    amount = amount- amount*CouponData.m_iCouponDicount / 100;
                    break;
                }
            }
            return amount;
        }
        public RazorPayPaymentDataModal CreateRazorPaymentRequest(PaymentRequestDTO paymentRequestDTO)
        {
            razorPayHelper = new RazorPayHelper();
            RazorPayPaymentDataModal razorPayPaymentDataModal = new RazorPayPaymentDataModal();
            razorPayPaymentDataModal.m_iAmountInPaise = paymentRequestDTO.m_iClassroomPayment;
            razorPayPaymentDataModal.m_strCurrency = paymentRequestDTO.m_strCurrency;
            razorPayPaymentDataModal.m_strPaymentCaptureCode = "1";
            razorPayPaymentDataModal.m_strOrderId = razorPayHelper.CreateOrder(razorPayPaymentDataModal);
            return razorPayPaymentDataModal;
        }
        public bool ValidateRazorPayPaymentRequest(RazorPayPaymentResponseModal razorPayPaymentResponseModal)
        {
            string strGeneratedHash = HmacValidator.HmacSha256Digest(razorPayPaymentResponseModal.m_strOrderId+"|"+
                razorPayPaymentResponseModal.m_strRazorPayPaymentId,MvcApplication._strRazorPaySecret);
            bool result=false;
            if (strGeneratedHash == razorPayPaymentResponseModal.m_strRazorPaySignature)
            {
                result = true;
            }
            return result;
        }
        public int GetPriceAccordingToCurrency(int currencyTypeEnum,int Price)
        {
            int NewPrice=Price;
            switch (currencyTypeEnum)
            {
                case (int)CurrencyTypeEnum.INR:
                    {
                        NewPrice = CURRENCY_INR_VALUE * Price;
                        break;
                    }
                case (int)CurrencyTypeEnum.USD:
                    {
                        NewPrice = CURRENCY_USD_VALUE * Price;
                        break;
                    }
            }
            return NewPrice;
        }
       
    }
    public static class HmacValidator
    {
        public static string HmacSha256Digest(this string message, string secret)
        {
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] keyBytes = encoding.GetBytes(secret);
            byte[] messageBytes = encoding.GetBytes(message);
            HMACSHA256 cryptographer = new HMACSHA256(keyBytes);
            byte[] bytes = cryptographer.ComputeHash(messageBytes);

            return BitConverter.ToString(bytes).Replace("-", "").ToLower();
        }
    }
}