using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.Utilities
{
    public class PriceUtils
    {
        public static int GetPriceForForeignStudents(int PriceInPaise)
        {
            if (PriceInPaise <=10000)
            {
                return 3;
            }
            return (PriceInPaise / 100) / 100 * 3;
        }
    }
}