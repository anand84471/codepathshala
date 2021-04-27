using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.Models
{
    public class CouponDetailsModal
    {
        public string m_strCouponCode;
        public int m_iCouponDicount;
        public CouponDetailsModal(string CouponCode,int CouponDiscount)
        {
            this.m_iCouponDicount = CouponDiscount;
            this.m_strCouponCode = CouponCode;
        }
    }
}