using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.Vendors.AWS.Entity
{
    public abstract class AWSServiceEntity
    {
        protected string m_strAccessKey;
        protected string m_strSecurityKey;
        protected AWSServiceEntity()
        {
            m_strAccessKey = MvcApplication._strAmazonAwsAccessKey;
            m_strSecurityKey = MvcApplication._strAwsSecurityKey;
        }
    }
}