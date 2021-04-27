using StudentDashboard.BusinessLayer;
using StudentDashboard.DTO;
using StudentDashboard.HttpRequest;
using StudentDashboard.HttpRequest.Document;
using StudentDashboard.HttpResponse;
using StudentDashboard.Models.Base;
using StudentDashboard.Models.Category;
using StudentDashboard.Models.Course;
using StudentDashboard.Models.Document;
using StudentDashboard.Models.Instructor;
using StudentDashboard.Models.Student;
using StudentDashboard.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace StudentDashboard.ServiceLayer
{
    public class DocumentService
    {
        StringBuilder m_strLogMessage;
        DocumentDTO objDocumentDTO;
        public DocumentService()
        {
            m_strLogMessage = new StringBuilder();
            objDocumentDTO = new DocumentDTO();
        }

        public async Task<bool> CheckTestAccess(long TestId, string AccessCode)
        {
            return await objDocumentDTO.CheckTestAccess(TestId, AccessCode);
        }
        public async Task<bool> CheckCourseAccess(long CourseId, string AccessCode)
        {
            return await objDocumentDTO.CheckCourseAccess(CourseId, AccessCode);
        }
        public async Task<bool> CheckAssignmentAccess(long AssignmentId, string AccessCode)
        {
            return await objDocumentDTO.CheckAssignmentAccess(AssignmentId, AccessCode);
        }
        public async Task<bool> CheckClassroomAccess(long ClassroomId, string AccessCode)
        {
            return await objDocumentDTO.CheckClassroomAccess(ClassroomId, AccessCode);
        }
        //public async Task<GetAssignmentDetailsResponseForAnonymous> GetAssignmentDetailsWithAccessCode(long AssignmentId, string AccessCode)
        //{
        //    return await objDocumentDTO.GetAssignmentDetailsWithAccessCodes(AssignmentId, AccessCode);
        //}
        public async Task<GetTestDetailsResponseWithAccessCode> GetTestDetails(long TestId, string AccessCode)
        {
            return await objDocumentDTO.GetTestDetails(TestId, AccessCode);
        }
        public async Task<GetWebsiteHomeDetailsResponse> GetHomeDetails()
        {
            return await objDocumentDTO.GetHomeDetails();
        }
        public async Task<bool> InsertSMSNotification(string Message, string SmsReceiver, int SmsNotificationTypeId)
        {
            return await objDocumentDTO.InsertSMSNotification(Message, SmsReceiver, SmsNotificationTypeId);
        }
        public async Task<bool> ValidatePhoneNoVarificationLink(string UserId,string Guid,int RequestType)
        {
            bool result = false;
            try
            {
                switch(RequestType)
                {
                    case 2:
                        {
                            result = await objDocumentDTO.VialidateStudentPhoneNoVarificationLink(UserId, Guid);
                            break;
                        }
                    case 1:
                        {
                            result = await objDocumentDTO.VialidateInstructorPhoneNoVarificationLink(UserId, Guid);
                            break;
                        }
                }
            }
            catch(Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "ValidatePhoneNoVarificationLink", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<ClassRoomModal> GetClassroomDetailsForStudent(long ClassroomId)
        {
            return await objDocumentDTO.GetClassroomDetailsForStudent(ClassroomId);
        }
        public async Task<List<CourseDetailsModel>> SearchForCourse(string SearchString, int MaxRowToReturn, int NoOfRowsFetched, int SortingTypeId)
        {
            return await objDocumentDTO.SearchForCourse(SearchString,MaxRowToReturn,NoOfRowsFetched,SortingTypeId);
        }
        public async Task<List<TestDetailsModel>> SearchForTest(ContentSearchRequest contentSearchRequest)
        {
            return await objDocumentDTO.SearchForTest(contentSearchRequest.m_strSearchString, 10, contentSearchRequest.m_llLastFetchedContentId);
        }
        public async Task<List<AssignmentDetailsModel>> SearchForAssignment(ContentSearchRequest contentSearchRequest)
        {
            return await objDocumentDTO.SearchForAssignment(contentSearchRequest.m_strSearchString, 10, contentSearchRequest.m_llLastFetchedContentId);
        }
        public async Task<List<ClassroomBasicDetailsModalForHome>> GetClasroomsForHomePage()
        {
            return await objDocumentDTO.GetClasroomsForHomePage();
        }
        public async Task<ClassroomJoinDetailsModal> GetClassroomDetailsForStudentJoin(long ClassroomId)
        {
            return await objDocumentDTO.GetClassroomDetailsForStudentJoin(ClassroomId);
        }
        public async Task<ClassroomJoinDetailsModal> GetLiveClassDetailsForStudent(string ClassroomSku)
        {
            LiveClassBusinessLayer objLiveClassBusinessLayer = new LiveClassBusinessLayer();
            return await objDocumentDTO.GetClassroomDetailsForStudentJoin(objLiveClassBusinessLayer.GetClassroomIdFromSku(ClassroomSku));
        }
        public long GetClassroomIdFromSku(string ClassroomSku)
        {
            LiveClassBusinessLayer objLiveClassBusinessLayer = new LiveClassBusinessLayer();
            return objLiveClassBusinessLayer.GetClassroomIdFromSku(ClassroomSku);
        }
        public async Task<bool> InsertNewSubscriber(AddEmailSubscriberRequest addEmailSubscriberRequest)
        {
            return await objDocumentDTO.InsertNewSubscribe(addEmailSubscriberRequest.m_strEmailAddress);
        }
        public async Task<List<GetAllClassroomCategory>> GetAllCategories()
        {
            return await objDocumentDTO.GetAllCategories();
        }
        public async Task<AvgReviewModel> GetAvgClassroomRating(long ClassroomId)
        {
            AvgReviewModel avgReviewModel = new AvgReviewModel();
            List<RatingNormal> lsRatingNormal = null;
            try
            {
                lsRatingNormal = await objDocumentDTO.GetAvgClassroomRating(ClassroomId);
                var TotalRatings = 0;
                var AvgRatingSum = 0;
                foreach (var data in lsRatingNormal)
                {
                    TotalRatings += data.m_iNoOfRating;

                    switch (data.m_iRating)
                    {
                        case 1:
                            {
                                AvgRatingSum += 1 * data.m_iNoOfRating;
                                avgReviewModel.m_fPercentage1StartRating = data.m_iNoOfRating;
                                break;
                            }
                        case 2:
                            {
                                AvgRatingSum += 2 * data.m_iNoOfRating;
                                avgReviewModel.m_fPercentage2StartRating = data.m_iNoOfRating;
                                break;
                            }
                        case 3:
                            {
                                AvgRatingSum += 3 * data.m_iNoOfRating;
                                avgReviewModel.m_fPercentage3StartRating = data.m_iNoOfRating;
                                break;
                            }
                        case 4:
                            {
                                AvgRatingSum += 4 * data.m_iNoOfRating;
                                avgReviewModel.m_fPercentage4StartRating = data.m_iNoOfRating;
                                break;
                            }
                        case 5:
                            {
                                AvgRatingSum += 5 * data.m_iNoOfRating;
                                avgReviewModel.m_fPercentage5StartRating = data.m_iNoOfRating;
                                break;
                            }
                    }
                }
                if (TotalRatings != 0)
                {
                    avgReviewModel.m_iTotalReviews = TotalRatings;
                    avgReviewModel.m_fPercentage1StartRating = MasterUtilities.GetPercentage(avgReviewModel.m_fPercentage1StartRating, TotalRatings);
                    avgReviewModel.m_fPercentage2StartRating = MasterUtilities.GetPercentage(avgReviewModel.m_fPercentage2StartRating, TotalRatings);
                    avgReviewModel.m_fPercentage3StartRating = MasterUtilities.GetPercentage(avgReviewModel.m_fPercentage3StartRating, TotalRatings);
                    avgReviewModel.m_fPercentage4StartRating = MasterUtilities.GetPercentage(avgReviewModel.m_fPercentage4StartRating, TotalRatings);
                    avgReviewModel.m_fPercentage5StartRating = MasterUtilities.GetPercentage(avgReviewModel.m_fPercentage5StartRating, TotalRatings);
                    avgReviewModel.m_fAvgRating = AvgRatingSum / TotalRatings;
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "VarifyPhoneNo", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return avgReviewModel;
        }
        public async Task<List<ReviewModel>> GetAllClassroomReviews(MasterSearchRequest masterSearchRequest, long ClassroomId)
        {
            List<ReviewModel> lsReviewModel = null;
            try
            {
                lsReviewModel = await objDocumentDTO.GetAllClassroomReviews(ClassroomId, masterSearchRequest.m_iNoOfRowsFetched, Constants.MAX_ITEMS_TO_BE_RETURNED);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "VarifyPhoneNo", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return lsReviewModel;
        }
        public async Task<ClassroomReviewsResponse> GetClassroomReview(long ClassroomId)
        {
            ClassroomReviewsResponse classroomReviewsResponse = new ClassroomReviewsResponse();
            try
            {
                classroomReviewsResponse.avgReviewModel = await GetAvgClassroomRating(ClassroomId);
                classroomReviewsResponse.lsReviews = await GetAllClassroomReviews(new MasterSearchRequest() { m_iNoOfRowsFetched = 0 }, ClassroomId);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "VarifyPhoneNo", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return classroomReviewsResponse;
        }
    }
}