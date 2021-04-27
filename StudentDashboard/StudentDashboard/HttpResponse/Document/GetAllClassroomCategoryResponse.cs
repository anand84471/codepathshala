using Newtonsoft.Json;
using StudentDashboard.Models.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpResponse.Document
{
    public class GetAllClassroomCategoryResponse:APIDefaultResponse
    {
        [JsonProperty("categories")]
        public List<GetAllClassroomCategory> m_lsGetAllClassroomCategory;

    }
}