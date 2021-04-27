using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace CPDataService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface ICpDataService
    {
        [OperationContract]
        string GetData(int value);
        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);
        [OperationContract]
        bool RegisterNewUser(string FirstName, string LastName, string PhoneNo, string Email, string Password);
        [OperationContract]
        DataSet ValidateLoginDetails(string Email, string Password);
        [OperationContract]
        bool RegisterNewInstructor(string FirstName, string LastName, string PhoneNo, string Email, string Password,
            string PhoneNoVerificationGuid, string EmailIdVerificationGuid);
        [OperationContract]
        DataSet ValidateInstructorLoginDetails(string Email, string Password);
        [OperationContract]
        DataSet GetAllCountryDetails();
        [OperationContract]
        DataSet GetAllCityDetailsOfState(int StateId);
        [OperationContract]
        DataSet GetAllStateDetailsOfCountry(int CountryId);
        [OperationContract]
        DataSet CheckIsSchoolUserIdAlreadyExists(string SchoolUserIdToCheck);
        [OperationContract]
        bool InsertNewSchoolDetails(string SchoolName, string AddressLine1, string AddressLine2, int CityId, int PinCode, string SchoolUserId,
                                          string Password, string PhoneNo, string Email);
        [OperationContract]
        DataSet ValidateSchoolLoginDetails(string Email, string Password);
        [OperationContract]
        bool InsertNewCourse(string CourseName, string CourseDescription,
            string CourseImageUrl, string CourseThumbnailSmall, string CoursethumbnailMedium, int InstructorId, ref long CourseId);
        [OperationContract]
        bool InertNewCourseIndex(string IndexName, string IndexDescription, long CourseId, ref long IndexId);
        [OperationContract]
        bool InsertNewTopic(string TopicName, string TopicDescription, string FileUploadPath, short FileUploadTypeId, long IndexId, string FileAttachmetPath);
        [OperationContract]
        bool InsertNewTest(string TestName, string TestDescription, string FilePath, short FileTypeId, short TestType, ref long TestId);
        [OperationContract]
        bool InsertNewAssignment(string AssignmentName, string FilePath, short FileTypeId, short AssignmentType, string AssignmentDescription, ref long AssignmentId);
        [OperationContract]
        bool InsertNewMcqAssignment(long AssignmentId, string QuestionStatement, string Option1, string Option2, string Option3, string Option4, short CorrectOption);
        [OperationContract]
        bool InsertNewMcqTestQuestion(long TestId, string QuestionStatement, string Option1, string Option2, string Option3, string Option4, short CorrectOption,
                                int TimeForQuestionInSeconds, int Marks);
        [OperationContract]
        bool InsertTestIdToIndex(long TestId, long IndexId);
        [OperationContract]
        bool InsertAssignmentIdToIndex(long AssignmentId, long IndexId);
        [OperationContract]
        DataSet GetAllCourse(long InstructorId);
        [OperationContract]
        bool GetInstructorIdFromUserId(string InstructorId, ref int Id);
        [OperationContract]
        DataSet GetIndexDetailsOfCourse(long CourseId);
        [OperationContract]
        DataSet GetCourseDetails(long CourseId);
        [OperationContract]
        bool InsertNewIndependentAssignment(int InstructorId, string AssignmentName, string AssignmentDescription, string FilePath, short FileTypeId, short AssignmentType, ref long AssignmentId);
        [OperationContract]
        bool InsertActivityForInstructor(int InstructorId, string ActivityMessgae);
        [OperationContract]
        bool UpdateTestDetails(long TestId, string TestName, string TestDescription, string FilePath, byte FileType);
        [OperationContract]
        bool UpdateAssignmentDetails(long AssignmentId, string AssignmentName, string AssignmentDescription, string FilePath, byte FileType);
        [OperationContract]
        bool UpdateIndexTopicDetails(long TopicId, string TopicName, string TopicDescription, string FilePath, byte FileType, string FileAttachmentPath);
        [OperationContract]
        bool UpdateCourseIndexDetails(long IndexId, string IndexName, string IndexDescription);
        [OperationContract]
        bool UpdateCourseDetails(long CourseId, string CourseDescription);
        [OperationContract]
        bool ActivateCourse(long CourseId, string ShareUrl, string AccessCode, int CourseJoiningFeeInPaise);
        [OperationContract]
        bool DeleteCourse(long CourseId);
        [OperationContract]
        DataSet GetAssignmentDetails(long AssignmentId);
        [OperationContract]
        DataSet GetMcqAssignmentDetails(long AssignmentId);
        [OperationContract]
        DataSet GetInstructorDetails(int Id);
        [OperationContract]
        bool UpdateInstructorDetails(string FirstName, string LastName, string PhoneNo, string Gender, string AddressLine1, string AddressLine2,
                                          int? CityId, int? StateId, string PinCode, int InstructorId);
        [OperationContract]
        bool UpdateInstructorPassword(string Password, int InstructorId);
        [OperationContract]
        DataSet GetInstructorPostLoginDetails(int Id);
        [OperationContract]
        bool InsertNewIndependentTest(int InstructorId, string TestName, string TestDescription, string FilePath, short FileTypeId, short TestType, ref long TestId);
        [OperationContract]
        DataSet GetInstructorTestDetails(int Id);
        [OperationContract]
        DataSet GetInstructorAssignmentDetails(int Id);
        [OperationContract]
        DataSet GetInstructorActivityDetails(int Id);
        [OperationContract]
        bool ActivateTest(long TestId, string ShareCode, string TinyUrl);
        [OperationContract]
        bool DeleteAssignmentOfCourse(long AssignmentId);
        [OperationContract]
        bool ActivateAssignment(long AssignmentId, string ShareCode, string TinyUrl);
        [OperationContract]
        bool DeleteTest(long TestId);
        [OperationContract]
        bool InsertContactFormDetails(string Name, string Email, string PhoneNo, string Subject, string Message);
        [OperationContract]
        DataSet GetIndexTopicDetails(long IndexId);
        [OperationContract]
        DataSet GetIndexDetails(long IndexId);
        [OperationContract]
        bool DeleteMcqQuestionOfAssignment(long QuestionId);
        [OperationContract]
        bool UpdateMcqQuestionOfAssignment(long QuestionId, string QuestionStatement, string Option1, string Option2, string Option3, string Option4, byte CorrectOption);
        [OperationContract]
        bool DeleteIndexTopic(long TopicId);
        [OperationContract]
        bool UpdateIndexTopic(long TopicId, string TopicName, string TopicDescription, string FilePathMapToServer, byte? FileTypeId);
        [OperationContract]
        bool UpdateFullCourseDetails(long CourseId, string CourseName, string CourseDescription);
        [OperationContract]
        bool DeleteIndex(long IndexId);
        [OperationContract]
        DataSet GetMcqTestDetails(long TestId);
        [OperationContract]
        DataSet GetMcqtestQuestionDetails(long TestId);
        [OperationContract]
        bool UpdateMcqTestDetails(long TestId, string TestName, string TestDescroption);
        [OperationContract]
        bool UpdateMcqQuestionDetails(long Questionid, string QuestionStatement, string Option1, string Option2, string Option3, string Option4,
                 byte CorrectOption, int iTimeForQuestion, int iMarksForQuestion);
        [OperationContract]
        bool DeleteMcqTest(long TestId);
        [OperationContract]
        bool InsertNewAssignmentToCourse(string AssignmentName, string AssignmentDescription, byte AssignmentTypeId, string FilePath, byte? FileTypeId,
                  long CourseId);
        [OperationContract]
        bool InsertNewTestToCourse(string TestName, string TestDescription, byte TestTypeId, string FilePath, byte? FileTypeId,
                    long CourseId);
        [OperationContract]
        bool DeleteMcqTestQuestion(long QuestionId);
        [OperationContract]
        DataSet GetAllAssignmentsForCourse(long CourseId);
        [OperationContract]
        bool DeleteTestOfCourse(long TestId);
        [OperationContract]
        bool DeleteInpependentAssignment(long AssignmentId);
        [OperationContract]
        bool DeleteIndepenetTest(long TestId);
        [OperationContract]
        bool InsertSubjectiveAssignmentQuestion(long AssignmentId, string QuestionStatement, string Hint);
        [OperationContract]
        bool DeleteSubjectiveAssignmentQuestion(long AssignmentId);
        [OperationContract]
        bool DeleteSubjectiveAssignmentOfCourse(long AssignmentId);
        [OperationContract]
        bool UpdateSubjectiveAssignmentQuestion(long QuestionId, string QuestionStatement, string Hint);
        [OperationContract]
        DataSet GetAllQuestionsOfSubjectiveAssignment(long AssignmentId);
        [OperationContract]
        bool RegisterNewStudent(string FirstName, string LastName, string UserId, string HashedPassword, string PhoneNo,
            string PhoneNoVerificationGuid, string EmailIdVerificationGuid);
        [OperationContract]
        bool ValidateStudentLogin(string UserId, string HashedPassword, ref long StudentId);
        [OperationContract]
        bool InsertActivityForStudent(string ActivityMessage, long StudentId);
        [OperationContract]
        bool JoinStudentToCourse(long CourseId, long StudentId);
        [OperationContract]
        DataSet SearchForCourse(string SerachString, int MaxRowToReturn, int NoOfRowsFetch, int SortType);
        [OperationContract]
        DataSet GetStudentDetails(long StudentId);
        [OperationContract]
        bool GetStudentIdFromUserId(string UserId, ref long StudentId);
        [OperationContract]
        bool UpdateStudentDetails(string FirstName, string LastName, string AddressLine1, string AddressLine2, string PinCode,
                                          int? StateId, int? CityId, string Gender, long StudentId, string PhoneNo);
        [OperationContract]
        bool UpdateStudentPassword(long StudentId, string OldHashedPassword, string NewHashedPassword);
        [OperationContract]
        DataSet SearchForTest(string SerachString, int MaxRowToReturn, long LastFetchedId);
        [OperationContract]
        DataSet SearchForAssignment(string SerachString, int MaxRowToReturn, long LastFetchedId);
        [OperationContract]
        DataSet SearchForInstructor(string SerachString, int MaxRowToReturn);
        [OperationContract]
        DataSet GetJoinedCoursesForStudent(long StudentId, string SearchString, int MaxRowCountToReturn);
        [OperationContract]
        bool JoinStudentToInstructor(long StudentId, int InstructorId);
        [OperationContract]
        DataSet GetAllJoinedInstructorForStudent(long StudentId, string SearchString, int MaxRowCount);
        [OperationContract]
        bool InsertAssignmentResponse(long StudentId, long AssignmentId, DateTime AssignmentStartTime, DateTime AssignmentFinishTime, string Response,
                     int PercentageScore, int TotalNoOfQuestions, ref long SubmissionId);
        [OperationContract]
        bool InsertAssignmentFeedback(long SubmissionId, string FeedBack, int Rating);
        [OperationContract]
        DataSet GetAllAssignmentSubmissionsForStudent(long StudentId);
        [OperationContract]
        DataSet GetAssignmentResponse(long SubmissionId, long StudentId);
        [OperationContract]
        bool InsertTestResponse(long StudentId, long TestId, DateTime TestStartTime, DateTime TestFinishTime, string Response,
                    int PercentageScore, int TotalNoOfQuestions, ref long SubmissionId);
        [OperationContract]
        DataSet GetTestResponse(long SubmissionId, long StudentId);
        [OperationContract]
        DataSet GetAllTestSubmissionsForStudent(long StudentId);
        [OperationContract]
        DataSet GetStudentHomeDetails(long StudentId);
        [OperationContract]
        DataSet GetTestsOfCourse(long CourseId);
        [OperationContract]
        DataSet GetAllTestSubmissions(long StudentId);
        [OperationContract]
        DataSet GetAllAssignmentsSubmissions(long AssignmentId);
        [OperationContract]
        DataSet GetStudentJoinedToCourse(long CourseId);
        [OperationContract]
        DataSet GetAllStudentsJoinedToInstructor(int InstructorId, int NoOfRowsFetched, string SearchString);
        [OperationContract]
        bool InsertNewCourseV2(string CourseName, string CourseDescription, int InstructorId, string AboutCourse, string CourseImagePath, ref long CourseId);
        [OperationContract]
        bool InsertNewIndexToV2Course(long CourseId, string IndexName, string IndexContetHtml, ref long IndexId);
        [OperationContract]
        bool InsertNewTopicToV2Course(long IndexId, string TopicName, string TopicHtml);
        [OperationContract]
        bool InsertNewAlertForInstructor(int InstructorId, string AlertMessage, int AlertTypeId, long? EffectiveContentid, long StudentId);
        [OperationContract]
        DataSet GetAllAlertForInstructor(int InstructorId);
        [OperationContract]
        bool MarkAlertReadForInstructor(long AlertId);
        [OperationContract]
        bool GetInstructorIdByAssignmentId(long AssignmentId, ref int InstructorId);
        [OperationContract]
        bool GetInstructorIdByTestId(long TestId, ref int InstructorId);
        [OperationContract]
        bool GetInstructorIdByCourseId(long CourseId, ref int InstructorId);
        [OperationContract]
        DataSet SearchForCourseOfInstructor(string SerachString, int MaxRowToReturn, int InstructorId);
        [OperationContract]
        DataSet SearchForAssignmentOfInstructor(string SerachString, int MaxRowToReturn, int InstructorId);
        [OperationContract]
        DataSet SearchForTestOfInstructor(string SerachString, int MaxRowToReturn, int InstructorId);
        [OperationContract]
        DataSet SearchForCourseForStudent(string SerachString, int MaxRowToReturn, int NoOfRowsFetch, int SortType, long StudentId);
        [OperationContract]
        DataSet CheckStudentHasJoinedTheCourse(long StudentId, long CourseId);
        [OperationContract]
        DataSet CheckStudentHasSubmittedTheAssignment(long StudentId, long AssignmentId);
        [OperationContract]
        DataSet CheckStudentHasSubmittedTheTest(long StudentId, long TestId);
        [OperationContract]
        DataSet CheckAssignmentResponseIdExistsForStudent(long StudentId, long SubmissionId);
        [OperationContract]
        DataSet CheckTestResponseIdExistsForStudent(long StudentId, long SubmissionId);
        [OperationContract]
        DataSet GetInstructorProfileDetails(int InstructorId);
        [OperationContract]
        DataSet CheckTestAccess(long TestId, string AccessCode);
        [OperationContract]
        DataSet CheckAssignmentAccess(long AssignmentId, string AccessCode);
        [OperationContract]
        DataSet GetIndependentAssignmentDetails(long AssignmentId);
        [OperationContract]
        DataSet GetIndependentTestDetails(long TestId);
        [OperationContract]
        DataSet GetTestDetailsWithAccessCode(long TestId, string AccessCode);
        [OperationContract]
        DataSet GetAssignmentDetailsWithAC(long AssignmentId, string AccessCode);
        [OperationContract]
        DataSet GetWebsiteAboutDetails();
        [OperationContract]
        bool UpdateNotificationStatus(bool Status, long NotificationId);
        [OperationContract]
        DataSet GetAllNotificationToPrecess(int MaxRetryCount);
        [OperationContract]
        bool InsertSmsNotification(int NotificationTypeId, string SmsBody, string ReceiverPhoneNo);
        [OperationContract]
        bool InsertStudentPasswordRecoveryRequest(string UserId, string Token, string OTP);
        [OperationContract]
        DataSet ValidateStudentPasswordRecoveryRequest(string UserId, string Token, string OTP);
        [OperationContract]
        bool ChanegPasswordAfterAuthentication(string UserId, string Token, string HashedPassword);
        [OperationContract]
        bool MarkOtpVarified(string UserId, string Token);
        [OperationContract]
        bool ValidatePhoneNoVarificationLinkForStudent(string UserId, string guid);
        [OperationContract]
        bool ValidatePhoneNoVarificationLinkForInstructor(string UserId, string guid);
        [OperationContract]
        bool InsertInstructorPasswordRecoveryRequest(string UserId, string Token, string OTP);
        [OperationContract]
        DataSet ValidateInstructorPasswordRecoveryRequest(string UserId, string Token, string OTP);
        [OperationContract]
        bool ChangePasswordAfterAuthenticationForInstructor(string UserId, string Token, string HashedPassword);
        [OperationContract]
        bool MarkPassowordVarificationOtpVarifiedForInstructor(string UserId, string Token);
        [OperationContract]
        DataSet CheckCourseIdExsitsForInstructor(int InstructoId, long CourseId);
        [OperationContract]
        DataSet CheckIndexIdExsitsForInstructor(int InstructoId, long IndexId);
        [OperationContract]
        DataSet CheckCourseAccess(long CourseId, string AccessCode);
        [OperationContract]
        DataSet CheckTestIdExistsForAnyCourseForInstructor(long Testid, int InstructorId);
        [OperationContract]
        DataSet CheckAssignmentIdExistsForAnyCourseForInstructor(long AssignmentId, int InstructorId);
        [OperationContract]
        DataSet CheckTestIdExistsForInstructor(long Testid, int InstructorId);
        [OperationContract]
        DataSet CheckAssignmentIdExistsForInstructor(long AssignmentId, int InstructorId);
        [OperationContract]
        bool InsertCompletedTopicforStudent(long TopicId, long StudentId);
        [OperationContract]
        bool InsertCourseQuestionByStudent(long CourseId, long StudentId, string Question);
        [OperationContract]
        bool InsertAnswerForCourseQuestion(long QuestionId, int InstructorId, string Answer);
        [OperationContract]
        DataSet GetIndexTopicProgressForStudent(long IndexId, long StudentId);
        [OperationContract]
        DataSet GetStudentCourseProgress(long CourseId, long StudentId);
        [OperationContract]
        DataSet GetStudentAssignmentProgress(long AssignmentId, long StudentId);
        [OperationContract]
        DataSet GetStudentTestProgress(long TestId, long StudentId);
        [OperationContract]
        DataSet GetAllQuestionAskForCourseByStudent(long Studentid, long CourseId);
        [OperationContract]
        DataSet GetClassRoomDetailsForInstructor(int InstrcutorId, long ClassRoomId);
        [OperationContract]
        DataSet GetAllClassRoomForInstrcutor(int InstrcutorId);
        [OperationContract]
        long InsertNewClassRoomForInstructor(int InstrcuctorId, string ClassRoomName, string ClassRoomDescription, string BackGroundImageUrl,
            string ClassroomMeetingName, string ThumbnailSmall, string ThumbnailMedium, int NoOfDemoClassrooms);
        [OperationContract]
        bool InertNewPostToClassroom(long ClassroomId, string Post);
        [OperationContract]
        bool InertNewMeetingToClassroom(long ClassroomId, string MeetingName, string MeetingPassword);
        [OperationContract]
        DataSet GetMeetingDetailsOfClassroom(long ClassRoomId);
        [OperationContract]
        bool ActivateClassroom(long ClassroomId, string ShareCode, string ShareUrl,
            int ClassroomPublicType, int ClassroomJoiningAmountInPaise, string StartTime, string ArrayOpeningDays, int NofDemoClasses,
            int Category, int Level);
        [OperationContract]
        DataSet GetClasroomDetails(long ClassRoomId);
        [OperationContract]
        bool JoinStudentToClassroom(long ClassroomId, long StudentId);
        [OperationContract]
        DataSet GetJoinedClassroomForStudent(long StudentId);
        [OperationContract]
        bool JoinStudentToMeeting(long MeetingId, long StudentId);
        [OperationContract]
        bool ReportMeetingLeftForStudent(long MeetingId, long StudentId);
        [OperationContract]
        bool ReportMeetingLeftForHost(long MeetingId);
        [OperationContract]
        DataSet GetAllMeetingForClassroom(long ClassroomId);
        [OperationContract]
        DataSet CheckClassroomAccess(long ClassroomId, string AccessCode);
        [OperationContract]
        DataSet GetAllStudentsJoinedToClassroom(long ClassroomId);
        [OperationContract]
        DataSet CheckInstructorClassroomAccess(long ClassroomId, int InstructorId);
        [OperationContract]
        DataSet CheckStudentClassroomAccess(long ClassroomId, long StudentId);
        [OperationContract]
        DataSet GetAllStudentsJoinedToMeeting(long ClassroomId, long MeetingId);
        [OperationContract]
        bool InsertNewStudentClassroomMessage(long ClassroomId, string Message, long StudentId);
        [OperationContract]
        bool InsertNewInstructorClassroomMessage(long ClassroomId, string Message);
        [OperationContract]
        DataSet GetAllClassroomMessage(long ClassroomId);
        [OperationContract]
        DataSet GetAllClassroomMessageAfterLastMessage(long ClassroomId, long LastMessageId);
        [OperationContract]
        bool UpdateClassroomDetails(long ClassroomId, string ClassroomName, string ClassroomDescription, DateTime? classroomStartDate,
            DateTime? classroomRegistrationCloseDate, int NoOfDemoSessions);
        [OperationContract]
        bool DeleteClassroom(long ClassroomId);
        [OperationContract]
        bool AddNewAssignmentToClassroom(long ClassroomId, long AssignmentId);
        [OperationContract]
        DataSet GetAllClassroomAssignments(long ClassroomId);
        [OperationContract]
        bool DeleteClassroomAssignment(long ClassroomId, long AssignmentId);
        [OperationContract]
        bool DeleteClassroomTest(long ClassroomId, long TestId);
        [OperationContract]
        bool AddNewTestToClassroom(long ClassroomId, long TestId);
        [OperationContract]
        DataSet GetAllClassroomTest(long ClassroomId);
        [OperationContract]
        DataSet GetAllClassroomMeetingForStudent(long ClassroomId, long StudentId);
        [OperationContract]
        DataSet GetAllClassroomAssignmntForStudent(long ClassroomId, long StudentId);
        [OperationContract]
        DataSet GetAllClassroomAssignmentSubmissionsForStudent(long ClassroomId, long StudentId);
        [OperationContract]
        DataSet GetAllClassroomTestSubmissionsForStudent(long ClassroomId, long StudentId);
        [OperationContract]
        DataSet GetAllClassroomTestForStudent(long ClassroomId, long StudentId);
        [OperationContract]
        DataSet GetClassroomHomeDetailsForStudent(long ClassroomId, long StudentId);
        [OperationContract]
        DataSet GetInstructorClassroomSearchDetails(int InstructorId, string SearchString);
        [OperationContract]
        bool UpdateInstructorProfilePicture(int InstructorId, string Url, string MediumSizeUrl,
            string SmallSizeUrl);
        [OperationContract]
        bool UpdateStudentProfilePicture(long StudentId, string OriginalFile, string SmallThumbnailUrl,
            string MediumThumbnailUrl);
        [OperationContract]
        DataSet GetClassroomMeetingDetails(long ClassroomId, long MeetingId);
        [OperationContract]
        bool InsertClassroomAttachment(long ClassroomId, string AttachmentName, string AttachmentDescription, string AttachmentUrl);
        [OperationContract]
        bool UpdateClassroomAttachmentDetails(long AttachmentId, string AttachmentName, string AttachmentDescription, string AttachmentUrl);
        [OperationContract]
        bool DeleteClassroomAttachment(long AttachmentId);
        [OperationContract]
        DataSet GetClassroomAttachments(long ClassroomId);
        [OperationContract]
        DataSet GetInstructorProfileDetailsForStudent(int InstructorId, long StudentId);
        [OperationContract]
        DataSet GetClassroomSchedule(long ClassroomId);
        [OperationContract]
        bool UpdateClassroomSchedule(long ClassroomId, string ClassroomSchedule);
        [OperationContract]
        bool InsertClassroomSchedule(long ClassroomId, string ClassroomSchedule);
        [OperationContract]
        DataSet GetPublicClassroomDetailsForStudent(int NoOfRowsFetched, long StudentId,
            int NoOfRecordsToBeFetched, string QueryString);
        [OperationContract]
        DataSet SearchForCourseForNotLoggedUser(string SerachString, int MaxRowToReturn, int NoOfRowsFetch, int SortType, long StudentId);
        [OperationContract]
        DataSet GetInstructorAcademicRecords(int InstructorId);
        [OperationContract]
        bool UpdateInstructorAcademicRecord(
            string LinkedInId, string GoogleScholarId,
            int InstructorId, string SchoolDetails);
        [OperationContract]
        bool UpdateInstructorBio(int InstructorId, string InstructoBioData);
        [OperationContract]
        DataSet GetClassroomPaymentDetails(long ClassroomId, long StudentId);
        [OperationContract]
        bool CreatePaymentOrder(string OrederId, string CustomerName, string CustomerEmail, string CustomerPhoneNo,
            int AmountInPaise, string CustomerAddress);
        [OperationContract]
        bool InsertRazorPayTxnDetails(string OrederId, string RazorPayPaymentId, string RazorPayOderId, string RazorPaySignature);
        [OperationContract]
        bool GetInstructorIdByClassroomId(long ClassroomId, ref int InstructorId);
        [OperationContract]
        bool GetCoursePrice(long CourseId, ref int CoursePrice);
        [OperationContract]

        DataSet GetCoursePaymentDetails(long CourseId, long StudentId);
        [OperationContract]
        DataSet GetInstructorEarnings(int InstructorId);
        [OperationContract]
        DataSet GetMonthwiseInstructorClassroomEarning(int InstructorId);
        [OperationContract]
        DataSet GetMonthwiseInstructorCourseEarning(int InstructorId);
        [OperationContract]
        bool InsertNewTestSeries(int InstructorId, string TestSeriesName, string TestSeriesDescription, string TestSeriesImage,
            ref long TestSeriesId);
        [OperationContract]
        bool InsertNewTestToTestSeries(long TestSeriesId, long TestId);
        [OperationContract]
        bool DeletTestFromTestSeries(long TestSeriesContentId);
        [OperationContract]
        DataSet GetTestSearchResultForStudent(long StudentId, string SearchString, int MaxRowToFetch, long LastFetchedId);
        [OperationContract]
        bool AddNewTestAnonymousTestSubmission(long TestId, string TestAccessCode);
        [OperationContract]
        bool UpdateClassroomSyllabus(long ClassroomId, string ClassroomSyallabus);
        [OperationContract]
        DataSet GetClassroomsForHomePage();
        [OperationContract]
        DataSet GetClassRoomDetailsForStudent(long ClassRoomId);
        [OperationContract]
        bool InsertEmailSubscriber(string EmailId);
        [OperationContract]
        bool InsertStudentAlert(int AlertTypeId, long StudentId, string TargetUrl);
        [OperationContract]
        DataSet GetStudentRecentCourseJoin(long StudentId);
        [OperationContract]
        DataSet GetStudentRecentLiveClassJoin(long StudentId);
        [OperationContract]
        DataSet GetSearchResultForStudent(string SearchString, int MaxRowToReturn, int NoOfRowsFetch, long StudentId);
        [OperationContract]
        bool UpdateClassroomBackground(long ClassroomId, string OriginalImagePath, string SmallIcon, string MediumIcon);
        [OperationContract]
        bool UpdateCourseBackground(long CourseId, string OriginalImagePath, string SmallIcon, string MediumIcon);
        [OperationContract]
        bool JoinNewStudent(long StudentStaertedFollowingId, long StudentGettingFollowedId);
        [OperationContract]
        bool UnfollowStudent(long StudentStaertedFollowingId, long StudentGettingFollowedId);
        [OperationContract]
        bool FollowBackStudent(long StudentStaertedFollowingId, long StudentGettingFollowedId);
        [OperationContract]
        DataSet GetAllStudentsToFollow(long StudentId, int NoOfRowsFetched, int NoOfRowsToBeFetched, string SearchString);
        [OperationContract]
        DataSet GetStudentPublicProfileDetails(long StudentId);
        [OperationContract]
        DataSet GetAllStudentsFollowedByStudent(long StudentId, int NoOfRowsFetched, int NoOfRowsToBeFetched);
        [OperationContract]
        DataSet GetStudentFriendDetails(long StudentId, long FriendId);
        [OperationContract]
        DataSet CheckStudentFollowingStudent(long StudentId, long StudentToBeFollowedId);
        [OperationContract]
        DataSet GetStudentSelfPublicDetails(long StudentId);
        [OperationContract]
        bool UpdateClassroomsingleMeetingDetails(long MeetingId, string VideoUrl, string TopicName, string TopicNotes);
        [OperationContract]
        DataSet GetLiveClassMeetingDetails(long MeetingId);
        [OperationContract]
        DataSet GetClassroomAllMeetingDetailsForStudent(long StudentId, long ClassroomId);
        [OperationContract]
        DataSet GetClassroomMeetingDetailsForStudent(long StudentId, long MeetingId);
        [OperationContract]
        DataSet GetClassroomSyllabus(long ClassroomId);
        [OperationContract]
        bool MarkStudentClassroomPaymentSuccessful(long ClassroomId, long StudentId);
        [OperationContract]
        DataSet SearchInstructorByUserId(string SearchString);
        [OperationContract]
        bool InsertOrUpdateClassroomFeedbackByStudent(long ClassroomId, long StudentId, string
            FeedbackMessage, int NoOfRatings);
        [OperationContract]
        DataSet GetAllCoupons();
        [OperationContract]
        bool SendClassroomNotification(long ClassroomId, string NotificationMessage);
        [OperationContract]
        bool RegisterNewStudentViaGmail(string GmailId, string FirstName, string LastName, string UserId, string PhoneNo,
            string PhoneNoVarificationGuid, string ProfileUrl);
        [OperationContract]
        DataSet CheckGmailUserAlreadyExists(string GmailId, string UserId);
        [OperationContract]
        bool VarifyStudentPhoneNo(string UserId, string Otp, string PhoneNoGuid);
        [OperationContract]
        bool InsertOtpToVarifyPhoneNoOfStudent(long StudentId, string Otp);
        [OperationContract]
        bool UpdatePhoneNoOfGmailRegStudent(string UserId, string Token, string PhoneNo);
        [OperationContract]
        bool InsertInstructorContactUsDetail(int InstructorId, string Email, string PhoneNo, string Subject, string Message);
        [OperationContract]
        DataSet GetAllClassroomCategories();
        [OperationContract]
        DataSet GetStudentsJoinedToClassroomForStudent(long ClassroomId, long StudentId,
                                              int MaxRowsToBeFetched, int NoOfRowsFetched);
        [OperationContract]
        DataSet CheckClassroomMeetingOccuredToday(long ClassroomId);
        [OperationContract]
        DataSet GetAllTrialClassroomMeetingDetailsForStudent(long StudentId, long ClassroomId);
        [OperationContract]
        DataSet GetAllClassroomReviews(long ClassroomId, int NoOfRowsToBeFetched, int NoOfRowsFetched);
        [OperationContract]
        DataSet GetAvgRatingForClassroom(long ClassroomId);

    }
    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
