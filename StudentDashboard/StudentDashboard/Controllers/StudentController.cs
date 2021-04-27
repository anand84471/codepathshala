using StudentDashboard.HttpRequest;
using StudentDashboard.HttpResponse;
using StudentDashboard.Models;
using StudentDashboard.Models.Instructor;
using StudentDashboard.Models.OAuth;
using StudentDashboard.Models.Student;
using StudentDashboard.Security;
using StudentDashboard.Security.Student;
using StudentDashboard.ServiceLayer;
using StudentDashboard.Utilities;
using StudentDashboard.ValidationHandler;
using System;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace StudentDashboard.Controllers
{
    [RoutePrefix("Student")]
    [StudentAuthenticationFilter]
    public class StudentController : Controller
    {
        // GET: Student
        StringBuilder m_strLogMessage = new StringBuilder();
        StudentService objStudentService = new StudentService();
        DocumentService objDocumentService = new DocumentService();
        public ActionResult Index(string return_url = null)
        {
            if (Session["user_id"] != null)
            {
                if (return_url == null)
                {
                    return Redirect("/Student/Home");
                }

                return Redirect(return_url);
            }
            if (return_url != null)
            {
                ViewBag.ReturnUrl = "/Student/ValidateLogin?return_url=" + return_url;
                ViewBag.RedirectUri = return_url;
            }
            else
            {
                ViewBag.ReturnUrl = "/Student/ValidateLogin";
            }
            return View();

        }
        [HttpGet]
        [Route("Join")]
        public ActionResult Join()
        {
            return View();
        }
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> Register(FormCollection collection)
        {
            try
            {
                StudentRegisterModal objRegiserModel = new StudentRegisterModal();
                objRegiserModel.m_strFirstName = collection["firstName"];
                objRegiserModel.m_strLastName = collection["lastName"];
                objRegiserModel.m_strPassword = collection["password"];
                objRegiserModel.m_strEmail = collection["email"];
                objRegiserModel.m_strPhoneNo = collection["phoneNo"];
                objRegiserModel.m_strCountryCode = collection["countryCode"];
                var objDynamicRoutingAPIRequestValidator = new StudentAccountRegisterValidator();
                {
                    var result = await objDynamicRoutingAPIRequestValidator.ValidateAsync(objRegiserModel);
                    if (result.IsValid)
                    {
                        if (await objStudentService.RegisterNewStudent(objRegiserModel))
                        {
                            Response.Redirect("./RegistrationSuccessful?UserId=" + objRegiserModel.m_strEmail);
                        }
                        else
                        {
                            ViewBag.IsRegistered = false;
                        }
                    }
                    else
                    {
                        StringBuilder m_strStringBuilder = new StringBuilder();
                        foreach (var failure in result.Errors)
                        {
                            m_strStringBuilder.Append("Property " + failure.PropertyName + " failed validation. Error was: " + failure.ErrorMessage);
                        }
                        ViewBag.ErrorMessage = m_strStringBuilder.ToString();
                    }
                }
                return View("Join");
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "Register", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
                return View("Error");
            }
        }
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> RegisterLiveClass(FormCollection collection,string return_url=null)
        {
            try
            {
                StudentRegisterModal objRegiserModel = new StudentRegisterModal();
                objRegiserModel.m_strFirstName = collection["firstName"];
                objRegiserModel.m_strLastName = collection["lastName"];
                objRegiserModel.m_strPassword = collection["password"];
                objRegiserModel.m_strEmail = collection["email"];
                objRegiserModel.m_strUserId = objRegiserModel.m_strEmail;
                objRegiserModel.m_strCountryCode = collection["countryCode"];
                objRegiserModel.m_strPhoneNo = collection["phoneNo"];
                objRegiserModel.m_llClassroomId = long.Parse(collection["classroom_id"]);
                long ClassroomId = objRegiserModel.m_llClassroomId;
                var objDynamicRoutingAPIRequestValidator = new StudentAccountRegisterValidator();
                {
                    var result = await objDynamicRoutingAPIRequestValidator.ValidateAsync(objRegiserModel);
                    if (result.IsValid)
                    {
                        if (await objStudentService.RegisterNewStudent(objRegiserModel))
                        {
                            objRegiserModel.m_strUserId = objRegiserModel.m_strEmail;
                            if (objStudentService.ValidateLogin(objRegiserModel))
                            {
                                ViewBag.Token = JwtManager.GenerateToken(objRegiserModel.m_strUserId);
                                ViewBag.InstructorUserName = objRegiserModel.m_strUserId;
                                ViewBag.IsLoggedIn = true;
                                Session["student_email"] = objRegiserModel.m_strUserId;
                                Session["user_id"] = objRegiserModel.m_llStudentId;
                                objRegiserModel = await objStudentService.GetStudentBasicDetails(objRegiserModel.m_llStudentId);
                                Session["student_profile_picture_url"] = objRegiserModel.m_strProfileUrl;
                                if (await objStudentService.RegisterStudentFreeToClassroom(ClassroomId, (long)Session["user_id"]))
                                {
                                    Response.Redirect("~/Student/Home?return_url="+return_url);
                                }
                            }
                        }
                        else
                        {
                            ViewBag.IsRegistered = false;
                        }
                    }
                    else
                    {
                        StringBuilder m_strStringBuilder = new StringBuilder();
                        foreach (var failure in result.Errors)
                        {
                            m_strStringBuilder.Append("Property " + failure.PropertyName + " failed validation. Error was: " + failure.ErrorMessage);
                        }
                        ViewBag.ErrorMessage = m_strStringBuilder.ToString();
                    }
                }
                return View("Join");
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "Register", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
                return View("Error");
            }
        }
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> ValidateLogin(FormCollection collection, string return_url = null)
        {
            string strCurrentMethodName = "Register";
            string ViewName = "";
            try
            {
                StudentRegisterModal objStudentRegisterModal = new StudentRegisterModal();
                objStudentRegisterModal.m_strUserId = collection["userEmail"];
                objStudentRegisterModal.m_strPassword = collection["userPassword"];
                if (collection["remeberMe"] != null)
                {
                    objStudentRegisterModal.m_bIsRemeberMe = collection["remeberMe"].Equals("true") ? true : false;
                }
                var objStudentLoginValidator = new StudentLoginValidator();
                {
                    var ValidationResult = objStudentLoginValidator.Validate(objStudentRegisterModal);
                    if (ValidationResult.IsValid)
                    {
                        if (objStudentService.ValidateLogin(objStudentRegisterModal))
                        {

                            ViewName = "Home";
                            ViewBag.Token = JwtManager.GenerateToken(objStudentRegisterModal.m_strUserId);
                            ViewBag.InstructorUserName = objStudentRegisterModal.m_strUserId;
                            ViewBag.IsLoggedIn = true;
                            Session["student_email"] = objStudentRegisterModal.m_strUserId;
                            Session["user_id"] = objStudentRegisterModal.m_llStudentId;
                            objStudentRegisterModal = await objStudentService.GetStudentBasicDetails(objStudentRegisterModal.m_llStudentId);
                            Session["student_profile_picture_url"] = objStudentRegisterModal.m_strProfileUrl;
                            if (return_url != null)
                            {
                                return Redirect("Home?redirect_url=" + return_url);
                            }
                            else
                            {
                                return Redirect("Home");
                            }
                        }
                        else
                        {
                            ViewName = "Index";
                            ViewBag.IsLoggedIn = false;
                        }
                    }
                    else
                    {
                        StringBuilder m_strStringBuilder = new StringBuilder();
                        foreach (var failure in ValidationResult.Errors)
                        {
                            m_strStringBuilder.Append("Property " + failure.PropertyName + " failed validation. Error was: " + failure.ErrorMessage);
                        }
                        ViewBag.IsLoggedIn = false;
                        ViewName = "Index";
                        ViewBag.ErrorMessage = m_strStringBuilder.ToString();
                    }
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
                ViewName = "Error";
            }
            return View(ViewName);
        }
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> ResetPassword(FormCollection collection)
        {
            string strCurrentMethodName = "ResetPassword";
            string ViewName = "ForgotPassword";
            try
            {
                StudentRegisterModal objStudentRegisterModal = new StudentRegisterModal();
                objStudentRegisterModal.m_strUserId = collection["userEmail"];
                string token = await objStudentService.InsertPasswordRecovery(objStudentRegisterModal.m_strUserId);
                if (token != null && token != string.Empty)
                {
                    string sid = objStudentRegisterModal.m_strUserId;
                    return RedirectToAction("PasswordAuthRequest", new { sid, token });
                }
                else
                {
                    ViewBag.Message = "User Id does not exist";
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
                ViewName = "Error";
            }
            return View(ViewName);

        }
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> SubmitUpdatePasswordOtp(FormCollection collection)
        {
            string strCurrentMethodName = "SubmitUpdatePasswordOtp";
            string ViewName = "";
            try
            {
                StudentUpdatePasswordRequestModal objStudentUpdatePasswordRequestModal = new StudentUpdatePasswordRequestModal();
                objStudentUpdatePasswordRequestModal.m_strUserName = collection["userName"];
                objStudentUpdatePasswordRequestModal.m_strToken = collection["token"];
                objStudentUpdatePasswordRequestModal.m_strOtp = collection["otp"];
                string sid = objStudentUpdatePasswordRequestModal.m_strUserName;
                string token = objStudentUpdatePasswordRequestModal.m_strToken;
                if (await objStudentService.ValidatePasswordRecodevrtOtp(objStudentUpdatePasswordRequestModal))
                {

                    return RedirectToAction("ChangePassword", new { sid, token });
                }
                else
                {
                    ViewBag.StudnentUserName = sid;
                    ViewBag.Token = token;
                    ViewBag.InvalidOtp = true;
                    return View("PasswordAuthRequest");
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
                ViewName = "Error";
            }
            return View(ViewName);
        }
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> ChangePasswordNow(FormCollection collection)
        {
            string strCurrentMethodName = "ChangePasswordNow";
            string ViewName = "";
            try
            {
                StudentUpdatePasswordRequestModal objStudentUpdatePasswordRequestModal = new StudentUpdatePasswordRequestModal();
                objStudentUpdatePasswordRequestModal.m_strUserName = collection["userName"];
                objStudentUpdatePasswordRequestModal.m_strToken = collection["token"];
                objStudentUpdatePasswordRequestModal.m_strPassword = collection["password"];
                objStudentUpdatePasswordRequestModal.m_strMatchPassword = collection["confirmPassword"];
                string sid = objStudentUpdatePasswordRequestModal.m_strUserName;
                string token = objStudentUpdatePasswordRequestModal.m_strToken;
                if (await objStudentService.ChangePasswordAfterAuth(objStudentUpdatePasswordRequestModal))
                {
                    string UserId = objStudentUpdatePasswordRequestModal.m_strUserName;
                    return RedirectToAction("PasswordUpdatedSuccessfully", new { UserId });
                }
                else
                {
                    ViewBag.StudnentUserName = sid;
                    ViewBag.Token = token;
                    ViewBag.InvalidOtp = true;
                    return View("PasswordAuthRequest");
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
                ViewName = "Error";
            }
            return View(ViewName);
        }
        [HttpGet]
        public PartialViewResult ChangePassword(string sid, string token)
        {
            ViewBag.StudentId = sid;
            ViewBag.Token = token;
            return PartialView();
        }
        [HttpGet]
        public PartialViewResult ForgotPassword()
        {
            return PartialView();
        }
        [HttpGet]
        public PartialViewResult Contact()
        {
            return PartialView();
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult PasswordAuthRequest(string sid, string token)
        {
            string strCurrentMethodName = "PasswordAuthRequest";
            try
            {
                ViewBag.StudnentUserName = sid;
                ViewBag.Token = token;
                return View();
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return View("Error");

        }
        [HttpGet]
        public ActionResult PasswordUpdatedSuccessfully(string UserId)
        {
            ViewBag.UserMail = UserId;
            ViewBag.IsRegistered = true;
            return View();
        }
        [HttpGet]
        public ActionResult RegistrationSuccessful(string UserId)
        {
            ViewBag.UserMail = UserId;
            ViewBag.IsRegistered = true;
            return View();
        }
        public async Task<PartialViewResult> Home(string redirect_url = null)
        {
            try
            {
                long StudentId = (long)Session["user_id"];

                StudentHomeModal objStudentHomeModal = await objStudentService.GetStudentHomeDetails(StudentId);
                if (objStudentHomeModal != null)
                {
                    ViewBag.Token = JwtManager.GenerateToken(StudentId.ToString());
                    ViewBag.redirect_url = redirect_url;
                    return PartialView(objStudentHomeModal);
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "Home", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return PartialView("../Shared/Error");
        }
        [HttpGet]
        public ActionResult Logout()
        {
            string strCurrentMethodName = "Logout";
            try
            {
                Session.Remove("user_id");
                Session.Remove("student_user_name");
                return RedirectToAction("Index");
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
                return RedirectToAction("Errors");
            }
        }
        [HttpGet]
        public async Task<PartialViewResult> Account()
        {

            long Id = (long)Session["user_id"];
            StudentRegisterModal objModel = await objStudentService.GetStudentDetails(Id);
            if (objModel == null)
            {
                return PartialView("Error");
            }
            return PartialView(objModel);
        }
        [HttpGet]
        public async Task<ActionResult> EditAccount()
        {

            StudentRegisterModal objModel = await objStudentService.GetStudentDetails((long)Session["user_id"]);
            if (objModel == null)
            {
                return PartialView("Error");
            }
            return PartialView(objModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateDetails(FormCollection collection)
        {
            string strCurrentMethodName = "Register";
            try
            {
                StudentRegisterModal objRegiserModel = new StudentRegisterModal();
                objRegiserModel.m_strFirstName = collection["firstName"];
                objRegiserModel.m_strLastName = collection["lastName"];
                objRegiserModel.m_strPhoneNo = collection["countryCode"] + collection["phoneNo"];
                objRegiserModel.m_strAddressLine1 = collection["address1"];
                objRegiserModel.m_strAddressLine2 = collection["address2"];
                objRegiserModel.m_iCityId = int.Parse(collection["city"]);
                objRegiserModel.m_iStateId = int.Parse(collection["state"]);
                objRegiserModel.m_strGender = collection["gender"];
                objRegiserModel.m_strPinCode = collection["pinCode"];
                objRegiserModel.m_llStudentId = (long)Session["user_id"];
                if (await objStudentService.UpdateStudentDetails(objRegiserModel))
                {
                    ViewBag.IsUpdated = true;
                    return RedirectToAction("Account");
                }
                else
                {
                    ViewBag.IsRegistered = false;
                }
                return View();
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
                return View("Error");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RequestAssignmentAccess(FormCollection collection)
        {
            string strCurrentMethodName = "Register";
            ViewBag.IsAccessError = true;
            try
            {
                if (collection != null)
                {
                    AssignmentAccessModal objAssignmentAccessModal = new AssignmentAccessModal();
                    long.TryParse(collection["assignment_id"], out objAssignmentAccessModal.m_llAssignmentId);
                    objAssignmentAccessModal.m_strAccessCode = collection["assignment_access_code"].ToString();
                    if (await objDocumentService.CheckAssignmentAccess(objAssignmentAccessModal.m_llAssignmentId, objAssignmentAccessModal.m_strAccessCode))
                    {
                        return Redirect("./GiveAssignment?id=" + objAssignmentAccessModal.m_llAssignmentId + "&&access_code=" +
                            objAssignmentAccessModal.m_strAccessCode);
                    }
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
                return View("Error");
            }
            return View("NewAssignment");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RequestClassroomAccess(FormCollection collection)
        {
            string strCurrentMethodName = "Register";
            ViewBag.IsAccessError = true;
            try
            {
                if (collection != null)
                {
                    AssignmentAccessModal objAssignmentAccessModal = new AssignmentAccessModal();
                    long.TryParse(collection["classroom_id"], out objAssignmentAccessModal.m_llAssignmentId);
                    objAssignmentAccessModal.m_strAccessCode = collection["access_code"].ToString();
                    if (await objDocumentService.CheckClassroomAccess(objAssignmentAccessModal.m_llAssignmentId, objAssignmentAccessModal.m_strAccessCode))
                    {
                        return Redirect("./JoinClassroom?id=" + objAssignmentAccessModal.m_llAssignmentId + "&&access_code=" +
                            objAssignmentAccessModal.m_strAccessCode);
                    }
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
                return View("Error");
            }
            return View("JoinNewClassroom");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RequestTestAccess(FormCollection collection)
        {
            string strCurrentMethodName = "RequestTestAccess";
            ViewBag.IsAccessError = true;
            try
            {
                if (collection != null)
                {
                    AssignmentAccessModal objAssignmentAccessModal = new AssignmentAccessModal();
                    long.TryParse(collection["test_id"], out objAssignmentAccessModal.m_llAssignmentId);
                    objAssignmentAccessModal.m_strAccessCode = collection["test_access_code"].ToString();
                    if (await objDocumentService.CheckTestAccess(objAssignmentAccessModal.m_llAssignmentId, objAssignmentAccessModal.m_strAccessCode))
                    {
                        return Redirect("./StartTest?id=" + objAssignmentAccessModal.m_llAssignmentId + "&&access_code=" +
                            objAssignmentAccessModal.m_strAccessCode);
                    }
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
                return View("Error");
            }
            return View("NewTest");
        }
        [HttpGet]
        public PartialViewResult JoinNewCourse()
        {
            try
            {
                return PartialView();
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "JoinNewCourse", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
                return PartialView("Error");
            }
        }
        [HttpGet]
        public async Task<ActionResult> JoinCourse(long id)
        {
            try
            {
                if (!await objStudentService.CheckIsStudentHasJoinedTheCourse(long.Parse(Session["user_id"].ToString()), id))
                {
                    Session["course_id"] = id;
                    ViewBag.id = id;
                    return PartialView();
                }
                else
                {
                    return RedirectToAction("./LearnCourse", new { id });
                }

            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "JoinCourse", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
                return PartialView("Error");
            }

        }
        [HttpGet]
        public PartialViewResult MyCourses()
        {
            try
            {
                return PartialView();
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "MyCourses", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
                return PartialView("Error");
            }

        }
        [HttpGet]
        public PartialViewResult Instructors()
        {
            try
            {
                return PartialView();
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "Instructors", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
                return PartialView("Error");
            }
        }
        [HttpGet]
        public PartialViewResult JoinInstructor()
        {
            try
            {
                return PartialView();
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "JoinInstructor", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
                return PartialView("Error");
            }
        }
        [HttpGet]
        public PartialViewResult JoinNewStudent()
        {
            try
            {
                return PartialView();
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "JoinNewStudent", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
                return PartialView("Error");
            }
        }
        [HttpGet]
        public PartialViewResult JoinedStudents()
        {
            try
            {
                return PartialView();
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "JoinedStduents", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
                return PartialView("Error");
            }
        }
        [HttpGet]
        public PartialViewResult AssignmentSubmissions()
        {
            try
            {
                return PartialView();
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "AssignmentSubmissions", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
                return PartialView("Error");
            }

        }
        [HttpGet]
        public PartialViewResult NewAssignment()
        {
            try
            {
                return PartialView();
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "AssignmentSubmissions", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
                return PartialView("Error");
            }
        }
        [HttpGet]
        public async Task<ActionResult> GiveAssignment(long id, string access_code)
        {

            if (access_code == null || await objDocumentService.CheckAssignmentAccess(id, access_code))
            {
                ViewBag.id = id;
                ViewBag.AccessCode = access_code;
            }
            else
            {
                return Redirect("Home");
            }
            return PartialView();
        }
        [HttpGet]
        public async Task<ActionResult> AssignmentResponse(long id)
        {
            try
            {
                if (await objStudentService.CheckIsAssignmentSubmissionIdExsitsForStudent((long)Session["user_id"], id))
                {
                    ViewBag.id = id;
                    return PartialView();
                }
                else
                {
                    return RedirectToAction("Home");
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "AssignmentResponse", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
                return PartialView("Error");
            }
        }
        [HttpGet]
        public PartialViewResult MyAssignments()
        {
            try
            {
                return PartialView();
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "MyAssignments", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
                return PartialView("Error");
            }
        }
        [HttpGet]
        public PartialViewResult NewTest()
        {
            try
            {
                return PartialView();
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "NewTest", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
                return PartialView("Error");
            }
        }
        [HttpGet]
        public async Task<ActionResult> StartTest(long id, string access_code)
        {
            try
            {
                if (access_code == null || await objDocumentService.CheckTestAccess(id, access_code))
                {
                    ViewBag.id = id;

                }
                else
                {
                    return Redirect("Home");
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "StartTest", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
                return PartialView("Error");
            }
            return PartialView();
        }
        [HttpGet]
        public PartialViewResult TestSubmissions()
        {
            try
            {
                return PartialView();
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "TestSubmissions", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
                return PartialView("Error");
            }

        }
        public async Task<ActionResult> TestResponse(long id)
        {
            try
            {
                if (await objStudentService.CheckIsTestSubmissionIdExsitsForStudent((long)Session["user_id"], id))
                {
                    ViewBag.id = id;
                    return PartialView();
                }
                else
                {
                    return RedirectToAction("Home");
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "TestResponse", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
                return PartialView("Error");
            }
        }
        [HttpGet]
        public async Task<ActionResult> LearnCourse(long id)
        {
            try
            {
                if (await objStudentService.CheckIsStudentHasJoinedTheCourse(long.Parse(Session["user_id"].ToString()), id))
                {
                    ViewBag.id = id;
                    return PartialView();
                }
                else
                {
                    return RedirectToAction("./JoinCourse", new { id });
                }

            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "LearnCourse", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
                return PartialView("Error");
            }

        }
        [HttpGet]
        public async Task<ActionResult> TeacherProfile(int id)
        {
            try
            {
                InstructorProfileDetailsModal objInstructorProfileDetailsModal = await objStudentService.GetInstructorProfileDetails(id, (long)Session["user_id"]);
                if (objInstructorProfileDetailsModal != null)
                {
                    objInstructorProfileDetailsModal.m_lsCourses = await objStudentService.GetAllCourseDetailsForInstructor(id);
                    objInstructorProfileDetailsModal.m_iInstructorId = id;
                    return PartialView(objInstructorProfileDetailsModal);
                }
                else
                {
                    return RedirectToAction("Home");
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "LearnCourse", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
                return PartialView("Error");
            }
        }
        [HttpGet]
        public ActionResult ViewAssignment(int id)
        {
            try
            {
                return View();
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "LearnCourse", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
                return PartialView("Error");
            }
        }
        [HttpGet]
        public ActionResult JoinMeeting()
        {
            try
            {
                return View();
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "LearnCourse", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
                return PartialView("Error");
            }
        }
        [HttpGet]
        public async Task<ActionResult> JoinClassroomMeeting(long ClassroomId)
        {
            JitsiMeetingModal objJitsiMeetingModal = null;
            try
            {
                if (await objStudentService.CheckStudentAccessToClassroom((long)Session["user_id"], ClassroomId))
                {
                    objJitsiMeetingModal = await objStudentService.GetClassroomMeetingDetails(ClassroomId);
                    return View(objJitsiMeetingModal);
                }
                else
                {
                    Response.Redirect("Home");
                }
                return View(objJitsiMeetingModal);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "LearnCourse", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
                return PartialView("Error");
            }
        }
        //[HttpGet]
        //public async Task<ActionResult> JoinClassroomMeetingMobile(string ClassroomName,string meetingName,string Password)
        //{
        //    try
        //    {
        //        JitsiMeetingModal objJitsiMeetingModal = await objStudentService.GetClassroomMeetingDetails(ClassroomId);
        //        return View(objJitsiMeetingModal);
        //    }
        //    catch (Exception Ex)
        //    {
        //        m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
        //        m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "LearnCourse", Ex.ToString());
        //        m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
        //        MainLogger.Error(m_strLogMessage);
        //        return PartialView("Error");
        //    }
        //}
        [HttpGet]
        public async Task<ActionResult> JoinClassroom(long id, string access_code)
        {
            try
            {
                if (await objDocumentService.CheckClassroomAccess(id, access_code))
                {
                    ViewBag.Id = id;
                }
                ClassroomJoinDetailsModal classroomJoinDetailsModal = await objStudentService.GetClassroomDetailsForStudentJoin(id);
                ViewBag.Id = id;
                return View(classroomJoinDetailsModal);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "LearnCourse", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
                return PartialView("Error");
            }
        }
        [HttpGet]
        public async Task<ActionResult> ViewClassroom(long classroom_id)
        {
            try
            {
                if (await objStudentService.CheckStudentAccessToClassroom((long)Session["user_id"], classroom_id))
                {
                    StudentClassroomHomeDetails studentClassroomHomeDetails = await objStudentService.GetStudentClassroomHomeDetails(classroom_id, (long)Session["user_id"]);
                    if (studentClassroomHomeDetails.m_bShouldBlockClassroomAccess)
                    {
                        return Redirect("ClassroomPayNow?classroom_id=" + classroom_id);
                    }
                    ViewBag.Id = classroom_id;
                    return View(studentClassroomHomeDetails);
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "LearnCourse", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
                return PartialView("Error");
            }
            return PartialView("UnAuthorizedAccess");
        }
        [HttpGet]
        public async Task<ActionResult> ClassroomPayNow(long classroom_id)
        {
            try
            {
                if (await objStudentService.CheckStudentAccessToClassroom((long)Session["user_id"], classroom_id))
                {
                    StudentClassroomHomeDetails studentClassroomHomeDetails = await objStudentService.GetStudentClassroomHomeDetails(classroom_id, (long)Session["user_id"]);
                    ViewBag.Id = classroom_id;
                    return View(studentClassroomHomeDetails);
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "ClassroomPayNow", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
                return PartialView("Error");
            }
            return PartialView("UnAuthorizedAccess");
        }

        [HttpGet]
        public ActionResult MyClassrooms()
        {
            //id =classroom id
            try
            {
                return View();
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "LearnCourse", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
                return PartialView("Error");
            }

        }

        [HttpGet]
        public ActionResult JoinNewClassroom()
        {
            try
            {

                return View();
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "LearnCourse", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
                return PartialView("Error");
            }
        }
        [HttpGet]
        public ActionResult JoinPrivateClassroom()
        {
            try
            {

                return View();
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "LearnCourse", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
                return PartialView("Error");
            }
        }
        [HttpGet]
        public async Task<ActionResult> PreviewClassroom(long ClassroomId)
        {
            try
            {
                if (await objStudentService.CheckStudentAccessToClassroom((long)Session["user_id"], ClassroomId))
                {
                    Response.Redirect("./ViewClassroom?classroom_id=" + ClassroomId);
                }
                ClassroomJoinDetailsModal classroomJoinDetailsModal = await objStudentService.GetClassroomDetailsForStudentJoin(ClassroomId);
                ViewBag.Id = ClassroomId;
                return View(classroomJoinDetailsModal);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "LearnCourse", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
                return PartialView("Error");
            }
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> UpdateProfilePictute(FormCollection collection)
        {
            string strCurrentMethodName = "UpdateProfilePictute";
            string ViewName = "Home";
            try
            {
                StudentProfileChangeRequest studentProfileChangeRequest = new StudentProfileChangeRequest();
                studentProfileChangeRequest.m_llStudentId = (long)Session["user_id"];
                studentProfileChangeRequest.imageUploadDetailsModal = new ImageUploadDetailsModal
                {
                    m_strOriginalFileUrl = collection["url"],
                    m_strMediumSizeUrl = collection["medium_size_url"],
                    m_strSmallSizeUrl = collection["small_size_url"]
                };
                if (await objStudentService.UpdateProfilePicture(studentProfileChangeRequest))
                {
                    Session["student_profile_picture_url"] = studentProfileChangeRequest.imageUploadDetailsModal.m_strSmallSizeUrl;
                    return RedirectToAction("Home");
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
                ViewName = "Error";
            }
            return View(ViewName);
        }
        [HttpGet]
        public PartialViewResult Activity()
        {
            string strCurrentMethodName = "Activity";
            try
            {
                return PartialView();
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
                return PartialView("Errors");
            }

        }
        [HttpGet]
        public PartialViewResult FetchTests()
        {
            string strCurrentMethodName = "FetchTests";
            try
            {
                return PartialView();
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
                return PartialView("Errors");
            }
        }
        [HttpGet]
        public async Task<ActionResult> Search(string q)
        {
            try
            {
                StudentSearchRequestModal objSearchRequest = new StudentSearchRequestModal();
                objSearchRequest.m_strQueryString = q;
                objSearchRequest.m_llStudentId = (long)Session["user_id"];
                StudentSearchResponse studentSearchResponse = await objStudentService.GetStudentSearchResult(objSearchRequest);
                return View(studentSearchResponse);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "Register", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
                return View("Error");
            }
            //return View("UnAuthorizedAccess.cshtml");
        }
        [HttpGet]
        public async Task<ActionResult> StudentProfile(long id)
        {
            try
            {

                if (!await objStudentService.CheckStudentFollowingStudent(long.Parse(Session["user_id"].ToString()), id))
                {
                    ViewBag.id = id;
                    return PartialView();
                }
                else
                {
                    return Redirect("StudentFriend?id=" + id);
                }

            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "LearnCourse", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
                return PartialView("Error");
            }
        }
        [HttpGet]
        public async Task<ActionResult> StudentFriend(long id)
        {
            try
            {
                if (await objStudentService.CheckStudentFollowingStudent(long.Parse(Session["user_id"].ToString()), id))
                {
                    ViewBag.id = id;
                    return PartialView();
                }
                else
                {
                    return Redirect("./StudentProfile?id=" + id);
                }

            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "LearnCourse", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
                return PartialView("Error");
            }

        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<string> GoogleFormRegistration(GoogleSignInRequest googleSignInRequest)
        {
            string returnUrl = "Join";
            try
            {
                StudentRegisterModal objRegiserModel = objStudentService.GetStudentModalFromGoogleModal(googleSignInRequest);
                var objDynamicRoutingAPIRequestValidator = new StudentAccountRegisterValidator();
                {
                    if (await objStudentService.RegisterNewStudentViaGmail(objRegiserModel))
                    {
                        StudentRegisterModal userDetails = await objStudentService.CheckGmailUserAlreadyExists(objRegiserModel);
                        objRegiserModel.m_strUserId = objRegiserModel.m_strEmail;
                        StudentRegisterModal studentDetails = await objStudentService.CheckGmailUserAlreadyExists(objRegiserModel);
                        if (studentDetails != null)
                        {
                            ViewBag.Token = JwtManager.GenerateToken(objRegiserModel.m_strUserId);
                            ViewBag.InstructorUserName = objRegiserModel.m_strUserId;
                            ViewBag.IsLoggedIn = true;
                            Session["student_email"] = objRegiserModel.m_strUserId;
                            Session["user_id"] = studentDetails.m_llStudentId;
                            Session["student_profile_picture_url"] = studentDetails.m_strProfileUrl;
                            if (googleSignInRequest.m_bShouldVarifyPhoneNo == "true")
                            {
                                returnUrl = "/Student/VarifyPhoneNo?user_id=" + objRegiserModel.m_strUserId + "&&token=" + objRegiserModel.m_strPhoneNoVarificationGuid;
                            }
                            else
                            {
                                returnUrl = "/Student/Home";
                            }
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "Register", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
                returnUrl = "Error";
            }
            return returnUrl;
        }
        [HttpGet]
        public async Task<ActionResult> VarifyPhoneNo(string user_id, string token,string redirect_url=null)
        {
            try
            {
                if (await objStudentService.InsertOtpToVarifyAccount(long.Parse(Session["user_id"].ToString())))
                {
                    ViewBag.token = token;
                    ViewBag.userId = user_id;
                    ViewBag.redirectUrl = redirect_url;
                    return PartialView();
                }
                else
                {
                    return Redirect("./");
                }

            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "LearnCourse", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
                return PartialView("Error");
            }
        }
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> VarifyPhoneNo(FormCollection collection,string redirect_url = null)
        {
            string strCurrentMethodName = "VarifyPhoneNo";
            string ViewName = "";
            try
            {
                string token = collection["token"];
                string otp = collection["otp"];
                string userId = collection["userName"];

                if (await objStudentService.VarifyPhoneNo(otp, userId, token))
                {
                    if (redirect_url != null)
                    {
                        return Redirect("Home?redirect_url="+ redirect_url);
                    }
                    else
                    {
                        return Redirect("Home");
                    }
                }
                else
                {
                    ViewBag.Token = token;
                    ViewBag.InvalidOtp = true;
                    ViewBag.redirectUrl = redirect_url;
                    return View("VarifyPhoneNo");
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
                ViewName = "Error";
            }
            return View(ViewName);
        }
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> ValidateGmailLogin(FormCollection collection, string return_url = null)
        {
            string strCurrentMethodName = "ValidateGmailLogin";
            string ViewName = "Index";
            try
            {
                StudentRegisterModal objStudentRegisterModal = new StudentRegisterModal();
                objStudentRegisterModal.m_strEmail = collection["user_id"];
                objStudentRegisterModal.m_strUserId = collection["user_id"];
                objStudentRegisterModal.m_strGmailId = collection["gmail_id"];
                objStudentRegisterModal.m_strProfileUrl = collection["profile_url"];
                objStudentRegisterModal.m_strFirstName = collection["first_name"];
                objStudentRegisterModal.m_strLastName = collection["last_name"];
                StudentRegisterModal userDetails = await objStudentService.CheckGmailUserAlreadyExists(objStudentRegisterModal);
                if (userDetails == null)
                {
                    //register student
                    if (await objStudentService.RegisterNewStudentViaGmail(objStudentRegisterModal))
                    {
                        userDetails = await objStudentService.CheckGmailUserAlreadyExists(objStudentRegisterModal);
                    }
                }
                if (userDetails != null)
                {
                    Session["user_id"] = userDetails.m_llStudentId;
                    Session["student_profile_picture_url"] = userDetails.m_strProfileUrl;
                    Session["student_email"] = objStudentRegisterModal.m_strUserId;
                    if (userDetails.m_strPhoneNo != "" && userDetails != null)
                    {
                        ViewBag.Token = JwtManager.GenerateToken(objStudentRegisterModal.m_strUserId);
                       
                        ViewBag.InstructorUserName = objStudentRegisterModal.m_strUserId;
                        ViewBag.IsLoggedIn = true;
                        if (return_url != null)
                        {
                            return Redirect("Home?redirect_url=" + return_url);
                        }
                        else
                        {
                            return Redirect("Home");
                        }
                    }
                    else
                    {
                        return Redirect("SavePhoneNoStep?user_id=" + objStudentRegisterModal.m_strUserId + "&&token=" + userDetails.m_strPhoneNoVarificationGuid+ "&&redirect_url="+ return_url);
                    }
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
                ViewName = "Error";
            }
            return View(ViewName);
        }
        [HttpGet]
        public ActionResult SavePhoneNoStep(string user_id, string token,string redirect_url=null)
        {
            ViewBag.token = token;
            ViewBag.userId = user_id;
            ViewBag.redirectUrl = redirect_url;
            return View();
        }
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> UpdatePhoneNo(FormCollection collection,string redirect_url=null)
        {
            string ViewName = "Index";
            string strCurrentMethodName = "UpdatePhoneNo";
            try
            {
                string phoneNo = collection["country_code"]+collection["phone_no"];
                string token = collection["token"];
                string email = collection["user_id"];
                string VarifyPhone= collection["varify_phone"];
                if (await objStudentService.UpdatePhoneNoOfGmailRegStudentAsync(phoneNo, email, token))
                {
                    if (VarifyPhone == "true")
                    {
                        return Redirect("VarifyPhoneNo?user_id="+email+"&token="+token+ "&redirect_url=" + redirect_url);
                    }
                    if (redirect_url != null)
                    {
                        return Redirect("Home?redirect_url="+redirect_url);
                    }
                    return Redirect("Home");
                }
                else
                {
                    ViewBag.Token = token;
                    ViewBag.InvalidOtp = true;
                    return View("VarifyPhoneNo");
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
                ViewName = "Error";
            }
            return View(ViewName);

        }
    }
}