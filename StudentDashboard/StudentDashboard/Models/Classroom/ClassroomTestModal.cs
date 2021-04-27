using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.Models.Classroom
{
    public class ClassroomTestModal
    {
        [JsonProperty("classroom_id")]
        public long m_llClassroomId;
        [JsonProperty("test_name")]
        public string m_strTestName;
        [JsonProperty("no_of_submissions")]
        public int m_iNoOfSubmissions;
        [JsonProperty("creation_date")]
        public string m_strCreationDate;
        [JsonProperty("test_id")]
        public long m_llTestId;
        [JsonProperty("no_of_questions")]
        public int m_iNoOfQuestions;
        public ClassroomTestModal(long ClassroomId,long TestId,string TestName,string CreationDate,int NoOfSubmissions,int NoOfQuestions)
        {
            this.m_llClassroomId = ClassroomId;
            this.m_llTestId = TestId;
            this.m_strTestName = TestName;
            this.m_strCreationDate = CreationDate;
            this.m_iNoOfSubmissions = NoOfSubmissions;
            this.m_iNoOfQuestions = NoOfQuestions;
        }
    }
}