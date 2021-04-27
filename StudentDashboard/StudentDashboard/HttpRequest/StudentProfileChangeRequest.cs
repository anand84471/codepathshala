using StudentDashboard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpRequest
{
    public class StudentProfileChangeRequest
    {
        public ImageUploadDetailsModal imageUploadDetailsModal;
        public long m_llStudentId;
    }
}