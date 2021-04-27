using Newtonsoft.Json;
using StudentDashboard.Models.Instructor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpResponse.ClassRoom
{
    public class ClassroomDetailsResponse:APIDefaultResponse
    {
        [JsonProperty("classroom_details")]
        public ClassRoomModal m_objClassRoomModal;
    }
}