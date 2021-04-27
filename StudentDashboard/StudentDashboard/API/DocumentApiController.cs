using StudentDashboard.HttpRequest;
using StudentDashboard.HttpRequest.Document;
using StudentDashboard.HttpResponse;
using StudentDashboard.HttpResponse.Document;
using StudentDashboard.Models.Category;
using StudentDashboard.Models.Course;
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
    [AllowAnonymous]
    [RoutePrefix("api/v1/document")]
    public class DocumentApiController : ApiController
    {
        StringBuilder m_strLogMessage = new StringBuilder();
        DocumentService objDocumentService = new DocumentService();
        HomeService objHomeService=new HomeService();
        [Route("FetchFullTestDetails")]
        [HttpPost]
        public async Task<TestModel> GetFullTestDetails(long id,string AccessCode)
        {
            TestModel objResponse = null;
            try
            {
                if (await objDocumentService.CheckTestAccess(id, AccessCode))
                {
                    objResponse = await objHomeService.GetFullMcqTestDetails(id);
                    if (objResponse != null)
                    {
                        objResponse.m_iResponseCode = Constants.API_RESPONSE_CODE_SUCCESS;
                        objResponse.m_strResponseMessage = Constants.API_RESPONSE_MESSAGE_SUCCESS;
                    }
                    else
                    {
                        objResponse = new TestModel();
                        objResponse.m_iResponseCode = Constants.API_RESPONSE_CODE_FAIL;
                        objResponse.m_strResponseMessage = Constants.API_RESPONSE_MESSAGE_FAIL;
                    }
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetTestDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objResponse;
        }
        [Route("FetchTestDetails")]
        [HttpPost]
        public async Task<GetTestDetailsResponseWithAccessCode> GetTestDetails(long id, string AccessCode)
        {
            GetTestDetailsResponseWithAccessCode objResponse = null;
            try
            {
                objResponse = await objDocumentService.GetTestDetails(id, AccessCode);
                if (objResponse != null)
                {
                    objResponse.m_iResponseCode = Constants.API_RESPONSE_CODE_SUCCESS;
                    objResponse.m_strResponseMessage = Constants.API_RESPONSE_MESSAGE_SUCCESS;
                }
                else
                {
                    objResponse = new GetTestDetailsResponseWithAccessCode();
                    objResponse.m_iResponseCode = Constants.API_RESPONSE_CODE_FAIL;
                    objResponse.m_strResponseMessage = Constants.API_RESPONSE_MESSAGE_FAIL;
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetTestDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objResponse;
        }
        [Route("FetchFullAssignmentDetails")]
        [HttpPost]
        public async Task<AssignmentModel> GetFullAssignmentDetails(long AssignmentId,string AccessCode)
        {
            AssignmentModel objResponse = null;
            try
            {
                if(await objDocumentService.CheckAssignmentAccess(AssignmentId,AccessCode))
                {
                    objResponse = await objHomeService.GetAssignmentDetails(AssignmentId);
                    if (objResponse != null)
                    {
                        objResponse.m_iResponseCode = Constants.API_RESPONSE_CODE_SUCCESS;
                        objResponse.m_strResponseMessage = Constants.API_RESPONSE_MESSAGE_SUCCESS;
                    }
                    else
                    {
                        objResponse = new AssignmentModel();
                        objResponse.m_iResponseCode = Constants.API_RESPONSE_CODE_FAIL;
                        objResponse.m_strResponseMessage = Constants.API_RESPONSE_MESSAGE_FAIL;
                    }
                }
                
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetCourseIndexDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objResponse;
        }
        [Route("FetchAssignmentDetails")]
        [HttpPost]
        public async Task<AssignmentModel> GetAssignmentDetails(long AssignmentId, string AccessCode)
        {
            AssignmentModel objResponse = null;
            try
            {
                if (await objDocumentService.CheckAssignmentAccess(AssignmentId, AccessCode))
                {
                    objResponse = await objHomeService.GetIndependentAssignmentDetailsWithoutQuestion(AssignmentId);
                    if (objResponse != null)
                    {
                        objResponse.m_iResponseCode = Constants.API_RESPONSE_CODE_SUCCESS;
                        objResponse.m_strResponseMessage = Constants.API_RESPONSE_MESSAGE_SUCCESS;
                    }
                    else
                    {
                        objResponse = new AssignmentModel();
                        objResponse.m_iResponseCode = Constants.API_RESPONSE_CODE_FAIL;
                        objResponse.m_strResponseMessage = Constants.API_RESPONSE_MESSAGE_FAIL;
                    }
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetCourseIndexDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objResponse;
        }
        [Route("FetchHomeDetails")]
        [HttpPost]
        public async Task<GetWebsiteHomeDetailsResponse> GetHomeDetails()
        {
            GetWebsiteHomeDetailsResponse objResponse = null;
            try
            {
                objResponse = await objDocumentService.GetHomeDetails();
                if (objResponse != null)
                {
                    objResponse.m_iResponseCode = Constants.API_RESPONSE_CODE_SUCCESS;
                    objResponse.m_strResponseMessage = Constants.API_RESPONSE_MESSAGE_SUCCESS;
                }
                else
                {
                    objResponse = new GetWebsiteHomeDetailsResponse();
                    objResponse.m_iResponseCode = Constants.API_RESPONSE_CODE_FAIL;
                    objResponse.m_strResponseMessage = Constants.API_RESPONSE_MESSAGE_FAIL;
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetCourseIndexDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objResponse;
        }
        [Route("SearchCourse")]
        [HttpPost]
        public async Task<SearchCourseHttpResponse> SearchCourseForStudent([FromBody]SerchCourseRequest objSerchCourseRequest)
        {
            SearchCourseHttpResponse objResponse = new SearchCourseHttpResponse();
            try
            {

                if (objSerchCourseRequest != null)
                {
                    objResponse.lsCourseDetailsModel = await objDocumentService.SearchForCourse(objSerchCourseRequest.m_strKey, 10, objSerchCourseRequest.m_iNoOfRowsFetched,
                                      objSerchCourseRequest.m_iSortingId);
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
        [Route("FetchFeeTests")]
        [HttpPost]
        public async Task<SearchForTestResponse> FetcheFreeTests(ContentSearchRequest contentSearchRequest)
        {
            SearchForTestResponse objResponse = new SearchForTestResponse();
            try
            {

                if (contentSearchRequest != null)
                {
                    objResponse.m_lsTestDetailsModel = await objDocumentService.SearchForTest(contentSearchRequest);
                    objResponse.SetSuccessResponse();
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
        [Route("FetchFeeAssignments")]
        [HttpPost]
        public async Task<SearchForAssignmentsApiResponse> FetcheFreeAssignments(ContentSearchRequest contentSearchRequest)
        {
            SearchForAssignmentsApiResponse objResponse = new SearchForAssignmentsApiResponse();
            try
            {

                if (contentSearchRequest != null)
                {
                    objResponse.m_lsAssignments = await objDocumentService.SearchForAssignment(contentSearchRequest);
                    objResponse.SetSuccessResponse();
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
        [Route("FetchClassroomsForHomePage")]
        [HttpPost]
        public async Task<GetClassroomsForHomePageResponse> GetCLassroomsForHomePage()
        {
            GetClassroomsForHomePageResponse objResponse = new GetClassroomsForHomePageResponse();
            try
            {
                objResponse.m_lsClassroomBasicDetailsModal = await objDocumentService.GetClasroomsForHomePage();
                if(objResponse!=null)
                {
                    objResponse.SetSuccessResponse();
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
        [Route("Subscribe")]
        [HttpPost]
        public async Task<APIDefaultResponse> Subscribe([FromBody]AddEmailSubscriberRequest request)
        {
            APIDefaultResponse objResponse = new APIDefaultResponse();
            try
            {
                if (request != null&& await objDocumentService.InsertNewSubscriber(request))
                {
                    objResponse.SetSuccessResponse();
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetTestDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objResponse;
        }
        [Route("GetAllClassroomCategories")]
        [HttpPost]
        public async Task<GetAllClassroomCategoryResponse> GetAllCategories()
        {
            GetAllClassroomCategoryResponse objResponse = new GetAllClassroomCategoryResponse();
            try
            {
                objResponse.m_lsGetAllClassroomCategory = await objDocumentService.GetAllCategories();
                if (objResponse.m_lsGetAllClassroomCategory!=null)
                {
                    objResponse.SetSuccessResponse();
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetAllCategories", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objResponse;
        }
        [Route("GetClassroomReviews")]
        [HttpPost]
        public async Task<MasterApiResponse<ClassroomReviewsResponse>> FetchAllClassroomReviews(long ClassroomId)
        {
            MasterApiResponse<ClassroomReviewsResponse> objResponse = new MasterApiResponse<ClassroomReviewsResponse>();
            try
            {
                objResponse.data = await objDocumentService.GetClassroomReview(ClassroomId);
                if (objResponse.data != null)
                {
                    objResponse.SetSuccessResponse();
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "FetchAllLiveClassMeetingDetailForStudent", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objResponse;
        }

    }
}
