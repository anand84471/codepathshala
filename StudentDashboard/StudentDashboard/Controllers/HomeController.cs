using StudentDashboard.HttpResponse;
using StudentDashboard.ServiceLayer;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace StudentDashboard.Controllers
{
    public class HomeController : Controller
    {
        DocumentService objDocumentService=new DocumentService();
        public async Task<ActionResult> Index()
        {
            GetWebsiteHomeDetailsResponse objResponse = await objDocumentService.GetHomeDetails();
            return View(objResponse);
        }
        
        public async Task<ActionResult> About()
        {
            GetWebsiteHomeDetailsResponse objResponse = await objDocumentService.GetHomeDetails();
            return View(objResponse);
        }
    }
}