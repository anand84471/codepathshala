using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Routing;

namespace StudentDashboard.Security.Instrcutor
{
    public class InstructorAuthenticationFilter: ActionFilterAttribute, IAuthenticationFilter
    {
        void IAuthenticationFilter.OnAuthentication(AuthenticationContext filterContext)
        {
            List<string >allowAnnonymousAction = new List<string> {
                "Index","LogIn","ForgotPassword","Join","RegistrationSuccessful","Register","ValidateLogin","LoginFirst","PasswordAuthRequest","ResetPassword",
                "PasswordUpdatedSuccessfully","SubmitUpdatePasswordOtp","ChangePassword","ChangePasswordNow"
            };
            if (string.IsNullOrEmpty(Convert.ToString(filterContext.HttpContext.Session["instructor_id"]))&&!(allowAnnonymousAction.Contains(filterContext.ActionDescriptor.ActionName)))
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
                    { "controller", "Instructor" },
                    { "action", "Index" },
                    {"return_url" ,HttpContext.Current.Request.Url.AbsoluteUri}
                    });
            }
        }
    }
}