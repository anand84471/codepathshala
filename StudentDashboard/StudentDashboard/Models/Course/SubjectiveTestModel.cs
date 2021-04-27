using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.Models.Course
{
    public class SubjectiveTestModel:TestModel
    {
        [JsonProperty("questions")]
        public List<SubjectiveQuestion> m_lsSubjectiveQuestion { get; set; }
    }
}