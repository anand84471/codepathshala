using Newtonsoft.Json;

namespace StudentDashboard.Models.Instructor
{
    public class MasterInstructorDetails
    {
        [JsonProperty("instructor_name")]
        public string m_strInstructorName;
        [JsonProperty("profile_url")]
        public string m_strProfileUrl;
        [JsonProperty("instructor_id")]
        public int m_llIntructorId;
        public MasterInstructorDetails(string InstructorName,string ProfileUrl,int InstructorId)
        {
            this.m_llIntructorId = InstructorId;
            this.m_strProfileUrl = ProfileUrl;
            this.m_strInstructorName = InstructorName;
        }
    }
}