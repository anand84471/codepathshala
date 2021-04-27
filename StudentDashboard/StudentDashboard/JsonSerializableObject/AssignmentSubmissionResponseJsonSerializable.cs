using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.JsonSerializableObject
{
    public class AssignmentSubmissionResponseJsonSerializable
    {
        public long m_llQuestionId { get; set; }

        public int m_iOptionSelected { get; set; }

        public int m_CorrectOption { get; set; }

        public string m_strQuestionStatement { get; set; }

        public string m_strOption1 { get; set; }

        public string m_strOption2 { get; set; }

        public string m_strOption3 { get; set; }

        public string m_strOption4 { get; set; }
    }
}