using StudentDashboard.HttpResponse;
using StudentDashboard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpRequest
{
    public class InstructorProfileChangeRequest
    {
        public int m_iInstructorId;
        public ImageUploadDetailsModal imageUploadDetailsModal; 
    }
}