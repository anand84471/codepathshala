using StudentDashboard.HttpRequest;
using StudentDashboard.HttpResponse;
using StudentDashboard.HttpResponse.ClassRoom;
using StudentDashboard.Models;
using StudentDashboard.Models.Alert;
using StudentDashboard.Models.Classroom;
using StudentDashboard.Models.Course;
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

namespace StudentDashboard.DTO
{
    public class HomeDTO
    {
        CPDataService.CpDataServiceClient objCPDataService;
        StringBuilder m_strLogMessage = new StringBuilder();
        public HomeDTO()
        {
            objCPDataService = new CPDataService.CpDataServiceClient();
        }
        public async Task<bool> RegisterNewUser(StaeModel objRegisterModel)
        {
            bool result = false;
            try
            {
                result =await objCPDataService.RegisterNewUserAsync(objRegisterModel.strFisrtName, objRegisterModel.strLastName, objRegisterModel.strPhoneNo, objRegisterModel.strEmail, objRegisterModel.strPassword);

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
        public async Task<bool> ValidateLogineDetails(StaeModel objRegisterModel)
        {
            bool result = false;
            try
            {
                DataSet ds = new DataSet();
               
                ds = await objCPDataService.ValidateLoginDetailsAsync(objRegisterModel.strEmail, objRegisterModel.strPassword);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    List<StaeModel> lsRegisterModel = ds.Tables[0].AsEnumerable().Select(
                      dataRow => new StaeModel(
                          dataRow.Field<string>("FIRST_NAME"),
                          dataRow.Field<string>("LAST_NAME")
                          )).ToList();
                    if(lsRegisterModel.Count==1)
                    {
                        objRegisterModel.strFisrtName = lsRegisterModel[0].strFisrtName;
                        objRegisterModel.strLastName = lsRegisterModel[0].strLastName;
                        result = true;
                    }
                }
               
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "ValidateLogineDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        //public async Task<bool> GetStudentDetails(int StudentId)
        //{
        //    bool result = false;
        //    try
        //    {
        //        //    result = objCPDataService.RegisterNewUser(objRegisterModel.strFisrtName, objRegisterModel.strLastName, objRegisterModel.strPhoneNo, objRegisterModel.strEmail, objRegisterModel.strPassword);
        //        //}
        //    }
        //    catch (Exception Ex)
        //    {
        //        m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
        //        m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetStudentDetails", Ex.ToString());
        //        m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
        //        MainLogger.Error(m_strLogMessage);
        //    }
        //    return result;
        //}
        public bool InsertNewAssignment(AssignmentModel objAssignmentModel)
        {
            bool result = false;
            try
            {
                long AssignmentId=0;
                result = objCPDataService.InsertNewAssignment(objAssignmentModel.m_strAssignmentName,objAssignmentModel.m_strFileUploadPath,objAssignmentModel.m_iFileUploadTypeId,objAssignmentModel.m_iAssignmentType,objAssignmentModel.m_strAssignmentDescription,ref AssignmentId);
                objAssignmentModel.m_llAssignemntId = AssignmentId;
            }

            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "InsertNewAssignment", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public bool InsertNewIndex(IndexModel objIndexModel)
        {
            bool result = false;
            try
            {
                long IndexId=0;
                result = objCPDataService.InertNewCourseIndex(objIndexModel.m_strIndexName,objIndexModel.m_strIndexDescription,objIndexModel.m_llCourseId,
                                                              ref IndexId);
                if(IndexId!=0)
                {
                    objIndexModel.m_llIndexId = IndexId;
                }

            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "InsertNewIndex", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public bool InsertNewTest(TestModel objTestModel)
        {
            bool result = false;
            try
            {
                long TestId = 0;
                result = objCPDataService.InsertNewTest(objTestModel.m_strTestName,objTestModel.m_strTestDescription,objTestModel.m_strFilePath,objTestModel.m_iFileTypeId,objTestModel.m_iTestType,ref TestId);
                objTestModel.m_llTestId = TestId;
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "InsertNewTest", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<bool> InsertNewTopic(TopicModel objTopicModel )
        {
            bool result = false;
            try
            {
                result = await objCPDataService.InsertNewTopicAsync(objTopicModel.m_strTopicName, objTopicModel.m_strTopicDescription, 
                                     objTopicModel.m_strFilePath, objTopicModel.m_iFileUploadTypeId,objTopicModel.m_llIndexId,objTopicModel.m_strFileAttachmentPath);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "InsertNewTopic", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public bool InsertNewCourse(CourseModel objCourseModel)
        {
            bool result = false;
            try
            {
                long CourseId = 0;
                result = objCPDataService.InsertNewCourse(objCourseModel.m_strCourseName, objCourseModel.m_strCourseDescription,
                               objCourseModel.imageUploadDetailsModal.m_strOriginalFileUrl,
                               objCourseModel.imageUploadDetailsModal.m_strSmallSizeUrl,
                               objCourseModel.imageUploadDetailsModal.m_strMediumSizeUrl,
                                                       objCourseModel.m_iInstructorId, ref CourseId);
                if (CourseId!=-1)
                {
                    objCourseModel.m_llCourseId = CourseId;
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "InsertNewCourse", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<bool> CheckCourseIdExistsForInstrcutor(int InstructorId, long CourseId)
        {
            bool result = false;
            try
            {

                DataSet ds = await objCPDataService.CheckCourseIdExsitsForInstructorAsync(InstructorId, CourseId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    result = true;
                }

            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "CheckCourseIdExistsForInstrcutor", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<bool> CheckAssignmentIdExistsForInstrcutor(int InstructorId, long AssignmentId)
        {
            bool result = false;
            try
            {
                DataSet ds = await objCPDataService.CheckAssignmentIdExistsForInstructorAsync(AssignmentId, InstructorId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    result = true;
                }

            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "CheckCourseIdExistsForInstrcutor", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<bool> CheckTestIdExistsForInstrcutor(int InstructorId, long TestId)
        {
            bool result = false;
            try
            {
                DataSet ds = await objCPDataService.CheckTestIdExistsForInstructorAsync(TestId, InstructorId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    result = true;
                }

            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "CheckCourseIdExistsForInstrcutor", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<bool> CheckAssignmentIdExistsForAnyCourseForInstrcutor(int InstructorId, long AssignmentId)
        {
            bool result = false;
            try
            {

                DataSet ds = await objCPDataService.CheckAssignmentIdExistsForAnyCourseForInstructorAsync(AssignmentId, InstructorId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && (ds.Tables[0].Rows.Count > 0
                    || ds.Tables[1].Rows.Count>0||ds.Tables[2].Rows.Count>0))
                {
                    result = true;
                }

            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "CheckAssignmentIdExistsForAnyCourseForInstrcutor", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<bool> CheckTestIdExistsForAnyCourseForInstrcutor(int InstructorId, long TestId)
        {
            bool result = true;
            try
            {
                DataSet ds = await objCPDataService.CheckTestIdExistsForAnyCourseForInstructorAsync(TestId, InstructorId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    result = true;
                }

            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "CheckAssignmentIdExistsForAnyCourseForInstrcutor", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<bool> CheckIndexIdExistsForInstrcutor(int InstructorId, long IndexId)
        {
            bool result = false;
            try
            {

                DataSet ds = await objCPDataService.CheckIndexIdExsitsForInstructorAsync(InstructorId, IndexId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    result = true;
                }

            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "CheckIndexIdExistsForInstrcutor", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<bool> InsertNewMcqAssignmentQuestion(McqQuestion objMcqQuestion)
        {
            bool result = false;
            try
            {
                result =await objCPDataService.InsertNewMcqAssignmentAsync(objMcqQuestion.m_llAssignmentId, objMcqQuestion.m_strQuestionStatement,
                                          objMcqQuestion.m_strOption1,objMcqQuestion.m_strOption2,objMcqQuestion.m_strOption3,objMcqQuestion.m_strOption4,
                                          objMcqQuestion.m_iCorrectOption);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "InsertNewMcqAssignmentQuestion", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<bool> InsertNewMcqTestQuestion(McqQuestion objMcqQuestion)
        {
            bool result = false;
            try
            {
                result = await objCPDataService.InsertNewMcqTestQuestionAsync(objMcqQuestion.m_llTestId, objMcqQuestion.m_strQuestionStatement,
                                          objMcqQuestion.m_strOption1, objMcqQuestion.m_strOption2, objMcqQuestion.m_strOption3, objMcqQuestion.m_strOption4,
                                          objMcqQuestion.m_iCorrectOption,objMcqQuestion.m_iTimeInSeconds,objMcqQuestion.m_iMarks);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "InsertNewMcqTestQuestion", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<bool> InsertAssignmentIdToIndex(long AssignmentId,long IndexId)
        {
            bool result = false;
            try
            {
                result =await objCPDataService.InsertAssignmentIdToIndexAsync(AssignmentId,IndexId);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "InsertAssignmentIdToIndex", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<bool> InsertTestIdToIndex(long Test, long IndexId)
        {
            bool result = false;
            try
            {
                result = await objCPDataService.InsertTestIdToIndexAsync(Test, IndexId);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "InsertTestIdToIndex", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }

        public async Task<List<CourseDetailsModel>> GetAllCourseDetailsForInstructor(long InstructorId)
        {
            List<CourseDetailsModel> result =null;
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
        public async Task<List<SubjectiveQuestion>> GetAllQuestionsOfSubjectiveAssignment(long AssignmentId)
        {
            List<SubjectiveQuestion> lsSubjectiveQuestion = null;
            try
            {
                DataSet ds = new DataSet();
                ds = await objCPDataService.GetAllQuestionsOfSubjectiveAssignmentAsync(AssignmentId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    lsSubjectiveQuestion = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new SubjectiveQuestion(
                         AssignmentId,
                         dataRow.Field<long>("QUESTION_ID"),
                         dataRow.Field<string>("QUESTION_STATEMENT"),
                         dataRow.Field<string>("QUESTION_HINT"),
                         dataRow.Field<DateTime>("ROW_INSERTION_DATETIME").ToString("d MMM yyyy"),
                         dataRow.Field<DateTime>("ROW_UPDATION_DATETIME").ToString("d MMM yyyy")
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
            return lsSubjectiveQuestion;
        }
        public  bool GetInstructorIdFromUserId(string InstructorId, ref int Id)
        {
            bool result = true;
            try
            {
                result = objCPDataService.GetInstructorIdFromUserId(InstructorId, ref Id);
            }
            catch(Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetInstructorIdFromUserId", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<TestModel> GetIndependentTestDetails(long TestId)
        {
            TestModel objTestModel = null;
            try
            {
                DataSet ds = await objCPDataService.GetIndependentTestDetailsAsync(TestId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    objTestModel = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new TestModel(
                         dataRow.Field<long>("TEST_ID"),
                         dataRow.Field<string>("TEST_NAME"),
                         dataRow.Field<string>("TEST_DESCRIPTION"),
                         dataRow.Field<DateTime>("ROW_INSERTION_DATETIME").ToString("d MMM yyyy"),
                         dataRow.Field<DateTime>("ROW_UPDATION_DATETIME").ToString("d MMM yyyy"),
                         dataRow.Field<int?>("TOTAL_NO_OF_QUESTIONS"),
                         dataRow.Field<int?>("TOTAL_MARKS"),
                         dataRow.Field<int?>("TOTAL_ALLOWED_TIME"),
                         dataRow.Field<bool>("IS_ACTIVE"),
                         dataRow.Field<string>("TINY_URL"),
                         dataRow.Field<string>("SHARE_CODE")
                         )).ToList()[0];
                }

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
        public async Task<TestModel> GetTestDetails(long TestId)
        {
            TestModel objTestModel = null;
            try
            {
                DataSet ds = await objCPDataService.GetMcqTestDetailsAsync(TestId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    objTestModel = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new TestModel(
                         dataRow.Field<long>("TEST_ID"),
                         dataRow.Field<string>("TEST_NAME"),
                         dataRow.Field<string>("TEST_DESCRIPTION"),
                         dataRow.Field<DateTime>("ROW_INSERTION_DATETIME").ToString("d MMM yyyy"),
                         dataRow.Field<DateTime>("ROW_UPDATION_DATETIME").ToString("d MMM yyyy"),
                         dataRow.Field<int?>("TOTAL_NO_OF_QUESTIONS"),
                         dataRow.Field<int?>("TOTAL_MARKS"),
                         dataRow.Field<int?>("TOTAL_ALLOWED_TIME")
                       
                         )).ToList()[0];
                }

            }
            catch(Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetTestDetails", Ex.ToString());
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
                DataSet ds = await objCPDataService.GetStudentTestProgressAsync(TestId, StudentId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    objTestModel = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new StudentTestProgressModal(
                         dataRow.Field<long>("TEST_ID"),
                         dataRow.Field<string>("TEST_NAME"),
                         dataRow.Field<string>("TEST_DESCRIPTION"),
                         dataRow.Field<int>("TOTAL_NO_OF_QUESTIONS"),
                         dataRow.Field<int>("TOTAL_ALLOWED_TIME"),
                         dataRow.Field<int>("TOTAL_MARKS"),
                         dataRow.Field<long?>("SUBMISSION_ID")

                         )).ToList()[0];
                }

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
        public async Task<List<McqQuestion>> GetMcqTestQuestions(long TestId)
        {
            List<McqQuestion> lsMcqQuestion = null;
            try
            {
                DataSet ds = await objCPDataService.GetMcqtestQuestionDetailsAsync(TestId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    lsMcqQuestion = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new McqQuestion(
                         dataRow.Field<long>("ID"),
                         dataRow.Field<string>("QUESTION_STATEMENT"),
                         dataRow.Field<string>("OPTION1"),
                         dataRow.Field<string>("OPTION2"),
                         dataRow.Field<string>("OPTION3"),
                         dataRow.Field<string>("OPTION4"),
                         dataRow.Field<byte>("CORRECT_ANSWER"),
                         dataRow.Field<DateTime>("ROW_INSERTION_DATETIME").ToString("d MMM yyyy"),
                         dataRow.Field<DateTime>("ROW_UPDATION_DATETIME").ToString("d MMM yyyy"),
                         dataRow.Field<int>("MARKS"),
                         dataRow.Field<int>("TIME_FOR_QUESTION_IN_SECONDS")
                         )).ToList();
                }

            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetMcqTestQuestions", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return lsMcqQuestion;
        }

        public async Task<List<CourseIndexDetails>> GetCourseIndexDetails(long CourseId)
        {
            List<CourseIndexDetails> lsIndexDetails = new List<CourseIndexDetails>();
            try
            {
                DataSet ds = new DataSet();
                ds =await objCPDataService.GetIndexDetailsOfCourseAsync(CourseId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    lsIndexDetails = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new CourseIndexDetails(
                         dataRow.Field<string>("INDEX_NAME"),
                         dataRow.Field<string>("INDEX_DESCRIPTION"),
                         dataRow.Field<DateTime>("ROW_INSERTION_DATETIME").ToString("d MMM yyyy"),
                         dataRow.Field<DateTime>("ROW_UPDATION_DATETIME").ToString("d MMM yyyy"),
                         dataRow.Field<String>("ASSIGNMENT_NAME"),
                         dataRow.Field<byte?>("ASSIGNMENT_TYPE"),
                         dataRow.Field<String>("TEST_NAME"),
                         dataRow.Field<byte?>("TEST_TYPE"),
                         dataRow.Field<int>("TOPIC_COUNT"),
                         dataRow.Field<long>("INDEX_ID"),
                         dataRow.Field<long?>("ASSIGNMENT_ID"),
                         dataRow.Field<long?>("TEST_ID")
                         )).ToList();
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetCourseIndexDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return lsIndexDetails;
        }
        public async Task<List<CourseIndexDetails>> GetStudentCourseProgress(long CourseId,long StudentId)
        {
            List<CourseIndexDetails> lsIndexDetails = new List<CourseIndexDetails>();
            try
            {
                DataSet ds = new DataSet();
                ds = await objCPDataService.GetIndexDetailsOfCourseAsync(CourseId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    lsIndexDetails = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new CourseIndexDetails(
                         dataRow.Field<string>("INDEX_NAME"),
                         dataRow.Field<string>("INDEX_DESCRIPTION"),
                         dataRow.Field<DateTime>("ROW_INSERTION_DATETIME").ToString("d MMM yyyy"),
                         dataRow.Field<DateTime>("ROW_UPDATION_DATETIME").ToString("d MMM yyyy"),
                         dataRow.Field<String>("ASSIGNMENT_NAME"),
                         dataRow.Field<byte?>("ASSIGNMENT_TYPE"),
                         dataRow.Field<String>("TEST_NAME"),
                         dataRow.Field<byte?>("TEST_TYPE"),
                         dataRow.Field<int>("TOPIC_COUNT"),
                         dataRow.Field<long>("INDEX_ID"),
                         dataRow.Field<long?>("ASSIGNMENT_ID"),
                         dataRow.Field<long?>("TEST_ID")
                         )).ToList();
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetCourseIndexDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return lsIndexDetails;
        }
        public async Task<GetCourseDetailsApiResponse> GetCourseDetails(long CourseId)
        {
            List<GetCourseDetailsApiResponse> lsGetCourseDetailsApiResponse = null;
            GetCourseDetailsApiResponse objGetCourseDetailsApiResponse = null;
            try
            {
                DataSet ds = await objCPDataService.GetCourseDetailsAsync(CourseId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    lsGetCourseDetailsApiResponse = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new GetCourseDetailsApiResponse(
                         dataRow.Field<string>("COURSE_NAME"),
                         dataRow.Field<string>("COURSE_DESCRIPTION"),
                         dataRow.Field<DateTime>("ROW_INSERTION_DATETIME").ToString("d MMM yyyy"),
                         dataRow.Field<DateTime>("ROW_UPDATION_DETETIME").ToString("d MMM yyyy"),
                         dataRow.Field<string>("COURSE_STATUS_NAME"),
                         dataRow.Field<string>("ACCESS_CODE"),
                         dataRow.Field<string>("TINY_SHARE_URL"),
                         dataRow.Field<int>("COURSE_JOINING_FEE")
                         )).ToList();
                }
                if(lsGetCourseDetailsApiResponse!=null&&lsGetCourseDetailsApiResponse.Count>0)
                {
                    objGetCourseDetailsApiResponse = lsGetCourseDetailsApiResponse[0];
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

       

        public async Task<bool> UpdateTestDetails(TestModel objTestModel)
        {
            bool result = false;
            try
            {
                result =await objCPDataService.UpdateTestDetailsAsync(objTestModel.m_llTestId,objTestModel.m_strTestName,objTestModel.m_strTestDescription,
                                                           objTestModel.m_strFilePath,(byte)objTestModel.m_iFileTypeId);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "UpdateTestDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<bool> UpdateAssignmentDetails(AssignmentModel objAssignmentModel)
        {
            bool result = false;
            try
            {
                result = await objCPDataService.UpdateAssignmentDetailsAsync(objAssignmentModel.m_llAssignemntId,objAssignmentModel.m_strAssignmentName,
                                          objAssignmentModel.m_strAssignmentDescription,objAssignmentModel.m_strFileUploadPath,(byte)objAssignmentModel.m_iFileUploadTypeId);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "UpdateAssignmentDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<bool> UpdateIndexTopicDetails(TopicModel objTopicModel)
        {
            bool result = false;
            try
            {
                result = await objCPDataService.UpdateIndexTopicDetailsAsync(objTopicModel.m_llTopicId,objTopicModel.m_strTopicName,objTopicModel.m_strTopicDescription,
                                                                  objTopicModel.m_strFilePath,(byte)objTopicModel.m_iFileUploadTypeId,objTopicModel.m_strFileAttachmentPath);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "UpdateIndexTopicDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<bool> UpdateCourseIndexDetails(IndexModel objIndexModel)
        {
            bool result = false;
            try
            {
                result = await objCPDataService.UpdateCourseIndexDetailsAsync(objIndexModel.m_llIndexId,objIndexModel.m_strIndexName,objIndexModel.m_strIndexDescription);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "UpdateCourseIndexDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<bool> UpdateCourseDetails(CourseModel objCourseModel)
        {
            bool result = false;
            try
            {
                result = await objCPDataService.UpdateCourseDetailsAsync(objCourseModel.m_llCourseId,objCourseModel.m_strCourseDescription);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "UpdateCourseDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<bool> ActivateCourse(long CourseId,string ShareCode,string TinyUrl,int CourseJoiningFeeInPaise)
        {
            bool result = false;
            try
            {
                result = await objCPDataService.ActivateCourseAsync(CourseId, TinyUrl, ShareCode, CourseJoiningFeeInPaise);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "ActivateCourse", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<bool> ActivateClassroom(ActivateClassroomRequest activateClassroomRequest)
        {
            bool result = false;
            try
            {
                result = await objCPDataService.ActivateClassroomAsync(activateClassroomRequest.m_llClassroomId,
                    activateClassroomRequest.m_strClassroomShareCode,activateClassroomRequest.m_strTinyUrl,
                    activateClassroomRequest.m_bPublicType,activateClassroomRequest.m_iClassroomJoiningFee,
                    activateClassroomRequest.m_strClassroomStartTime,activateClassroomRequest.m_strClassroomBitwiseSchedule,
                    activateClassroomRequest.m_iNoOfDemoClasses, activateClassroomRequest.m_iCategoryId,
                    activateClassroomRequest.m_iLevel);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "ActivateCourse", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<bool> DeleteCourse(long CourseId)
        {
            bool result = false;
            try
            {
                result =await objCPDataService.DeleteCourseAsync(CourseId);
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
        public async Task<bool> InsertActivityForInstructor(int InstructorId, string ActivityMessage)
        {
            bool result = false;
            try
            {
                result = await objCPDataService.InsertActivityForInstructorAsync(InstructorId, ActivityMessage);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "InsertTestIdToIndex", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public bool InsertNewIndependentAssignment(AssignmentModel objAssignmentModel)
        {
            bool result = false;
            try
            {
                long AssignmentId = 0;
                result = objCPDataService.InsertNewIndependentAssignment(objAssignmentModel.m_iInstructorId, objAssignmentModel.m_strAssignmentName,objAssignmentModel.m_strAssignmentDescription,objAssignmentModel.m_strFileUploadPath
                                ,objAssignmentModel.m_iFileUploadTypeId,objAssignmentModel.m_iAssignmentType,ref AssignmentId);
                if(result)
                {
                    objAssignmentModel.m_llAssignemntId = AssignmentId;
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "InsertTestIdToIndex", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public bool InsertNewIndependentTest(TestModel objTestModel)
        {
            bool result = false;
            try
            {
                long TestId = 0;
                result = objCPDataService.InsertNewIndependentTest(objTestModel.m_iInstructorId, objTestModel.m_strTestName,objTestModel.m_strTestDescription,
                    objTestModel.m_strFilePath,objTestModel.m_iFileTypeId,objTestModel.m_iTestType,ref TestId);
                if (result)
                {
                    objTestModel.m_llTestId = TestId;
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "InsertNewIndependentTest", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<List<AssignmentDetailsModel>> GetAssignmentForInstructor(int InstructorId)
        {
            List<AssignmentDetailsModel> lsAssignmentDetailsModel = null;
            try
            {
                DataSet ds = await objCPDataService.GetInstructorAssignmentDetailsAsync(InstructorId);
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
                          dataRow.Field<int>("NO_OF_SUBJECTIVE_QUESTIONS"),
                           dataRow.Field<int>("NO_OF_SUBMISSIONS")
                         )).ToList();
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetAssignmentForInstructor", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return lsAssignmentDetailsModel;
        }
        public async Task<List<TestDetailsModel>> GetInstructorTestDetails(int InstructorId)
        {
            List<TestDetailsModel> lsTestDetailsModel = null;
            try
            {
                DataSet ds = new DataSet();
                ds =await objCPDataService.GetInstructorTestDetailsAsync(InstructorId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    lsTestDetailsModel = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new TestDetailsModel(
                         dataRow.Field<long>("TEST_ID"),
                         dataRow.Field<string>("TEST_NAME"),
                         dataRow.Field<string>("TEST_DESCRIPTION"),
                         dataRow.Field<DateTime>("ROW_INSERTION_DATETIME").ToString("d MMM yyyy"),
                         dataRow.Field<int>("NO_OF_QUESTIONS"),
                          dataRow.Field<byte>("TEST_TYPE"),
                          dataRow.Field<int>("NO_OF_SUBMISSIONS")
                         )).ToList();
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetInstructorTestDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return lsTestDetailsModel;
        }
        public async Task<List<TopicModel>> GetIndexTopicDetails(long TopicId)
        {
            List<TopicModel> lsTopicModel = null;
            try
            {
                DataSet ds = new DataSet();
                ds = await objCPDataService.GetIndexTopicDetailsAsync(TopicId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    lsTopicModel = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new TopicModel(
                         dataRow.Field<long>("ID"),
                         dataRow.Field<string>("TOPIC_NAME"),
                         dataRow.Field<string>("TOPIC_DESCRIPTION"),
                         dataRow.Field<string>("FILE_PATH_MAP_TO_SERVER"),
                         dataRow.Field<string>("FILE_ATTACHMENT_PATH"),
                         dataRow.Field<DateTime>("ROW_INSERTION_DATETIME").ToString("d MMM yyyy"),
                         dataRow.Field<DateTime>("ROw_UODATION_DATETIME").ToString("d MMM yyyy")
                         )).ToList();
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetInstructorTestDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return lsTopicModel;
        }
        public async Task<List<StudentTopicModal>> FetchTopicProgressForStudent(long TopicId,long StudentId)
        {
            List<StudentTopicModal> lsTopicModel = null;
            try
            {
                DataSet ds = new DataSet();
                ds = await objCPDataService.GetIndexTopicProgressForStudentAsync(TopicId, StudentId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    lsTopicModel = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new StudentTopicModal(
                         dataRow.Field<long>("TOPIC_ID"),
                         dataRow.Field<string>("TOPIC_NAME"),
                         dataRow.Field<string>("TOPIC_DESCRIPTION"),
                         dataRow.Field<string>("FILE_PATH_MAP_TO_SERVER"),
                         dataRow.Field<bool?>("IS_COMPLETED"),
                         dataRow.Field<string>("FILE_ATTACHMENT_PATH")
                         )).ToList();
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetInstructorTestDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return lsTopicModel;
        }
        public async Task<IndexModel>  GetIndexDetails(long TopicId)
        {
            IndexModel objIndexModel = null;
            try
            {
                List<IndexModel> lsIndexModel=null;
                DataSet ds = await objCPDataService.GetIndexDetailsAsync(TopicId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    lsIndexModel = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new IndexModel(
                         dataRow.Field<long>("ID"),
                         dataRow.Field<string>("INDEX_NAME"),
                         dataRow.Field<string>("INDEX_DESCRIPTION"),
                         dataRow.Field<DateTime>("ROW_INSERTION_DATETIME").ToString("d MMM yyyy"),
                         dataRow.Field<DateTime>("ROw_UPDATION_DATETIME").ToString("d MMM yyyy")
                         )).ToList();
                }
                if(lsIndexModel != null&&lsIndexModel.Count>0)
                {
                    objIndexModel = lsIndexModel[0];
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetInstructorTestDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objIndexModel;
        }
        public async Task<StudentIndexModal> GetStudentIndexDetails(long IndexId)
        {
            StudentIndexModal objIndexModel = null;
            try
            {
                List<StudentIndexModal> lsIndexModel = null;
                DataSet ds = await objCPDataService.GetIndexDetailsAsync(IndexId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    lsIndexModel = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new StudentIndexModal(
                         dataRow.Field<long>("ID"),
                         dataRow.Field<string>("INDEX_NAME"),
                         dataRow.Field<string>("INDEX_DESCRIPTION"),
                         dataRow.Field<DateTime>("ROW_INSERTION_DATETIME").ToString("d MMM yyyy"),
                         dataRow.Field<DateTime>("ROw_UPDATION_DATETIME").ToString("d MMM yyyy")
                         )).ToList();
                }
                if (lsIndexModel != null && lsIndexModel.Count > 0)
                {
                    objIndexModel = lsIndexModel[0];
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetInstructorTestDetails", Ex.ToString());
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
                List<AssignmentModel> lsAssignmentModel = null;
                DataSet ds = new DataSet();
                ds =await objCPDataService.GetAssignmentDetailsAsync(AssignmentId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    lsAssignmentModel = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new AssignmentModel(
                         dataRow.Field<string>("ASSIGNMENT_NAME"),
                         dataRow.Field<string>("ASSIGNMENT_DESCRIPTION"),
                         dataRow.Field<DateTime>("ROW_INSERTION_DATETIME").ToString("d MMM yyyy"),
                         dataRow.Field<DateTime>("ROw_UPDATION_DATETIME").ToString("d MMM yyyy"),
                         dataRow.Field<byte>("ASSIGNMENT_TYPE")
                         )).ToList();
                }
                if (lsAssignmentModel != null && lsAssignmentModel.Count > 0)
                {
                    objAssignmentModel = lsAssignmentModel[0];
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetInstructorTestDetails", Ex.ToString());
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
                List<AssignmentModel> lsAssignmentModel = null;
                DataSet ds = new DataSet();
                ds = await objCPDataService.GetIndependentAssignmentDetailsAsync(AssignmentId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    lsAssignmentModel = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new AssignmentModel(
                         dataRow.Field<string>("ASSIGNMENT_NAME"),
                         dataRow.Field<string>("ASSIGNMENT_DESCRIPTION"),
                         dataRow.Field<DateTime>("ROW_INSERTION_DATETIME").ToString("d MMM yyyy"),
                         dataRow.Field<DateTime>("ROw_UPDATION_DATETIME").ToString("d MMM yyyy"),
                         dataRow.Field<byte>("ASSIGNMENT_TYPE"),
                         dataRow.Field<string>("TINY_URL"),
                         dataRow.Field<string>("SHARE_CODE"),
                         dataRow.Field<bool>("IS_ACTIVE"),
                         dataRow.Field<int>("NO_OF_QUESTIONS")
                         )).ToList();
                }
                if (lsAssignmentModel != null && lsAssignmentModel.Count > 0)
                {
                    objAssignmentModel = lsAssignmentModel[0];
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetInstructorTestDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objAssignmentModel;
        }


        public async Task<GetAssignmentSubssionDetials> GetAssignmentResponse(long Submissionid, long StudentId)
        {
            GetAssignmentSubssionDetials objGetAssignmentSubssionDetials = null;
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
        public async Task< List<ActivityModal>> GetInstructorActivityDetails(int InstructorId)
        {
            List<ActivityModal> lsActivityModal = null;
            try
            {
                DataSet ds = await objCPDataService.GetInstructorActivityDetailsAsync(InstructorId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    lsActivityModal = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new ActivityModal(
                         dataRow.Field<DateTime>("ROW_INSERTION_DATETIME"),
                         dataRow.Field<DateTime>("ROW_INSERTION_DATETIME").ToString("d MMM yyyy"),
                         dataRow.Field<string>("ACTIVITY_MESSAGE")
                         
                         )).ToList();
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetInstructorTestDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return lsActivityModal;
        }
        public async Task<List<McqQuestion>> GetMcqQuestionDetails(long AssignmentId)
        {
            List<McqQuestion> lsMcqQuestions = null;
            try
            {
                DataSet ds = await objCPDataService.GetMcqAssignmentDetailsAsync(AssignmentId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    lsMcqQuestions = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new McqQuestion(
                         dataRow.Field<long>("ID"),
                         dataRow.Field<string>("QUESTION_STATEMENT"),
                         dataRow.Field<string>("OPTION1"),
                         dataRow.Field<string>("OPTION2"),
                         dataRow.Field<string>("OPTION3"),
                         dataRow.Field<string>("OPTION4"),
                         dataRow.Field<byte>("CORRECT_ANSWER"),
                         dataRow.Field<DateTime>("ROW_INSERTION_DATETIME").ToString("d MMM yyyy"),
                         dataRow.Field<DateTime>("ROW_UPDATION_DATETIME").ToString("d MMM yyyy")
                         )).ToList();
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetMcqQuestionDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return lsMcqQuestions;
        }
        public async Task<bool> DeleteTest(long TestId)
        {
            bool result = false;
            try
            {
                result = await objCPDataService.DeleteTestAsync(TestId);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "DeleteTest", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<bool> DeleteTestOfCourse(long TestId)
        {
            bool result = false;
            try
            {
                result = await objCPDataService.DeleteTestOfCourseAsync(TestId);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "DeleteTestOfCourse", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<bool> ActivateAssignment(long Assignmentid,string ShareCode, string TinyUrl)
        {
            bool result = false;
            try
            {
                result =await objCPDataService.ActivateAssignmentAsync(Assignmentid, ShareCode,TinyUrl);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "ActivateAssignment", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<bool> DeleteAssignment(long AssignmentId)
        {
            bool result = false;
            try
            {
                result = await objCPDataService.DeleteAssignmentOfCourseAsync(AssignmentId);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "DeleteAssignment", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<bool> DeleteIndependentAssignment(long AssignmentId)
        {
            bool result = false;
            try
            {
                result = await objCPDataService.DeleteInpependentAssignmentAsync(AssignmentId);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "DeleteIndependentAssignment", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<bool> DeleteIndependentTest(long TestId)
        {
            bool result = false;
            try
            {
                result = await objCPDataService.DeleteIndepenetTestAsync(TestId);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "DeleteIndependentTest", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }

        public async Task<bool> ActivateTest(long TestId,string TinyUrl,string AccesssCode)
        {
            bool result = false;
            try
            {
                result = await objCPDataService.ActivateTestAsync(TestId, AccesssCode,TinyUrl);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "ActivateTest", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<bool> InserContatUsRequest(ContactUsApiRequest objContactUsApiRequest)
        {
            bool result = false;
            try
            {
                result = await objCPDataService.InsertContactFormDetailsAsync(objContactUsApiRequest.m_strName,objContactUsApiRequest.m_strEmail,
                                        objContactUsApiRequest.m_strPhoneNo,objContactUsApiRequest.m_strSubject,objContactUsApiRequest.m_strMessage);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "InserContatUsRequest", Ex.ToString());
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
                result = await objCPDataService.DeleteMcqQuestionOfAssignmentAsync(QuestionId);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "ActivateTest", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<bool> UpdateMcqAssignmentQuestion(McqQuestion objMcqQuestion)
        {
            bool result = false;
            try
            {
                result = await objCPDataService.UpdateMcqQuestionOfAssignmentAsync(objMcqQuestion.m_llQuestionId,objMcqQuestion.m_strQuestionStatement,
                                   objMcqQuestion.m_strOption1,objMcqQuestion.m_strOption2,objMcqQuestion.m_strOption3,objMcqQuestion.m_strOption4,(byte)objMcqQuestion.m_iCorrectOption);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "ActivateTest", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<bool> DeleteIndexTopic(long TopicId)
        {
            bool result = false;
            try
            {
                result =await objCPDataService.DeleteIndexTopicAsync(TopicId);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "DeleteIndexTopic", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<bool> UpdateIndexTopic(TopicModel objTopicModel)
        {
            bool result = false;
            try
            {
                result =await objCPDataService.UpdateIndexTopicAsync(objTopicModel.m_llTopicId,objTopicModel.m_strTopicName,objTopicModel.m_strTopicDescription,
                                objTopicModel.m_strFilePath,objTopicModel.m_iFileUploadTypeIdNew);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "ActivateTest", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<bool> DeleteCourseIndex(long IndexId)
        {
            bool result = false;
            try
            {
                result = await objCPDataService.DeleteIndexAsync(IndexId);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "DeleteCourseIndex", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<bool> UpdateCourseIndex(IndexModel objIndexModel)
        {
            bool result = false;
            try
            {
                result = await objCPDataService.UpdateCourseIndexDetailsAsync(objIndexModel.m_llIndexId,objIndexModel.m_strIndexName,objIndexModel.m_strIndexDescription);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "DeleteCourseIndex", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<bool> UpdateFullCourseDetails(CourseDetailsModel objCourse)
        {
            bool result = false;
            try
            {
                result = await objCPDataService.UpdateFullCourseDetailsAsync(objCourse.m_llCourseid,objCourse.m_strCourseName,objCourse.m_strCourseDescription);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "UpdateFullCourseDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<bool> UpdateTestDetails(TestDetailsModel objTestDetails)
        {
            bool result = false;
            try
            {
                result = await objCPDataService.UpdateTestDetailsAsync(objTestDetails.m_llTestId,objTestDetails.m_strTestName,objTestDetails.m_strTestDescription,"",0);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "UpdateTestDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<bool> AddMcqTestQuestion(McqQuestion objMcqQuestion)
        {
            bool result = false;
            try
            {
                result =await  objCPDataService.InsertNewMcqTestQuestionAsync(objMcqQuestion.m_llTestId, objMcqQuestion.m_strQuestionStatement, objMcqQuestion.m_strOption1, objMcqQuestion.m_strOption2,
                    objMcqQuestion.m_strOption3, objMcqQuestion.m_strOption4, objMcqQuestion.m_iCorrectOption, objMcqQuestion.m_iTimeInSeconds, objMcqQuestion.m_iMarks);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "AddMcqTestQuestion", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<bool> UpdateMcqTestQuestion(McqQuestion objMcqQuestion)
        {
            bool result = false;
            try
            {
                result = await objCPDataService.UpdateMcqQuestionDetailsAsync(objMcqQuestion.m_llTestId, objMcqQuestion.m_strQuestionStatement, objMcqQuestion.m_strOption1, objMcqQuestion.m_strOption2,
                    objMcqQuestion.m_strOption3, objMcqQuestion.m_strOption4, (byte)objMcqQuestion.m_iCorrectOption, objMcqQuestion.m_iTimeInSeconds, objMcqQuestion.m_iMarks);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "AddMcqTestQuestion", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<bool> DeleteMcqTestQuestion(long QuestionId)
        {
            bool result = false;
            try
            {
                result =await objCPDataService.DeleteMcqTestQuestionAsync(QuestionId);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "DeleteMcqTestQuestion", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<bool> InsertNewTestToCourse(TestModel objTestModel)
        {
            bool result = false;
            try
            {
                result = await objCPDataService.InsertNewTestToCourseAsync(objTestModel.m_strTestName, objTestModel.m_strTestDescription, (byte)Constants.TestQuestionType.MCQ, null,null,
                    objTestModel.m_llCourseId);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "InsertNewTestToCourse", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<bool> InsertNewAssignmentToCourse(AssignmentModel objAssignmentModel)
        {
            bool result = false;
            try
            {
                result = await objCPDataService.InsertNewAssignmentToCourseAsync(objAssignmentModel.m_strAssignmentName,objAssignmentModel.m_strAssignmentDescription,(byte)objAssignmentModel.m_iAssignmentType,
                                              null,null,objAssignmentModel.m_llCourseId);
                
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "InsertNewAssignmentToCourse", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<List<BasicAssignmentDetails>> GetAssignmentForCourse(long CourseId)
        {
            List<BasicAssignmentDetails> lsAssignmentDetailsModel = null;
            try
            {
                DataSet ds = await objCPDataService.GetAllAssignmentsForCourseAsync(CourseId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    lsAssignmentDetailsModel = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new BasicAssignmentDetails(
                         dataRow.Field<long>("ASSIGNMENT_ID"),
                         dataRow.Field<string>("ASSIGNMENT_NAME")
                         )).ToList();
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetAssignmentForInstructor", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return lsAssignmentDetailsModel;
        }
        public async Task<List<BasicTestDetails>> GetTestOfCourse(long CourseId)
        {
            List<BasicTestDetails> lsBasicTestDetails = null;
            try
            {
                DataSet ds = new DataSet();
                ds = await objCPDataService.GetTestsOfCourseAsync(CourseId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    lsBasicTestDetails = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new BasicTestDetails(
                         dataRow.Field<long>("TEST_ID"),
                         dataRow.Field<string>("TEST_NAME")
                         )).ToList();
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetAssignmentForInstructor", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return lsBasicTestDetails;
        }
        public async Task<bool> InsertSubjectiveAssignmentQuestion(SubjectiveQuestion objSubjectiveQuestion)
        {
            bool result = false;
            try
            {
                result =await objCPDataService.InsertSubjectiveAssignmentQuestionAsync(objSubjectiveQuestion.m_llAsssignmentId,
                    objSubjectiveQuestion.m_strQuestionStatement, objSubjectiveQuestion.m_strHint);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "InsertSubjectiveAssignmentQuestion", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<bool> UpdateSubjectiveAssignmentQuestion(SubjectiveQuestion objSubjectiveQuestion)
        {
            bool result = false;
            try
            {
                result =await objCPDataService.UpdateSubjectiveAssignmentQuestionAsync(objSubjectiveQuestion.m_llQuestionId,
                                 objSubjectiveQuestion.m_strQuestionStatement, objSubjectiveQuestion.m_strHint);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "UpdateSubjectiveAssignmentQuestion", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task< bool> DeleteSubjectiveAssignmentQuestion(long QuestionId)
        {
            bool result = false;
            try
            {
                result = await objCPDataService.DeleteSubjectiveAssignmentQuestionAsync(QuestionId);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "DeleteSubjectiveAssignmentQuestion", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<bool> DeleteSubjectiveAssignmentOfCourse(long AssignmentId)
        {
            bool result = false;
            try
            {
                result = await objCPDataService.DeleteSubjectiveAssignmentOfCourseAsync(AssignmentId);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "DeleteSubjectiveAssignmentQuestion", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<List<AssignmentSubmissionResponseModal>> GetAllSubmissionsOfAnAssignment(long AssignmentId)
        {
            List<AssignmentSubmissionResponseModal> lsAssignmentSubmissionResponseModal = new List<AssignmentSubmissionResponseModal>();
            try
            {
                DataSet ds = await objCPDataService.GetAllAssignmentsSubmissionsAsync(AssignmentId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    //  public AssignmentSubmissionResponseModal(string StudentName,long SubmissionId,long StudentId,string SubmissionDate,int PercentageScore)
                    lsAssignmentSubmissionResponseModal = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new AssignmentSubmissionResponseModal(
                         dataRow.Field<string>("STUDENT_NAME"),
                         dataRow.Field<long>("SUBMISSION_ID"),
                         dataRow.Field<long>("STUDENT_ID"),
                         dataRow.Field<DateTime>("ROW_INSERTION_DATETIME").ToString("d MMM yyyy"),
                         dataRow.Field<int>("ASSIGNMANT_PERCENTAGE_SCORE")
                         )).ToList();
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetCourseIndexDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return lsAssignmentSubmissionResponseModal;
        }
        public async Task<List<AssignmentSubmissionResponseModal>> GetAllTestSubmissions(long TestId)
        {
            List<AssignmentSubmissionResponseModal> lsAssignmentSubmissionResponseModal = new List<AssignmentSubmissionResponseModal>();
            try
            {
                DataSet ds = await objCPDataService.GetAllTestSubmissionsAsync(TestId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    //  public AssignmentSubmissionResponseModal(string StudentName,long SubmissionId,long StudentId,string SubmissionDate,int PercentageScore)
                    lsAssignmentSubmissionResponseModal = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new AssignmentSubmissionResponseModal(
                         dataRow.Field<string>("STUDENT_NAME"),
                         dataRow.Field<long>("SUBMISSION_ID"),
                         dataRow.Field<long>("STUDENT_ID"),
                         dataRow.Field<DateTime>("ROW_INSERTION_DATETIME").ToString("d MMM yyyy"),
                         dataRow.Field<int>("TEST_SCORE")
                         )).ToList();
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetCourseIndexDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return lsAssignmentSubmissionResponseModal;
        }
        public async Task<List<CoursesJoinedResponseModal>> GetAllStudentsJoinedToCourse(long CourseId)
        {
            List<CoursesJoinedResponseModal> lsCoursesJoinedResponseModal = new List<CoursesJoinedResponseModal>();
            try
            {
                DataSet ds = await objCPDataService.GetStudentJoinedToCourseAsync(CourseId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    //  public AssignmentSubmissionResponseModal(string StudentName,long SubmissionId,long StudentId,string SubmissionDate,int PercentageScore)
                    lsCoursesJoinedResponseModal = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new CoursesJoinedResponseModal(
                         dataRow.Field<string>("STUDENT_NAME"),
                         dataRow.Field<DateTime>("ROW_INSERTION_DATETIME").ToString("d MMM yyyy"),
                         dataRow.Field<long>("STUDENT_ID"),
                         dataRow.Field<string>("PROFILE_URL")
                         )).ToList();
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetCourseIndexDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return lsCoursesJoinedResponseModal;
        }
        public async Task<List<CoursesJoinedResponseModal>> GetAllStudentsJoinedToInstructor(int InstructorId,MasterSearchRequest masterSearchRequest)
        {
            List<CoursesJoinedResponseModal> lsCoursesJoinedResponseModal = new List<CoursesJoinedResponseModal>();
            try
            {
                DataSet ds = await objCPDataService.GetAllStudentsJoinedToInstructorAsync(InstructorId, masterSearchRequest.m_iNoOfRowsFetched, masterSearchRequest.m_strSearchString);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    //  public AssignmentSubmissionResponseModal(string StudentName,long SubmissionId,long StudentId,string SubmissionDate,int PercentageScore)
                    lsCoursesJoinedResponseModal = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new CoursesJoinedResponseModal(
                         dataRow.Field<string>("STUDENT_NAME"),
                         dataRow.Field<DateTime>("ROW_INSERTION_DATETIME").ToString("d MMM yyyy"),
                         dataRow.Field<long>("STUDENT_ID"),
                         dataRow.Field<string>("PROFILE_URL")
                         )).ToList();
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetAllStudentsJoinedToInstructor", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return lsCoursesJoinedResponseModal;
        }
        public bool InsertNewCourseV2(InsertCourseV2Request objInsertCourseV2Request)
        {
            bool result = false;
            try
            {
                result = objCPDataService.InsertNewCourseV2(objInsertCourseV2Request.m_strCourseName, objInsertCourseV2Request.m_strCourseDescription,
                             objInsertCourseV2Request.m_iInstructorId,objInsertCourseV2Request.m_strAboutCourse,objInsertCourseV2Request.m_strCourseDescription,
                             ref objInsertCourseV2Request.m_llCourseId);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "DeleteSubjectiveAssignmentQuestion", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<bool> InsertNewAlertForInstructor(int InstructorId, string AlertMessage,int AlertTypeId,long StudentId,long? EffectiveId)
        {
            bool result = false;
            try
            {
                result = await objCPDataService.InsertNewAlertForInstructorAsync(InstructorId, AlertMessage, AlertTypeId, EffectiveId, StudentId);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "InsertNewAlertForInstructor", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<List<AlertDetailsModal>> GetAllAlertOfInstructor(int InstructorId)
        {
            List<AlertDetailsModal> lsAlertDetailsModal = new List<AlertDetailsModal>();
            try
            {
                DataSet ds = await objCPDataService.GetAllAlertForInstructorAsync(InstructorId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    //  public AssignmentSubmissionResponseModal(string StudentName,long SubmissionId,long StudentId,string SubmissionDate,int PercentageScore)
                    lsAlertDetailsModal = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new AlertDetailsModal(
                         dataRow.Field<long>("ID"),
                          dataRow.Field<string>("ALERT_MESSAGE"),
                         dataRow.Field<DateTime>("ROW_INSERTION_DATETIME").ToString("d MMM yyyy"),
                         dataRow.Field<string>("ALERT_TYPE_IOCN")
                         )).ToList();
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetCourseIndexDetails", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return lsAlertDetailsModal;
        }
        public bool GetInstructorIdByCourseId(ref int InstructorId,long CourseId)
        {
            bool result = false;
            try
            {
                result = objCPDataService.GetInstructorIdByCourseId(CourseId,ref InstructorId);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "InsertNewAlertForInstructor", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public bool GetInstructorIdByClassroomId(ref int InstructorId, long ClassroomId)
        {
            bool result = false;
            try
            {
                result = objCPDataService.GetInstructorIdByClassroomId(ClassroomId, ref InstructorId);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "InsertNewAlertForInstructor", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public bool GetInstructorIdByAssignmentId(ref int InstructorId, long AssignmentId)
        {
            bool result = false;
            try
            {
                result = objCPDataService.GetInstructorIdByAssignmentId(AssignmentId, ref InstructorId);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetInstructorIdByAssignmentId", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public bool GetInstructorIdByTestId(ref int InstructorId, long TestId)
        {
            bool result = false;
            try
            {
                result = objCPDataService.GetInstructorIdByTestId(TestId, ref InstructorId);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetInstructorIdByTestId", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<List<AssignmentDetailsModel>> SearchForAssignmentOfInstructor(string SearchString, int MaxRowToReturn,int InstructorId)
        {
            List<AssignmentDetailsModel> lsAssignmentDetailsModel = null;
            try
            {
                DataSet ds = await objCPDataService.SearchForAssignmentOfInstructorAsync(SearchString, MaxRowToReturn, InstructorId);
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
        public async Task<StudentAssignmentProgressModal> GetStudentAssignmentProgress(long AssignmentId,long StudentId)
        {
            StudentAssignmentProgressModal objAssignmentDetailsModel = null;
            try
            {
                DataSet ds = await objCPDataService.GetStudentAssignmentProgressAsync(AssignmentId, StudentId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    objAssignmentDetailsModel = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new StudentAssignmentProgressModal(
                         dataRow.Field<long>("ASSIGNMENT_ID"),
                         dataRow.Field<string>("ASSIGNMENT_NAME"),
                         dataRow.Field<string>("ASSIGNMENT_DESCRIPTION"),
                         dataRow.Field<string>("ASSIGNMENT_TYPE"),
                         dataRow.Field<int>("NO_OF_QUESTIONS"),
                         dataRow.Field<int>("NO_OF_SUBJECTIVE_QUESTIONS"),
                         dataRow.Field<long?>("SUBMISSION_ID"),
                         0,
                         dataRow.Field<int?>("ASSIGNMANT_PERCENTAGE_SCORE")
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
            return objAssignmentDetailsModel;
        }
        public async Task<List<TestDetailsModel>> SearchForTestOfInstructor(string SearchString, int MaxRowToReturn,int InstructorId)
        {
            List<TestDetailsModel> lsTestDetailsModel = null;
            try
            {
                DataSet ds = await objCPDataService.SearchForTestOfInstructorAsync(SearchString, MaxRowToReturn, InstructorId);
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
        public async Task<List<CourseDetailsModel>> SearchForCourseOfInstructor(string SearchString, int MaxRowToReturn,int InstructorId)
        {
            List<CourseDetailsModel> lsCourseDetailsModel = null;
            try
            {
                DataSet ds = await objCPDataService.SearchForCourseOfInstructorAsync(SearchString, MaxRowToReturn, InstructorId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    lsCourseDetailsModel = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new CourseDetailsModel(
                         dataRow.Field<long>("COURSE_ID"),
                         dataRow.Field<string>("COURSE_NAME"),
                         dataRow.Field<string>("COURSE_DESCRIPTION"),
                         dataRow.Field<DateTime>("COURSE_ACTIVATION_DATETIME").ToString("d MMM yyyy")
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
        public async Task<GetTestSubmissionDetailsResponse> GetTestResponse(long Submissionid, long StudentId)
        {
            GetTestSubmissionDetailsResponse objGetTestSubmissionDetailsResponse = null;
            try
            {
                DataSet ds = await objCPDataService.GetTestResponseAsync(Submissionid, StudentId);
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
        public async Task<long> InsertNewClassroom(ClassRoomModal objClassRoomModal)
        {
            long ClassRoomId=-1;
            try
            {
                ClassRoomId = await objCPDataService.InsertNewClassRoomForInstructorAsync(objClassRoomModal.m_iInstrutcorId,
                    objClassRoomModal.m_strClassRoomName, objClassRoomModal.m_strClassRoomDescription,
                    objClassRoomModal.imageUploadDetailsModal.m_strOriginalFileUrl, 
                    objClassRoomModal.m_strClassroomMeetingName,
                    objClassRoomModal.imageUploadDetailsModal.m_strSmallSizeUrl,
                    objClassRoomModal.imageUploadDetailsModal.m_strMediumSizeUrl, objClassRoomModal.m_iNoOfDemoSessions);
            }
            catch(Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "InsertNewClassroom", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return ClassRoomId;
        }
        public async Task<List<ClassRoomModal>> GetAllClassroomForIsntrcutor(int InstrucorId)
        {
            List<ClassRoomModal> objClassRoomModal = new List<ClassRoomModal>();
            try
            {
                DataSet ds = await objCPDataService.GetAllClassRoomForInstrcutorAsync(InstrucorId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    objClassRoomModal = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new ClassRoomModal(
                         dataRow.Field<long>("CLASSROOM_ID"),
                         dataRow.Field<string>("CLASSROOM_NAME"),
                         dataRow.Field<DateTime>("ROW_INSERTION_DETATIME").ToString("d MMM yyyy"),
                         dataRow.Field<string>("COURSE_STATUS_NAME"),
                         dataRow.Field<int>("NO_OF_STUDENTS_JOINED")
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
        //public async Task<ClassRoomModal> GetClassroomDetailsForInstructor(long ClassroomId,int InstructorId)
        //{
        //    ClassRoomModal objClassRoomModal = new ClassRoomModal();
        //    try
        //    {
        //        DataSet ds = await objCPDataService.GetClassRoomDetailsForInstructorAsync( InstructorId, ClassroomId);
        //        if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
        //        {
        //            objClassRoomModal = ds.Tables[0].AsEnumerable().Select(
        //             dataRow => new ClassRoomModal(
        //                 dataRow.Field<long>("CLASSROOM_ID"),
        //                 dataRow.Field<string>("CLASSROOM_NAME"),
        //                  dataRow.Field<string>("CLASSROOM_DESCRIPTION"),
        //                 dataRow.Field<DateTime>("ROW_INSERTION_DETATIME").ToString("d MMM yyyy"),
        //                 dataRow.Field<string>("COURSE_STATUS_NAME"),
        //                 dataRow.Field<int>("NO_OF_POSTS"),
        //                  dataRow.Field<string>("SHARE_URL"),
        //                   dataRow.Field<string>("SHARE_CODE")
        //                 )).ToList()[0];
        //        }
        //    }
        //    catch (Exception Ex)
        //    {
        //        m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
        //        m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetAllClassroomForIsntrcutor", Ex.ToString());
        //        m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
        //        MainLogger.Error(m_strLogMessage);
        //    }
        //    return objClassRoomModal;
        //}
        public async Task<bool> InsertNewPostToClassroom(ClassroomPostModal objClassroomPostModal)
        {
            bool result = false;
            try
            {
                result = await objCPDataService.InertNewPostToClassroomAsync(objClassroomPostModal.m_llClassroomId,
                    objClassroomPostModal.m_strPost);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "InsertNewClassroom", Ex.ToString());
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
                result = await objCPDataService.ReportMeetingLeftForHostAsync(MeetingId);
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
            List<ClassroomMeetingModal> lsClassroomMeetingModal = new List<ClassroomMeetingModal>();
            try
            {
                DataSet ds = await objCPDataService.GetAllMeetingForClassroomAsync(ClassroomId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    lsClassroomMeetingModal = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new ClassroomMeetingModal(
                         dataRow.Field<long>("MEETING_ID"),
                         dataRow.Field<DateTime>("MEETING_START_TIME").ToString("f"),
                         dataRow.Field<DateTime?>("MEETING_CLOSE_TIME")==null? Constants.CLASSROOM_MEETING_NOT_CLOSED_MESSAGE : dataRow.Field<DateTime>("MEETING_CLOSE_TIME").ToString("hh:mm tt"),
                         dataRow.Field<bool>("IS_ACTIVE"),
                         dataRow.Field<int>("NO_OF_PARTICIPANTS"),
                         dataRow.Field<string>("MEETING_TOPIC"),
                         dataRow.Field<string>("MEETING_DESCRIPTION"),
                         dataRow.Field<string>("VIDEO_URL")
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
            return lsClassroomMeetingModal;
        }
        public async Task<List<StudentClassrromJoinModal>> GetAllStudentsJoinedToClassroomResponse(long ClassroomId)
        {
            List<StudentClassrromJoinModal> lsClassroomMeetingModal = new List<StudentClassrromJoinModal>();
            try
            {
                DataSet ds = await objCPDataService.GetAllStudentsJoinedToClassroomAsync(ClassroomId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    lsClassroomMeetingModal = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new StudentClassrromJoinModal(
                         dataRow.Field<long>("STUDENT_ID"),
                         dataRow.Field<string>("STUDENT_NAME").ToString(),
                         dataRow.Field<DateTime>("ROW_INSERTION_DATETIME").ToString("d MMM yyyy"))).ToList();
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetAllClassroomForIsntrcutor", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return lsClassroomMeetingModal;
        }
        public async Task<List<StudentMeetingJoinedResponse>> GetAllStudentsJoinedToMeetingResponse(long MeetingId,long ClassroomId)
        {
            List<StudentMeetingJoinedResponse> lsClassroomMeetingModal = new List<StudentMeetingJoinedResponse>();
            try
            {
                DataSet ds = await objCPDataService.GetAllStudentsJoinedToMeetingAsync(ClassroomId,MeetingId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    lsClassroomMeetingModal = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new StudentMeetingJoinedResponse(
                         dataRow.Field<long>("STUDENT_ID"),
                         dataRow.Field<string>("STUDENT_NAME").ToString(),
                         dataRow.Field<DateTime>("MEETING_JOIN_TIME").ToString("hh:mm:ss tt"))).ToList();
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetAllClassroomForIsntrcutor", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return lsClassroomMeetingModal;
        }
        public async Task<bool> InsertNewMessageToClassroom(InsertInstructorMessageToClassroom insertInstructorMessageToClassroom)
        {
            bool result = false;
            try
            {
                result = await objCPDataService.InsertNewInstructorClassroomMessageAsync(insertInstructorMessageToClassroom.m_llClassroomId,
                    insertInstructorMessageToClassroom.m_strMessage);
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
        public async Task<List<ClassroomInstructorMessageModal>> GetAllClassroomMessageForInstructor(long ClassroomId)
        {
            List<ClassroomInstructorMessageModal> lsResponse = new List<ClassroomInstructorMessageModal>();
            try
            {
                DataSet ds = await objCPDataService.GetAllClassroomMessageAsync(ClassroomId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    lsResponse = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new ClassroomInstructorMessageModal(
                         dataRow.Field<string>("MESSAGE"),
                         dataRow.Field<string>("STUDENT_NAME"),
                         dataRow.Field<DateTime>("ROW_INSERTION_DATETIME").ToString("f"),
                         dataRow.Field<long>("MESSAGE_ID"),
                         dataRow.Field<bool>("IS_INSTRUCTOR")
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
        public async Task<List<ClassroomInstructorMessageModal>> GetAllClassroomLastMessagesForInstructor(long ClassroomId,long LastMessageId)
        {
            List<ClassroomInstructorMessageModal> lsResponse = new List<ClassroomInstructorMessageModal>();
            try
            {
                DataSet ds = await objCPDataService.GetAllClassroomMessageAfterLastMessageAsync(ClassroomId, LastMessageId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    lsResponse = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new ClassroomInstructorMessageModal(
                         dataRow.Field<string>("MESSAGE"),
                         dataRow.Field<string>("STUDENT_NAME"),
                         dataRow.Field<DateTime>("ROW_INSERTION_DATETIME").ToString("f"),
                         dataRow.Field<long>("MESSAGE_ID"),
                         dataRow.Field<bool>("IS_INSTRUCTOR")
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
        public async Task<bool> DeleteClassroom(long ClassroomId)
        {
            bool result = false;
            try
            {
                result = await objCPDataService.DeleteClassroomAsync(ClassroomId);
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
                result = await objCPDataService.UpdateClassroomDetailsAsync(classRoomModal.m_llClassRoomId,
                    classRoomModal.m_strClassRoomName, classRoomModal.m_strClassRoomDescription,classRoomModal.m_dtClassStartDate,
                    classRoomModal.m_dtRegistrationCloseDate, classRoomModal.m_iNoOfDemoSessions);
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
            bool result = false;
            try
            {
                result = await objCPDataService.AddNewAssignmentToClassroomAsync(ClassroomId, AssignmentId);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "InsertnewAssignmentToClassroom", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<List<ClassroomAssignmentModal>> GetAllClassroomAssignments(long ClassroomId)
        {
            List<ClassroomAssignmentModal> lsResponse = new List<ClassroomAssignmentModal>();
            try
            {
                DataSet ds = await objCPDataService.GetAllClassroomAssignmentsAsync(ClassroomId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    lsResponse = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new ClassroomAssignmentModal(
                         dataRow.Field<string>("ASSIGNMENT_NAME"),
                         dataRow.Field<int>("NO_OF_SUBMISSIONS"),
                           dataRow.Field<long>("ASSIGNMENT_ID"),
                         dataRow.Field<DateTime>("ROW_INSERTION_DATETIME").ToString("d MMM yyyy")
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
        public async Task<bool> InsertnewTestToClassroom(long TestId, long ClassroomId)
        {
            bool result = false;
            try
            {
                result = await objCPDataService.AddNewTestToClassroomAsync(ClassroomId,TestId);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "InsertnewAssignmentToClassroom", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<List<ClassroomTestModal>> GetAllClassroomTests(long ClassroomId)
        {
            List<ClassroomTestModal> lsResponse = new List<ClassroomTestModal>();
            try
            {
                DataSet ds = await objCPDataService.GetAllClassroomTestAsync(ClassroomId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    lsResponse = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new ClassroomTestModal(
                         ClassroomId,
                         dataRow.Field<long>("TEST_ID"),
                         dataRow.Field<string>("TEST_NAME"),
                         dataRow.Field<DateTime>("ROW_INSERTION_DATETIME").ToString("d MMM yyyy"),
                         dataRow.Field<int>("NO_OF_SUBMISSIONS"),
                         dataRow.Field<int>("NO_OF_QUESTIONS")
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
        public async Task<bool> DeleteClassroomTest(long ClassroomId,long TestId)
        {
            bool result = false;
            try
            {
                result = await objCPDataService.DeleteClassroomTestAsync(ClassroomId,TestId);
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
        public async Task<List<ClassroomBasicDetailsModal>> GetInstructorSearchResultForClassrom(int InstructorId,string SearchString)
        {
            List<ClassroomBasicDetailsModal> lsResponse = new List<ClassroomBasicDetailsModal>();
            try
            {
                DataSet ds = await objCPDataService.GetInstructorClassroomSearchDetailsAsync(InstructorId,SearchString);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    lsResponse = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new ClassroomBasicDetailsModal(
                         dataRow.Field<long>("CLASSROOM_ID"),
                         dataRow.Field<string>("CLASSROOM_NAME"),
                         dataRow.Field<DateTime>("ROW_INSERTION_DETATIME").ToString("d MMM yyyy"),
                         dataRow.Field<string>("STATUS")
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
        public async Task<bool> UpdateInstructorProfilePicture(InstructorProfileChangeRequest instructorProfileChangeRequest )
        {
            bool result = false;
            try
            {
                result = await objCPDataService.UpdateInstructorProfilePictureAsync(instructorProfileChangeRequest.m_iInstructorId, instructorProfileChangeRequest.imageUploadDetailsModal.m_strOriginalFileUrl,
                    instructorProfileChangeRequest.imageUploadDetailsModal.m_strMediumSizeUrl,
                    instructorProfileChangeRequest.imageUploadDetailsModal.m_strSmallSizeUrl);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "UpdateInstructorProfilePicture", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<bool> AddAttachmentToClassroom(ClassroomAttachmentRequest classroomAttachmentRequest)
        {
            bool result = false;
            try
            {
                result = await objCPDataService.InsertClassroomAttachmentAsync(classroomAttachmentRequest.m_llClassroomId, classroomAttachmentRequest.m_strAttachmentName,
                    classroomAttachmentRequest.m_strAttachmentDescription, classroomAttachmentRequest.m_strAttachmentUrl);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "UpdateInstructorProfilePicture", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<List<ClassroomAttachmentDetailsResponse>> GetAllClassroomAttachments(long ClassroomId)
        {
            List<ClassroomAttachmentDetailsResponse> lsResponse = new List<ClassroomAttachmentDetailsResponse>();
            try
            {
                DataSet ds = await objCPDataService.GetClassroomAttachmentsAsync(ClassroomId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    lsResponse = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new ClassroomAttachmentDetailsResponse(
                         dataRow.Field<long>("CLASSROOM_ATTACHMENT_ID"),
                         dataRow.Field<string>("ATTACHMENT_NAME"),
                         dataRow.Field<string>("ATTACHMENT_DESCRIPTION"),
                         dataRow.Field<DateTime>("ROW_INSERTION_DATETIME").ToString("d MMM yyyy"),
                          dataRow.Field<string>("ATTACMENT_URL")
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
        public async Task<bool> DeleteClassroomAttachment(long AttachmentId)
        {
            bool result = false;
            try
            {
                result = await objCPDataService.DeleteClassroomAttachmentAsync(AttachmentId);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "UpdateInstructorProfilePicture", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<bool> InsertClassroomSchedule(long ClassroomId,string ClassroomScheduleObj)
        {
            bool result = false;
            try
            {
                result = await objCPDataService.InsertClassroomScheduleAsync(ClassroomId, ClassroomScheduleObj);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "UpdateInstructorProfilePicture", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<bool> UpdateClassroomSchedule(long ClassroomId, string ClassroomScheduleObj)
        {
            bool result = false;
            try
            {
                result = await objCPDataService.UpdateClassroomScheduleAsync(ClassroomId, ClassroomScheduleObj);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "UpdateInstructorProfilePicture", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
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
        //public bool GetClassroomScheduleDetails(string TestSeriesName,string TestSeriesDescription,ref long ClassroomId)
        //{
        //    ClassroomScheduleDTO classroomScheduleDTO = null;
        //    try
        //    {
        //        DataSet ds = await objCPDataService.InsertNewTestSeriesAsync(ClassroomId);
        //        if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
        //        {
        //            classroomScheduleDTO = ds.Tables[0].AsEnumerable().Select(
        //             dataRow => new ClassroomScheduleDTO(
        //                 dataRow.Field<string>("CLASSROOM_SCHEDULE_OBJ"),
        //                 dataRow.Field<DateTime?>("CLASSROOM_SCHEDULE_LAST_UPDATION_TIME"),
        //                 dataRow.Field<DateTime?>("CLASSROOM_SCHEDULE_SET_TIME")
        //                 )).ToList()[0];
        //        }
        //    }
        //    catch (Exception Ex)
        //    {
        //        m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
        //        m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "UpdateInstructorProfilePicture", Ex.ToString());
        //        m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
        //        MainLogger.Error(m_strLogMessage);
        //    }
        //    return classroomScheduleDTO;
        //}
        public async Task<bool> UpdateClassroomImage(UpdateClassroomBackgroundImageRequest updateClassroomBackgroundImageRequest)
        {
            bool result = false;
            try
            {
                result = await objCPDataService.UpdateClassroomBackgroundAsync(updateClassroomBackgroundImageRequest.m_llClassroomId,
                    updateClassroomBackgroundImageRequest.imageUploadDetailsModal.m_strOriginalFileUrl,
                    updateClassroomBackgroundImageRequest.imageUploadDetailsModal.m_strSmallSizeUrl,
                    updateClassroomBackgroundImageRequest.imageUploadDetailsModal.m_strMediumSizeUrl);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "UpdateInstructorProfilePicture", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<bool> UpdateCourseImage(UpdateCourseBackgroundImageRequest updateCourseBackgroundImageRequest)
        {
            bool result = false;
            try
            {
                result = await objCPDataService.UpdateCourseBackgroundAsync(updateCourseBackgroundImageRequest.m_llCourseId,
                    updateCourseBackgroundImageRequest.imageUploadDetailsModal.m_strOriginalFileUrl,
                    updateCourseBackgroundImageRequest.imageUploadDetailsModal.m_strSmallSizeUrl,
                    updateCourseBackgroundImageRequest.imageUploadDetailsModal.m_strMediumSizeUrl);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "UpdateCourseImageRequest", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
          public async Task<bool> InsertInstructorContatUsRequest(IntructorContactUsRequest intructorContactUsRequest)
        {
            bool result = false;
            try
            {
                result = await objCPDataService.InsertInstructorContactUsDetailAsync(intructorContactUsRequest.m_iInstructorId,
                    intructorContactUsRequest.m_strEmail, intructorContactUsRequest.m_strPhoneNo, intructorContactUsRequest.m_strSubject, intructorContactUsRequest.m_strMessage);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "InsertInstructorContatUsRequest", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
     
    }
}
 