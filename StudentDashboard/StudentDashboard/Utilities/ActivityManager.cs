using StudentDashboard.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace StudentDashboard.Utilities
{
    public class ActivityManager
    {
        public long m_llHostId { get; set; }
        public string m_strHostName { get; set; }
        private StringBuilder m_strLogMessage;
        public ActivityManager()
        {
            m_strLogMessage = new StringBuilder();
        }
        public string CreateActivityMessageForinstructor(long HostId,string HostName,int ActivityType)
        {
            StringBuilder Message = new StringBuilder();
            try
            {
                switch(ActivityType)
                {
                    case (int)Constants.ActivityType.COURSE_CREATED:
                        {
                            Message.Append(Constants.NEW_COURSE_CREATED);
                            Message.AppendFormat("at time: {0} with id: {1} and name: {2} ", DateTime.Now.ToShortTimeString(), HostId, HostName);
                            break;
                        }
                    case (int)Constants.ActivityType.ASSIGNMENT_CREATED:
                        {
                            Message.Append(Constants.NEW_ASSIGNMENT_CREATED);
                            Message.AppendFormat("at time: {0} with id: {1} and name: {2} ", DateTime.Now.ToShortTimeString(), HostId, HostName);
                            break;
                        }
                    case (int)Constants.ActivityType.TEST_CREATED:
                        {
                            Message.Append(Constants.NEW_TEST_CREATED);
                            Message.AppendFormat("at time: {0} with id: {1} and name: {2} ", DateTime.Now.ToShortTimeString(), HostId, HostName);
                            break;
                        }
                }
                
            }
            catch(Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "CreateNewActivity", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return Message.ToString();
        }

    }
}