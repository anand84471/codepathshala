using StudentDashboard.Models;
using StudentDashboard.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentDashboard.Controllers
{
    public class SchoolController : Controller
    {
        SchoolService objSchoolService = new SchoolService();
        // GET: School
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Home()
        {
            return View();
        }
        public ActionResult Join()
        {
            return View();

        }
        public ActionResult RegisterNow(FormCollection collection)
        {
            try
            {

                SchoolRegisterModel objSchoolRegiserModel = new SchoolRegisterModel();
                objSchoolRegiserModel.m_strSchoolName = collection["name"];
                objSchoolRegiserModel.m_strAddressLine1 = collection["address1"];
                objSchoolRegiserModel.m_strAddressLine2 = collection["address2"];
                objSchoolRegiserModel.m_strPhoneNo = collection["phoneNo"];
                objSchoolRegiserModel.m_strSchoolUserId = collection["userName"];
                objSchoolRegiserModel.m_strEmailId = collection["email"];
                objSchoolRegiserModel.m_iCityId = int.Parse(collection["city"]);
                objSchoolRegiserModel.m_iPinCode = int.Parse(collection["pinCode"]);
                objSchoolRegiserModel.m_strPassword = collection["password"];
                if (!objSchoolService.RegisterNewSchool(objSchoolRegiserModel))
                {
                    ViewBag.IsRegistered = false;
                    return View("Join");
                }
                else
                {
                    Response.Redirect("../School");
                    ViewBag.IsRegistered = true;
                }
                return View("Error");
            }
            catch (Exception Ex)
            {
                return View("Error");
            }
        }
        public ActionResult ValidateLogin(FormCollection collection)
        {
            SchoolRegisterModel objSchoolRegiserModel = new SchoolRegisterModel();
            objSchoolRegiserModel.m_strPassword = collection["password"];
            objSchoolRegiserModel.m_strSchoolUserId = collection["userName"];
            if (objSchoolService.ValidateSchoolLoginDetails(objSchoolRegiserModel.m_strSchoolUserId, objSchoolRegiserModel.m_strPassword))
            {
                Response.Redirect("./Home");
            }
            else
            {
                ViewBag.IsLoggedIn = false;
                return View("Index");
            }
            return View("Index");
        }
    }
}