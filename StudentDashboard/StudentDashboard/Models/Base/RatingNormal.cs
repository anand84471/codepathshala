using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.Models.Base
{
    public class RatingNormal
    {
        public int m_iRating;
        public int m_iNoOfRating;
        public RatingNormal(int Rating,int CountOfRating)
        {
            this.m_iNoOfRating = CountOfRating;
            this.m_iRating = Rating;
        }
    }
}