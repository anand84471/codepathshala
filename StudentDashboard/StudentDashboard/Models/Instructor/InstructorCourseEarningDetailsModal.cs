using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.Models.Instructor
{
    public class InstructorCourseEarningDetailsModal
    {
        [JsonProperty("month")]
        public string m_strMonthName;
        [JsonProperty("earning")]
        public int m_iEarning;
        public InstructorCourseEarningDetailsModal(string MonthName,int Earning)
        {
            this.m_strMonthName = MonthName;
            this.m_iEarning = Earning/100;
        }
    }
}