using StudentDashboard.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace StudentDashboard.Security
{
    public class SignInWithGoogleValidator
    {
        private const string GoogleApiTokenInfoUrl = "https://www.googleapis.com/oauth2/v3/tokeninfo?id_token={0}";

        //public SignInWithGoogleProfileDetails GetUserDetails(string providerToken)
        //{
        //    var httpclient = new httpclie();
        //    var requesturi = new uri(string.format(googleapitokeninfourl, providertoken));

        //    httpresponsemessage httpresponsemessage;
        //    try
        //    {
        //        httpresponsemessage = httpclient.getasync(requesturi).result;
        //    }
        //    catch (exception ex)
        //    {
        //        return null;
        //    }

        //    if (httpresponsemessage.statuscode != httpstatuscode.ok)
        //    {
        //        return null;
        //    }

        //    var response = httpresponsemessage.content.readasstringasync().result;
        //    var googleapitokeninfo = jsonconvert.deserializeobject<googleapitokeninfo>(response);

        //    if (!supportedclientsids.contains(googleapitokeninfo.aud))
        //    {
        //        log.warnformat("google api token info aud field ({0}) not containing the required client id", googleapitokeninfo.aud);
        //        return null;
        //    }

        //    return new signinwithgoogleprofiledetails
        //    {
        //        email = googleapitokeninfo.email,
        //        firstname = googleapitokeninfo.given_name,
        //        lastname = googleapitokeninfo.family_name,
        //        locale = googleapitokeninfo.locale,
        //        name = googleapitokeninfo.name,
        //        provideruserid = googleapitokeninfo.sub
        //    };
        //}
    }
}