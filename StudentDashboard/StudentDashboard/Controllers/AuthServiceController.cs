using StudentDashboard.ServiceLayer;
using StudentDashboard.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace StudentDashboard.Controllers
{
    public class AuthServiceController : Controller
    {
        DocumentService objDocumentService = new DocumentService();
        StringBuilder m_strLogMessage = new StringBuilder();
        // GET: AuthService
        public async Task<ActionResult> Index(string guid, string sid, int rt)
        {
            string strCurrentMethodName = "Index";
            try
            {
               if(await objDocumentService.ValidatePhoneNoVarificationLink(sid, guid,rt))
                {
                    ViewBag.IsValidRequest = true;
                }
                else
                {
                    ViewBag.IsValidRequest = false;
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
                return View("Error");
            }
            return View();
        }
    }
}