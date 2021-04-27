using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.Vendors.AWS.Entity
{
    public class AWSS3ServiceEntity:AWSServiceEntity
    {
        private readonly string m_strAwsBucketName;
        private readonly string m_strAwsFolderStudent;
        private readonly string m_strAwsFolderInstructor;
        public AWSS3ServiceEntity():base()
        {
            m_strAwsBucketName = MvcApplication._strAwsBucketName;
            m_strAwsFolderInstructor = MvcApplication._strAwsBucketFolderInstructor;
            m_strAwsFolderStudent = MvcApplication._strAwsBucketFolderStudent;
        }
        public string GetBucketName()
        {
            return m_strAwsBucketName;
        }
        public string GetStudentBucketFolderName()
        {
            return m_strAwsFolderStudent;
        }
        public string GetAccessKey()
        {
            return m_strAccessKey;
        }
        public string GetSecurityKey()
        {
            return m_strSecurityKey;
        }
        public string GetInstructorBucketFolderName()
        {
            return m_strAwsFolderInstructor;
        }
    }
}