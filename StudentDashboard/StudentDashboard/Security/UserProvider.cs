using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;

namespace StudentDashboard.Security
{
    public class StudentUserProvider
    {
        public long GetUserId()
        {
            return 1;
        }

    }
}
