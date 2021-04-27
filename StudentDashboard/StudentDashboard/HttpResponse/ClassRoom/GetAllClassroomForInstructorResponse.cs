using Newtonsoft.Json;
using StudentDashboard.Models.Instructor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpResponse.ClassRoom
{
    public class GetAllClassroomForInstructorResponse:APIDefaultResponse
    {
        [JsonProperty("classrooms")]
        public List<ClassRoomModal> m_lsClassRoomModal;
        public GetAllClassroomForInstructorResponse():base()
        {

        }
    }
}