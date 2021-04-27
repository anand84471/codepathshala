using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.Models.Course
{
    public class ObjectiveTestModel:TestModel
    {
        [JsonProperty("questions")]
        public List<ObjectiveQuestion> m_lsObjectiveQuestion { get; set; }
    }
}