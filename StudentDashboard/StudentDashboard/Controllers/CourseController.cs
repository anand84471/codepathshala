using StudentDashboard.DTO;
using StudentDashboard.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace StudentDashboard.Controllers
{
    public class CourseController : Controller
    {
        StringBuilder m_strLogMessage = new StringBuilder();
        DocumentService objDocumentService = new DocumentService();
        // GET: Course
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<ActionResult> Details(long id,string access_code)
        {
            if(Session["user_id"]!=null)
            {
                Response.Redirect(MvcApplication._strApplicationBaseUrl+"/Student/JoinCourse?id="+id);
            }
            ViewBag.ReturnUrl = MvcApplication._strApplicationBaseUrl+"/student?return_url="+ MvcApplication._strApplicationBaseUrl +"/Student/JoinCourse?id=" + id;
            ViewBag.Id = id;
            return PartialView();
        }
    }
}