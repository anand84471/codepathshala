using StudentDashboard.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace StudentDashboard.Controllers
{
    [AllowAnonymous]
    public class AssignmentController : Controller
    {
        DocumentService objDocumentService=new DocumentService();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Details(long id,string access_code)
        {
            ViewBag.Id = id;
            ViewBag.AccessCode = access_code;
            if (Session["student_id"]!=null)
            {
               
                Response.Redirect("Student/Assignment/Details?id="+ id, true);
            }
            return View();
        }
        public ActionResult ViewResponse(string id)
        {
            return View();
        }
        public ActionResult Submissions(long id, string access_id)
        {
            return View();
        }
        public async Task<ActionResult> StartAssignment(long id, string ShareCode, bool StartTest = true)
        {
            if (await objDocumentService.CheckAssignmentAccess(id, ShareCode))
            {
                ViewBag.Id = id;
                ViewBag.AccessCode = ShareCode;
                return View();
            }
            return View("Error");
        }

    }

}