using StudentDashboard.HttpResponse;
using StudentDashboard.Security;
using StudentDashboard.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace StudentDashboard.API
{
    [JwtAuthentication]
    [RoutePrefix("school/api/v1")]
    public class SchoolApiController : ApiController
    {

        SchoolService objSchoolService = new SchoolService();
        [HttpPost]
        [Route("ValidateUserId")]
        public APIDefaultResponse CheckIfSchoolUserIdAlreadyExists(string SchoolId)
        {
            APIDefaultResponse objApiDefaultResponse = new APIDefaultResponse();
            if (objSchoolService.CheckIsSchoolUserNameAlreadyExists(SchoolId))
            {
                objApiDefaultResponse.m_iResponseCode = Constants.API_RESPONSE_CODE_SUCCESS;
                objApiDefaultResponse.m_strResponseMessage = Constants.API_RESPONSE_MESSAGE_SUCCESS;
            }
            else
            {
                objApiDefaultResponse.m_iResponseCode = Constants.API_RESPONSE_CODE_FAIL;
                objApiDefaultResponse.m_strResponseMessage = Constants.API_RESPONSE_MESSAGE_FAIL;
            }
            return objApiDefaultResponse;
        }
        [HttpPost]
        [Route("GetStates")]
        public GetStateResponse GetStates()
        {
            GetStateResponse objGetStateResponse = new GetStateResponse();
            objGetStateResponse.m_lsStateDetails = objSchoolService.GetStates();
            if(objGetStateResponse.m_lsStateDetails!=null)
            {
                objGetStateResponse.m_iResponseCode = Constants.API_RESPONSE_CODE_SUCCESS;
                objGetStateResponse.m_strResponseMessage = Constants.API_RESPONSE_MESSAGE_SUCCESS;
            }
            else
            {
                objGetStateResponse.m_iResponseCode = Constants.API_RESPONSE_CODE_FAIL;
                objGetStateResponse.m_strResponseMessage = Constants.API_RESPONSE_MESSAGE_FAIL;
            }
            return objGetStateResponse;
        }
        [HttpPost]
        [Route("GetCities")]
        public GetCityResponse GetCities(int StateId)
        {
            GetCityResponse objGetCityResponse = new GetCityResponse();
            objGetCityResponse.m_lsAllCity = objSchoolService.GetCities(StateId);
            if (objGetCityResponse.m_lsAllCity != null)
            {
                objGetCityResponse.m_iResponseCode = Constants.API_RESPONSE_CODE_SUCCESS;
                objGetCityResponse.m_strResponseMessage = Constants.API_RESPONSE_MESSAGE_SUCCESS;
            }
            else
            {
                objGetCityResponse.m_iResponseCode = Constants.API_RESPONSE_CODE_FAIL;
                objGetCityResponse.m_strResponseMessage = Constants.API_RESPONSE_MESSAGE_FAIL;
            }
            return objGetCityResponse;
        }

    }
}
