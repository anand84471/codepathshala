using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.Models.Student
{
    public class StudentBasicClassroomDetails
    {
        public string m_strClassroomName;
        public long m_llClassroomId;
        public string m_strClassroomDescription;
        public string m_strClassroomJoiningDate;
        public StudentBasicClassroomDetails(long ClassroomId,string ClassroomName,string ClassroomDescription,
            string ClassroomJoiningDate)
        {
            this.m_strClassroomDescription = ClassroomDescription;
            this.m_strClassroomName = ClassroomName;
            this.m_strClassroomJoiningDate = ClassroomJoiningDate;
            this.m_llClassroomId = ClassroomId;
        }
    }
}