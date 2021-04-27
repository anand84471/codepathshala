using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Routing;

namespace StudentDashboard.Security.Student
{
    public class StudentAuthenticationFilter: ActionFilterAttribute,IAuthenticationFilter
    {
        void IAuthenticationFilter.OnAuthentication(AuthenticationContext filterContext)
        {
            List<string> allowAnnonymousAction = new List<string> {
                "Index","LogIn","ForgotPassword","Join","RegistrationSuccessful","Register","ValidateLogin","LoginFirst","PasswordAuthRequest","ResetPassword",
                "ChangePassword","SubmitUpdatePasswordOtp","ChangePasswordNow","PasswordUpdatedSuccessfully","RegisterLiveClass","GoogleFormRegistration",
                "ValidateGmailLogin","SavePhoneNoStep","UpdatePhoneNo","VarifyPhoneNo"
            };
            if (string.IsNullOrEmpty(Convert.ToString(filterContext.HttpContext.Session["user_id"])) && !(allowAnnonymousAction.Contains(filterContext.ActionDescriptor.ActionName)))
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }

        void IAuthenticationFilter.OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            if (filterContext.Result == null || filterContext.Result is HttpUnauthorizedResult)
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary {
                    { "controller", "Student" },
                    { "action", "Index" }, 
                    {"return_url" ,HttpContext.Current.Request.Url.AbsoluteUri}
                    });
            }
        }
    }
}