using Newtonsoft.Json;
using StudentDashboard.BusinessLayer;
using StudentDashboard.DTO;
using StudentDashboard.HttpRequest;
using StudentDashboard.Models;
using StudentDashboard.Models.Classroom;
using StudentDashboard.Models.Course;
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
    public class InstructorService
    {
        InstructorDTO objInstructorDTO;
        DocumentService objDocumentService;
        InstructorBusinessLayer objInstructorBusinessLayer;
        SMSServiceManager objSMSServiceManager;
        StringBuilder m_strLogMessage;
        ImageCompressionUtilities imageCompressionUtilities;
        public InstructorService()
        {
            objInstructorDTO = new InstructorDTO();
            objDocumentService = new DocumentService();
            objInstructorBusinessLayer = new InstructorBusinessLayer();
            objSMSServiceManager = new SMSServiceManager();
            m_strLogMessage = new StringBuilder();
        }
        public async Task<bool> RegisterNewUser(InstructorRegisterModel objInstructorRegisterModel)
        {
            bool result = false;
            try
            {
                string EncryptedPassword = SHA256Encryption.ComputeSha256Hash(objInstructorRegisterModel.m_strPassword);
                objInstructorRegisterModel.m_strPassword = EncryptedPassword;
                objInstructorRegisterModel.m_strPhoneNoVarificationGuid = objInstructorBusinessLayer.GetSmsVerificationString();
                objInstructorRegisterModel.m_strEmailVarificationGuid = objInstructorBusinessLayer.GetEmailVerficationString();
                objInstructorRegisterModel.m_strPhoneNo = objInstructorRegisterModel.m_strCountryCode + objInstructorRegisterModel.m_strPhoneNo;
                if (await objInstructorDTO.RegisterNewInstructor(objInstructorRegisterModel))
                {
                    result = true;
                    var SmsVarificationLink = objInstructorBusinessLayer.GetLinkForSmsVarification(objInstructorRegisterModel.m_strEmailVarificationGuid,
                        objInstructorRegisterModel.m_strEmail, Constants.SMS_VERIFICATION_LINK_TYPE_ID_FOR_INSTRUCTOR);
                    await objSMSServiceManager.SendInstructorPhoneNoVarification(SmsVarificationLink, objInstructorRegisterModel.m_strPhoneNo);
                }

            }
            catch (Exception Ex)
            {

            }
            return result;

        }
        public async Task<bool> ValidateLoginDetails(InstructorRegisterModel objInstructorRegisterModel)
        {
            bool result = false;
            try
            {
                objInstructorRegisterModel.m_strPassword = SHA256Encryption.ComputeSha256Hash(objInstructorRegisterModel.m_strPassword);
                result = await objInstructorDTO.ValidateInstructorLoginDetails(objInstructorRegisterModel);

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
        public async Task<InstructorRegisterModel> GetInstructorPostLoginDetails(int Id)
        {
            return await objInstructorDTO.GetInstructorPostLoginDetails(Id);
        }
        public async Task<InstructorRegisterModel> GetInstructorDetails(int InstructorId)
        {
            return await objInstructorDTO.GetInstructorDetails(InstructorId);
        }
        public async Task<bool> UpdateInstructorDetails(InstructorRegisterModel objInstructorRegisterModel)
        {
            return await objInstructorDTO.UpdateInstructorDetails(objInstructorRegisterModel);
        }
        public async Task<string> InsertPasswordRecovery(string InstrcutorUserId)
        {
            string AuthToken = null;
            try
            {
                var InstrcutorId = objInstructorDTO.GetInstructorIdFromUserId(InstrcutorUserId);
                if (InstrcutorId != -1)
                {
                    InstructorRegisterModel objStudentRegisterModal = await objInstructorDTO.GetInstructorDetails(InstrcutorId);
                    if (objStudentRegisterModal != null)
                    {
                        string OTP = objInstructorBusinessLayer.GenerateOtp();
                        AuthToken = objInstructorBusinessLayer.GeneratePasswordVeryficationToken();
                        if (!objStudentRegisterModal.m_strPhoneNo.StartsWith("+"))
                        {
                            objStudentRegisterModal.m_strPhoneNo = "+91" + objStudentRegisterModal.m_strPhoneNo;
                        }
                        await objSMSServiceManager.SendInstructorPasswordRecoveryOTP(OTP,objStudentRegisterModal.m_strPhoneNo);
                        await objInstructorDTO.InsertPasswordRecoveryForInstructor(InstrcutorUserId, AuthToken, OTP);
                    }
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "InsertPasswordRecovery", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return AuthToken;
        }
        public async Task<bool> ValidatePasswordRecodevrtOtp(StudentUpdatePasswordRequestModal objStudentUpdatePasswordRequestModal)
        {

            bool result = false;
            try
            {
                if (await objInstructorDTO.ValidatePasswordRecoveryOtpForInstructor(objStudentUpdatePasswordRequestModal.m_strUserName,
                    objStudentUpdatePasswordRequestModal.m_strToken, objStudentUpdatePasswordRequestModal.m_strOtp))
                {
                    result = await objInstructorDTO.MarkOtpVerifiedForPasswordRecoveryForInstructor(objStudentUpdatePasswordRequestModal.m_strUserName,
                        objStudentUpdatePasswordRequestModal.m_strToken);
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
        public async Task<bool> ChangePasswordAfterAuth(StudentUpdatePasswordRequestModal objStudentUpdatePasswordRequestModal)
        {

            bool result = false;
            try
            {
                if (objStudentUpdatePasswordRequestModal.m_strPassword.Equals(objStudentUpdatePasswordRequestModal.m_strMatchPassword))
                {
                    objStudentUpdatePasswordRequestModal.m_strHashedPassword = SHA256Encryption.ComputeSha256Hash(objStudentUpdatePasswordRequestModal.m_strPassword);
                    result = await objInstructorDTO.UpdateInstructorPasswordAfterAuth(objStudentUpdatePasswordRequestModal.m_strUserName,
                        objStudentUpdatePasswordRequestModal.m_strToken, objStudentUpdatePasswordRequestModal.m_strHashedPassword);
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "ChangePasswordAfterAuth", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
        public async Task<bool> InertNewMeetingToClassroom(JitsiMeetingModal objJitsiMeetingModal)
        {

            bool result = false;
            try
            {
                objJitsiMeetingModal.m_strMeetingName = objInstructorBusinessLayer.GetRandomMeetingName();
                objJitsiMeetingModal.m_strMeetingPassword = objInstructorBusinessLayer.GetRandomMeetingPassword();
                objJitsiMeetingModal.m_strMeetingPassword = "";
                result = await objInstructorDTO.InertNewMeetingToClassroom(objJitsiMeetingModal);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "ChangePasswordAfterAuth", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }

        public async Task<ClassRoomModal> GetClassroomDetailsForInstructor(long ClassroomId, int InstructorId)
        {
            ClassRoomModal objClassRoomModal = null;
            try
            {
                objClassRoomModal = await objInstructorDTO.GetClassroomDetailsForInstructor(ClassroomId, InstructorId);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "GetClassroomDetailsForInstructor", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objClassRoomModal;
        }
        public async Task<JitsiMeetingModal> GetClassroomMeetingDetails(long ClassroomId, int InstructorId)
        {

            JitsiMeetingModal objJitsiMeetingModal = null;
            try
            {
                objJitsiMeetingModal = await objInstructorDTO.GetClassroomMeetingDetails(ClassroomId, -1);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "ChangePasswordAfterAuth", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return objJitsiMeetingModal;
        }
        public async Task<bool> CheckClassroomAccess(long ClassroomId, int InstructorId)
        {
            return await objInstructorDTO.CheckClassroomAccess(ClassroomId, InstructorId);
        }
        public async Task<bool> DeleteClassroomAssignment(long ClassroomID, long AssignmentId)
        {
            return await objInstructorDTO.DeleteClassroomAssignment(ClassroomID, AssignmentId);
        }
        public async Task<InstructorRegisterModel> GetInstructorBasicDetails(int Id)
        {
            return await objInstructorDTO.GetInstructorBasicDetails(Id);
        }
        public async Task<bool> UpdateInstructorAcademicRecords(InstructorAcademicRecordUpdateRequest
            instructorAcademicRecordUpdateRequest)
        {
            InstructorAcademicRecordDTO instructorAcademicRecordDTO = new InstructorAcademicRecordDTO();
            instructorAcademicRecordDTO.m_iInstructorId = instructorAcademicRecordUpdateRequest.m_iInstructorId;
            instructorAcademicRecordDTO.m_strLinkedIn = instructorAcademicRecordUpdateRequest.m_strLinkedIn;
            instructorAcademicRecordDTO.m_strGoogleScholarId = instructorAcademicRecordUpdateRequest.m_strGoogleScholarId;
            instructorAcademicRecordDTO.m_strSchoolDetails = JsonConvert.SerializeObject(instructorAcademicRecordUpdateRequest.instructorSchoolDetailsModal);
            return await objInstructorDTO.UpdateInstructorAcademicRecord(instructorAcademicRecordDTO);
        }
        public async Task<bool> UpdateInstructorBio(int InstructorId, string IntructorBio)
        {
            return await objInstructorDTO.UpdateInstructorBio(InstructorId, IntructorBio);
        }
        public async Task<InstrucorEarningDetailsModal> GetInstructorEarningDetails(int InstructorId)
        {
            InstrucorEarningDetailsModal instrucorEarningDetailsModal = await objInstructorDTO.GetInstructorEarningDetails(InstructorId);
            if (instrucorEarningDetailsModal != null)
            {
                instrucorEarningDetailsModal.m_lsInstructorClassroomEarningModal = await objInstructorDTO.GetInstructorClassroomEarning(InstructorId);
                instrucorEarningDetailsModal.m_lsInstructorCourseEarningDetailsModal = await objInstructorDTO.GetInstructorCourseEarning(InstructorId);
            }
            return instrucorEarningDetailsModal;
        }
        public long AddNewTestSeries(InsertTestSeriesRequest insertTestSeriesRequest)
        {
            return objInstructorDTO.AddNewTestSeries(insertTestSeriesRequest);
        }
        public async Task<bool> UpdateClassroomVideoUrl(UpdateClassroomVideoUrlRequest updateClassroomVideoUrlRequest)
        {
            return await objInstructorDTO.UpdateClassroomVideoUrl(updateClassroomVideoUrlRequest);
        }

        public async Task<GetLiveClassDetailsForInstructor> GetLiveClassDetailsForInstructor(long MeetingId)
        {
            return await objInstructorDTO.GetLiveClassDetailsForInstructor(MeetingId);
        }
        public async Task<bool> UpdateClassroomSyllabus(ClassroomSyllabusDetailsModal classroomSyllabusDetailsModal)
        {
            classroomSyllabusDetailsModal.m_strSerializedSyllabus = JsonConvert.SerializeObject(classroomSyllabusDetailsModal.m_lsClassroomWeekWiseSyallabus);
            return await objInstructorDTO.UpdateClassroomSyllabus(classroomSyllabusDetailsModal.m_llClassroomId, classroomSyllabusDetailsModal.m_strSerializedSyllabus);
        }
        public async Task<ClassroomSyllabusDetailsModal> GetClassroomSyllabus(long ClassroomId)
        {
            ClassroomSyllabusDetailsModal classroomSyllabusDetailsModal = await objInstructorDTO.GetClassroomSyllabus(ClassroomId);
            if (classroomSyllabusDetailsModal != null)
            {
                classroomSyllabusDetailsModal.m_lsClassroomWeekWiseSyallabus = JsonConvert.DeserializeObject<List<ClassroomWeekWiseSyallabus>>(classroomSyllabusDetailsModal.m_strSerializedSyllabus);
            }
            return classroomSyllabusDetailsModal;
        }
        public async Task<List<MasterInstructorDetails>> SearchInstrucrByUserId(SearchInstructorByUserIdRequest searchInstructorByUserIdRequest)
        {
            return await objInstructorDTO.SearchInstrucrByUserId(searchInstructorByUserIdRequest);
        }
        public async Task<bool> SendClassroomNotification(ClassroomNotificationMasterDetails classroomNotificationMasterDetails)
        {
            return await objInstructorDTO.SendClassroomNotification(classroomNotificationMasterDetails.m_llClassroomId,
                classroomNotificationMasterDetails.m_strNotification);
        }
        public string GetClassroomInstructorView(ClassRoomModal classroomModal)
        {
            string ViewName = Constants.CLASSROOM_VIEW_INACTIVE;
            if (classroomModal.m_strClassroomStatus == Constants.CONTENT_STATUS_ACTIVE)
            {
                if(classroomModal.m_bIsVarifiedByAdmin==true&& classroomModal.m_iAdminVarificationCode == (int)Constants.ClassroomVarificationStateId.ACCEPTED)
                {
                    ViewName = Constants.CLASSROOM_VIEW_VARIFIED;
                }
                else
                {
                    ViewName = Constants.CLASSROOM_VIEW_VARIFICATION_PENDING;
                }
            }
            return ViewName;
        }
        public async Task<bool> CheckIsClassroomAlreadyTakenoday(long ClassroomId)
        {
            bool result = false;
            try
            {
                result = await objInstructorDTO.CheckIsClassroomAlreadyTakenoday(ClassroomId);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "CheckIsClassroomAlreadyTakenoday", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return result;
        }
    }
}