using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.Models
{
    public class StaeModel
    {
        public string strFisrtName { get; set; }
        public string strLastName { get; set; }
        public string strPhoneNo { get; set; }
        public string strCity { get; set; }
        public string strState { get; set; }
        public string strPineCode { get; set; }
        public string strEmail { get; set; }
        public string strPassword { get; set; }
        public StaeModel(string FirstName,string LastName)
        {

        }
        public StaeModel()
        {

        }

    }
}