using Newtonsoft.Json;
using StudentDashboard.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.HttpResponse
{
    public class GetPublicClassroomsResponse
    {
        [JsonProperty("classroom_name")]
        public string m_strClasssroomName;
        [JsonProperty("classroom_id")]
        public long m_llClasssroomId;
        [JsonProperty("classroom_creation_date")]
        public string m_strCreationDate;
        [JsonProperty("no_of_enrollments")]
        public int m_iNoOfEnrollments;
        [JsonProperty("is_joined")]
        public bool m_IsJoined;
        [JsonProperty("joining_date")]
        public string m_strJoiningDate;
        [JsonProperty("classroom_joining_fee")]
        public string m_strClassrooomJoiningFee;
        [JsonProperty("instructor_name")]
        public string m_strInstructorName;
        [JsonProperty("classroom_image")]
        public string m_strClassroomImage;
        [JsonProperty("instructor_image")]
        public string m_strInstructorImage;
        [JsonProperty("instructor_id")]
        public int m_iInstructorId;
        [JsonProperty("classroom_rating")]
        public string m_strClassroomRating;
        [JsonProperty("no_of_ratings")]
        public int m_iNoOfRatings;
        [JsonProperty("is_registration_closed")]
        public bool m_bIsRegistrationClosed;
        public int PriceForForeignStudents;
        public GetPublicClassroomsResponse(string classroomName,
            long classroomId,string creationDate,int noOfEnrollments,long? StudentJoinId,
            DateTime? JoiningDate,int ClassroomJoiningFeeInPaise,string InstructorName,string InstructorImageUrl,
            int InstructorId,string ClassroomImageUrl,DateTime? registrationCloseDate)
        {
            this.m_strClasssroomName = classroomName;
            this.m_llClasssroomId = classroomId;
            this.m_strCreationDate = creationDate;
            this.m_iNoOfEnrollments = noOfEnrollments;
            if(StudentJoinId!=null)
            {
                m_IsJoined = true;
            }
            if(ClassroomJoiningFeeInPaise==0)
            {
                this.m_strClassrooomJoiningFee = "free";
            }
            else
            {
                this.m_strClassrooomJoiningFee = "&#8377 "+(ClassroomJoiningFeeInPaise/100).ToString()+ " / US&#36 " + PriceUtils.GetPriceForForeignStudents(ClassroomJoiningFeeInPaise);
            }
            this.m_strClassroomImage = ClassroomImageUrl;
            this.m_strInstructorName = InstructorName;
            this.m_iInstructorId = InstructorId;
            this.m_strInstructorImage = InstructorImageUrl;
            if (registrationCloseDate != null)
            {
                if ((DateTime)registrationCloseDate < DateTime.Now)
                {
                    m_bIsRegistrationClosed = true;
                }
            }
        }
    }
}