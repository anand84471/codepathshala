using StudentDashboard.HttpRequest;
using StudentDashboard.HttpResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace StudentDashboard.Controllers
{
    [RoutePrefix("student/api/v1")]
    public class StudentApiController : ApiController
    {
        [HttpPost]
        [Route("FetchtStudentDetails")]
        public GetStudentResponseModel GetStudentDetails([FromBody]GetStudentRequestDetailsModel GetStudentResponseModel)
        {
            GetStudentResponseModel response = new GetStudentResponseModel();
            response.m_iResponseCode = 1;
            response.m_stgrResponseMessage = "success";
            response.m_objStudentDetails = new StudentDetailsModel();
            return response;
        } 
    }
}
