using StudentDashboard.HttpResponse;
using StudentDashboard.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace StudentDashboard.Controllers
{
    public class AboutUsController : Controller
    {
        DocumentService objDocumentService = new DocumentService();
        public async Task<ActionResult> Index()
        {
            GetWebsiteHomeDetailsResponse objResponse = await objDocumentService.GetHomeDetails();
            return View(objResponse);
        }
       

    }
}