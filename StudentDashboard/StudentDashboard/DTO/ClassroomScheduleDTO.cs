using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.DTO
{
    public class ClassroomScheduleDTO
    {
        public DateTime? m_dtClassroomScheduleInsertionTime;
        public DateTime? m_dtClassroomScheduleUpdationTime;
        public string m_strClassroomScheduleObj;
        public ClassroomScheduleDTO(string ClassroomScheduleObj,DateTime? ClassroomScheduleInsertionTime,
            DateTime? ClassroomScheduleUpdationTime)
        {
            this.m_strClassroomScheduleObj = ClassroomScheduleObj;
            this.m_dtClassroomScheduleInsertionTime = ClassroomScheduleInsertionTime;
            this.m_dtClassroomScheduleUpdationTime = ClassroomScheduleUpdationTime;
        }
    }
}