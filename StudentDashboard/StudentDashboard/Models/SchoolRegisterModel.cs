using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.Models
{
    public class SchoolRegisterModel
    {
        public string m_strSchoolName { get; set; }
        public string m_strAddressLine1 { get; set; }
        public string m_strAddressLine2 { get; set; }
        public int m_iCityId { get; set; }
        public int m_iPinCode { get; set; }
        public string m_strSchoolUserId { get; set; }
        public string m_strPassword { get; set; }
        public string m_strPhoneNo { get; set; }
        public string m_strEmailId { get; set; }
    }
}