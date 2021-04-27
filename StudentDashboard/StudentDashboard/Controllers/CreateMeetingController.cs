using StudentDashboard.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentDashboard.Controllers
{
    public class CreateMeetingController : Controller
    {
        HomeService objHomeService = new HomeService();
        // GET: CreateMeeting
        public ActionResult Index()
        {
            return View(objHomeService.GetMeetingObject());
        }
    }
}