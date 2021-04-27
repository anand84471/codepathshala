using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.Models.Student
{
    public class StudentProfileDetails
    {
        [JsonProperty("first_name")]
        public string m_strFirstName { get; set; }
        [JsonProperty("las_name")]
        public string m_strLastName { get; set; }
        public string m_strAddressLine1 { get; set; }
       
        public string m_strAddressLine2 { get; set; }
        [JsonProperty("phone_no")]
        public string m_strPhoneNo { get; set; }
        public string m_strUserId { get; set; }
        [JsonProperty("email_address")]
        public string m_strEmail { get; set; }
        public string m_strHashedPassword { get; set; }
        public string m_strPassword { get; set; }
        public bool m_bIsRemeberMe { get; set; }
        public long m_llStudentId { get; set; }
        [JsonProperty("state")]
        public string m_strState { get; set; }
        public string m_strCity { get; set; }
        public string m_strGender { get; set; }
        public string m_strPinCode { get; set; }
        [JsonProperty("joining_date")]
        public string m_strDateOfJoining { get; set; }
        public string m_strLastUpdated { get; set; }
        [JsonProperty("full_address")]
        public string m_strFullAddress { get; set; }
        public string m_strCountryCode { get; set; }
        public string m_strToken { get; set; }
        public bool IsEmailVarified { get; set; }
        public bool IsPhoneNoVarified { get; set; }
        public string m_strEmailVarificationGuid { get; set; }

        public string m_strPasswrodRecoveryAuthToken;
        [JsonProperty("profile_url")]
        public string m_strProfileUrl;
        [JsonProperty("live_courses_joined")]
        public int m_iNoOfLiveCoursesJoined;
        [JsonProperty("no_of_students_followed")]
        public int m_iNoOfStudnetsFollowedHim;
        [JsonProperty("no_of_students_following")]
        public int m_iNoOfStudentHeIsFollowing;
        [JsonProperty("no_of_live_classes_joined")]
        public int m_iNoOfLiveClassAttended;
        [JsonProperty("no_of_tests_submitted")]
        public int m_iNoOfTestsSubmitted;
        [JsonProperty("no_of_instructors_followed")]
        public int m_iNoOfInstructorsFollowed;
        [JsonProperty("no_of_assignments_submitted")]
        public int m_iNoOfAssignmentsSubmitted;
        public StudentProfileDetails(string FirstName,
            string LastName,string DateOfJoing,string AddresLine1, string AddressLine2, string CityName, string StateName,string PhoneNo,string EmailAddress,
            int NoOfInstructorsFollowed,int NoOfStudentsFolllowed,int NoOfStudentsFollowing,int NoOfLiveCoursesJoned,int NoOfLiveClassAttened,
            string PinCode,int NoOfAssignmentsSubmitted,int NoOfTestsSubmitted,string ProfileUrl)
        {
            this.m_strCity = CityName;
            this.m_strAddressLine1 = AddresLine1;
            this.m_strState = StateName;
            this.m_strAddressLine2 = AddressLine2;
            this.m_strFirstName = FirstName;
            this.m_strLastName = LastName;
            this.m_iNoOfLiveCoursesJoined = NoOfLiveCoursesJoned;
            this.m_iNoOfLiveClassAttended = NoOfLiveClassAttened;
            this.m_strPhoneNo = PhoneNo;
            this.m_strPinCode = PinCode;
            this.m_strPhoneNo = PhoneNo;
            this.m_strEmail = EmailAddress;
            this.m_iNoOfAssignmentsSubmitted = NoOfAssignmentsSubmitted;
            this.m_iNoOfTestsSubmitted = NoOfTestsSubmitted;
            this.m_strProfileUrl = ProfileUrl;
            this.m_strDateOfJoining = DateOfJoing;
            this.m_iNoOfInstructorsFollowed = NoOfInstructorsFollowed;
            this.m_iNoOfStudentHeIsFollowing = NoOfStudentsFollowing;

            if (this.m_strState == null)
            {
                this.m_strFullAddress = "Not set";
            }
            else
            {
                this.m_strFullAddress = this.m_strAddressLine1 + ", " + this.m_strAddressLine2 + ", " + this.m_strCity + ", " + this.m_strState + ", pincode- " + this.m_strPinCode;
            }
        }
    }
}