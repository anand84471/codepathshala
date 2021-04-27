using StudentDashboard.HttpResponse;
using StudentDashboard.ServiceLayer;
using StudentDashboard.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace StudentDashboard.API
{
    [RoutePrefix("api/v1/student")]
    public class StudentApiController : ApiController
    {
        StudentService objStudentService = new StudentService();
        StringBuilder m_strLogMessage = new StringBuilder();
        [Route("SearchCourse")]
        [HttpPost]
        public SearchCourseHttpResponse SearchCourse([FromBody]string key)
        {
            SearchCourseHttpResponse objResponse = null;
            try
            {
                //objResponse.lsCourseDetailsModel = objStudentService.SearchForCourse(key, 10);
                if (objResponse != null)
                {
                    objResponse.m_iResponseCode = Constants.API_RESPONSE_CODE_SUCCESS;
                    objResponse.m_strResponseMessage = Constants.API_RESPONSE_MESSAGE_SUCCESS;
                }
                else
                {
                    objResponse.m_iResponseCode = Constants.API_RESPONSE_CODE_FAIL;
                    objResponse.m_strResponseMessage = Constants.API_RESPONSE_MESSAGE_FAIL;
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetCourseDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objResponse;
        }
        [Route("JoinCourse")]
        [HttpPost]
        public async Task<APIDefaultResponse> JoinCourse(long CourseId,long StudentId)
        {
            SearchCourseHttpResponse objResponse = null;
            try
            {
                if (await objStudentService.JoinStudentToCourse(CourseId, StudentId))
                {
                    objResponse.m_iResponseCode = Constants.API_RESPONSE_CODE_SUCCESS;
                    objResponse.m_strResponseMessage = Constants.API_RESPONSE_MESSAGE_SUCCESS;
                }
                else
                {
                    objResponse.m_iResponseCode = Constants.API_RESPONSE_CODE_FAIL;
                    objResponse.m_strResponseMessage = Constants.API_RESPONSE_MESSAGE_FAIL;
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetCourseDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objResponse;
        }

    }
}
