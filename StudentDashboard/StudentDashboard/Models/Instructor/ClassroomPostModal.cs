using Newtonsoft.Json;
namespace StudentDashboard.Models.Instructor
{
    public class ClassroomPostModal
    {
        [JsonProperty("post")]
        public string m_strPost;
        [JsonProperty("post_id")]
        public long m_llPostId;
        [JsonProperty("classroom_id")]
        public long m_llClassroomId;
        public ClassroomPostModal()
        {

        }
        public ClassroomPostModal(long PostId,string Post)
        {
            this.m_llPostId = PostId;
            this.m_strPost = Post;
        }
    }
}