using StudentDashboard.BusinessLayer;
using StudentDashboard.HttpRequest;
using StudentDashboard.HttpResponse;
using StudentDashboard.HttpResponse.ClassRoom;
using StudentDashboard.Models;
using StudentDashboard.Models.Base;
using StudentDashboard.Models.Classroom;
using StudentDashboard.Models.Course;
using StudentDashboard.Models.Instructor;
using StudentDashboard.Models.RazorPay;
using StudentDashboard.Models.Social;
using StudentDashboard.Models.Student;
using StudentDashboard.ServiceLayer;
using StudentDashboard.Utilities;
using StudentDashboard.Views.Student;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace StudentDashboard.DTO
{
    public class StudentDTO
    {
        CPDataService.CpDataServiceClient objCPDataService;
        StringBuilder m_strLogMessage = new StringBuilder();
        public StudentDTO()
        {
            objCPDataService = new CPDataService.CpDataServiceClient();
            
        }
        public async Task<bool> RegisterNewStudent(StudentRegisterModal objStudentDetailsModal)
        {
            bool result = false;
            try
            {
                result = await objCPDataService.RegisterNewStudentAsync(objStudentDetailsModal.m_strFirstName,objStudentDetailsModal.m_strLastName,
                               objStudentDetailsModal.m_strEmail,objStudentDetailsModal.m_strHashedPassword,objStudentDetailsModal.m_strPhoneNo,
                               objStudentDetailsModal.m_strPhoneNoVarificationGuid,objStudentDetailsModal.m_strEmailVarificationGuid);

            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "RegisterNewStudent", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public long GetStudentIdFromUserId(string UserId)
        {
            long StudentId = -1;
            try
            {
                objCPDataService.GetStudentIdFromUserId(UserId,ref StudentId);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "InsertActivityForInstructor", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return StudentId;
        }
        public async Task<bool> InsertActivityForInstructor(long StudentId, string ActivityMessage)
        {
            bool result = false;
            try
            {
                result =await  objCPDataService.InsertActivityForStudentAsync( ActivityMessage,StudentId);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "InsertActivityForInstructor", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public bool ValidateLogin(string UserId,string HashedPassword,ref long StudentId)
        {
            bool result = false;
            try
            {
                result = objCPDataService.ValidateStudentLogin(UserId,HashedPassword,ref StudentId);

            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "RegisterNewStudent", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async  Task<bool> ValidatePasswordRecodevrtOtp(string UserId, string Token,string OTP)
        {
            bool result = false;
            DateTime? TokenExpiryTime=null;
            try
            {
                DataSet ds = await objCPDataService.ValidateStudentPasswordRecoveryRequestAsync(UserId, Token, OTP);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    TokenExpiryTime = ds.Tables[0].AsEnumerable().Select(
                     dataRow => (dataRow.Field<DateTime?>("LAST_PASSWORD_RECOVERY_REQUEST_TIME"))).ToList()[0];
                }
                if(TokenExpiryTime!=null&&DateTime.Now-TokenExpiryTime>TimeSpan.FromSeconds(MvcApplication._forgotPasswordExpiryTimeInMinutes))
                {
                    result = true;
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "ValidatePasswordRecodevrtOtp", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<bool> InsertPasswordRecovery(string UserId,string Token,string OTP)
        {
            bool result = false;
            try
            {
                result = await objCPDataService.InsertStudentPasswordRecoveryRequestAsync(UserId, Token, OTP);

            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "RegisterNewStudent", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<bool> MarkOtpVerifiedForPasswordRecodevry(string UserId, string Token)
        {
            bool result = false;
            try
            {
                result = await objCPDataService.MarkOtpVarifiedAsync(UserId, Token);

            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "RegisterNewStudent", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<bool> UpdateStudentPasswordAfterAuth(string UserId, string Token, string HashedPasword)
        {
            bool result = false;
            try
            {
                result = await objCPDataService.ChanegPasswordAfterAuthenticationAsync(UserId, Token, HashedPasword);

            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "RegisterNewStudent", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<bool> AddQuestionAskForCourse(StudentCourseQuestionModal objStudentCourseQuestionModal)
        {
            bool result = false;
            try
            {
                result = await objCPDataService.InsertCourseQuestionByStudentAsync(objStudentCourseQuestionModal.m_llCourseId,
                    objStudentCourseQuestionModal.m_llStudentId, objStudentCourseQuestionModal.m_strQuestionStatement
                    );
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "AddQuestionAskForCourse", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<List<StudentCourseQuestionModal>> GetAllQuestionOfStudentCourse(long StudentId,long CourseId)
        {
            List<StudentCourseQuestionModal> lsCourseDetailsModel = null;
            try
            {
                DataSet ds = await objCPDataService.GetAllQuestionAskForCourseByStudentAsync(StudentId, CourseId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    lsCourseDetailsModel = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new StudentCourseQuestionModal(
                         dataRow.Field<string>("QUESTION"),
                         dataRow.Field<DateTime>("ROW_INSERTION_DATETIME").ToString("dd MMM yyy HH:mm:ss")
                         )).ToList();
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "SearchForCourse", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return lsCourseDetailsModel;
        }
        public async Task<List<CourseDetailsModel>> SearchForCourse(string SearchString,int MaxRowToReturn,int NoOfRowsFetched,int SortingTypeId)
        {
            List<CourseDetailsModel> lsCourseDetailsModel = null;
            try
            {
                DataSet ds  =await  objCPDataService.SearchForCourseAsync(SearchString,MaxRowToReturn, NoOfRowsFetched, SortingTypeId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    lsCourseDetailsModel = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new CourseDetailsModel(
                         dataRow.Field<long>("COURSE_ID"),
                         dataRow.Field<string>("COURSE_NAME"),
                         dataRow.Field<string>("COURSE_DESCRIPTION"),
                         dataRow.Field<DateTime>("COURSE_ACTIVATION_DATETIME").ToString("d MMM yyyy"),
                         dataRow.Field<int>("NO_OF_STUDENTS_JOINED"),
                         dataRow.Field<string>("INSTRUCTOR_NAME")
                         )).ToList();
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "SearchForCourse", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return lsCourseDetailsModel;
        }
        public async Task<List<CourseDetailsModel>> SearchForCourse(string SearchString, int MaxRowToReturn, int NoOfRowsFetched, int SortingTypeId,long StudentId)
        {
            List<CourseDetailsModel> lsCourseDetailsModel = null;
            try
            {
                DataSet ds = await objCPDataService.SearchForCourseForStudentAsync(SearchString, MaxRowToReturn, NoOfRowsFetched, SortingTypeId, StudentId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    lsCourseDetailsModel = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new CourseDetailsModel(
                         dataRow.Field<long>("COURSE_ID"),
                         dataRow.Field<string>("COURSE_NAME"),
                         dataRow.Field<string>("COURSE_DESCRIPTION"),
                         dataRow.Field<DateTime>("COURSE_ACTIVATION_DATETIME").ToString("d MMM yyyy"),
                         dataRow.Field<int>("NO_OF_STUDENTS_JOINED"),
                         dataRow.Field<string>("INSTRUCTOR_NAME")
                         )).ToList();
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "SearchForCourse", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return lsCourseDetailsModel;
        }
        public async Task<StudentRegisterModal> GetStudentDetails(long StudentId)
        {
            StudentRegisterModal objStudentRegisterModal = null;
            try
            {
                DataSet ds =await objCPDataService.GetStudentDetailsAsync(StudentId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    objStudentRegisterModal = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new StudentRegisterModal(
                         dataRow.Field<string>("FIRST_NAME"),
                         dataRow.Field<string>("LAST_NAME"),
                         dataRow.Field<string>("STUDENT_USER_ID"),
                         dataRow.Field<string>("PHONENO"),
                         dataRow.Field<string>("ADDRESS_LINE_1"),
                         dataRow.Field<string>("ADDRESS_LINE_2"),
                         dataRow.Field<string>("CITY_NAME"),
                         dataRow.Field<string>("STATE_NAME"),
                         dataRow.Field<string>("PIN_CODE"),
                         dataRow.Field<string>("GENDER"),
                         dataRow.Field<DateTime>("ROW_UPDATION_DATETIME").ToString("d MMM yyyy"),
                         dataRow.Field<DateTime>("ROW_INSERTION_DATETIME").ToString("d MMM yyyy"),
                         dataRow.Field<int?>("CITY_ID"),
                         dataRow.Field<int?>("STATE_ID")
                         )).ToList()[0];
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetStudentDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objStudentRegisterModal;
        }
        public async Task<bool> UpdateStudentDetails(StudentRegisterModal objStudentRegisterModal)
        {
            bool result = false;
            try
            {
                result = await objCPDataService.UpdateStudentDetailsAsync(objStudentRegisterModal.m_strFirstName, objStudentRegisterModal.m_strLastName,
                            objStudentRegisterModal.m_strAddressLine1,objStudentRegisterModal.m_strAddressLine2,objStudentRegisterModal.m_strPinCode,
                            objStudentRegisterModal.m_iStateId,objStudentRegisterModal.m_iStateId,objStudentRegisterModal.m_strGender,objStudentRegisterModal.m_llStudentId,
                            objStudentRegisterModal.m_strPhoneNo);

            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "RegisterNewStudent", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<bool> JoinStudentToCourse(long CourseId,long StudentId)
        {
            bool result = false;
            try
            {
                result = await objCPDataService.JoinStudentToCourseAsync(CourseId,StudentId);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "RegisterNewStudent", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<List<SearchInstructorResponseModal>> SearchForInstructor(string SearchString,int MaxRowToReturn )
        {
            List<SearchInstructorResponseModal> lsSearchInstructorResponseModal = null;
            try
            {
                DataSet ds =await objCPDataService.SearchForInstructorAsync(SearchString, MaxRowToReturn);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    lsSearchInstructorResponseModal = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new SearchInstructorResponseModal(
                         dataRow.Field<int>("ID"),
                         dataRow.Field<string>("INSTRUCTOR_FIRST_NAME"),
                         dataRow.Field<string>("INSTRUCTOR_LAST_NAME"),
                         dataRow.Field<DateTime>("ROW_INSERTION_DATETIME").ToString("d MMM yyyy"),
                          dataRow.Field<int>("NO_OF_COURSE_CREATED"),
                          dataRow.Field<int>("NO_OF_STUDENTS_JOINED")
                         )).ToList();
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "RegisterNewStudent", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return lsSearchInstructorResponseModal;
        }
        public async Task<List<StudentJoinedCoursesResponseModal>> SerachForJoinedCourses(long StudentId,string SearchString, int MaxRowToReturn)
        {
            List<StudentJoinedCoursesResponseModal> lsStudentJoinedCoursesResponseModal = null;
            try
            {
                DataSet ds =await objCPDataService.GetJoinedCoursesForStudentAsync(StudentId,SearchString, MaxRowToReturn);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    lsStudentJoinedCoursesResponseModal = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new StudentJoinedCoursesResponseModal(
                         dataRow.Field<long>("COURSE_ID"),
                         dataRow.Field<string>("COURSE_NAME"),
                         dataRow.Field<string>("COURSE_DESCRIPTION"),
                         dataRow.Field<DateTime>("ROW_INSERTION_DATETIME").ToString("d MMM yyyy"),
                          dataRow.Field<int>("COURSE_PROGRESS")
                         )).ToList();
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "RegisterNewStudent", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return lsStudentJoinedCoursesResponseModal;
        }
        public async Task<bool> JoinStudentToInstructor(long StudentId,int InstructorId)
        {
            bool result = false;
            try
            {
                result = await objCPDataService.JoinStudentToInstructorAsync(StudentId, InstructorId);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "RegisterNewStudent", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<bool> InsertReadTopicByStudent(long StudentId, long TopicId)
        {
            bool result = false;
            try
            {
                result = await objCPDataService.InsertCompletedTopicforStudentAsync(TopicId, StudentId);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "RegisterNewStudent", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<List<SearchInstructorResponseModal>> GetAllInstructorJoinedForStudent(long StudentId,string SearchString, int MaxRowToReturn)
        {
            List<SearchInstructorResponseModal> lsSearchInstructorResponseModal = null;
            try
            {
                DataSet ds =await objCPDataService.GetAllJoinedInstructorForStudentAsync(StudentId,SearchString, MaxRowToReturn);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    lsSearchInstructorResponseModal = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new SearchInstructorResponseModal(
                         dataRow.Field<int>("INSTRUCTOR_ID"),
                         dataRow.Field<string>("INSTRUCTOR_FIRST_NAME"),
                         dataRow.Field<string>("INSTRUCTOR_LAST_NAME"),
                         dataRow.Field<DateTime>("JOIN_DATE").ToString("d MMM yyyy"),
                          dataRow.Field<int>("NO_OF_COURSE_CREATED"),
                          dataRow.Field<int>("NO_OF_STUDENTS_JOINED")
                         )).ToList();
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "RegisterNewStudent", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return lsSearchInstructorResponseModal;
        }
        public async Task<List<AssignmentDetailsModel>> SearchForAssignment(string SearchString, int MaxRowToReturn,long LastFetchedId)
        {
            List<AssignmentDetailsModel> lsAssignmentDetailsModel = null;
            try
            {
                DataSet ds = await objCPDataService.SearchForAssignmentAsync(SearchString, MaxRowToReturn,LastFetchedId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    lsAssignmentDetailsModel = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new AssignmentDetailsModel(
                         dataRow.Field<long>("ASSIGNMENT_ID"),
                         dataRow.Field<string>("ASSIGNMENT_NAME"),
                         dataRow.Field<string>("ASSIGNMENT_DESCRIPTION"),
                         dataRow.Field<byte>("ASSIGNMENT_TYPE"),
                         dataRow.Field<DateTime>("ROW_INSERTION_DATETIME").ToString("d MMM yyyy"),
                         dataRow.Field<int>("NO_OF_QUESTIONS"),
                          dataRow.Field<int>("NO_OF_SUBJECTIVE_QUESTIONS")
                         )).ToList();
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "RegisterNewStudent", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return lsAssignmentDetailsModel;
        }
        public  bool InserAssignmentResponse(AssignmentSubmissionRequest objAssignmentSubmissionRequest)
        {
            bool result = false;
            try
            {
                result = objCPDataService.InsertAssignmentResponse(objAssignmentSubmissionRequest.m_llStudentId, objAssignmentSubmissionRequest.m_llAssignmentId,
                           objAssignmentSubmissionRequest.m_dtStartTime, objAssignmentSubmissionRequest.m_dtFinishTime, objAssignmentSubmissionRequest.m_strResponse,
                           objAssignmentSubmissionRequest.m_iPercentageScore, objAssignmentSubmissionRequest.m_iTotalNoOfQuestions, ref objAssignmentSubmissionRequest.m_llSubmissionId);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "RegisterNewStudent", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<List<TestDetailsModel>> SearchForTest(string SearchString, int MaxRowToReturn,long LastFetchedId)
        {
            List<TestDetailsModel> lsTestDetailsModel = null;
            try
            {
                DataSet ds = await objCPDataService.SearchForTestAsync(SearchString, MaxRowToReturn, LastFetchedId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    lsTestDetailsModel = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new TestDetailsModel(
                         dataRow.Field<long>("TEST_ID"),
                         dataRow.Field<string>("TEST_NAME"),
                         dataRow.Field<string>("TEST_DESCRIPTION"),
                         dataRow.Field<DateTime>("ROW_INSERTION_DATETIME").ToString("d MMM yyyy"),
                         dataRow.Field<int>("NO_OF_QUESTIONS"),
                         dataRow.Field<byte>("TEST_TYPE")
                         )).ToList();
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "RegisterNewStudent", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return lsTestDetailsModel;
        }
        public async Task<List<AssignmentsSubmissionModal>> GetAllAssignmentSubmissions(long StudentId)
        {
            List<AssignmentsSubmissionModal> lsAssignmentsSubmissionOfStudentResponse = null;
            try
            {
                DataSet ds = await objCPDataService.GetAllAssignmentSubmissionsForStudentAsync(StudentId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    lsAssignmentsSubmissionOfStudentResponse = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new AssignmentsSubmissionModal(
                         dataRow.Field<long>("SUBMISSION_ID"),
                         dataRow.Field<int>("TOTAL_NO_OF_QUESTIONS"),
                         dataRow.Field<DateTime>("ROW_INSERTION_DATETIME").ToString("d MMM yyyy"),
                         dataRow.Field<int>("ASSIGNMANT_PERCENTAGE_SCORE"),
                         dataRow.Field<string>("ASSIGNMENT_NAME")
                         )).ToList();
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetAllAssignmentSubmissions", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return lsAssignmentsSubmissionOfStudentResponse;
        }
        public async Task<GetAssignmentSubssionDetials> GetAssignmentResponse(long Submissionid,long StudentId)
        {
            GetAssignmentSubssionDetials objGetAssignmentSubssionDetials=null;
            try
            {

                DataSet ds = await objCPDataService.GetAssignmentResponseAsync(Submissionid, StudentId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    objGetAssignmentSubssionDetials = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new GetAssignmentSubssionDetials(
                         dataRow.Field<string>("ASSIGNMENT_NAME"),
                         dataRow.Field<string>("ASSIGNMENT_RESPONSE_FOR_MCQ"),
                         dataRow.Field<DateTime>("ASSIGNMENT_START_TIME").ToString("d MMM yyyy"),
                         dataRow.Field<DateTime>("ASSIGNMENT_START_TIME"),
                          dataRow.Field<DateTime>("ASSIGNMENT_FINISH_TIME")
                         )).ToList()[0];
                }

            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "RegisterNewStudent", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objGetAssignmentSubssionDetials;
        }
        public bool InsertTestResponse(TestSubmissionRequest objTestSubmissionRequest)
        {
            bool result = false;
            try
            {
                result = objCPDataService.InsertTestResponse(objTestSubmissionRequest.m_llStudentId, objTestSubmissionRequest.m_llTestId,
                           objTestSubmissionRequest.m_dtStartTime, objTestSubmissionRequest.m_dtFinishTime, objTestSubmissionRequest.m_strResponse,
                           objTestSubmissionRequest.m_iPercentageScore, objTestSubmissionRequest.m_iTotalNoOfQuestions, ref objTestSubmissionRequest.m_llSubmissionId);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "RegisterNewStudent", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<List<TestSubmissionModal>> GetAllTestSubmissions(long StudentId)
        {
            List<TestSubmissionModal> lsTestSubmissionModal = null;
            try
            {
                DataSet ds = await objCPDataService.GetAllTestSubmissionsForStudentAsync(StudentId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    lsTestSubmissionModal = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new TestSubmissionModal(
                         dataRow.Field<long>("SUBMISSION_ID"),
                         dataRow.Field<int>("TOTAL_NO_OF_QUESTIONS"),
                         dataRow.Field<DateTime>("ROW_INSERTION_DATETIME").ToString("d MMM yyyy"),
                         dataRow.Field<int>("TEST_SCORE"),
                         dataRow.Field<string>("TEST_NAME")
                         )).ToList();
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetAllAssignmentSubmissions", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return lsTestSubmissionModal;
        }
        public async Task< GetTestSubmissionDetailsResponse> GetTestResponse(long Submissionid,long StudentId)
        {
            GetTestSubmissionDetailsResponse objGetTestSubmissionDetailsResponse = null;
            try
            {
                DataSet ds =await objCPDataService.GetTestResponseAsync(Submissionid,StudentId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    objGetTestSubmissionDetailsResponse = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new GetTestSubmissionDetailsResponse(
                         dataRow.Field<string>("TEST_NAME"),
                         dataRow.Field<string>("TEST_RESPONSE"),
                         dataRow.Field<DateTime>("TEST_START_TIME").ToString("d MMM yyyy"),
                         dataRow.Field<DateTime>("TEST_START_TIME"),
                          dataRow.Field<DateTime>("TEST_FINISH_TIME")
                         )).ToList()[0];
                }

            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "RegisterNewStudent", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objGetTestSubmissionDetailsResponse;
        }
        public async Task<StudentHomeModal> GetStudentHomeDetails(long StudentId)
        {
            StudentHomeModal objStudentHomeModal = null;
            try
            {
                DataSet ds = await objCPDataService.GetStudentHomeDetailsAsync(StudentId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    objStudentHomeModal = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new StudentHomeModal(
                         dataRow.Field<int>("COURSES_JOINED"),
                         dataRow.Field<int>("COURSES_COMPLETED"),
                         dataRow.Field<int>("INSTRUCTORS_JOINED"), 
                         dataRow.Field<int>("LIVE_CLASSES_JOINED"),
                         dataRow.Field<int>("ASSIGNMENTS_SUBMITTED")
                         )).ToList()[0];
                }

            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "RegisterNewStudent", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objStudentHomeModal;
        }
        public async Task<bool> CheckIsStudentHasJoinedTheCourse(long StudentId,long CourseId)
        {
            bool result=false;   
            try
            {

                DataSet ds = await objCPDataService.CheckStudentHasJoinedTheCourseAsync(StudentId, CourseId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    result = true;
                }

            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "CheckIsStudentHasJoinedTheCourse", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<bool> CheckIsStudentHasSubmittedTheTest(long StudentId, long TestId)
        {
            bool result = false;
            try
            {

                DataSet ds = await objCPDataService.CheckStudentHasSubmittedTheTestAsync(StudentId, TestId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    result = true;
                }

            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "CheckIsStudentHasSubmittedTheTest", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<bool> CheckIsStudentHasSubmittedTheAssignment(long StudentId, long AssignmentId)
        {
            bool result = false;
            try
            {

                DataSet ds = await objCPDataService.CheckStudentHasSubmittedTheAssignmentAsync(StudentId, AssignmentId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    result = true;
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "CheckIsStudentHasSubmittedTheTest", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<bool> CheckIsTestSubmissionIdExsitsForStudent(long StudentId, long SubmissionId)
        {
            bool result = false;
            try
            {

                DataSet ds = await objCPDataService.CheckTestResponseIdExistsForStudentAsync(StudentId, SubmissionId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    result = true;
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "CheckIsTestSubmissionIdExsitsForStudent", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<bool> CheckIsAssignmentSubmissionIdExsitsForStudent(long StudentId, long ResponseId)
        {
            bool result = false;
            try
            {
         
                DataSet ds = await objCPDataService.CheckAssignmentResponseIdExistsForStudentAsync(StudentId, ResponseId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    result = true;
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "CheckIsStudentHasSubmittedTheTest", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<InstructorProfileDetailsModal> GetInstructorProfileDetails(int InstructorId,long StudentId)
        {
            InstructorProfileDetailsModal result = null;
            try
            {

                DataSet ds = await objCPDataService.GetInstructorProfileDetailsForStudentAsync(InstructorId, StudentId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    result = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new InstructorProfileDetailsModal(
                         dataRow.Field<string>("INSTRUCTOR_FULL_NAME"),
                         dataRow.Field<DateTime>("ROW_INSERTION_DATETIME").ToString("d MMM yyyy"),
                         dataRow.Field<int>("NO_OF_COURSES_CREATED"),
                         dataRow.Field<int>("NO_OF_ASSIGNMENTS_CREATED"),
                         dataRow.Field<int>("NO_OF_TESTS_CREATED"),
                         dataRow.Field<int>("NO_OF_STUDENTS_JOINED"),
                         dataRow.Field<int>("NO_OF_STUDENTS_JOINED_THE_COURSE"),
                         dataRow.Field<DateTime?>("FOLLOWING_DATE") == null ? null : dataRow.Field<DateTime>("FOLLOWING_DATE").ToString("d MMM yyyy"),
                         dataRow.Field<string>("PROFILE_URL")
                         )).ToList()[0];
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "CheckIsStudentHasSubmittedTheTest", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<List<CourseDetailsModel>> GetAllCourseDetailsForInstructor(int InstructorId)
        {
            List<CourseDetailsModel> result = null;
            try
            {
                DataSet ds = new DataSet();
                ds = await objCPDataService.GetAllCourseAsync(InstructorId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    result = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new CourseDetailsModel(
                         dataRow.Field<long>("COURSE_ID"),
                         dataRow.Field<string>("COURSE_NAME"),
                         dataRow.Field<DateTime>("ROW_INSERTION_DATETIME").ToString("d MMM yyyy"),
                         dataRow.Field<int>("TOTAL_INDEXES"),
                          dataRow.Field<string>("COURSE_STATUS_NAME"),
                           dataRow.Field<int>("NO_OF_STUDENTS_JOINED")
                         )).ToList();
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetAllCourseDetailsForInstructor", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<ClassRoomModal> GetClassroomDetailsForStudent(long ClassroomId)
        {
            ClassRoomModal objClassRoomModal = new ClassRoomModal();
            try
            {
                DataSet ds = await objCPDataService.GetClasroomDetailsAsync(ClassroomId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    objClassRoomModal = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new ClassRoomModal(
                          dataRow.Field<long>("CLASSROOM_ID"),
                          dataRow.Field<string>("CLASSROOM_NAME"),
                          dataRow.Field<string>("CLASSROOM_DESCRIPTION"),
                          dataRow.Field<DateTime>("ROW_INSERTION_DETATIME").ToString("d MMM yyyy"),
                          dataRow.Field<int>("NO_OF_STUDENTS_JOINED").ToString(),
                          dataRow.Field<bool?>("IS_MEETING_ACTIVE"),
                          dataRow.Field<string>("BACK_GROUND_IMAGE_PATH"),
                          dataRow.Field<string>("CLASSROOM_MEETING_NAME"),
                          dataRow.Field<int>("CLASSROOM_CHARGE_IN_PAISE"),
                          dataRow.Field<DateTime?>("CLASS_START_DATE"),
                          dataRow.Field<DateTime?>("REGISTRATION_CLOSE_DATE"),
                          dataRow.Field<int>("NO_OF_DEMO_CLASSES")
                         )).ToList()[0];
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetClassroomDetailsForStudent", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objClassRoomModal;
        }
        public async Task<bool> JoinClassroom(long ClassroomId, long StudentId)
        {
            bool result = false;
            try
            {

                result = await objCPDataService.JoinStudentToClassroomAsync(ClassroomId,StudentId);
                
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "CheckIsStudentHasSubmittedTheTest", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<bool> MarkClassroomPaymentSuccess(long ClassroomId, long StudentId)
        {
            bool result = false;
            try
            {

                result = await objCPDataService.MarkStudentClassroomPaymentSuccessfulAsync(ClassroomId, StudentId);

            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "CheckIsStudentHasSubmittedTheTest", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<bool> JoinClassroomMeeting(long MeetingId, long StudentId)
        {
            bool result = false;
            try
            {

                result = await objCPDataService.JoinStudentToMeetingAsync(MeetingId, StudentId);

            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "JoinClassroomMeeting", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<List<StudentClassroomModal>> GetJoinedClassroom(long StudentId)
        {
            List<StudentClassroomModal> objClassRoomModal = new List<StudentClassroomModal>();
            try
            {
                DataSet ds = await objCPDataService.GetJoinedClassroomForStudentAsync(StudentId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    objClassRoomModal = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new StudentClassroomModal(
                         dataRow.Field<long>("CLASSROOM_ID"),
                         dataRow.Field<string>("CLASSROOM_NAME"),
                          dataRow.Field<string>("CLASSROOM_DESCRIPTION"),
                         dataRow.Field<DateTime>("JOINING_DATE").ToString("d MMM yyyy")

                         )).ToList();
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetAllClassroomForIsntrcutor", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objClassRoomModal;
        }
      
        public async Task<JitsiMeetingModal> GetClassroomMeetingDetails(long ClassroomId)
        {
            JitsiMeetingModal objJitsiMeetingModal = null;
            try
            {
                DataSet ds = await objCPDataService.GetMeetingDetailsOfClassroomAsync(ClassroomId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    objJitsiMeetingModal = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new JitsiMeetingModal(
                         dataRow.Field<long>("MEETING_ID"),
                         dataRow.Field<string>("MEETING_NAME"),
                          dataRow.Field<string>("MEETING_PASSWORD"),
                         dataRow.Field<string>("CLASSROOM_NAME")
                         )).ToList()[0];
                }
                if(objJitsiMeetingModal!=null)
                {
                    objJitsiMeetingModal.m_llClassroomId = ClassroomId;
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetAllClassroomForIsntrcutor", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objJitsiMeetingModal;
        }
        public async Task<bool> InsertNewMessageToClassroomByStudent(InsertStudentMessageToClassroom insertStudentMessageToClassroom)
        {
            bool result = false;
            try
            {

                result = await objCPDataService.InsertNewStudentClassroomMessageAsync(insertStudentMessageToClassroom.m_llClassroomId,
                    insertStudentMessageToClassroom.m_strMessage, insertStudentMessageToClassroom.m_llStudentId);

            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "JoinClassroomMeeting", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<bool> CheckStudentAccessToClassroom(long StudentId, long ClassroomId)
        {
            bool result = false;
            try
            {

                DataSet ds = await objCPDataService.CheckStudentClassroomAccessAsync(ClassroomId,StudentId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    result = true;
                }

            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "CheckIsStudentHasJoinedTheCourse", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<List<ClassroomStudentMessageModal>> GetAllClassroomLastMessagesForStudentAfterLast(long ClassroomId, long LastMessageId,long StudentId)
        {
            List<ClassroomStudentMessageModal> lsResponse = new List<ClassroomStudentMessageModal>();
            try
            {
                DataSet ds = await objCPDataService.GetAllClassroomMessageAfterLastMessageAsync(ClassroomId,LastMessageId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    lsResponse = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new ClassroomStudentMessageModal(
                         dataRow.Field<string>("MESSAGE"),
                         dataRow.Field<string>("STUDENT_NAME"),
                         dataRow.Field<DateTime>("ROW_INSERTION_DATETIME").ToString("f"),
                         dataRow.Field<long>("MESSAGE_ID"),
                         dataRow.Field<bool>("IS_INSTRUCTOR"),
                         dataRow.Field<long?>("STUDENT_ID") != null && dataRow.Field<long?>("STUDENT_ID") == StudentId ? true : false
                         )).ToList();
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetAllClassroomForIsntrcutor", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return lsResponse;
        }
        public async Task<List<ClassroomStudentMessageModal>> GetAllClassroomMessageForStudent(long ClassroomId,long StudentId)
        {
            List<ClassroomStudentMessageModal> lsResponse = new List<ClassroomStudentMessageModal>();
            try
            {
                DataSet ds = await objCPDataService.GetAllClassroomMessageAsync(ClassroomId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    lsResponse = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new ClassroomStudentMessageModal(
                         dataRow.Field<string>("MESSAGE"),
                         dataRow.Field<string>("STUDENT_NAME"),
                         dataRow.Field<DateTime>("ROW_INSERTION_DATETIME").ToString("f"),
                         dataRow.Field<long>("MESSAGE_ID"),
                         dataRow.Field<bool>("IS_INSTRUCTOR"),
                         dataRow.Field<long?>("STUDENT_ID") != null && dataRow.Field<long?>("STUDENT_ID") == StudentId ? true : false
                         )).ToList();
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetAllClassroomForIsntrcutor", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return lsResponse;
        }
        public async Task<List<StudentClassroomAssignmentModal>> GetAllClassroomAssignment(long ClassroomId, long StudentId)
        {
            List<StudentClassroomAssignmentModal> lsResponse = new List<StudentClassroomAssignmentModal>();
            try
            {
                DataSet ds = await objCPDataService.GetAllClassroomAssignmntForStudentAsync(ClassroomId, StudentId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    lsResponse = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new StudentClassroomAssignmentModal(
                         dataRow.Field<long>("ASSIGNMENT_ID"),
                         dataRow.Field<string>("ASSIGNMENT_NAME"),
                         dataRow.Field<DateTime>("ROW_INSERTION_DATETIME").ToString("d MMM yyyy"),
                         dataRow.Field<long?>("SUBMISSION_ID"),
                         dataRow.Field<int?>("ASSIGNMANT_PERCENTAGE_SCORE"),
                         dataRow.Field<string>("ASSIGNMENT_TYPE_NAME"),
                         dataRow.Field<int>("NO_OF_MCQ_QUESTIONS")+ dataRow.Field<int>("NO_OF_SUBJECTIVE_QUESTIONS"),
                         dataRow.Field<string>("SHARE_CODE")
                         )).ToList();
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetAllClassroomForIsntrcutor", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return lsResponse;
        }
        public async Task<List<StudentClassroomAssignmentModal>> GetAllClassroomSubmittedAssignment(long ClassroomId, long StudentId)
        {
            List<StudentClassroomAssignmentModal> lsResponse = new List<StudentClassroomAssignmentModal>();
            try
            {
                DataSet ds = await objCPDataService.GetAllClassroomAssignmentSubmissionsForStudentAsync(ClassroomId, StudentId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    lsResponse = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new StudentClassroomAssignmentModal(
                          dataRow.Field<string>("ASSIGNMENT_NAME"),
                         dataRow.Field<long>("SUBMISSION_ID"),
                         dataRow.Field<DateTime>("SUBMISSION_DATE").ToString("d MMM yyyy"),
                         dataRow.Field<int?>("ASSIGNMANT_PERCENTAGE_SCORE"),
                         dataRow.Field<int>("TOTAL_NO_OF_QUESTIONS")
                         )).ToList();
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetAllClassroomForIsntrcutor", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return lsResponse;
        }
        public async Task<List<StudentClassroomTestModal>> GetAllClassroomTest(long ClassroomId, long StudentId)
        {
            List<StudentClassroomTestModal> lsResponse = new List<StudentClassroomTestModal>();
            try
            {
                DataSet ds = await objCPDataService.GetAllClassroomTestForStudentAsync(ClassroomId, StudentId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    lsResponse = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new StudentClassroomTestModal(
                         dataRow.Field<long>("TEST_ID"),
                         dataRow.Field<long?>("SUBMISSION_ID"),
                         dataRow.Field<string>("TEST_NAME"),
                         dataRow.Field<string>("TEST_DESCRIPTION"),
                         dataRow.Field<DateTime>("ROW_INSERTION_DATETIME").ToString("d MMM yyyy"),
                         dataRow.Field<int>("NO_OF_QUESTIONS"),
                         dataRow.Field<string>("TEST_TYPE_NAME"),
                         dataRow.Field<string>("SHARE_CODE")
                         )).ToList();
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetAllClassroomForIsntrcutor", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return lsResponse;
        }
        public async Task<List<StudentClassroomTestModal>> GetAllClassroomTestSubmissons(long ClassroomId, long StudentId)
        {
            List<StudentClassroomTestModal> lsResponse = new List<StudentClassroomTestModal>();
            try
            {
                DataSet ds = await objCPDataService.GetAllClassroomTestSubmissionsForStudentAsync(ClassroomId, StudentId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    lsResponse = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new StudentClassroomTestModal(
                         dataRow.Field<long>("SUBMISSION_ID"),
                         dataRow.Field<string>("TEST_NAME"),
                         dataRow.Field<DateTime>("SUBMISSION_DATE").ToString("d MMM yyyy"),
                         dataRow.Field<int>("TEST_SCORE"),
                         dataRow.Field<int>("PERCENTAGE_SCORE")
                         )).ToList();
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetAllClassroomForIsntrcutor", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return lsResponse;
        }
        public async Task<StudentClassroomHomeDetails> GetStudentClassroomHomeDetails(long ClassroomId, long StudentId)
        {
            StudentClassroomHomeDetails studentClassroomHomeDetails = null;
            try
            {
                DataSet ds = await objCPDataService.GetClassroomHomeDetailsForStudentAsync(ClassroomId, StudentId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    studentClassroomHomeDetails = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new StudentClassroomHomeDetails(
                         ClassroomId,
                         dataRow.Field<int>("NO_OF_MEETINGS_JOINED"),
                         dataRow.Field<int>("NO_OF_TESTS_SUBMITTED"),
                         dataRow.Field<int>("NO_OF_ASSIGNMENTS_SUBMITTED"),
                         dataRow.Field<bool>("IS_PAYMENT_DONE"),
                         dataRow.Field<DateTime?>("CLASSROOM_START_DATE"),
                         dataRow.Field<int>("CLASSROOM_JOINING_FEE_IN_PAISE"),
                         dataRow.Field<DateTime>("CLASS_JOINING_DATE").ToString("d MMM yyyy"),
                         dataRow.Field<int>("TOTAL_NO_OF_TESTS"),
                         dataRow.Field<int>("TOTAL_NO_OF_MEETINGS"),
                         dataRow.Field<int?>("CLASSROOM_RATING")
                         )).ToList()[0];
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetAllClassroomForIsntrcutor", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return studentClassroomHomeDetails;
        }
        public async Task<bool> UpdateProfilePicture(StudentProfileChangeRequest objStudentProfilePictureUpdtaeRequest)
        {
            bool result = false;
            try
            {
                result  = await objCPDataService.UpdateStudentProfilePictureAsync(objStudentProfilePictureUpdtaeRequest.m_llStudentId, 
                    objStudentProfilePictureUpdtaeRequest.imageUploadDetailsModal.m_strOriginalFileUrl,
                    objStudentProfilePictureUpdtaeRequest.imageUploadDetailsModal.m_strSmallSizeUrl,
                    objStudentProfilePictureUpdtaeRequest.imageUploadDetailsModal.m_strMediumSizeUrl
                    );
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "UpdateProfilePicture", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<StudentRegisterModal> GetStudentBasicDetails(long StudentId)
        {
            StudentRegisterModal objStudentRegisterModal = null;
            try
            {
                DataSet ds = await objCPDataService.GetStudentDetailsAsync(StudentId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    objStudentRegisterModal = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new StudentRegisterModal(
                         dataRow.Field<string>("FIRST_NAME"),
                         dataRow.Field<string>("LAST_NAME"),
                         dataRow.Field<string>("PROFILE_URL")
                         )).ToList()[0];
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetStudentDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objStudentRegisterModal;
        }
        public async Task<List<GetPublicClassroomsResponse>> SearchClassroom(int NoOfRowsFecthed, int NoOfRecordsToBeFteched,
            long StudentId,string QueryString)
        {
            List<GetPublicClassroomsResponse> lsResponse = new List<GetPublicClassroomsResponse>();
            try
            {
                DataSet ds = await objCPDataService.GetPublicClassroomDetailsForStudentAsync(NoOfRowsFecthed,
                    StudentId, NoOfRecordsToBeFteched, QueryString);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    lsResponse = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new GetPublicClassroomsResponse(
                         dataRow.Field<string>("CLASSROOM_NAME"),
                         dataRow.Field<long>("CLASSROOM_ID"),
                         dataRow.Field<DateTime>("ACTIVATION_DATETIME").ToString("d MMM yyyy"),
                         dataRow.Field<int>("NO_OF_ENROLLMENT"),
                         dataRow.Field<long?>("ID"),
                         dataRow.Field<DateTime?>("ROW_INSERTION_DATETIME") ,
                         dataRow.Field<int>("CLASSROOM_CHARGE_IN_PAISE"),
                         dataRow.Field<string>("INSTRUCTOR_NAME"),
                         dataRow.Field<string>("INSTRUCTOR_IMAGE_URL"),
                         dataRow.Field<int>("INSTRUCTOR_ID"),
                         dataRow.Field<string>("CLASSROOM_BG_IMAGE_URL"),
                         dataRow.Field<DateTime?>("REGISTRATION_CLOSE_DATE")
                         )).ToList();
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetAllClassroomForIsntrcutor", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return lsResponse;
        }
        public async Task<PaymentRequestDTO> GetClassroomPaymentInfo(long ClassroomId,long StudentId)
        {
            PaymentRequestDTO classroomPaymentRequestDTO = new PaymentRequestDTO();
            try
            {
                DataSet ds = await objCPDataService.GetClassroomPaymentDetailsAsync(ClassroomId, StudentId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    classroomPaymentRequestDTO = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new PaymentRequestDTO(
                         dataRow.Field<string>("STUDENT_NAME"),
                         dataRow.Field<string>("STUDENT_USER_ID"),
                         dataRow.Field<string>("PHONENO"),
                         dataRow.Field<int>("CLASSROOM_CHARGE")
                         )).ToList()[0];
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetAllClassroomForIsntrcutor", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return classroomPaymentRequestDTO;
        }
        public async Task<PaymentRequestDTO> GetCoursePaymentInfo(long CourseId, long StudentId)
        {
            PaymentRequestDTO classroomPaymentRequestDTO = new PaymentRequestDTO();
            try
            {
                DataSet ds = await objCPDataService.GetCoursePaymentDetailsAsync(CourseId, StudentId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    classroomPaymentRequestDTO = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new PaymentRequestDTO(
                         dataRow.Field<string>("STUDENT_NAME"),
                         dataRow.Field<string>("STUDENT_USER_ID"),
                         dataRow.Field<string>("PHONENO"),
                         dataRow.Field<int>("COURSE_CHARGE")
                         )).ToList()[0];
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetAllClassroomForIsntrcutor", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return classroomPaymentRequestDTO;
        }
        public async Task<bool> InsertPaymentOrderRequest(RazorPayPaymentRequestModal razorPayPaymentRequestModal)
        {
            bool result = false;
            try
            {
                result = await objCPDataService.CreatePaymentOrderAsync(razorPayPaymentRequestModal.m_strOrderId,
                    razorPayPaymentRequestModal.razorPayCustomerData.m_strName,
                    razorPayPaymentRequestModal.razorPayCustomerData.m_strEmail,
                    razorPayPaymentRequestModal.razorPayCustomerData.m_strContact,
                    razorPayPaymentRequestModal.razorPayPaymentDataModal.m_iAmountInPaise,
                    razorPayPaymentRequestModal.razorPayCustomerData.m_strAddress);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "CheckIsStudentHasJoinedTheCourse", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<bool> InsertRazorPayPaymentResponse(RazorPayPaymentResponseModal razorPayPaymentResponseModal)
        {
            bool result = false;
            try
            {
                result = await objCPDataService.InsertRazorPayTxnDetailsAsync(
                    razorPayPaymentResponseModal.m_strOrderId,
                    razorPayPaymentResponseModal.m_strRazorPayPaymentId,
                    razorPayPaymentResponseModal.m_strRazorPayOrderId,
                    razorPayPaymentResponseModal.m_strRazorPaySignature);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "CheckIsStudentHasJoinedTheCourse", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<List<StudentTestSearchResultModal>> GetFreeTestsForStudent(StudentTestSearchRequest studentTestSearchRequest)
        {
            List<StudentTestSearchResultModal> lsStudentTestSearchResultModal = new List<StudentTestSearchResultModal>();
            try
            {
                DataSet ds = await objCPDataService.GetTestSearchResultForStudentAsync(studentTestSearchRequest.m_llStudentId,
                    studentTestSearchRequest.m_strSearchString, studentTestSearchRequest.m_iNoOfRowsToBeFetched, studentTestSearchRequest.m_llLastTestFetched);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    lsStudentTestSearchResultModal = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new StudentTestSearchResultModal(
                         dataRow.Field<long>("TEST_ID"),
                         dataRow.Field<string>("TEST_NAME"),
                         dataRow.Field<string>("SHARE_CODE"),
                       
                         dataRow.Field<string>("TEST_DESCRIPTION"),
                           dataRow.Field<DateTime>("TEST_ACTIVATION_DATETIME").ToString("d MMM yyyy"),
                         dataRow.Field<long?>("SUBMISSION_ID"),
                         dataRow.Field<DateTime?>("SUBMISSION_DATE") == null ? null : dataRow.Field<DateTime>("SUBMISSION_DATE").ToString("d MMM yyyy"),
                         dataRow.Field<int>("NO_OF_QUESTIONS"),
                         dataRow.Field<int?>("TOTAL_ALLOWED_TIME")
                         )).ToList();
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetAllClassroomForIsntrcutor", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return lsStudentTestSearchResultModal;
        }
        public async Task<ClassroomJoinDetailsModal> GetClassroomDetailsForStudentJoin(long ClassroomId)
        {
            ClassroomJoinDetailsModal objClassRoomModal = new ClassroomJoinDetailsModal();
            try
            {
                DataSet ds = await objCPDataService.GetClassRoomDetailsForStudentAsync(ClassroomId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    objClassRoomModal = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new ClassroomJoinDetailsModal(
                          dataRow.Field<int>("NO_OF_STUDENTS_JOINED"),
                          dataRow.Field<int>("NO_OF_ASSIGNMENTS"),
                          dataRow.Field<int>("NO_OF_MEETINGS"),
                          dataRow.Field<int>("NO_OF_TESTS"),
                          dataRow.Field<int>("NO_OF_ATTACHENTS"),
                          dataRow.Field<string>("CLASSROOM_SYLLABUS"),
                          dataRow.Field<string>("CLASSROOM_SCHEDULE_OBJ")
                         )).ToList()[0];
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetAllClassroomForIsntrcutor", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objClassRoomModal;
        }
        public async Task<List<StudentBasicClassroomDetails>> GetStudentHomeClassroomDetails(long StudentId)
        {
            List<StudentBasicClassroomDetails> lsStudentBasicClassroomDetails = null;
            try
            {
                DataSet ds = await objCPDataService.GetStudentRecentLiveClassJoinAsync(StudentId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    lsStudentBasicClassroomDetails = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new StudentBasicClassroomDetails(
                         dataRow.Field<long>("CLASSROOM_ID"),
                         dataRow.Field<string>("CLASSROOM_NAME"),
                         dataRow.Field<string>("CLASSROOM_DESCRIPTION"),
                         dataRow.Field<DateTime>("ROW_INSERTION_DATETIME").ToString("d MMM yyyy")
                         )).ToList();
                }

            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "RegisterNewStudent", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return lsStudentBasicClassroomDetails;
        }
        public async Task<List<StudentBasicCourseDetails>> GetStudentHomeCoursesDetails(long StudentId)
        {
            List<StudentBasicCourseDetails> lsStudentBasicCourseDetails = null;
            try
            {
                DataSet ds = await objCPDataService.GetStudentRecentCourseJoinAsync(StudentId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    lsStudentBasicCourseDetails = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new StudentBasicCourseDetails(
                         dataRow.Field<long>("COURSE_ID"),
                         dataRow.Field<string>("COURSE_NAME"),
                         dataRow.Field<string>("COURSE_DESCRIPTION"),
                         dataRow.Field<DateTime>("ROW_INSERTION_DATETIME").ToString("d MMM yyyy")
                         )).ToList();
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "RegisterNewStudent", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return lsStudentBasicCourseDetails;
        }
        public async Task<List<SearchInstructorResponseModal>> SearchForInstructorNew(string SearchString, int MaxRowToReturn,int NoOfRowsFetched,long StudentId)
        {
            List<SearchInstructorResponseModal> lsSearchInstructorResponseModal = null;
            try
            {
                DataSet ds = await objCPDataService.GetSearchResultForStudentAsync(SearchString, MaxRowToReturn, NoOfRowsFetched, StudentId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    lsSearchInstructorResponseModal = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new SearchInstructorResponseModal(
                         dataRow.Field<int>("ID"),
                         dataRow.Field<string>("INSTRUCTOR_FIRST_NAME"),
                         dataRow.Field<string>("INSTRUCTOR_LAST_NAME"),
                         dataRow.Field<DateTime>("ROW_INSERTION_DATETIME").ToString("d MMM yyyy"),
                          dataRow.Field<int>("NO_OF_COURSE_CREATED"),
                          dataRow.Field<int>("NO_OF_STUDENTS_JOINED"),
                          dataRow.Field<string>("PROFILE_URL")
                         )).ToList();
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "RegisterNewStudent", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return lsSearchInstructorResponseModal;
        }
        public async Task<List<StudentDetailToFolllow>> GetAllStudentsToJoin(GetStudentsToFollowRequest getStudentsToFollowRequest)
        {
            List<StudentDetailToFolllow> lsStudentDetailToFolllow = null;
            try
            {
                DataSet ds = await objCPDataService.GetAllStudentsToFollowAsync(getStudentsToFollowRequest.m_llStudentId,
                    getStudentsToFollowRequest.m_iNoOfRowsFetched, getStudentsToFollowRequest.m_iNoOfRowsToBeFetched, getStudentsToFollowRequest.m_strSearchString);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    lsStudentDetailToFolllow = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new StudentDetailToFolllow(
                         dataRow.Field<long>("STUDENT_ID"),
                         dataRow.Field<string>("STUDENT_NAME"),
                         dataRow.Field<string>("PROFILE_URL"),
                         dataRow.Field<int>("NO_OF_FOLLOWERS"),
                         dataRow.Field<DateTime>("JOINING_DATE").ToString("d MMM yyyy")
                         )).ToList();
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "RegisterNewStudent", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return lsStudentDetailToFolllow;
        }
        public async Task<StudentPublicProfileResponse> GetStudentPublicProfileResponse(long StudentId)
        {
            StudentPublicProfileResponse studentPublicProfileResponse = null;
            try
            {
                DataSet ds = await objCPDataService.GetStudentPublicProfileDetailsAsync(StudentId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    studentPublicProfileResponse = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new StudentPublicProfileResponse(
                         dataRow.Field<long>("STUDENT_ID"),
                         dataRow.Field<string>("STUDENT_NAME"),
                         dataRow.Field<DateTime>("JOINING_DATE").ToString("d MMM yyyy"),
                         dataRow.Field<int>("NO_OF_STUDENT_FOLLOWERS"),
                         dataRow.Field<int>("NO_OF_COURSES_JOINED"),
                         dataRow.Field<int>("LIVE_COURSE_JOINED"),
                         dataRow.Field<string>("PROFILE_URL"),
                         dataRow.Field<int>("NO_OF_LIVE_CLASSESS_ATTENDED"),
                         dataRow.Field<int>("NO_OF_INSTRUCTOR_FOLLOWING")
                         )).ToList()[0];
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "RegisterNewStudent", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return studentPublicProfileResponse;
        }
        public async Task<bool> JoinStudentToStudent(long StudentId,long StudentToBeJoinedId)
        {
            bool result = false;
            try
            {
                result = await objCPDataService.JoinNewStudentAsync(StudentId, StudentToBeJoinedId);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "JoinStudentToStudent", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<List<StudentFollowedByStudentDetails>> GetAllStudentsFollwoedByStudent(GetAllStudentFollowedByStudentRequest getAllStudentFollowedByStudentRequest)
        {
            List<StudentFollowedByStudentDetails> lsStudentFollowedByStudentDetails = null;
            try
            {
                DataSet ds = await objCPDataService.GetAllStudentsFollowedByStudentAsync(getAllStudentFollowedByStudentRequest.m_llStudentId,
                    getAllStudentFollowedByStudentRequest.m_iNoOfRowsFetched,
                    getAllStudentFollowedByStudentRequest.m_iNoOfRowsToBeFetched);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    lsStudentFollowedByStudentDetails = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new StudentFollowedByStudentDetails(
                         dataRow.Field<long>("STUDENT_ID"),
                         dataRow.Field<string>("STUDENT_NAME"),
                         dataRow.Field<DateTime>("FOLLOW_START_DATE").ToString("d MMM yyyy"),
                         dataRow.Field<bool?>("IS_BACKED_FOLLOWED"),
                         dataRow.Field<string>("PROFILE_URL")
                         )).ToList();
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetAllStudentsFollwoedByStudent", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return lsStudentFollowedByStudentDetails;
        }
        public async Task<bool> CheckStudentFollowingStudent(long StudentId, long StudentToBeFollowedId)
        {
            bool result = false;
            try
            {
                DataSet ds = await objCPDataService.CheckStudentFollowingStudentAsync(StudentId, StudentToBeFollowedId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    result = true;
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "CheckIsStudentHasJoinedTheCourse", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<StudentToStudentConnectionDetails> FetchStudentToStudentConnectionDetails(long StudentId,long FriendId)
        {
            StudentToStudentConnectionDetails studentPublicProfileResponse = null;
            try
            {
                DataSet ds = await objCPDataService.GetStudentFriendDetailsAsync(StudentId, FriendId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    studentPublicProfileResponse = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new StudentToStudentConnectionDetails(
                         dataRow.Field<long>("STUDENT_ID"),
                         dataRow.Field<string>("STUDENT_NAME"),
                         dataRow.Field<DateTime>("JOINING_DATE").ToString("d MMM yyyy"),
                         dataRow.Field<int>("NO_OF_STUDENT_FOLLOWERS"),
                         dataRow.Field<int>("NO_OF_COURSES_JOINED"),
                         dataRow.Field<int>("LIVE_COURSE_JOINED"),
                         dataRow.Field<string>("PROFILE_URL"),
                         dataRow.Field<int>("NO_OF_LIVE_CLASSESS_ATTENDED"),
                         dataRow.Field<int>("NO_OF_INSTRUCTOR_FOLLOWING"),
                         dataRow.Field<DateTime>("FOLLOW_START_DATE").ToString("d MMM yyyy")
                         )).ToList()[0];
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "RegisterNewStudent", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return studentPublicProfileResponse;
        }
        public async Task<StudentProfileDetails> FetchStudentSelfDetails(long StudentId)
        {
            StudentProfileDetails studentProfileDetails = null;
            try
            {
                DataSet ds = await objCPDataService.GetStudentSelfPublicDetailsAsync(StudentId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    studentProfileDetails = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new StudentProfileDetails(
                         dataRow.Field<string>("FIRST_NAME"),
                         dataRow.Field<string>("LAST_NAME"),
                         dataRow.Field<DateTime>("JOINING_DATE").ToString("d MMM yyyy"),
                         dataRow.Field<string>("ADDRESS_LINE_1"),
                         dataRow.Field<string>("ADDRESS_LINE_2"),
                         dataRow.Field<string>("CITY_NAME"),
                         dataRow.Field<string>("STATE_NAME"),
                         dataRow.Field<string>("PHONE_NO"),
                         dataRow.Field<string>("EMAIL_ADDRESS"),
                         dataRow.Field<int>("NO_OF_INSTRUCTORS_FOLLOWED"),
                         dataRow.Field<int>("NO_OF_STUDENTS_FOLLOWED"),
                         dataRow.Field<int>("NO_OF_STUDENT_FOLLOWING"),
                         dataRow.Field<int>("NO_OF_LIVE_CLASSROOMS_JOINED"),
                         dataRow.Field<int>("NO_OF_LIVE_CLASSES_ATTENDED"),
                         dataRow.Field<string>("PIN_CODE"),
                         dataRow.Field<int>("NO_OF_ASSIGNMENTS_SUBMITTED"),
                         dataRow.Field<int>("NO_OF_TESTS_SUBMITTED"),
                         dataRow.Field<string>("IMAGE_PATH")
                         )).ToList()[0];
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "RegisterNewStudent", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return studentProfileDetails;
        }//
        public async Task<List<StudentLiveClassMeetingDetails>> GetAllLiveClassMeetingDetailsForStudnet(long StudentId,long ClassroomId)
        {
            List<StudentLiveClassMeetingDetails> lsStudentLiveClassMeetingDetails = null;
            try
            {
                DataSet ds = await objCPDataService.GetClassroomAllMeetingDetailsForStudentAsync(StudentId, ClassroomId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    lsStudentLiveClassMeetingDetails = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new StudentLiveClassMeetingDetails(
                         dataRow.Field<long>("MEETING_ID"),
                         dataRow.Field<string>("MEETING_TOPIC"),
                         dataRow.Field<long?>("STUDENT_MEETING_JOIN_ID"),
                         dataRow.Field<DateTime>("ROW_INSERTION_DATETIME").ToString("d MMM yyyy")
                         )).ToList();
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "RegisterNewStudent", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return lsStudentLiveClassMeetingDetails;
        }
        public async Task<List<StudentLiveClassMeetingDetails>> GetAllTrialLiveClassMeetingDetailsForStudnet(long StudentId, long ClassroomId)
        {
            List<StudentLiveClassMeetingDetails> lsStudentLiveClassMeetingDetails = null;
            try
            {
                DataSet ds = await objCPDataService.GetAllTrialClassroomMeetingDetailsForStudentAsync(StudentId, ClassroomId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    lsStudentLiveClassMeetingDetails = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new StudentLiveClassMeetingDetails(
                         dataRow.Field<long>("MEETING_ID"),
                         dataRow.Field<string>("MEETING_TOPIC"),
                         dataRow.Field<long?>("STUDENT_MEETING_JOIN_ID"),
                         dataRow.Field<DateTime>("ROW_INSERTION_DATETIME").ToString("d MMM yyyy")
                         )).ToList();
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetAllTrialLiveClassMeetingDetailsForStudnet", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return lsStudentLiveClassMeetingDetails;
        }
        public async Task<StudentLiveClassMeetingDetails> GetLiveClassMeetingDetailsForStudnet(GetClassroomMeetingDetailsForStudentRequest getClassroomMeetingDetailsForStudentRequest)
        {
            StudentLiveClassMeetingDetails studentLiveClassMeetingDetails = null;
            try
            {
                DataSet ds = await objCPDataService.GetClassroomMeetingDetailsForStudentAsync(getClassroomMeetingDetailsForStudentRequest.m_llStudentId,getClassroomMeetingDetailsForStudentRequest.m_llMeetingId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    studentLiveClassMeetingDetails = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new StudentLiveClassMeetingDetails(
                         dataRow.Field<long>("MEETING_ID"),
                         dataRow.Field<string>("MEETING_TOPIC"),
                         dataRow.Field<long?>("STUDENT_MEETING_JOIN_ID"),
                         dataRow.Field<string>("MEETING_DESCRIPTION"),
                         dataRow.Field<string>("VIDEO_URL"),
                         dataRow.Field<DateTime>("ROW_INSERTION_DATETIME").ToString("d MMM yyyy")
                         )).ToList()[0];
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "RegisterNewStudent", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return studentLiveClassMeetingDetails;
        }
        public async Task<ClassroomSyllabusDetailsModal> GetClassroomSyllabus(long ClassroomId)
        {
            ClassroomSyllabusDetailsModal classroomSyllabusDetailsModal = null;
            try
            {
                DataSet ds = await objCPDataService.GetClassroomSyllabusAsync(ClassroomId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    classroomSyllabusDetailsModal = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new ClassroomSyllabusDetailsModal(
                         dataRow.Field<string>("CLASSROOM_SYLLABUS")
                         )).ToList()[0];
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "UpdateClassroomSyllabus", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return classroomSyllabusDetailsModal;
        }
        public async Task<ClassroomScheduleDTO> GetClassroomScheduleDetails(long ClassroomId)
        {
            ClassroomScheduleDTO classroomScheduleDTO = null;
            try
            {
                DataSet ds = await objCPDataService.GetClassroomScheduleAsync(ClassroomId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    classroomScheduleDTO = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new ClassroomScheduleDTO(
                         dataRow.Field<string>("CLASSROOM_SCHEDULE_OBJ"),
                         dataRow.Field<DateTime?>("CLASSROOM_SCHEDULE_LAST_UPDATION_TIME"),
                         dataRow.Field<DateTime?>("CLASSROOM_SCHEDULE_SET_TIME")
                         )).ToList()[0];
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "UpdateInstructorProfilePicture", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return classroomScheduleDTO;
        }
        public async Task<bool> InsertClassroomFeedback(ClassroomFeedbackRequest classroomFeedbackRequest)
        {
            bool result = false;
            try
            {

                result = await objCPDataService.InsertOrUpdateClassroomFeedbackByStudentAsync(
                    classroomFeedbackRequest.m_llClassroomId, classroomFeedbackRequest.m_llStudentId,
                    classroomFeedbackRequest.m_strFeedbackMessage, classroomFeedbackRequest.m_iNoOfRatings);
                

            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "CheckIsStudentHasJoinedTheCourse", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<List<CouponDetailsModal>> GetAllCoupons()
        {
            List<CouponDetailsModal> m_lsCouponDetailsModal = null;
            try
            {
                DataSet ds = await objCPDataService.GetAllCouponsAsync();
                m_lsCouponDetailsModal = ds.Tables[0].AsEnumerable().Select(
                    dataRow => new CouponDetailsModal(
                        dataRow.Field<string>("COUPON_CODE"),
                        dataRow.Field<int>("COUPON_DISCOUNT_PERCENTAGE")
                        )).ToList();
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "CheckIsStudentHasJoinedTheCourse", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return m_lsCouponDetailsModal;
        }
        public async Task<bool> RegisterNewStudentViaEmail(StudentRegisterModal objStudentDetailsModal)
        {
            bool result = false;
            try
            {
                result = await objCPDataService.RegisterNewStudentViaGmailAsync(
                   objStudentDetailsModal.m_strGmailId, objStudentDetailsModal.m_strFirstName, objStudentDetailsModal.m_strLastName,
                               objStudentDetailsModal.m_strEmail, objStudentDetailsModal.m_strPhoneNo,
                               objStudentDetailsModal.m_strPhoneNoVarificationGuid, objStudentDetailsModal.m_strProfileUrl);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "RegisterNewStudent", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<StudentRegisterModal> CheckGmailUserAlreadyExists(StudentRegisterModal user)
        {
            StudentRegisterModal result = null;
            try
            {
                DataSet ds = await objCPDataService.CheckGmailUserAlreadyExistsAsync(user.m_strGmailId, user.m_strUserId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    result = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new StudentRegisterModal(
                         dataRow.Field<long>("STUDENT_ID"),
                         dataRow.Field<bool>("IS_PHONE_NO_VERIFIED"),
                         dataRow.Field<string>("PROFILE_URL"),
                         dataRow.Field<string>("PHONE_NO"), 
                         dataRow.Field<string>("PHONE_NO_VERIFICATION_LINK_GUID")
                         )).ToList()[0];
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "SearchForCourse", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<bool> InsertOtpToVarifyAccount(string Otp,long StudentId)
        {
            bool result = false;
            try
            {
                result = await objCPDataService.InsertOtpToVarifyPhoneNoOfStudentAsync(StudentId, Otp);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "RegisterNewStudent", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<bool> VarifyPhoneNo(string Otp, string StudentUserId,string PhoneNoVarificationGuid)
        {
            bool result = false;
            try
            {
                result = await objCPDataService.VarifyStudentPhoneNoAsync(StudentUserId, Otp, PhoneNoVarificationGuid);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "RegisterNewStudent", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<bool> UpdatePhoneNoOfGmailRegStudentAsync(string PhoneNo, string StudentUserId, string PhoneNoVarificationGuid)
        {
            bool result = false;
            try
            {
                result = await objCPDataService.UpdatePhoneNoOfGmailRegStudentAsync(StudentUserId, PhoneNoVarificationGuid, PhoneNo);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "RegisterNewStudent", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<List<StudentsJoinedToClassroomDetailsForStudent>> GetStudentsJoinedToClassroom(StudentsJoinedToClassroomRequestForStudent studentsJoinedToClassroomRequestForStudent)
        {
            List<StudentsJoinedToClassroomDetailsForStudent> lsStudentsJoinedToClassroomDetailsForStudent = new List<StudentsJoinedToClassroomDetailsForStudent>();
            try
            {
                DataSet ds = await objCPDataService.GetStudentsJoinedToClassroomForStudentAsync(studentsJoinedToClassroomRequestForStudent.m_llClassroomId,
                    studentsJoinedToClassroomRequestForStudent.m_llStudentId, studentsJoinedToClassroomRequestForStudent.m_iMaxRowsToBeFetched,
                    studentsJoinedToClassroomRequestForStudent.m_iNoOfRowsFetched);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    lsStudentsJoinedToClassroomDetailsForStudent = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new StudentsJoinedToClassroomDetailsForStudent(
                         dataRow.Field<long>("STUDENT_ID"),
                         dataRow.Field<string>("STUDENT_NAME"),
                         dataRow.Field<DateTime>("DATE_OF_JOINING").ToString("d MMM yyyy"),
                         dataRow.Field<int>("NO_OF_STUDENTS_FOLLOWERS"),
                         dataRow.Field<int>("NO_OF_STUDENTS_HE_FOLLOWING"),
                         dataRow.Field<string>("PROFILE_URL"),
                         dataRow.Field<DateTime?>("DATE_OF_FOLLOWING")
                         )).ToList();
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetAllClassroomForIsntrcutor", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return lsStudentsJoinedToClassroomDetailsForStudent;
        }
        public async Task<List<RatingNormal>> GetAvgClassroomRating(long ClassroomId)
        {
            List<RatingNormal> lsRatings = new List<RatingNormal>();
            try
            {
                DataSet ds = await objCPDataService.GetAvgRatingForClassroomAsync(ClassroomId);
                    
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    lsRatings = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new RatingNormal(
                         dataRow.Field<int>("CLASSROOM_CONTENT_RATING"),
                         dataRow.Field<int>("NO_OF_RATINGS")
                         )).ToList();
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetAllClassroomForIsntrcutor", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return lsRatings;
        }
        public async Task<List<ReviewModel>> GetAllClassroomReviews(long ClassroomId,int NoOfRowsFetched,int MaxRowsToBeFetched)
        {
            List<ReviewModel> lsReviews = new List<ReviewModel>();
            try
            {
                DataSet ds = await objCPDataService.GetAllClassroomReviewsAsync(ClassroomId, MaxRowsToBeFetched,NoOfRowsFetched);

                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    lsReviews = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new ReviewModel(
                         dataRow.Field<int>("NO_OF_RATINGS"),
                         dataRow.Field<string>("PROFILE_URL"),
                         dataRow.Field<long>("STUDENT_ID"),
                         dataRow.Field<string>("STUDENT_NAME"),
                         dataRow.Field<string>("FEEDBACK_MESSAGE"),
                         dataRow.Field<DateTime?>("FEEDBACK_DATE")
                         )).ToList();
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetAllClassroomForIsntrcutor", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return lsReviews;
        }
    }
}