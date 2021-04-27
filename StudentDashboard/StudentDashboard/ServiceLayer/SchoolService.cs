using StudentDashboard.DTO;
using StudentDashboard.Models;
using StudentDashboard.Models.Address;
using StudentDashboard.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentDashboard.ServiceLayer
{
    public class SchoolService
    {
        SchoolDTO objSchoolDTO;
        public SchoolService()
        {
            objSchoolDTO = new SchoolDTO();
        }
        public bool RegisterNewSchool(SchoolRegisterModel objRegisteSchoolModel)
        {
            objRegisteSchoolModel.m_strPassword = SHA256Encryption.ComputeSha256Hash(objRegisteSchoolModel.m_strPassword);
            return objSchoolDTO.RegisterNewSchool(objRegisteSchoolModel);
        }
        public bool CheckIsSchoolUserNameAlreadyExists(string SchoolUserName)
        {
            return objSchoolDTO.CheckIsSchoolUserNameAlreadyExists(SchoolUserName);
        }
        public List<StateModel> GetStates()
        {
            return objSchoolDTO.GetStates();
        }
        public List<CityModel> GetCities(int StateId)
        {
            return objSchoolDTO.GetCities(StateId);
        }
        public bool ValidateSchoolLoginDetails(string UserId, string Password)
        {
            Password= SHA256Encryption.ComputeSha256Hash(Password);
            return objSchoolDTO.ValidateSchoolLoginDetails(UserId, Password);
        }
    }
}