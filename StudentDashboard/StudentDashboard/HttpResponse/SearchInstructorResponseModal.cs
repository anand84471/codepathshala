using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpResponse
{
    public class SearchInstructorResponseModal
    {
        [JsonProperty("name")]
        public string m_strInstructorFullName;
        [JsonProperty("no_of_course")]
        public int m_iNoOfCourseCreated;
        [JsonProperty("date_of_joing")]
        public string m_strDateOfJoining;
        [JsonProperty("rating")]
        public double rating;
        [JsonProperty("no_of_followers")]
        public int m_iNoOfFollowers;
        [JsonProperty("id")]
        public int m_iInstructorId;
        [JsonProperty("no_of_students_joined")]
        public int m_iNoOfStudentsJoined;
        [JsonProperty("profile_url")]
        public string m_strProfileUrl;
        public SearchInstructorResponseModal()
        {

        }
        public SearchInstructorResponseModal(int InstructorId,string FirstName,string LastName,string DateOfJoining,int NoOfCourseCreated,int NoOfStudentsJoined)
        {
            this.m_strInstructorFullName = FirstName + " "+ LastName;
            this.m_strDateOfJoining = DateOfJoining;
            this.m_iNoOfCourseCreated = NoOfCourseCreated;
            this.m_iInstructorId = InstructorId;
            this.m_iNoOfStudentsJoined = NoOfStudentsJoined;
        }
        public SearchInstructorResponseModal(int InstructorId, string FirstName, string LastName, string DateOfJoining, int NoOfCourseCreated, int NoOfStudentsJoined,
            string ProfileUrl)
        {
            this.m_strInstructorFullName = FirstName + " " + LastName;
            this.m_strDateOfJoining = DateOfJoining;
            this.m_iNoOfCourseCreated = NoOfCourseCreated;
            this.m_iInstructorId = InstructorId;
            this.m_iNoOfStudentsJoined = NoOfStudentsJoined;
            this.m_strProfileUrl = ProfileUrl;
        }
    }
}