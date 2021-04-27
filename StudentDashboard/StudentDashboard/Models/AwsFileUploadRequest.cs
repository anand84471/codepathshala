using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.Models
{
    public class AwsFileUploadRequest
    {
        public string m_strFileName;
        public string m_strFilePath;
        public AwsFileUploadRequest(string FileName,string FilePath)
        {
            this.m_strFileName = FileName;
            this.m_strFilePath = FilePath;
        }
    }
}