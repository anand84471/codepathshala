using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentDashboard.Controllers
{
    public class AccountManagerController : Controller
    {
        // GET: AccountManager
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult EditAccountDetails()
        {
            return View();
        }
    }
}