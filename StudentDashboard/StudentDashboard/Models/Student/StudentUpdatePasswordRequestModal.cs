using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.Models.Student
{
    public class StudentUpdatePasswordRequestModal
    {
        public string m_strUserName;
        public string m_strToken;
        public string m_strOtp;
        public string m_strPassword;
        public string m_strMatchPassword;
        public string m_strHashedPassword;
    }
}