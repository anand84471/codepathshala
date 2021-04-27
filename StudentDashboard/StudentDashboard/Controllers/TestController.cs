using StudentDashboard.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace StudentDashboard.Controllers
{
    public class TestController : Controller
    {
        DocumentService objDocumentService=new DocumentService();
        public ActionResult Index()
        {

            return View();
        }
        public ActionResult Details(long id, string access_code)
        {
            ViewBag.Id = id;
            ViewBag.AccessCode = access_code;
            
            ViewBag.TestId = id;
            return View();
        }
        public async Task<ActionResult> StartTest(long id, string ShareCode, bool StartTest = true)
        {
            if (await objDocumentService.CheckTestAccess(id, ShareCode))
            {
                ViewBag.Id = id;
                ViewBag.AccessCode = ShareCode;
                return View();
            }
            return View("Error");
        }
    }
}