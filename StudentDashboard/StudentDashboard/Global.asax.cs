using StudentDashboard.Schedulers;
using StudentDashboard.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace StudentDashboard
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static Dictionary<int, string> _dtListSmsTypeMasterData;
        public static int _smsSchedulingTime;
        public static int _maxRetryCountForSms;
        public static bool _shouldStartSMSScheduler;
        public static string _saltForSHA256Encryption;
        public static int _forgotPasswordExpiryTimeInMinutes;
        public static string _strApplicationBaseUrl;
        public static string _strAmazonAwsAccessKey;
        public static string _strAwsSecurityKey;
        public static string _strAwsBucketName;
        public static string _strAwsBucketFolderInstructor;
        public static string _strAwsBucketFolderStudent;
        public static string _strAwsFileUploadBaseUrl;
        public static string _strRazorPayKey;
        public static string _strRazorPaySecret;
        public static string _strSendGridEmailApiKey;
        public static string _strSendGridEmailSenderName;
        public static string _strSendGridEmailSenerEmail;
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            InitializeConfigurationData();
            StartSchedulers();
        }
        private void InitializeMasterDateInCache()
        {
            //_dtListSmsTypeMasterData=
        }
        private void StartSchedulers()
        {
            if (_shouldStartSMSScheduler)
            {
                StartSMSNotificationScheduler();
            }
        }
        private void InitializeRazorPayConigurationData()
        {
            if (ConfigurationManager.AppSettings["RazorPayKeyValue"] != null)
            {
                _strRazorPayKey = (ConfigurationManager.AppSettings["RazorPayKeyValue"]);
            }
            if (ConfigurationManager.AppSettings["RazorPaySecretValue"] != null)
            {
                _strRazorPaySecret = (ConfigurationManager.AppSettings["RazorPaySecretValue"]);
            }
        }

        private void InitializeAwsConigurationData()
        {

            if (ConfigurationManager.AppSettings["AWSAccessKey"] != null)
            {
                _strAmazonAwsAccessKey = (ConfigurationManager.AppSettings["AWSAccessKey"]);
            }

            if (ConfigurationManager.AppSettings["AWSSecretKey"] != null)
            {
                _strAwsSecurityKey = (ConfigurationManager.AppSettings["AWSSecretKey"]);
            }

            if (ConfigurationManager.AppSettings["AWSS3BucketFolderNameForInstructor"] != null)
            {
                _strAwsBucketFolderInstructor = (ConfigurationManager.AppSettings["AWSS3BucketFolderNameForInstructor"]);
            }

            if (ConfigurationManager.AppSettings["AWSS3BucketFolderNameForStudent"] != null)
            {
                _strAwsBucketFolderStudent = (ConfigurationManager.AppSettings["AWSS3BucketFolderNameForStudent"]);
            }

            if (ConfigurationManager.AppSettings["AWSS3BucketName"] != null)
            {
                _strAwsBucketName = (ConfigurationManager.AppSettings["AWSS3BucketName"]);
            }
            if (ConfigurationManager.AppSettings["AWSFileBaseUrl"] != null)
            {
                _strAwsFileUploadBaseUrl = (ConfigurationManager.AppSettings["AWSFileBaseUrl"]);
            }
        }
        private void InitializeConfigurationData()
        {
            if(ConfigurationManager.AppSettings["MAX_RETRY_COUNT_FOR_SMS"]!=null)
            {
                _maxRetryCountForSms = int.Parse(ConfigurationManager.AppSettings["MAX_RETRY_COUNT_FOR_SMS"]);
            }
            if (ConfigurationManager.AppSettings["SMS_SCHEDULER_SERVICE_TIME_IN_MILLISECONDS"] != null)
            {
                _smsSchedulingTime = int.Parse(ConfigurationManager.AppSettings["SMS_SCHEDULER_SERVICE_TIME_IN_MILLISECONDS"]);
            }
            if (ConfigurationManager.AppSettings["SHOULD_START_SMS_SCHEDULER"] != null)
            {
                _shouldStartSMSScheduler = bool.Parse(ConfigurationManager.AppSettings["SHOULD_START_SMS_SCHEDULER"]);
            }
            if (ConfigurationManager.AppSettings["SALT_SHA256_ENCRYPTION"] != null)
            {
                _saltForSHA256Encryption = ConfigurationManager.AppSettings["SALT_SHA256_ENCRYPTION"].ToString();
            }
            if (ConfigurationManager.AppSettings["FORGOT_PASSOWORD_TOKEN_EXPIRY_TIME_IN_MINUTES"] != null)
            {
                _forgotPasswordExpiryTimeInMinutes = int.Parse(ConfigurationManager.AppSettings["FORGOT_PASSOWORD_TOKEN_EXPIRY_TIME_IN_MINUTES"].ToString());
            }
            if (ConfigurationManager.AppSettings["APPLICATION_BASE_URL"] != null)
            {
                _strApplicationBaseUrl = (ConfigurationManager.AppSettings["APPLICATION_BASE_URL"].ToString());
            }
            InitializeAwsConigurationData();
            InitializeRazorPayConigurationData();
        }
        private void InitializeEmailServiceConfigurationData()
        {
            if (ConfigurationManager.AppSettings["SendgridApiKey"] != null)
            {
                _strSendGridEmailApiKey = (ConfigurationManager.AppSettings["SendgridApiKey"].ToString());
            }
        }
        private void StartSMSNotificationScheduler()
        {
            try
            {
                ForgotPasswordScheduler objForgotPasswordScheduler = ForgotPasswordScheduler.GetInstance();
                Thread thread = new Thread(new ThreadStart(objForgotPasswordScheduler.StartDynamicRoutingSchedularService));
                thread.Start();
            }
            catch (Exception Ex)
            {
                StringBuilder m_strLogMessage = new StringBuilder();
                m_strLogMessage.AppendFormat("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage.AppendFormat("Exception occured in method :" + Ex.TargetSite);
                m_strLogMessage.AppendFormat(Ex.ToString());
                MainLogger.Error(m_strLogMessage);
            }
        }
    }
}
