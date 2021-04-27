using Newtonsoft.Json;
using StudentDashboard.Models.Instructor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.Models
{
    public class InstructorRegisterModel
    {
        [JsonProperty("first_name")]
        public string m_strFirstName { get; set; }
        [JsonProperty("last_name")]
        public string m_strLastName { get; set; }
        [JsonProperty("phone_no")]
        public string m_strPhoneNo { get; set; }
        [JsonProperty("city")]
        public string m_strCity { get; set; }
        public int? m_iCityid { get; set; }
        [JsonProperty("state")]
        public string m_strState { get; set; }
        public int? m_iStateId { get; set; }
        [JsonProperty("pin_code")]
        public string m_strPineCode { get; set; }
        [JsonProperty("school_name")]
        public string m_strSchoolName { get; set; }
        public string m_strCountryCode { get; set; }
        public int m_iSchoolId { get; set; }
        [JsonProperty("address_line_1")]
        public string m_strAddressLine1 { get; set; }
        [JsonProperty("address_line_2")]
        public string m_strAddressLine2 { get; set; }
        [JsonProperty("gender")]
        public string m_strGender { get; set; }
        [JsonProperty("email")]
        public string m_strEmail { get; set; }
        public string m_strPassword { get; set; }
        public bool m_bIsRememberMe { get; set; }
        public int m_iInstructorId { get; set; }
        [JsonProperty("last_updated")]
        public DateTime? m_dtLastUpdated { get; set; }
        [JsonProperty("date_of_joining")]
        public DateTime m_dtDateOfJoining { get; set; }
        public int m_iNoOfCourseCreated { get; set; }
        public int m_iNoOfAssignmentCreated { get; set; }
        public int m_iNoOfTestCreated { get; set; }
        public int m_iNoOfStudentsJoined { get; set; }
        public int m_iActiveCourses { get; set; }
        public int m_iInActiveCourses { get; set; }
        public int m_iDeletedCourses { get; set; }
        public string m_strFullAddress { get; set; }
        public string m_strPinCode { get; set; }
        public char m_cGender { get; set; }
        [JsonIgnore]
        public string m_strEmailVarificationGuid;
        [JsonIgnore]
        public string m_strPhoneNoVarificationGuid;
        public string m_strProfilePictureUrl;
        public string m_strLinkedInId;
        public string m_strGoogleScholarId;
        public string m_strInstructorBio;
        public InstructorSchoolDetailsModal instructorSchoolDetailsModal;
        public InstructorRegisterModel(string FirstName, string LastName,int InstructorId,string ProfileUrl)
        {
            this.m_strFirstName = FirstName;
            this.m_strLastName = LastName;
            this.m_iInstructorId = InstructorId;
            this.m_strProfilePictureUrl = ProfileUrl;
            if(this.m_strProfilePictureUrl==null|| this.m_strProfilePictureUrl=="")
            {
                this.m_strProfilePictureUrl = "../Images/avatar-user.png";
            }
        }
        public InstructorRegisterModel(int CourseCreated, int AssignmentCreated, int TestCreated, int ActiveCourses,int NoOfStudentJoined)
        {
            this.m_iNoOfAssignmentCreated = AssignmentCreated;
            this.m_iNoOfTestCreated = TestCreated;
            this.m_iNoOfCourseCreated = CourseCreated;  
            this.m_iNoOfStudentsJoined = NoOfStudentJoined;
            this.m_iActiveCourses = ActiveCourses;
        }
        public InstructorRegisterModel()
        {

        }
        public InstructorRegisterModel(string FirstName, string LastName,string InstrcutorEmail,string InstructorPhoneNo,string AddressLine1,
            string AddreessLine2,string CityName,string StateName,string PinCode,string Gender,string SchoolName,DateTime? LastUpdated,DateTime DateOfJoining,
            int? CityId, int? StateId,string AcademicRecord,string LinkedInId,string GoogleScholarId,string InsructorShortBio)
        {
            this.m_strFirstName = FirstName;
            this.m_strLastName = LastName;
            this.m_strEmail = InstrcutorEmail;
            this.m_strPhoneNo = InstructorPhoneNo;
            this.m_strAddressLine1 = AddressLine1;
            this.m_strAddressLine2 = AddreessLine2;
            this.m_strSchoolName =SchoolName ;
            this.m_strState = StateName;
            this.m_strCity = CityName;
            this.m_strPineCode = PinCode;
            this.m_strGender = Gender;
            this.m_dtDateOfJoining = DateOfJoining;
            this.m_dtLastUpdated = LastUpdated;
            this.m_iCityid = CityId;
            this.m_iStateId = StateId;
            if(this.m_strState==null)
            {
                this.m_strFullAddress = "Not set";
            }
            else
            {
                this.m_strFullAddress = this.m_strAddressLine1 + ", " + this.m_strAddressLine2 + ", " + this.m_strCity + ", " + this.m_strState + ", pincode- " + this.m_strPineCode;
            }
            if (m_dtLastUpdated==null)
            {
                this.m_dtLastUpdated = DateOfJoining;
            }
            if (AcademicRecord != null)
            {
                this.instructorSchoolDetailsModal = JsonConvert.DeserializeObject<InstructorSchoolDetailsModal>(AcademicRecord);
            }
            else
            {
                this.instructorSchoolDetailsModal = new InstructorSchoolDetailsModal();
            }
            this.m_strLinkedInId = LinkedInId;
            this.m_strGoogleScholarId = GoogleScholarId;
            this.m_strInstructorBio = InsructorShortBio;

        }
       
    }
}