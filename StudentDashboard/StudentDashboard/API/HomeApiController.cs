using StudentDashboard.HttpResponse;
using StudentDashboard.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace StudentDashboard.API
{
    [RoutePrefix("service")]
    public class HomeApiController : ApiController
    {
        HomeService objHomeService = new HomeService();
        
        [HttpPost]
        public FileUploadResponse UploadFile()
        {

            FileUploadResponse objFileUploadResponse = null;
            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count > 0)
            {
                var docfiles = new List<string>();
                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];
                    var filePath = HttpContext.Current.Server.MapPath("~/" + postedFile.FileName);
                    postedFile.SaveAs(filePath);
                    docfiles.Add(filePath);
                }
            }
            return new FileUploadResponse();
        } 
    }
}
