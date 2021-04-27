using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentDashboard.Security.Instrcutor
{
    public class InstructorAuthorizationController : Controller
    {
        // GET: InstructorAuthorization
        public ActionResult Index()
        {
            return View();
        }
    }
}