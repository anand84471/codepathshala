using StudentDashboard.Models;
using StudentDashboard.Models.Address;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace StudentDashboard.DTO
{
    public class SchoolDTO
    {
        CPDataService.CpDataServiceClient objCPDataService;
        public SchoolDTO()
        {
            objCPDataService = new CPDataService.CpDataServiceClient();
        }
        public bool RegisterNewSchool(SchoolRegisterModel objSchoolRegisterModel)
        {
            bool result = false;
            try
            {
                result = objCPDataService.InsertNewSchoolDetails(objSchoolRegisterModel.m_strSchoolName, objSchoolRegisterModel.m_strAddressLine1,
                                           objSchoolRegisterModel.m_strAddressLine2, objSchoolRegisterModel.m_iCityId, objSchoolRegisterModel.m_iPinCode,
                                           objSchoolRegisterModel.m_strSchoolUserId, objSchoolRegisterModel.m_strPassword, objSchoolRegisterModel.m_strPhoneNo,
                                           objSchoolRegisterModel.m_strPhoneNo);         
                    }
            catch (Exception Ex)
            {

            }
            return result;
        }
        
        public bool CheckIsSchoolUserNameAlreadyExists(string SchoolUserName)
        {
            bool result = true;
            try
            {
                DataSet ds = new DataSet();

                ds = objCPDataService.CheckIsSchoolUserIdAlreadyExists(SchoolUserName);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    result = false;
                }

            }
            catch (Exception Ex)
            {

            }
            return result;
        }
        public List<StateModel> GetStates()
        {
            List<StateModel> lsStateModel=null;
            try
            {
                DataSet ds = new DataSet();
                ds = objCPDataService.GetAllStateDetailsOfCountry(Constants.COUNTRY_CODE_FOR_INDIA);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    lsStateModel = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new StateModel(
                         dataRow.Field<int>("ID"),
                         dataRow.Field<string>("NAME")
                         )).ToList();
                }
            }
            catch (Exception Ex)
            {

            }
            return lsStateModel;
        }
        public List<CityModel> GetCities(int StateId)
        {
            List<CityModel> lsCityModel = null;
            try
            {
                DataSet ds = new DataSet();
                ds = objCPDataService.GetAllCityDetailsOfState(StateId);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    lsCityModel = ds.Tables[0].AsEnumerable().Select(
                     dataRow => new CityModel(
                         dataRow.Field<int>("ID"),
                         dataRow.Field<string>("NAME")
                         )).ToList();
                }
            }
            catch (Exception Ex)
            {

            }
            return lsCityModel;
        }
        public bool ValidateSchoolLoginDetails(string UserId,string Password)
        {
            bool result = false;
            try
            {
                DataSet ds = new DataSet();

                ds = objCPDataService.ValidateSchoolLoginDetails(UserId, Password);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    result = true;
                }

            }
            catch (Exception Ex)
            {

            }
            return result;
        }

    }
}