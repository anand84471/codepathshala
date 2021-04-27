using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.Models.OAuth
{
    public class GoogleSignInRequest
    {
 
        public string m_strGoogleId { get; set; }

        public string m_strFirstName { get; set; }
    
        public string m_strLastName { get; set; }
        
        public string m_strPhoneNo { get; set; }
    
        public string m_strProfileUrl { get; set; }

        public string m_strEmailId { get; set; }
        public string m_bShouldVarifyPhoneNo { get; set; }
    }
}