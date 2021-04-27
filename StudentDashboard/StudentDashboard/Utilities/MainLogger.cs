using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace StudentDashboard.Utilities
{
    public class MainLogger
    {
        private static Logger m_NLog = LogManager.GetLogger("CP_STUDENT_DASHBOARD_LOGGER");
        public static int m_iLoggerType;
        public static void Info(StringBuilder strLogMessage)
        {
            try
            {
                int iThreadID = System.Threading.Thread.CurrentThread.ManagedThreadId;
                //  m_log.InfoFormat("[ThreadID] : {0} {1}", iThreadID, strLogMessage);
                StringBuilder strBuilder = new StringBuilder();
                strBuilder.AppendFormat("[ThreadID]  : {0} {1}", iThreadID, strLogMessage);
                LogInfoMessageByLoggerType(strBuilder);
                strLogMessage.Clear();

            }
            catch (Exception ex)
            {
            }
        }
        public static void Error(StringBuilder strLogMessage)
        {
            try
            {
                int iThreadID = System.Threading.Thread.CurrentThread.ManagedThreadId;
                //  m_log.InfoFormat("[ThreadID] : {0} {1}", iThreadID, strLogMessage);
                StringBuilder strBuilder = new StringBuilder();
                strBuilder.AppendFormat("[ThreadID]  : {0} {1}", iThreadID, strLogMessage);
                // LogInfoMessageByLoggerType(strBuilder);
                LoggedErrorMessageByLoggerType(strBuilder);
                strLogMessage.Clear();

            }
            catch (Exception ex)
            {

            }
        }

        private static void LogInfoMessageByLoggerType(StringBuilder strLogMessage)
        {
            m_NLog.Info("{0}", strLogMessage);
        }
        private static void LoggedErrorMessageByLoggerType(StringBuilder strLogMessage)
        {
            m_NLog.Error("{0}", strLogMessage);
        }
    }
}