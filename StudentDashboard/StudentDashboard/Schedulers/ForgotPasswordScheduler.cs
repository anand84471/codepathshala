using StudentDashboard.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Web;

namespace StudentDashboard.Schedulers
{
    public class ForgotPasswordScheduler
    {
        private static readonly object InstanceLocker = new Object();
        StringBuilder m_strLogMessage;
        ForgotPasswordSchedulerService objForgotPasswordSchedulerService;
        private static volatile ForgotPasswordScheduler objForgotPasswordScheduler = null;
        private System.Timers.Timer AutoSmsSchedulerTimer;
        private ForgotPasswordScheduler()
        {
            objForgotPasswordSchedulerService = new ForgotPasswordSchedulerService();
            m_strLogMessage = new StringBuilder();
        }
        public static ForgotPasswordScheduler GetInstance()
        {
            if (objForgotPasswordScheduler == null)
            {
                lock (InstanceLocker)
                {
                    if (objForgotPasswordScheduler == null)
                    {
                        objForgotPasswordScheduler = new ForgotPasswordScheduler();
                    }
                }
            }
            return objForgotPasswordScheduler;
        }
        public void StartDynamicRoutingSchedularService()
        {
            try
            {
                long timeIntervalInMillSec = 0;
                if (MvcApplication._smsSchedulingTime!=0)
                {
                    timeIntervalInMillSec = MvcApplication._smsSchedulingTime;
                }
                AutoSmsSchedulerTimer = new System.Timers.Timer(timeIntervalInMillSec);
                AutoSmsSchedulerTimer.AutoReset = true;
                AutoSmsSchedulerTimer.Elapsed += new ElapsedEventHandler(this.PerformAction);
                this.AutoSmsSchedulerTimer.Start();
                PerformAction(null, null);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.AppendFormat("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage.AppendFormat("Exception occured in method :" + Ex.TargetSite);
                m_strLogMessage.AppendFormat(Ex.ToString());
                MainLogger.Error(m_strLogMessage);
            }
        }
        private  void PerformAction(object sender, ElapsedEventArgs e)
       {
            objForgotPasswordSchedulerService.ProcessSmsNotifications();
        }
    }
}