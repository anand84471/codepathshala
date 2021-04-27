using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.Models.Category
{
    public class GetAllClassroomCategory
    {
        [JsonProperty("category_id")]
        public int m_iCategoriesId;
        [JsonProperty("category_name")]
        public string m_strCategoryName;
        public GetAllClassroomCategory(int ClassroomCategory,string CategoryName)
        {
            this.m_iCategoriesId = ClassroomCategory;
            this.m_strCategoryName = CategoryName;
        }
    }
}