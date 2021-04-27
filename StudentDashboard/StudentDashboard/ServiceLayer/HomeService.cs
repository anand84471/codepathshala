using Newtonsoft.Json;
using StudentDashboard.BusinessLayer;
using StudentDashboard.DTO;
using StudentDashboard.HttpRequest;
using StudentDashboard.HttpResponse;
using StudentDashboard.HttpResponse.ClassRoom;
using StudentDashboard.JsonSerializableObject;
using StudentDashboard.Models;
using StudentDashboard.Models.Alert;
using StudentDashboard.Models.Classroom;
using StudentDashboard.Models.Course;
using StudentDashboard.Models.Files;
using StudentDashboard.Models.Instructor;
using StudentDashboard.Models.Student;
using StudentDashboard.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using static StudentDashboard.Constants;

namespace StudentDashboard.ServiceLayer
{
    public class HomeService
    {
        HomeDTO objHomeDTO;
        StringBuilder m_strLogMessage;
        ActivityManager objActivityManager;
        InstructorBusinessLayer objInstructorBusinessLayer;
       
        public HomeService()
        {
            objHomeDTO = new HomeDTO();
            objActivityManager = new ActivityManager();
            objInstructorBusinessLayer = new InstructorBusinessLayer();
            m_strLogMessage = new StringBuilder();
           
        }
        public JitsiMeetingModal GetMeetingObject()
        {
            JitsiMeetingModal result = new JitsiMeetingModal();
            try
            {

                result.m_strMeetingName = objInstructorBusinessLayer.GetRandomMeetingName();
                result.m_strMeetingPassword = objInstructorBusinessLayer.GetRandomMeetingPassword();
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "RegisterNewUser", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<bool> RegisterNewUser(StaeModel objRegisterModel )
        {
            bool result = false;
            try
            {
                string EncryptedPassword=SHA256Encryption.ComputeSha256Hash(objRegisterModel.strPassword);
                objRegisterModel.strPassword = EncryptedPassword;
                result =await objHomeDTO.RegisterNewUser(objRegisterModel);
            }
            catch(Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "RegisterNewUser", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
            
        }
        public async Task<bool> ValidateLoginDetails(StaeModel objRegisterModel)
        {
            bool result = false;
            try
            {
                objRegisterModel.strPassword = SHA256Encryption.ComputeSha256Hash(objRegisterModel.strPassword);
                result = await objHomeDTO.ValidateLogineDetails(objRegisterModel);
                if(result==false)
                {
                    m_strLogMessage.AppendFormat("Invalid login attemt userId={0}", objRegisterModel.strEmail);
                    MainLogger.Error(m_strLogMessage);
                }

            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "ValidateLoginDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;

        }
        public async Task<List<CourseDetailsModel>> GetAllCourseDetailsForInstructor(int InstructorId)
        {
            List<CourseDetailsModel> lsCouseModel = null;
            try
            {
               lsCouseModel=await objHomeDTO.GetAllCourseDetailsForInstructor(InstructorId);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetAllCourseDetailsForInstructor", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return lsCouseModel;
        }
        public async Task<bool> DeleteCourse(long CourseId)
        {
            bool result = false;
            try
            {
                result = await objHomeDTO.DeleteCourse(CourseId);
            }
            catch(Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "DeleteCourse", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<bool> DeleteMcqAssignmentQuestion(long QuestionId)
        {
            bool result = false;
            try
            {
                result = await objHomeDTO.DeleteMcqAssignmentQuestion(QuestionId);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "DeleteMcqAssignmentQuestion", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task< bool> UpdateMcqAssignmentQuestion(McqQuestion objMcqQuestion)
        {
            bool result = false;
            try
            {
                result = await objHomeDTO.UpdateMcqAssignmentQuestion(objMcqQuestion);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "UpdateMcqAssignmentQuestion", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public  async Task<bool> ActivateCourse(ActivateCourseHttpRequest activateCourseHttpRequest)
        {
            bool result = false;
            try
            {
                string ShareCode = objInstructorBusinessLayer.GetShareCodeForAssignment();
                string TinyUrl = await objInstructorBusinessLayer.GetTinyUrlForCourse(activateCourseHttpRequest.m_llCourseId, ShareCode);
                result = await objHomeDTO.ActivateCourse(activateCourseHttpRequest.m_llCourseId, ShareCode, TinyUrl, activateCourseHttpRequest.m_iCourseFee*100);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "DeleteCourse", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }

        public async Task<GetCourseDetailsApiResponse> GetCourseDetails(long CourseId)
        {
            GetCourseDetailsApiResponse objGetCourseDetailsApiResponse = null;
            try
            {
                objGetCourseDetailsApiResponse = await objHomeDTO.GetCourseDetails(CourseId);
                if(objGetCourseDetailsApiResponse!=null)
                {
                    objGetCourseDetailsApiResponse.m_lsIndexes = await objHomeDTO.GetCourseIndexDetails(CourseId);
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetCourseDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objGetCourseDetailsApiResponse;
        }
        public async Task<TestModel> GetTestDetails(long TestId)
        {
            TestModel objTestModel = null;
            try
            {
                objTestModel = await objHomeDTO.GetTestDetails(TestId);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetTestDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objTestModel;
        }
        public bool GetCourseDetails(int Id)
        {
            bool result = false;
            try
            {

            }
            catch(Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetCourseDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<bool> InsertNewTopic(TopicModel objTopicModel)
        {
            bool result = false;
            try
            {
                result = await objHomeDTO.InsertNewTopic(objTopicModel);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetCourseDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<bool> InsertTopics(IndexModel objIndexModel)
        {
            bool result = false;
            int SuccessFullyInsertedTopics = 0;
            try
            {
                foreach (var objTopicModel in objIndexModel.m_lsTopicModel)
                {
                    objTopicModel.m_llIndexId = objIndexModel.m_llIndexId;
                    if(await objHomeDTO.InsertNewTopic(objTopicModel))
                    {
                        SuccessFullyInsertedTopics++;
                    }
                }
                if(SuccessFullyInsertedTopics== objIndexModel.m_lsTopicModel.Count)
                {
                    result = true;
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "InsertTopics", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<AboutCourseResponse> GetAboutCourse(long CourseId)
        {
            AboutCourseResponse objAboutCourseResponse = new AboutCourseResponse();
            try
            {
                GetCourseDetailsApiResponse objGetCourseDetailsApiResponse=await objHomeDTO.GetCourseDetails(CourseId);
                List<CourseIndexDetails> lsCourseIndexDetails= await objHomeDTO.GetCourseIndexDetails(CourseId);
                if(lsCourseIndexDetails!=null&& objGetCourseDetailsApiResponse!=null)
                {
                    objAboutCourseResponse = new AboutCourseResponse(objGetCourseDetailsApiResponse.m_strCourseName,objGetCourseDetailsApiResponse.m_strCourseDescription,
                                        objGetCourseDetailsApiResponse.m_strCourseCreationDate,objGetCourseDetailsApiResponse.m_strCourseUpdationDate,
                                        objGetCourseDetailsApiResponse.m_strCourseStatus,objGetCourseDetailsApiResponse.m_strShareUrl,
                                        objGetCourseDetailsApiResponse.m_strCourseAccessCode, objGetCourseDetailsApiResponse.m_iCourseJoiningFee);
                    foreach (var indexes in lsCourseIndexDetails)
                    {
                        objAboutCourseResponse.AddIndex(indexes.m_strIndexName, indexes.m_llIndexId);
                        if (indexes.m_llAssignmentId != null) { objAboutCourseResponse.AddAssignment(indexes.m_strAssignmentName, indexes.m_llAssignmentId); }
                        if (indexes.m_llTestId != null) { objAboutCourseResponse.AddTest(indexes.m_strTestName, indexes.m_llTestId); }
                        objAboutCourseResponse.IncremetTopicCount(indexes.m_iTotalNoOfTopic);
                    }
                    List<BasicAssignmentDetails> lsAssignmentDetails =await  GetAssignmentForCourse(CourseId);
                    if (lsAssignmentDetails != null && lsAssignmentDetails.Count > 0) { objAboutCourseResponse.m_lsAssignmentDetails.AddRange(lsAssignmentDetails); }
                    List<BasicTestDetails> lsBasicTestDetails = await GetTestOfCourse(CourseId);
                    if (lsBasicTestDetails != null && lsBasicTestDetails.Count > 0) { objAboutCourseResponse.m_lsTestDetails.AddRange(lsBasicTestDetails); }
                    objAboutCourseResponse.SetCounts();
                    objAboutCourseResponse.m_iResponseCode = Constants.API_RESPONSE_CODE_SUCCESS;
                    objAboutCourseResponse.m_strResponseMessage = Constants.API_RESPONSE_MESSAGE_SUCCESS;
                }
                
                
            }
            catch(Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "InsertTopics", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objAboutCourseResponse;
        }
       
        public async Task<IndexModel> GetIndexDetails(int id)
        {
            IndexModel objIndexModel = null;
            try
            {
                objIndexModel = await objHomeDTO.GetIndexDetails(id);
                if(objIndexModel!=null)
                {
                    objIndexModel.m_iResponseCode = Constants.API_RESPONSE_CODE_SUCCESS;
                    objIndexModel.m_strResponseMessage = Constants.API_RESPONSE_MESSAGE_SUCCESS;
                    objIndexModel.m_lsTopicModel = await objHomeDTO.GetIndexTopicDetails(id);
                }
                else
                {
                    objIndexModel.m_iResponseCode = Constants.API_RESPONSE_CODE_FAIL;
                    objIndexModel.m_strResponseMessage = Constants.API_RESPONSE_MESSAGE_FAIL;
                }
            }
            catch(Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "InsertAssignment", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objIndexModel;
        }
        public async Task<StudentIndexModal> GetIndexProgressForStudent(long IndexId,long StudentId)
        {
            StudentIndexModal objIndexModel = null;
            try
            {
                objIndexModel = await objHomeDTO.GetStudentIndexDetails(IndexId);
                if (objIndexModel != null)
                {
                    objIndexModel.m_iResponseCode = Constants.API_RESPONSE_CODE_SUCCESS;
                    objIndexModel.m_strResponseMessage = Constants.API_RESPONSE_MESSAGE_SUCCESS;
                    objIndexModel.m_lsTopicModel = await objHomeDTO.FetchTopicProgressForStudent(IndexId, StudentId);
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "InsertAssignment", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objIndexModel;
        }
        public async Task<AssignmentModel> GetAssignmentDetails(long AssignmentId)
        {
            AssignmentModel objAssignmentModel = null;
            try
            {
                objAssignmentModel = await objHomeDTO.GetAssignmentDetails(AssignmentId);
                if (objAssignmentModel != null)
                {
                   switch(objAssignmentModel.m_iAssignmentType)
                    {
                        case (int)Constants.AssignmentQuestionType.MCQ:
                            {
                                objAssignmentModel.m_lsMcqQuestion =await  objHomeDTO.GetMcqQuestionDetails(AssignmentId);
                                break;
                            }
                        case (int)Constants.AssignmentQuestionType.SUBJECTIVE:
                            {
                                objAssignmentModel.m_lsSubjectiveQuestion = await objHomeDTO.GetAllQuestionsOfSubjectiveAssignment(AssignmentId);
                                break;
                            }
                    }
                    objAssignmentModel.SetRemaningValues();
                }
                else
                {
                  
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "InsertAssignment", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objAssignmentModel;
        }
        public async Task<AssignmentModel> GetIndependentAssignmentDetails(long AssignmentId)
        {
            AssignmentModel objAssignmentModel = null;
            try
            {
                objAssignmentModel = await objHomeDTO.GetIndependentAssignmentDetails(AssignmentId);
                if (objAssignmentModel != null)
                {
                    switch (objAssignmentModel.m_iAssignmentType)
                    {
                        case (int)Constants.AssignmentQuestionType.MCQ:
                            {
                                objAssignmentModel.m_lsMcqQuestion = await objHomeDTO.GetMcqQuestionDetails(AssignmentId);
                                break;
                            }
                        case (int)Constants.AssignmentQuestionType.SUBJECTIVE:
                            {
                                objAssignmentModel.m_lsSubjectiveQuestion = await objHomeDTO.GetAllQuestionsOfSubjectiveAssignment(AssignmentId);
                                break;
                            }
                    }
                    objAssignmentModel.SetRemaningValues();
                }
                else
                {

                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "InsertAssignment", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objAssignmentModel;
        }
        public async Task<AssignmentModel> GetIndependentAssignmentDetailsWithoutQuestion(long AssignmentId)
        {
            AssignmentModel objAssignmentModel = null;
            try
            {
                objAssignmentModel = await objHomeDTO.GetIndependentAssignmentDetails(AssignmentId);
                
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "InsertAssignment", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objAssignmentModel;
        }
        public async Task<TestModel> GetIndependentTestDetails(long TestId)
        {
            TestModel objTestModel = null;
            try
            {
                objTestModel = await objHomeDTO.GetIndependentTestDetails(TestId);
                if (objTestModel != null)
                {
                    objTestModel.m_lsMcqQuestion = await objHomeDTO.GetMcqTestQuestions(TestId);
                }
                else
                {

                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetIndependentTestDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objTestModel;
        }
        public async Task<TestModel> GetFullMcqTestDetails(long TestId)
        {
            TestModel objTestModel = null;
            try
            {
                objTestModel = await objHomeDTO.GetTestDetails(TestId);
                if (objTestModel != null)
                {
                    objTestModel.m_lsMcqQuestion = await objHomeDTO.GetMcqTestQuestions(TestId);  
                }
                else
                {

                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetFullTestDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objTestModel;
        }
        public async Task<StudentTestProgressModal> GetStudentTestProgress(long TestId,long StudentId)
        {
            StudentTestProgressModal objTestModel = null;
            try
            {
                objTestModel = await objHomeDTO.GetStudentTestProgress(TestId, StudentId);
                
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetStudentTestProgress", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objTestModel;
        }

        public async Task<StudentAssignmentProgressModal> FetchStudentAssignmentProgress(long StudentId,long AssignmentId)
        {
            StudentAssignmentProgressModal objAssignmentModal = null;
            try
            {
                objAssignmentModal = await objHomeDTO.GetStudentAssignmentProgress(AssignmentId, StudentId);
               
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetFullTestDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objAssignmentModal;
        }

        public async Task<bool> InsertNewMcqQuestion(McqQuestion objMcqQuestion)
        {
            return await objHomeDTO.InsertNewMcqTestQuestion(objMcqQuestion);
        }
        public async Task<bool> CheckAssignmentIdExistsForInstrcutor(int InstructorId, long AssignmentId)
        {
            return await objHomeDTO.CheckAssignmentIdExistsForInstrcutor(InstructorId, AssignmentId)||
                await objHomeDTO.CheckAssignmentIdExistsForAnyCourseForInstrcutor(InstructorId, AssignmentId);
        }
        public async Task<bool> CheckTestIdExistsForInstrcutor(int InstructorId, long TestId)
        {
            return await objHomeDTO.CheckTestIdExistsForInstrcutor(InstructorId, TestId)||await objHomeDTO.CheckTestIdExistsForAnyCourseForInstrcutor(InstructorId, TestId);
        }
       
        public async Task<bool> InsertNewMcqAssignmentQuestion(McqQuestion objMcqQuestion)
        {
            return await objHomeDTO.InsertNewMcqAssignmentQuestion(objMcqQuestion);
        }
        public async Task<bool> InsertAssignment(AssignmentModel objAssignmentmodel)
        {
            bool result = false;
            try {
                if (objAssignmentmodel.m_strAssignmentType.Equals(Constants.ASSIGNMENT_TYPE_MCQ))
                {
                    objAssignmentmodel.m_iAssignmentType = (short)Constants.AssignmentQuestionType.MCQ;
                }
                else if (objAssignmentmodel.m_strAssignmentType.Equals(Constants.ASSIGNMENT_TYPE_SUBJECTIVE))
                {
                    objAssignmentmodel.m_iAssignmentType = (short)Constants.AssignmentQuestionType.SUBJECTIVE;
                }
                if (objHomeDTO.InsertNewAssignment(objAssignmentmodel))
                {
                    if (await objHomeDTO.InsertAssignmentIdToIndex(objAssignmentmodel.m_llAssignemntId, objAssignmentmodel.m_llIndexId))
                    {
                        if (objAssignmentmodel.m_iAssignmentType == (short)Constants.AssignmentQuestionType.MCQ)
                        {
                            foreach (var Questions in objAssignmentmodel.m_lsMcqQuestion)
                            {
                                Questions.m_llAssignmentId = objAssignmentmodel.m_llAssignemntId;
                                result =await objHomeDTO.InsertNewMcqAssignmentQuestion(Questions);
                            }
                        }
                        else if (objAssignmentmodel.m_iAssignmentType == (short)Constants.AssignmentQuestionType.SUBJECTIVE)
                        {
                            foreach (var Questions in objAssignmentmodel.m_lsSubjectiveQuestion)
                            {
                                Questions.m_llAsssignmentId = objAssignmentmodel.m_llAssignemntId;
                                result = await objHomeDTO.InsertSubjectiveAssignmentQuestion(Questions);
                            }
                        }
                    }
                }
            }
            catch(Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "InsertAssignment", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<bool> InsertNewIndependentAssignment(AssignmentModel objAssignmentmodel)
        {
            bool result = false;
            try
            {
                if (objAssignmentmodel.m_strAssignmentType.Equals(Constants.ASSIGNMENT_TYPE_MCQ))
                {
                    objAssignmentmodel.m_iAssignmentType = (short)Constants.AssignmentQuestionType.MCQ;
                }
                else if (objAssignmentmodel.m_strAssignmentType.Equals(Constants.ASSIGNMENT_TYPE_SUBJECTIVE))
                {
                    objAssignmentmodel.m_iAssignmentType = (short)Constants.AssignmentQuestionType.SUBJECTIVE;
                }
                if (objHomeDTO.InsertNewIndependentAssignment(objAssignmentmodel))
                {
                    if (objAssignmentmodel.m_iAssignmentType == (short)Constants.AssignmentQuestionType.MCQ)
                    {
                        foreach (var Questions in objAssignmentmodel.m_lsMcqQuestion)
                        {
                            Questions.m_llAssignmentId = objAssignmentmodel.m_llAssignemntId;
                            result =await objHomeDTO.InsertNewMcqAssignmentQuestion(Questions);
                        }
                    }
                    else if (objAssignmentmodel.m_iAssignmentType == (short)Constants.AssignmentQuestionType.SUBJECTIVE)
                    {
                        foreach (var Questions in objAssignmentmodel.m_lsSubjectiveQuestion)
                        {
                            Questions.m_llAsssignmentId = objAssignmentmodel.m_llAssignemntId;
                            result = await objHomeDTO.InsertSubjectiveAssignmentQuestion(Questions);
                        }
                    }
                }
                if(result)
                {
                    if (objAssignmentmodel.m_bIsClassroomAssignment)
                    {
                        result=await InsertnewAssignmentToClassroom(objAssignmentmodel.m_llAssignemntId,objAssignmentmodel.m_llClassroomId);
                    }
                    else
                    {
                        result=await InsertActivityForInstructor(objAssignmentmodel.m_iInstructorId, objActivityManager.CreateActivityMessageForinstructor(objAssignmentmodel.m_llAssignemntId, objAssignmentmodel.m_strAssignmentName, (int)Constants.ActivityType.ASSIGNMENT_CREATED));
                    }
                    
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "InsertNewIndependentAssignment", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<bool> InsertTest(TestModel objTestmodel)
        {
            bool result = false;
            try
            {
                if (objTestmodel.m_strTestType.Equals(Constants.TEST_TYPE_MCQ))
                {
                    objTestmodel.m_iTestType = (short)Constants.TestQuestionType.MCQ;
                }
                else if (objTestmodel.m_strTestType.Equals(Constants.TEST_TYPE_SUBJECTIVE))
                {
                    objTestmodel.m_iTestType = (short)Constants.TestQuestionType.SUBJECTIVE;
                }
                if (objHomeDTO.InsertNewTest(objTestmodel))
                {
                    if (await objHomeDTO.InsertTestIdToIndex(objTestmodel.m_llTestId, objTestmodel.m_llIndexId))
                    {
                        if (objTestmodel.m_iTestType == (short)Constants.TestQuestionType.MCQ)
                        {
                            foreach (var Questions in objTestmodel.m_lsMcqQuestion)
                            {
                                Questions.m_llTestId = objTestmodel.m_llTestId;
                                result = await objHomeDTO.InsertNewMcqTestQuestion(Questions);
                            }
                        }
                        else if (objTestmodel.m_iTestType == (short)Constants.TestQuestionType.SUBJECTIVE)
                        {
                            foreach (var Questions in objTestmodel.m_lsMcqQuestion)
                            {

                            }
                        }

                    }
                }
                if(result)
                {
                    await InsertActivityForInstructor(objTestmodel.m_iInstructorId, objActivityManager.CreateActivityMessageForinstructor(objTestmodel.m_llTestId,objTestmodel.m_strTestName,(int)Constants.ActivityType.TEST_CREATED));
                }
            }
            catch(Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "InsertAssignment", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task< bool> InsertNewIndependentTest(TestModel objTestmodel)
        {
            bool result = false;
            try
            {
                if (objTestmodel.m_strTestType.Equals(Constants.TEST_TYPE_MCQ))
                {
                    objTestmodel.m_iTestType = (short)Constants.TestQuestionType.MCQ;
                }
                else if (objTestmodel.m_strTestType.Equals(Constants.TEST_TYPE_SUBJECTIVE))
                {
                    objTestmodel.m_iTestType = (short)Constants.TestQuestionType.SUBJECTIVE;
                }
                if (objHomeDTO.InsertNewIndependentTest(objTestmodel))
                {
                  
                        if (objTestmodel.m_iTestType == (short)Constants.TestQuestionType.MCQ)
                        {
                            foreach (var Questions in objTestmodel.m_lsMcqQuestion)
                            {
                                Questions.m_llTestId = objTestmodel.m_llTestId;
                                result = await objHomeDTO.InsertNewMcqTestQuestion(Questions);
                            }
                        }
                        else if (objTestmodel.m_iTestType == (short)Constants.TestQuestionType.SUBJECTIVE)
                        {
                            foreach (var Questions in objTestmodel.m_lsMcqQuestion)
                            {

                            }
                        }
                    if (objTestmodel.m_bIsClassroomAccess)
                    {
                        result = await objHomeDTO.InsertnewTestToClassroom(objTestmodel.m_llTestId, objTestmodel.m_llClassroomId);
                    }
                    
                }
                if(result)
                {
                    await InsertActivityForInstructor(objTestmodel.m_iInstructorId, objActivityManager.CreateActivityMessageForinstructor(objTestmodel.m_llTestId, objTestmodel.m_strTestName, (int)Constants.ActivityType.TEST_CREATED));
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "InsertAssignment", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }

        public async Task<List<AssignmentDetailsModel>> GetAssignmentForInstructor(int InstructorId)
        {
            return await objHomeDTO.GetAssignmentForInstructor(InstructorId);
        }
        public async Task<List<TestDetailsModel>> GetInstructorTestDetails(int InstructorId)
        {
            return await objHomeDTO.GetInstructorTestDetails(InstructorId);
        }
        public async Task<bool> InsertActivityForInstructor(int InstructorId, string ActivityMessage)
        {
            return await objHomeDTO.InsertActivityForInstructor(InstructorId, ActivityMessage);
        }
        public async Task<bool> InsertNewCourse(CourseModel objCourseModel)
        {
            bool result = objHomeDTO.InsertNewCourse(objCourseModel);
            if(result)
            {
                 await InsertActivityForInstructor(objCourseModel.m_iInstructorId, objActivityManager.CreateActivityMessageForinstructor(objCourseModel.m_llCourseId, objCourseModel.m_strCourseName, (int)Constants.ActivityType.COURSE_CREATED));
                
            }
            return result;
        }
        public async Task<List<ActivityModal>> GetInstructorActivityDetails(int InstructorId)
        {
            List<ActivityModal> lsActivityModal;
            lsActivityModal= await objHomeDTO.GetInstructorActivityDetails(InstructorId);
            if (lsActivityModal != null&& lsActivityModal.Count>0)
            {
                lsActivityModal = lsActivityModal.OrderByDescending((x) => x.m_dtDateTime).ToList();
            }
            return lsActivityModal;
        }
        private List<AssignmentQuestionResponse> ConvertFromJsonObjectToAssignmentResponse(List<AssignmentSubmissionResponseJsonSerializable> lsAssignmentSubmissionResponseJsonSerializable)
        {
            List<AssignmentQuestionResponse> lsAssignmentQuestionResponse = new List<AssignmentQuestionResponse>();
            AssignmentQuestionResponse objAssignmentQuestionResponse;
            foreach (var obj in lsAssignmentSubmissionResponseJsonSerializable)
            {
                objAssignmentQuestionResponse = new AssignmentQuestionResponse();
                objAssignmentQuestionResponse.m_CorrectOption = obj.m_CorrectOption;
                objAssignmentQuestionResponse.m_iOptionSelected = obj.m_iOptionSelected;
                objAssignmentQuestionResponse.m_strOption1 = obj.m_strOption1;
                objAssignmentQuestionResponse.m_strOption2 = obj.m_strOption2;
                objAssignmentQuestionResponse.m_strOption3 = obj.m_strOption3;
                objAssignmentQuestionResponse.m_strOption4 = obj.m_strOption4;
                objAssignmentQuestionResponse.m_strQuestionStatement = obj.m_strQuestionStatement;
                objAssignmentQuestionResponse.m_llQuestionId = obj.m_llQuestionId;
                lsAssignmentQuestionResponse.Add(objAssignmentQuestionResponse);
            }
            return lsAssignmentQuestionResponse;
        }
        public async Task<GetAssignmentSubssionDetials> GetAssignmentResponse(long SubmissionId, long StudentId)
        {
            GetAssignmentSubssionDetials objGetAssignmentSubssionDetials = null;
            try
            {
                objGetAssignmentSubssionDetials = await objHomeDTO.GetAssignmentResponse(SubmissionId, StudentId);
                if (objGetAssignmentSubssionDetials != null)
                {
                    List<AssignmentSubmissionResponseJsonSerializable> lsAssignmentSubmissionResponseJsonSerializable = JsonConvert.DeserializeObject<List<AssignmentSubmissionResponseJsonSerializable>>(objGetAssignmentSubssionDetials.m_strResponse);

                    objGetAssignmentSubssionDetials.m_lsAssignmentQuestionResponse = ConvertFromJsonObjectToAssignmentResponse(lsAssignmentSubmissionResponseJsonSerializable);
                }

            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetStudentDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objGetAssignmentSubssionDetials;
        }
        private List<TestQuestionResponse> ConvertFromJsonObjectToTestResponse(List<TestSubmissionResponseJsonSerializable> lsAssignmentSubmissionResponseJsonSerializable)
        {
            List<TestQuestionResponse> lsTestQuestionResponse = new List<TestQuestionResponse>();
            TestQuestionResponse objTestQuestionResponse;
            foreach (var obj in lsAssignmentSubmissionResponseJsonSerializable)
            {
                objTestQuestionResponse = new TestQuestionResponse();
                objTestQuestionResponse.m_CorrectOption = obj.m_CorrectOption;
                objTestQuestionResponse.m_iOptionSelected = obj.m_iOptionSelected;
                objTestQuestionResponse.m_strOption1 = obj.m_strOption1;
                objTestQuestionResponse.m_strOption2 = obj.m_strOption2;
                objTestQuestionResponse.m_strOption3 = obj.m_strOption3;
                objTestQuestionResponse.m_strOption4 = obj.m_strOption4;
                objTestQuestionResponse.m_strQuestionStatement = obj.m_strQuestionStatement;
                objTestQuestionResponse.m_llQuestionId = obj.m_llQuestionId;
                objTestQuestionResponse.m_iMarks = obj.m_iMarks;
                objTestQuestionResponse.m_iTimeInSeconds = obj.m_iTimeInSeconds;
                lsTestQuestionResponse.Add(objTestQuestionResponse);
            }
            return lsTestQuestionResponse;
        }
        public async Task<GetTestSubmissionDetailsResponse> GetTestResponse(long SubmissionId, long StudentId)
        {
            GetTestSubmissionDetailsResponse objGetTestSubmissionDetailsResponse = null;
            try
            {
                objGetTestSubmissionDetailsResponse = await objHomeDTO.GetTestResponse(SubmissionId, StudentId);
                if (objGetTestSubmissionDetailsResponse != null)
                {
                    List<TestSubmissionResponseJsonSerializable> lsTestSubmissionResponseJsonSerializable = JsonConvert.DeserializeObject<List<TestSubmissionResponseJsonSerializable>>(objGetTestSubmissionDetailsResponse.m_strResponse);

                    objGetTestSubmissionDetailsResponse.m_lsTestQuestionResponse = ConvertFromJsonObjectToTestResponse(lsTestSubmissionResponseJsonSerializable);
                }

            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetStudentDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objGetTestSubmissionDetailsResponse;
        }
        public async Task<bool> ActivateTest(long TestId)
        {
            string AccessCode = objInstructorBusinessLayer.GetShareCodeForAssignment();
            string TinyUrl = await objInstructorBusinessLayer.GetTinyUrlForTest(TestId, AccessCode);
           
            return await objHomeDTO.ActivateTest(TestId, TinyUrl, AccessCode);
        }
        public async Task<bool> DeleteTest(long TestId)
        {
            return await objHomeDTO.DeleteTest(TestId);
        }
        public async Task<bool> CheckCourseIdExistsForInstrcutor(int InstructorId, long CourseId)
        {
            return await objHomeDTO.CheckCourseIdExistsForInstrcutor(InstructorId, CourseId);
        }
        public async Task<bool> CheckIndexIdExistsForInstrcutor(int InstructorId, long IndexId)
        {
            return await objHomeDTO.CheckIndexIdExistsForInstrcutor(InstructorId, IndexId);
        }
        public async Task<bool> DeleteTestOfCourse(long TestId)
        {
            return await objHomeDTO.DeleteTestOfCourse(TestId);
        }
        public async Task<long> InsertNewClassroom(ClassRoomModal objClassRoomModal)
        {
            objClassRoomModal.m_strClassroomMeetingName = objInstructorBusinessLayer.GetRandomMeetingName();
            return await objHomeDTO.InsertNewClassroom(objClassRoomModal);
        }
        public async Task<bool> InsertNewPostToClassroom(ClassroomPostModal objClassroomPostModal)
        {
            return await objHomeDTO.InsertNewPostToClassroom(objClassroomPostModal);
        }
        public async Task<bool> ActivateAssignment(long AssignmentId)
        {
            string AccessCode = objInstructorBusinessLayer.GetShareCodeForAssignment();
            string TinyUrl = await objInstructorBusinessLayer.GetTinyUrlForAssignment(AssignmentId,AccessCode);
            return await objHomeDTO.ActivateAssignment(AssignmentId, AccessCode, TinyUrl);
        }
        public async Task<bool> DeleteAssignment(long AssignmentId)
        {
            return await objHomeDTO.DeleteAssignment(AssignmentId);
        }
        public async Task< bool> DeleteIndependentAssignment(long AssignmentId)
        {
            return await objHomeDTO.DeleteIndependentAssignment(AssignmentId);
        }
        public async Task<bool> DeleteIndependentTest(long TestId)
        {
            return await objHomeDTO.DeleteIndependentTest(TestId);
        }
        public async Task<bool> InserContatUsRequest(ContactUsApiRequest objContactUsApiRequest)
        {
            return await objHomeDTO.InserContatUsRequest(objContactUsApiRequest);
        }
        public async Task<bool> InsertInstructorContatUsRequest(IntructorContactUsRequest intructorContactUsRequest)
        {
            return await objHomeDTO.InsertInstructorContatUsRequest(intructorContactUsRequest);
        }
        public async Task<bool> DeleteIndexTopic(long TopicId)
        {
            return await objHomeDTO.DeleteIndexTopic(TopicId);
        }
        public async Task<bool> UpdateIndexTopic(TopicModel objTopicModel)
        {
            return await objHomeDTO.UpdateIndexTopic(objTopicModel);
        }
        public async Task<bool> DeleteIndex(long IndexId)
        {
            return await objHomeDTO.DeleteCourseIndex(IndexId);
        }
        public async Task<bool> UpdateAssignmentDetails(AssignmentModel objAssignmentDetailsModel)
        {
            return await objHomeDTO.UpdateAssignmentDetails(objAssignmentDetailsModel);
        }
        public async Task<bool> UpdateCourseIndex(IndexModel objIndexModel)
        {
            return await objHomeDTO.UpdateCourseIndex(objIndexModel);
        }
        public async Task<bool> UpdateFullCourseDetails(CourseDetailsModel objCourse)
        {
            return await objHomeDTO.UpdateFullCourseDetails(objCourse);
        }
        public async Task<bool> UpdateTestDetails(TestDetailsModel objTestDetails)
        {
            return await objHomeDTO.UpdateTestDetails(objTestDetails);
        }
        public async Task<bool> AddMcqTestQuestion(McqQuestion objMcqQuestion)
        {
            return await objHomeDTO.AddMcqTestQuestion(objMcqQuestion);
        }
        public async Task<bool> UpdateMcqTestQuestion(McqQuestion objMcqQuestion)
        {
            return await objHomeDTO.UpdateMcqTestQuestion(objMcqQuestion);
        }
        public async Task<bool> InsertNewSeperateAssignmentToCourse(AssignmentModel objAssignmentModel)
        {
            return await objHomeDTO.InsertNewAssignmentToCourse(objAssignmentModel);
        }
        public async Task<bool> CheckAssignmentIdExistsForAnyCourseForInstrcutor(int InstructorId, long AssignmentId)
        {
            return await objHomeDTO.CheckAssignmentIdExistsForAnyCourseForInstrcutor(InstructorId, AssignmentId);
        }
        public async Task<bool> CheckTestIdExistsForAnyCourseForInstrcutor(int InstructorId, long TestId)
        {
            return await objHomeDTO.CheckTestIdExistsForAnyCourseForInstrcutor(InstructorId, TestId);
        }
        public async Task<bool> DeleteMcqTestQuestion(long QuestionId)
        {
            return await objHomeDTO.DeleteMcqTestQuestion(QuestionId);
        }
        public async Task<bool> InsertNewTestToCourse(TestModel objTestModel)
        {
            return await objHomeDTO.InsertNewTestToCourse(objTestModel);
        }
        public async Task<bool> InsertNewAssignmentToCourse(AssignmentModel objAssignmentModel)
        {
            return await objHomeDTO.InsertNewAssignmentToCourse(objAssignmentModel);
        }
        public async Task<List<BasicAssignmentDetails>> GetAssignmentForCourse(long CourseId)
        {
            return await objHomeDTO.GetAssignmentForCourse(CourseId);
        }
        public async Task<List<BasicTestDetails>> GetTestOfCourse(long CourseId)
        {
            return await objHomeDTO.GetTestOfCourse(CourseId);
        }
        public async Task<bool> InsertSubjectiveAssignmentQuestion(SubjectiveQuestion objSubjectiveQuestion)
        {
            return await objHomeDTO.InsertSubjectiveAssignmentQuestion(objSubjectiveQuestion);
        }
        public async Task<bool> UpdateSubjectiveAssignmentQuestion(SubjectiveQuestion objSubjectiveQuestion)
        {
            return await objHomeDTO.UpdateSubjectiveAssignmentQuestion(objSubjectiveQuestion);
        }
        public async Task<bool> DeleteSubjectiveAssignmentQuestion(long QuestionId)
        {
            return await objHomeDTO.DeleteSubjectiveAssignmentQuestion(QuestionId);
        }
        public async Task<bool> DeleteSubjectiveAssignmentOfCourse(long AssignmentId)
        {
            return await objHomeDTO.DeleteSubjectiveAssignmentOfCourse(AssignmentId);
        }
        public async Task<List<SubjectiveQuestion>> GetAllQuestionsOfSubjectiveAssignment(long AssignmentId)
        {
            return await objHomeDTO.GetAllQuestionsOfSubjectiveAssignment(AssignmentId);
        }
        public async Task<List<AssignmentSubmissionResponseModal>> GetAllSubmissionsOfAnAssignment(long AssignmentId)
        {
            return await objHomeDTO.GetAllSubmissionsOfAnAssignment(AssignmentId);
        }
        public async Task<List<AssignmentSubmissionResponseModal>> GetAllTestSubmissions(long TestId)
        {
            return await objHomeDTO.GetAllTestSubmissions(TestId);
        }
        public async Task<List<CoursesJoinedResponseModal>> GetAllStudentsJoinedToCourse(long CourseId)
        {
            return await objHomeDTO.GetAllStudentsJoinedToCourse(CourseId);
        }
        public async Task<List<CoursesJoinedResponseModal>> GetAllStudentsJoinedToInstructor(int InstructorId,MasterSearchRequest masterSearchRequest)
        {
            return await objHomeDTO.GetAllStudentsJoinedToInstructor(InstructorId, masterSearchRequest);
        }
        public  bool InsertNewCourseV2(InsertCourseV2Request objInsertCourseV2Request)
        {
            return  objHomeDTO.InsertNewCourseV2(objInsertCourseV2Request);
        }
        public async Task<bool> InsertNewAlertForInstructor(int InstructorId, string AlertMessage, int AlertTypeId, long StudentId, long? EffectiveId)
        {
            return await objHomeDTO.InsertNewAlertForInstructor(InstructorId, AlertMessage, AlertTypeId, StudentId, EffectiveId);
        }
        public async Task<List<AlertDetailsModal>> GetAllAlertOfInstructor(int InstructorId)
        {
            return await objHomeDTO.GetAllAlertOfInstructor(InstructorId);
        }
        public bool GetInstructorIdByCourseId(ref int InstructorId, long CourseId)
        {
            return  objHomeDTO.GetInstructorIdByCourseId(ref InstructorId, CourseId);
        }
        public  bool GetInstructorIdByAssignmentId(ref int InstructorId, long AssignmentId)
        {
            return  objHomeDTO.GetInstructorIdByAssignmentId(ref InstructorId, AssignmentId);
        }
        public bool GetInstructorIdByTestId(ref int InstructorId, long TestId)
        {
            return  objHomeDTO.GetInstructorIdByTestId(ref InstructorId, TestId);
        }
        public bool GetInstructorIdByClassroomId(ref int InstructorId, long ClassroomId)
        {
            return objHomeDTO.GetInstructorIdByClassroomId(ref InstructorId, ClassroomId);
        }
        public async Task<List<AssignmentDetailsModel>> SearchForAssignmentOfInstructor(string SearchString, int MaxRowToReturn, int InstructorId)
        {
            return await objHomeDTO.SearchForAssignmentOfInstructor( SearchString,  MaxRowToReturn,  InstructorId);
        }
        public async Task<List<TestDetailsModel>> SearchForTestOfInstructor(string SearchString, int MaxRowToReturn, int InstructorId)
        {
            return await objHomeDTO.SearchForTestOfInstructor(SearchString, MaxRowToReturn, InstructorId);
        }
        public async Task<List<CourseDetailsModel>> SearchForCourseOfInstructor(string SearchString, int MaxRowToReturn, int InstructorId)
        {
            return await objHomeDTO.SearchForCourseOfInstructor(SearchString, MaxRowToReturn, InstructorId);
        }
        public async Task<InstructorSearchResponse> GetInstructorSearchDetails(InstructorSearchRequest objInstructorSearchRequest)
        {
            InstructorSearchResponse objInstructorSearchResponse = null;
            try
            {
                objInstructorSearchResponse = new InstructorSearchResponse();
                objInstructorSearchResponse.m_lsAssignments = await SearchForAssignmentOfInstructor(objInstructorSearchRequest.m_strSerachStraing,4,objInstructorSearchRequest.m_iInstructorId);
                objInstructorSearchResponse.m_lsCourses = await SearchForCourseOfInstructor(objInstructorSearchRequest.m_strSerachStraing, 4, objInstructorSearchRequest.m_iInstructorId);
                objInstructorSearchResponse.m_lsTestDetails = await SearchForTestOfInstructor(objInstructorSearchRequest.m_strSerachStraing, 4, objInstructorSearchRequest.m_iInstructorId);
                objInstructorSearchResponse.m_lsClassroomBasicDetailsModal = await GetInstructorSearchResultForClassrom(objInstructorSearchRequest.m_iInstructorId, objInstructorSearchRequest.m_strSerachStraing);

            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetInstructorSearchDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objInstructorSearchResponse;
        }
        public async Task<List<ClassRoomModal>> GetAllClassroomForIsntrcutor(int InstrucorId)
        {
            return await objHomeDTO.GetAllClassroomForIsntrcutor(InstrucorId);
        }
        public async Task<bool> ActivateClassroom(ActivateClassroomRequest activateClassroomRequest)
        {
            bool result = false;
            try
            {

                activateClassroomRequest.m_strClassroomShareCode = objInstructorBusinessLayer.GetShareCodeForAssignment();
                activateClassroomRequest.m_strTinyUrl  = await objInstructorBusinessLayer.GetTinyUrlForClassroom(activateClassroomRequest.m_llClassroomId,
                    activateClassroomRequest.m_strClassroomShareCode);

                activateClassroomRequest.m_iClassroomJoiningFee = activateClassroomRequest.m_iClassroomJoiningFee * 100;
                activateClassroomRequest.m_iClassroomJoiningFee = objInstructorBusinessLayer.GetPriceAccordingToCurrency(activateClassroomRequest.m_iCurrencyType, activateClassroomRequest.m_iClassroomJoiningFee);
                result = await objHomeDTO.ActivateClassroom(activateClassroomRequest);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "DeleteCourse", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<bool> MarkMeetingClosed(long MeetingId)
        {
            bool result = false;
            try
            {
                
                result = await objHomeDTO.MarkMeetingClosed(MeetingId);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "MarkMeetingClosed", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<List<ClassroomMeetingModal>> GetAllMeetingForClassroom(long ClassroomId)
        {
            List<ClassroomMeetingModal> result=new List<ClassroomMeetingModal>();
            try
            {

                result = await objHomeDTO.GetAllMeetingForClassroom(ClassroomId);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetAllMeetingForClassroom", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<List<StudentClassrromJoinModal>> GetAllStudentsJoinedToClassroomResponse(long ClassroomId)
        {
            List<StudentClassrromJoinModal> result = new List<StudentClassrromJoinModal>();
            try
            {

                result = await objHomeDTO.GetAllStudentsJoinedToClassroomResponse(ClassroomId);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetAllMeetingForClassroom", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<List<StudentMeetingJoinedResponse>> GetAllStudentsJoinedToMeetingResponse(long MeetingId, long ClassroomId)
        {
            List<StudentMeetingJoinedResponse> result = new List<StudentMeetingJoinedResponse>();
            try
            {
                result = await objHomeDTO.GetAllStudentsJoinedToMeetingResponse(MeetingId,ClassroomId);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetAllMeetingForClassroom", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<bool> InsertNewMessageToClassroom(InsertInstructorMessageToClassroom insertInstructorMessageToClassroom)
        {

            bool result = false;
            try
            {
                result = await objHomeDTO.InsertNewMessageToClassroom(insertInstructorMessageToClassroom);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetAllMeetingForClassroom", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<List<ClassroomInstructorMessageModal>> GetAllClassroomMessageForInstructor(long ClassroomId)
        {
            List<ClassroomInstructorMessageModal> result = null;
            try
            {
                result = await objHomeDTO.GetAllClassroomMessageForInstructor(ClassroomId);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetAllMeetingForClassroom", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<List<ClassroomInstructorMessageModal>> GetAllClassroomLastMessagesForInstructor(long ClassroomId, long LastMessageId)
        {
            List<ClassroomInstructorMessageModal> result = null;
            try
            {
                result = await objHomeDTO.GetAllClassroomLastMessagesForInstructor(ClassroomId, LastMessageId);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetAllMeetingForClassroom", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<bool> DeleteClassroom(long ClassroomId)
        {
            bool result = false;
            try
            {
                result = await objHomeDTO.DeleteClassroom(ClassroomId);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "DeleteClassroom", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<bool> UpdateClassroomDetails(ClassRoomModal classRoomModal)
        {
            bool result = false;
            try
            {
                result = await objHomeDTO.UpdateClassroomDetails(classRoomModal);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "UpdateClassroomDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<bool> InsertnewAssignmentToClassroom(long AssignmentId, long ClassroomId)
        {
            return await objHomeDTO.InsertnewAssignmentToClassroom(AssignmentId, ClassroomId);
        }
        public async Task<List<ClassroomAssignmentModal>> GetAllClassroomAssignments(long ClassroomId)
        {
            return await objHomeDTO.GetAllClassroomAssignments(ClassroomId);
        }
        public async Task<List<ClassroomTestModal>> GetAllClassroomTests(long ClassroomId)
        {
            return await objHomeDTO.GetAllClassroomTests(ClassroomId);
        }
        public async Task<bool> DeleteClassroomTest(long ClassroomId, long TestId)
        {
            return await objHomeDTO.DeleteClassroomTest(ClassroomId, TestId);
        }
        public async Task<string> UploadFiles(List<AwsFileUploadRequest> lsAwsFileUploadRequest,int FileTypeId)
        {
            string awsFilePath = null;
            switch(FileTypeId)
            {
                case (int)FileUploadTypeId.IMAGE:
                    {
                        foreach (var files in lsAwsFileUploadRequest)
                        {
                            awsFilePath=await objInstructorBusinessLayer.UploadImageAsync(files.m_strFileName, files.m_strFilePath);

                        }
                        break;
                    }
                case (int)FileUploadTypeId.VIDEO:
                    {
                        foreach (var files in lsAwsFileUploadRequest)
                        {
                            awsFilePath = await objInstructorBusinessLayer.UploadVideoAsync(files.m_strFileName, files.m_strFilePath);
                        }
                        break;
                    }
                case (int)FileUploadTypeId.PDF:
                    {
                        foreach (var files in lsAwsFileUploadRequest)
                        {
                            awsFilePath = await objInstructorBusinessLayer.UploadPdfAsync(files.m_strFileName, files.m_strFilePath);
                        }
                        break;
                    }
                case (int)FileUploadTypeId.CUSTOM:
                    {
                        foreach (var files in lsAwsFileUploadRequest)
                        {
                            awsFilePath = await objInstructorBusinessLayer.UploadCustomTypeAttachmentAsync(files.m_strFileName, files.m_strFilePath);
                        }
                        break;
                    }
            }
            foreach (var files in lsAwsFileUploadRequest)
            {
                DeleteFileFromServer(files.m_strFilePath);
            }
            return awsFilePath;
        }
        public async Task<ImageUploadMasterResponse> GenerateThumbnails(List<AwsFileUploadRequest> lsAwsFileUploadRequest)
        {
            ImageUploadMasterResponse imageUploadMasterResponse = new ImageUploadMasterResponse();
            ImageCompressionUtilities imageCompressionUtilities = new ImageCompressionUtilities();
            FileModal fileModal = null;
            foreach (var files in lsAwsFileUploadRequest)
            {
                imageUploadMasterResponse.m_strRawImagePathUrl= await objInstructorBusinessLayer.UploadImageAsync(files.m_strFileName, files.m_strFilePath);
                fileModal = imageCompressionUtilities.ResizeToProfileThumbnail(files.m_strFilePath);
                imageUploadMasterResponse.m_str50pxThumbnailUrl = await objInstructorBusinessLayer.UploadImageFileCompressedAsync(fileModal.m_strFileName, fileModal.m_strFilePath);
                DeleteFileFromServer(fileModal.m_strFilePath);
                fileModal = imageCompressionUtilities.ResizeToCourseThumbnail(files.m_strFilePath);
                imageUploadMasterResponse.m_str250pxThumb = await objInstructorBusinessLayer.UploadImageFileCompressedAsync(fileModal.m_strFileName, fileModal.m_strFilePath);
                DeleteFileFromServer(fileModal.m_strFilePath);
                DeleteFileFromServer(MasterUtilities.GetPhysicalPath(files.m_strFilePath));
            }
            return imageUploadMasterResponse;
        }
        public void DeleteFilesFromServer(List<AwsFileUploadRequest> lsAwsFileUploadRequest)
        {
            try
            {
                foreach (var files in lsAwsFileUploadRequest)
                {
                    DeleteFileFromServer(files.m_strFilePath);
                }
            }
            catch(Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "DeleteFilesFromServer", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
        }
        public async Task<List<ClassroomBasicDetailsModal>> GetInstructorSearchResultForClassrom(int InstructorId, string SearchString)
        {
            return await objHomeDTO.GetInstructorSearchResultForClassrom(InstructorId, SearchString);
        }
        public async Task<bool> UpdateInstructorProfilePicture(InstructorProfileChangeRequest instructorProfileChangeRequest)
        {
            return await objHomeDTO.UpdateInstructorProfilePicture(instructorProfileChangeRequest);
        }
        public async Task<bool> AddAttachmentToClassroom(ClassroomAttachmentRequest classroomAttachmentRequest)
        {
            return await objHomeDTO.AddAttachmentToClassroom(classroomAttachmentRequest);
        }
        public async Task<List<ClassroomAttachmentDetailsResponse>> GetAllClassroomAttachments(long ClassroomId)
        {
            return await objHomeDTO.GetAllClassroomAttachments(ClassroomId);
        }
        public async Task<bool> DeleteClassroomAttachment(long AttachmentId)
        {
            return await objHomeDTO.DeleteClassroomAttachment(AttachmentId);
        }
        public async Task<bool> InsertClassroomSchedule(ClassroomScheduleDetails classroomScheduleDetails)
        {
            string strClassroomScheduleSerialized = JsonConvert.SerializeObject(classroomScheduleDetails);
            return await objHomeDTO.InsertClassroomSchedule(classroomScheduleDetails.m_llClassroomId, strClassroomScheduleSerialized);
        }
        public async Task<bool> UpdateClassroomSchedule(ClassroomScheduleDetails classroomScheduleDetails)
        {
            string strClassroomScheduleSerialized = JsonConvert.SerializeObject(classroomScheduleDetails);
            return await objHomeDTO.UpdateClassroomSchedule(classroomScheduleDetails.m_llClassroomId, strClassroomScheduleSerialized);
        }
        public async Task<ClassroomScheduleDetails> GetClassroomSchedule(long ClassroomId)
        {
            ClassroomScheduleDTO classroomScheduleDTO = await objHomeDTO.GetClassroomScheduleDetails(ClassroomId);
            ClassroomScheduleDetails classroomScheduleDetails = null;
            if (classroomScheduleDTO!=null)
            {
                classroomScheduleDetails = JsonConvert.DeserializeObject<ClassroomScheduleDetails>(classroomScheduleDTO.m_strClassroomScheduleObj);
                classroomScheduleDetails.m_strClassroomScheduleInsertionTime = classroomScheduleDTO.m_dtClassroomScheduleInsertionTime.ToString();
                classroomScheduleDetails.m_strClassroomScheduleUpdationTime = classroomScheduleDTO.m_dtClassroomScheduleUpdationTime.ToString();
            }
            return classroomScheduleDetails;
        }
        private void DeleteFileFromServer(string FilePath)
        {
            if (System.IO.File.Exists(FilePath))
            {
                try
                {
                    System.IO.File.Delete(FilePath);
                }
                catch (System.IO.IOException e)
                {
                    Console.WriteLine(e.Message);
                    return;
                }
            }
        }
        public async Task<bool> UpdateClassroomImage(UpdateClassroomBackgroundImageRequest updateClassroomBackgroundImageRequest)
        {
            return await objHomeDTO.UpdateClassroomImage(updateClassroomBackgroundImageRequest);
        }
        public async Task<bool> UpdateCourseImage(UpdateCourseBackgroundImageRequest updateCourseBackgroundImageRequest)
        {
            return await objHomeDTO.UpdateCourseImage(updateCourseBackgroundImageRequest);
        }

    }

}