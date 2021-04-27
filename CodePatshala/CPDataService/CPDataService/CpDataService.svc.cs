using CPDataService.CLoggers;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace CPDataService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class CpDataService : ICpDataService
    {
        SqlConnection m_con;
        SqlCommand m_command;
        SqlDataReader m_reader;
        string m_ConnectionString;
        SqlTransaction m_transaction;
        StringBuilder m_strLogMessage = new StringBuilder();
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }
        public void InitDB()
        {
            try
            {

                m_ConnectionString = ConfigurationManager.ConnectionStrings["RegistrationConnString"].ToString();
                if (String.IsNullOrEmpty(m_ConnectionString))
                {
                    m_strLogMessage.Append("Connection String is not present to connect to DB");
                    m_strLogMessage.Append("Default Connection String is:" + "");
                    CpLogger.Info(m_strLogMessage);
                }
                else
                {
                    // m_log.Info("Connection String to connect to DB is:" + m_ConnectionString);
                }
                m_con = new SqlConnection(m_ConnectionString);

            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);

            }
        }
        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
        public bool RegisterNewUser(string FirstName,string LastName,string PhoneNo,string Email,string Password)
        {
            bool result = false;
            string strCurrentMethodName = "RegisterNewUser";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spInsertNewUserDetails", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@strFirstName", SqlDbType.VarChar,250).Value = FirstName;
                m_command.Parameters.Add("@strLastName", SqlDbType.VarChar, 250).Value = LastName;
                m_command.Parameters.Add("@strPhone", SqlDbType.VarChar, 10).Value = PhoneNo;
                m_command.Parameters.Add("@strEmail", SqlDbType.VarChar, 250).Value = Email;
                m_command.Parameters.Add("@strPassword", SqlDbType.VarChar, 250).Value = Password;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public DataSet ValidateLoginDetails(string Email, string Password)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "ValidateLoginDetails";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spValidateStudentDetails", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@strEmail", SqlDbType.VarChar, 250).Value = Email;
                m_command.Parameters.Add("@strPassword", SqlDbType.VarChar, 250).Value = Password;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet GetInstructorDetails(int Id)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetInstructorDetails";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetInstructorDetails", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@iInstructorId", SqlDbType.Int).Value = Id;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet GetInstructorTestDetails(int Id)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetInstructorTestDetails";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetInstructorTestDetails", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@iInstructorId", SqlDbType.Int).Value = Id;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet GetInstructorAssignmentDetails(int Id)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetInstructorAssignmentDetails";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetInstructorAssignmentDetails", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@iInstructorId", SqlDbType.Int).Value = Id;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet GetInstructorActivityDetails(int Id)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetInstructorActivityDetails";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetInstructorActivityDetails", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@iInstructorId", SqlDbType.Int).Value = Id;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }

        public bool RegisterNewInstructor(string FirstName, string LastName, string PhoneNo, string Email, string Password,
            string PhoneNoVerificationGuid,string EmailIdVerificationGuid)
        {
            bool result = false;
            string strCurrentMethodName = "RegisterNewInstructor";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_RegisterNewInstructor", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@strFirstName", SqlDbType.VarChar, 250).Value = FirstName;
                m_command.Parameters.Add("@strLastName", SqlDbType.VarChar, 250).Value = LastName;
                m_command.Parameters.Add("@strPhoneNo", SqlDbType.VarChar, 20).Value = PhoneNo;
                m_command.Parameters.Add("@strEmail", SqlDbType.VarChar, 250).Value = Email;
                m_command.Parameters.Add("@strPassword", SqlDbType.VarChar, 250).Value = Password;
                m_command.Parameters.Add("@strPhoneNoVerificationGuid", SqlDbType.VarChar, 100).Value = PhoneNoVerificationGuid;
                m_command.Parameters.Add("@strEmailVarificationGuid", SqlDbType.VarChar, 100).Value = EmailIdVerificationGuid;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool UpdateInstructorDetails(string FirstName, string LastName, string PhoneNo, string Gender,string AddressLine1,string AddressLine2,
                                          int? CityId,int? StateId,string PinCode,int InstructorId)
        {
            bool result = false;
            string strCurrentMethodName = "UpdateInstructorDetails";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spUpdateInstructorDetails", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@strFirstName", SqlDbType.VarChar, 250).Value = FirstName;
                m_command.Parameters.Add("@strLastName", SqlDbType.VarChar, 250).Value = LastName;
                m_command.Parameters.Add("@strPhoneNo", SqlDbType.VarChar, 10).Value = PhoneNo;
                m_command.Parameters.Add("@charGender", SqlDbType.VarChar, 1).Value = Gender;
                m_command.Parameters.Add("@strAddressLine1", SqlDbType.VarChar, 100).Value = AddressLine1;
                m_command.Parameters.Add("@strAddressLine2", SqlDbType.VarChar, 100).Value = AddressLine2;
                if(CityId==null)
                {
                    m_command.Parameters.Add("@iCityId", SqlDbType.Int).Value = DBNull.Value;
                }
                else
                {
                    m_command.Parameters.Add("@iCityId", SqlDbType.Int).Value = CityId;
                }
                if(StateId==null)
                {
                    m_command.Parameters.Add("@iStateId", SqlDbType.Int).Value = DBNull.Value;
                }
                else
                {
                    m_command.Parameters.Add("@iStateId", SqlDbType.Int).Value = StateId;
                }
                m_command.Parameters.Add("@strPinCode", SqlDbType.VarChar, 6).Value = PinCode;
                m_command.Parameters.Add("iInstructorId", SqlDbType.Int).Value = InstructorId;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool UpdateInstructorPassword(string Password, int InstructorId)
        {
            bool result = false;
            string strCurrentMethodName = "RegisterNewInstructor";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spUpdateInstructorDetails", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@strPassword", SqlDbType.VarChar, 250).Value = Password;
                m_command.Parameters.Add("iInstructorId", SqlDbType.Int).Value = InstructorId;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public DataSet ValidateInstructorLoginDetails(string Email, string Password)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "ValidateLoginDetails";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_ValidateInstructorLoginDetails", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@strEmail", SqlDbType.VarChar, 250).Value = Email;
                m_command.Parameters.Add("@strPassword", SqlDbType.VarChar, 250).Value = Password;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
                m_strLogMessage.Append("\n ---------------------------Funtion finished--------------------------------------");

                CpLogger.Info(m_strLogMessage);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet GetInstructorPostLoginDetails(int Id)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetInstructorPostLoginDetails";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spInstructorPostLoginDetails", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@iInstructorId", SqlDbType.Int).Value = Id;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
                m_strLogMessage.Append("\n ----------------------------Funtion finished--------------------------------------");
                CpLogger.Info(m_strLogMessage);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet GetAllCountryDetails()
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetAllCountryDetails";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetAllCountryDetails", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet GetAllStateDetailsOfCountry(int CountryId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetAllStateDetailsOfCountry";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetAllStatesOfCountry", m_con);
                m_command.Parameters.Add("@iCountryId", SqlDbType.Int).Value = CountryId;
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet GetAllCityDetailsOfState(int StateId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetAllCityDetailsOfState";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetAllCitiesOfState", m_con);
                m_command.Parameters.Add("@iStateId", SqlDbType.Int).Value = StateId;
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public bool InsertNewSchoolDetails(string SchoolName, string AddressLine1, string AddressLine2, int CityId, int PinCode,string SchoolUserId,
                                          string Password,string PhoneNo,string Email)
        {
            bool result = false;
            string strCurrentMethodName = "RegisterNewInstructor";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spInsertNewSchoolDetails", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@strName", SqlDbType.VarChar, 250).Value =SchoolName;
                m_command.Parameters.Add("@strAddressLine1", SqlDbType.VarChar, 250).Value = AddressLine1;
                m_command.Parameters.Add("@strAddressLine2", SqlDbType.VarChar, 250).Value = AddressLine2;
                m_command.Parameters.Add("@iCityId", SqlDbType.Int).Value = CityId;
                m_command.Parameters.Add("@iPinCode", SqlDbType.Int).Value = PinCode;
                m_command.Parameters.Add("@strSchoolUserId", SqlDbType.VarChar, 250).Value = SchoolUserId;
                m_command.Parameters.Add("@strPassword", SqlDbType.VarChar, 100).Value = Password;
                m_command.Parameters.Add("@strPhoneNo", SqlDbType.VarChar, 10).Value = PhoneNo;
                m_command.Parameters.Add("@strEmail", SqlDbType.VarChar, 1100).Value = Email;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public DataSet CheckIsSchoolUserIdAlreadyExists(string SchoolUserIdToCheck)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "CheckIsSchoolUserIdAlreadyExists";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spCheckIsSchoolIdExists", m_con);
                m_command.Parameters.Add("@strSchoolUserId", SqlDbType.VarChar,100).Value = SchoolUserIdToCheck;
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet ValidateSchoolLoginDetails(string Email, string Password)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "ValidateSchoolLoginDetails";
            try
            {
                InitDB();
                if (m_con != null)
                    m_command = new SqlCommand("Cp_spValidateSchoolLogIn", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@strUsername", SqlDbType.VarChar, 250).Value = Email;
                m_command.Parameters.Add("@strPassword", SqlDbType.VarChar, 250).Value = Password;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        //Cp_spInsertNewSchoolDetails

        public bool InsertNewCourse(string CourseName, string CourseDescription, 
            string CourseImageUrl,string CourseThumbnailSmall,string CoursethumbnailMedium,int InstructorId,ref long CourseId)
        {
            bool result = false;
            string strCurrentMethodName = "InertNewCourse";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spInsertNewCourseDetails", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@strCourseName", SqlDbType.VarChar, 250).Value = CourseName;
                m_command.Parameters.Add("@strCousreDescription", SqlDbType.VarChar, 500).Value = CourseDescription;
                m_command.Parameters.Add("@iInstructorId", SqlDbType.Int).Value = InstructorId;
                m_command.Parameters.Add("@llCourseId", SqlDbType.BigInt).Direction = ParameterDirection.Output;
                if(CourseImageUrl==null)
                {
                    m_command.Parameters.Add("@strCourseImageUrl", SqlDbType.VarChar, 250).Value = DBNull.Value;
                }
                else
                {
                    m_command.Parameters.Add("@strCourseImageUrl", SqlDbType.VarChar, 250).Value = CourseImageUrl;
                }
                if(CourseThumbnailSmall==null)
                {
                    m_command.Parameters.Add("@strCourseThumbnailSmall", SqlDbType.VarChar, 250).Value = DBNull.Value;
                }
                else
                {
                    m_command.Parameters.Add("@strCourseThumbnailSmall", SqlDbType.VarChar, 250).Value = CourseThumbnailSmall;
                }
                if(CoursethumbnailMedium==null)
                {
                    m_command.Parameters.Add("@strCourseThumbnailMedium", SqlDbType.VarChar, 250).Value = DBNull.Value;
                }
                else
                {
                    m_command.Parameters.Add("@strCourseThumbnailMedium", SqlDbType.VarChar, 250).Value = CoursethumbnailMedium;
                }
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    CourseId = (long)(m_command.Parameters["@llCourseId"].Value);
                    result = true;
                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool InertNewCourseIndex(string IndexName, string IndexDescription, long CourseId,ref long IndexId)
        {
            bool result = false;
            string strCurrentMethodName = "InertNewCourse";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spInsertCourseIndex", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@strIndexName", SqlDbType.VarChar, 250).Value = IndexName;
                m_command.Parameters.Add("@strIndexDescription", SqlDbType.VarChar, 500).Value = IndexDescription;
                m_command.Parameters.Add("@llCourseId", SqlDbType.BigInt).Value = CourseId;
                m_command.Parameters.Add("@llIndexId", SqlDbType.BigInt).Direction = ParameterDirection.Output;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    IndexId = (long)m_command.Parameters["@llIndexId"].Value;
                    result = true;
                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool InsertNewTopic(string TopicName, string TopicDescription, string FileUploadPath, short FileUploadTypeId, long IndexId,
            string FileAttachmetPath)
        {
            bool result = false;
            string strCurrentMethodName = "InsertNewTopic";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spInsertNewTopic", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@strTopicName", SqlDbType.VarChar, 100).Value = TopicName;
                m_command.Parameters.Add("@strTopicDescription", SqlDbType.VarChar, 500).Value = TopicDescription;
                if (FileUploadPath == null)
                {
                    m_command.Parameters.Add("@strFileUploadPath", SqlDbType.VarChar, 250).Value = DBNull.Value;
                }
                else
                {
                    m_command.Parameters.Add("@strFileUploadPath", SqlDbType.VarChar, 250).Value = FileUploadPath;
                }
                if(FileUploadTypeId==0)
                {
                    m_command.Parameters.Add("@iFileTypeId", SqlDbType.TinyInt).Value = DBNull.Value;
                }
                else {
                    m_command.Parameters.Add("@iFileTypeId", SqlDbType.TinyInt).Value = FileUploadTypeId;
                }
                if (FileAttachmetPath == null)
                {
                    m_command.Parameters.Add("@strFileAttacthment", SqlDbType.VarChar, 250).Value = DBNull.Value;
                }
                else
                {
                    m_command.Parameters.Add("@strFileAttacthment", SqlDbType.VarChar, 250).Value = FileAttachmetPath;
                }
                m_command.Parameters.Add("@llIndexId", SqlDbType.BigInt).Value = IndexId;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool InsertNewAssignment(string AssignmentName, string FilePath, short FileTypeId,short AssignmentType,string AssignmentDescription,ref long AssignmentId)
        {
            bool result = false;
            string strCurrentMethodName = "InsertNewAssignment";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spInsertNewAssignment", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@strAssignmentName", SqlDbType.VarChar, 250).Value = AssignmentName;
                if(FilePath!=null&&FilePath.Equals(string.Empty))
                {
                    m_command.Parameters.Add("@strFilePath", SqlDbType.VarChar, 150).Value = FilePath;
                }
                else
                {
                    m_command.Parameters.Add("@strFilePath", SqlDbType.VarChar, 150).Value = DBNull.Value;
                }
                
                m_command.Parameters.Add("@iAssignmentType", SqlDbType.TinyInt).Value = AssignmentType;
                if(FileTypeId==0)
                {
                    m_command.Parameters.Add("@iFileUploadTypeId", SqlDbType.TinyInt).Value = DBNull.Value;
                }
                else
                {
                    m_command.Parameters.Add("@iFileUploadTypeId", SqlDbType.TinyInt).Value = FileTypeId;
                }
                if(AssignmentDescription!=null)
                {
                    m_command.Parameters.Add("@strAssignmentDescription", SqlDbType.VarChar, 500).Value = AssignmentDescription;
                }
                else
                {
                    m_command.Parameters.Add("@strAssignmentDescription", SqlDbType.VarChar, 500).Value = DBNull.Value;
                }
                m_command.Parameters.Add("@llAssignmentId", SqlDbType.BigInt).Direction = ParameterDirection.Output;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    AssignmentId = (long)m_command.Parameters["@llAssignmentId"].Value;
                    result = true;

                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool InsertNewTest(string TestName,string TestDescription, string FilePath, short FileTypeId,short TestType,ref long TestId)
        {
            bool result = false;
            string strCurrentMethodName = "InsertNewTest";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spInsertNewTest", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@strTestName", SqlDbType.VarChar, 250).Value = TestName;
                m_command.Parameters.Add("@strTestDescription", SqlDbType.VarChar, 250).Value = TestDescription;
                if (FilePath != null && FilePath != string.Empty)
                {
                    m_command.Parameters.Add("@strFilePath", SqlDbType.VarChar, 150).Value = FilePath;
                }
                else
                {
                    m_command.Parameters.Add("@strFilePath", SqlDbType.VarChar, 150).Value = DBNull.Value;
                }
                if(FileTypeId==0)
                {
                    m_command.Parameters.Add("@iFileTypeId", SqlDbType.Int).Value = DBNull.Value;
                }
                else
                {
                    m_command.Parameters.Add("@iFileTypeId", SqlDbType.Int).Value = FileTypeId;
                }
               
                m_command.Parameters.Add("@iTestType", SqlDbType.TinyInt).Value = TestType;
                m_command.Parameters.Add("@llTestId", SqlDbType.BigInt).Direction = ParameterDirection.Output;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    TestId = (long)m_command.Parameters["@llTestId"].Value;
                    result = true;
                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool InsertNewMcqAssignment(long AssignmentId,string QuestionStatement,string Option1,string Option2, string Option3, string Option4,short CorrectOption)
        {
            bool result = false;
            string strCurrentMethodName = "InsertNewMcqAssignment";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spInsertNewMcqAssignmentQuestion", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llAssignmentId", SqlDbType.BigInt).Value = AssignmentId;
                m_command.Parameters.Add("@strQuestionStatement", SqlDbType.VarChar, 250).Value = QuestionStatement;
                m_command.Parameters.Add("@strOption1", SqlDbType.VarChar, 100).Value = Option1;
                m_command.Parameters.Add("@strOption2", SqlDbType.VarChar, 100).Value = Option2;
                m_command.Parameters.Add("@strOption3", SqlDbType.VarChar, 100).Value = Option3;
                m_command.Parameters.Add("@strOption4", SqlDbType.VarChar, 100).Value = Option4;
                m_command.Parameters.Add("@iCorrectOption", SqlDbType.TinyInt).Value = CorrectOption;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                { 
                    result = true;
                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool InsertNewMcqTestQuestion(long TestId, string QuestionStatement, string Option1, string Option2, string Option3, string Option4, short CorrectOption,
                                int TimeForQuestionInSeconds,int Marks)
        {
            bool result = false;
            string strCurrentMethodName = "InsertNewMcqTestQuestion";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spInsertNewMcqTestQuestion", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llTestId", SqlDbType.BigInt).Value = TestId;
                m_command.Parameters.Add("@strQuestionStatement", SqlDbType.VarChar, 250).Value = QuestionStatement;
                m_command.Parameters.Add("@strOption1", SqlDbType.VarChar, 100).Value = Option1;
                m_command.Parameters.Add("@strOption2", SqlDbType.VarChar, 100).Value = Option2;
                m_command.Parameters.Add("@strOption3", SqlDbType.VarChar, 100).Value = Option3;
                m_command.Parameters.Add("@strOption4", SqlDbType.VarChar, 100).Value = Option4;
                m_command.Parameters.Add("@iCorrectOption", SqlDbType.TinyInt).Value = CorrectOption;
                m_command.Parameters.Add("@iMarks", SqlDbType.Int).Value = Marks;
                m_command.Parameters.Add("@iTimeInSeconds", SqlDbType.Int).Value = TimeForQuestionInSeconds;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool InsertAssignmentIdToIndex(long AssignmentId,long IndexId)
        {
            bool result = false;
            string strCurrentMethodName = "InsertAssignmentIdToIndex";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spInsertAssignmentIdToIndex", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llAssignmentId", SqlDbType.BigInt).Value = AssignmentId;
                m_command.Parameters.Add("@llIndexId", SqlDbType.BigInt).Value = IndexId;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool InsertTestIdToIndex(long TestId, long IndexId)
        {
            bool result = false;
            string strCurrentMethodName = "InsertTestIdToIndex";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spInsertTestIdToIndex", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llTest", SqlDbType.BigInt).Value = TestId;
                m_command.Parameters.Add("@llIndexId", SqlDbType.BigInt).Value = IndexId;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool InsertActivityForInstructor(int InstructorId, string ActivityMessgae)
        {
            bool result = false;
            string strCurrentMethodName = "InsertActivityForInstructor";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spInsertActivityForInstructor", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llInstructorId", SqlDbType.Int).Value = InstructorId;
                m_command.Parameters.Add("@strActivityMessage", SqlDbType.VarChar,1000).Value = ActivityMessgae;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public DataSet GetAllCourse(long InstructorId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetAllCourse";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetAllCourseDetailsForInstructor", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llInstructorId", SqlDbType.BigInt).Value = InstructorId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public bool GetInstructorIdFromUserId(string InstructorId,ref int Id)
        {
            bool result = false;
            string strCurrentMethodName = "GetInstructorIdFromUserId";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetinstructorIdFromUserId", m_con);
                m_command.CommandType = CommandType.StoredProcedure;
                m_command.Parameters.Add("@llInstructorUserName", SqlDbType.VarChar, 250).Value = InstructorId;
                m_command.Parameters.Add("@llIstructorId", SqlDbType.Int).Direction = ParameterDirection.Output;
                m_con.Open();
                m_command.ExecuteNonQuery();
                Id = (int)m_command.Parameters["@llIstructorId"].Value;
                if(Id>0)
                {
                    result = true;
                }
                
            }
            catch(Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public DataSet GetIndexDetailsOfCourse(long CourseId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetAllCourse";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetCourseIndexDetails", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llCourseId", SqlDbType.BigInt).Value = CourseId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet GetStudentAssignmentProgress(long AssignmentId,long StudentId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetStudentAssignmentProgress";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spStudentAssignmentProgress", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llAssignmentId", SqlDbType.BigInt).Value = AssignmentId;
                m_command.Parameters.Add("@llStudentId", SqlDbType.BigInt).Value = StudentId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet GetStudentTestProgress(long TestId, long StudentId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetStudentTestProgress";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetStudentTestProgress", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llTestId", SqlDbType.BigInt).Value = TestId;
                m_command.Parameters.Add("@llStudentId", SqlDbType.BigInt).Value = StudentId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet GetStudentCourseProgress(long CourseId,long StudentId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetStudentCourseProgress";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetStudentCourseProgress", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llCourseId", SqlDbType.BigInt).Value = CourseId;
                m_command.Parameters.Add("@llStudentId", SqlDbType.BigInt).Value = StudentId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet GetAssignmentDetails(long AssignmentId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetAllCourse";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetAssignmentDetails", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llAssignmentId", SqlDbType.BigInt).Value = AssignmentId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet GetMcqAssignmentDetails(long AssignmentId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetMcqAssignmentDetails";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetMcqAssignmentDetails", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llAssignmentId", SqlDbType.BigInt).Value = AssignmentId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet GetCourseDetails(long CourseId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetCourseDetails";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetCourseDetail", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llCourseId", SqlDbType.BigInt).Value = CourseId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public bool InsertNewIndependentAssignment(int InstructorId,string AssignmentName, string AssignmentDescription,string FilePath, short FileTypeId, short AssignmentType, ref long AssignmentId)
        {
            bool result = false;
            string strCurrentMethodName = "InsertNewAssignment";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spInsertNewIndependentAssignment", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@strAssignmentName", SqlDbType.VarChar, 250).Value = AssignmentName;
                m_command.Parameters.Add("@iInstructorId", SqlDbType.Int).Value = InstructorId;
                if (AssignmentDescription != null && !AssignmentDescription.Equals(string.Empty))
                {
                    m_command.Parameters.Add("@strAssignmentDescription", SqlDbType.VarChar, 500).Value = AssignmentDescription;
                }
                else
                {
                    m_command.Parameters.Add("@strAssignmentDescription", SqlDbType.VarChar, 500).Value = DBNull.Value;
                }
                if (FilePath != null && FilePath.Equals(string.Empty))
                {
                    m_command.Parameters.Add("@strFilePath", SqlDbType.VarChar, 150).Value = FilePath;
                }
                else
                {
                    m_command.Parameters.Add("@strFilePath", SqlDbType.VarChar, 150).Value = DBNull.Value;
                }

                m_command.Parameters.Add("@iAssignmentType", SqlDbType.TinyInt).Value = AssignmentType;
                if (FileTypeId == 0)
                {
                    m_command.Parameters.Add("@iFileUploadTypeId", SqlDbType.TinyInt).Value = DBNull.Value;
                }
                else
                {
                    m_command.Parameters.Add("@iFileUploadTypeId", SqlDbType.TinyInt).Value = FileTypeId;
                }
                m_command.Parameters.Add("@llAssignmentId", SqlDbType.BigInt).Direction = ParameterDirection.Output;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    AssignmentId = (long)m_command.Parameters["@llAssignmentId"].Value;
                    result = true;
                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool InsertNewIndependentTest(int InstructorId, string TestName, string TestDescription, string FilePath, short FileTypeId, short TestType, ref long TestId)
        {
            bool result = false;
            string strCurrentMethodName = "InsertNewAssignment";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spInsertNewIndependentTest", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@strTestName", SqlDbType.VarChar, 250).Value = TestName;
                m_command.Parameters.Add("@iInstructorId", SqlDbType.Int).Value = InstructorId;
                if (TestDescription != null && !TestDescription.Equals(string.Empty))
                {
                    m_command.Parameters.Add("@strTestDescription", SqlDbType.VarChar, 500).Value = TestDescription;
                }
                else
                {
                    m_command.Parameters.Add("@strTestDescription", SqlDbType.VarChar, 500).Value = DBNull.Value;
                }
                if (FilePath != null && FilePath.Equals(string.Empty))
                {
                    m_command.Parameters.Add("@strFilePath", SqlDbType.VarChar, 150).Value = FilePath;
                }
                else
                {
                    m_command.Parameters.Add("@strFilePath", SqlDbType.VarChar, 150).Value = DBNull.Value;
                }

                m_command.Parameters.Add("@iTestType", SqlDbType.TinyInt).Value = TestType;
                if (FileTypeId == 0)
                {
                    m_command.Parameters.Add("@iFileUploadTypeId", SqlDbType.TinyInt).Value = DBNull.Value;
                }
                else
                {
                    m_command.Parameters.Add("@iFileUploadTypeId", SqlDbType.TinyInt).Value = FileTypeId;
                }
                m_command.Parameters.Add("@llTestId", SqlDbType.BigInt).Direction = ParameterDirection.Output;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    TestId = (long)m_command.Parameters["@llTestId"].Value;
                    result = true;
                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool DeleteCourse(long CourseId)
        {
            bool result = false;
            string strCurrentMethodName = "DeleteCourse";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spDeleteCourse", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llCourseId", SqlDbType.BigInt).Value = CourseId;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool ActivateCourse(long CourseId,string ShareUrl,string AccessCode,int CourseJoiningFeeInPaise)
        {
            bool result = false;
            string strCurrentMethodName = "ActivateCourse";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spActivateCourse", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llCourseId", SqlDbType.BigInt).Value = CourseId;
                m_command.Parameters.Add("@strShareUrl", SqlDbType.VarChar,100).Value = ShareUrl;
                m_command.Parameters.Add("@strAccessCode", SqlDbType.VarChar,20).Value = AccessCode;
                m_command.Parameters.Add("@iCoursePriceInPaise", SqlDbType.Int).Value = CourseJoiningFeeInPaise;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool UpdateCourseDetails(long CourseId,string CourseDescription)
        {
            bool result = false;
            string strCurrentMethodName = "UpdateCourseDetails";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spUpdateCourseDetails", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llCourseId", SqlDbType.BigInt).Value = CourseId;
                m_command.Parameters.Add("@strCourseDescription", SqlDbType.VarChar, 500).Value = CourseDescription;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool UpdateCourseIndexDetails(long IndexId, string IndexName,string IndexDescription)
        {
            bool result = false;
            string strCurrentMethodName = "UpdateCourseIndexDetails";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spUpdateCourseIndexDetails", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@iIndexId", SqlDbType.BigInt).Value = IndexId;
                m_command.Parameters.Add("@strIndexName", SqlDbType.VarChar, 500).Value = IndexName;
                m_command.Parameters.Add("@strIndexDescription", SqlDbType.VarChar, 500).Value = IndexDescription;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }

        public bool UpdateIndexTopicDetails(long TopicId, string TopicName, string TopicDescription,string FilePath,byte FileType,string FileAttachmentPath)
        {
            bool result = false;
            string strCurrentMethodName = "UpdateCourseIndexDetails";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spUpdateIndexTopic", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@iTopicId", SqlDbType.BigInt).Value = TopicId;
                m_command.Parameters.Add("@strTopicName", SqlDbType.VarChar, 100).Value = TopicName;
                m_command.Parameters.Add("@strTopicDescription", SqlDbType.VarChar, 250).Value = TopicDescription;
                m_command.Parameters.Add("@strFilePath", SqlDbType.VarChar, 250).Value = FilePath??string.Empty;
                m_command.Parameters.Add("@strFileAttacthment", SqlDbType.VarChar, 250).Value = FileAttachmentPath??string.Empty;
                m_command.Parameters.Add("@iFileType", SqlDbType.TinyInt).Value = FileType;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool UpdateAssignmentDetails(long AssignmentId, string AssignmentName, string AssignmentDescription,string FilePath,byte FileType)
        {
            bool result = false;
            string strCurrentMethodName = "UpdateCourseIndexDetails";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spUpdateAssignmentDetails", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llAssignmentId", SqlDbType.BigInt).Value = AssignmentId;
                m_command.Parameters.Add("@strAssignmentName", SqlDbType.VarChar, 250).Value = AssignmentName;
                if (AssignmentDescription != null)
                {
                    m_command.Parameters.Add("@strAssignmentDescription", SqlDbType.VarChar, 500).Value = AssignmentDescription;
                }
                else
                {
                    m_command.Parameters.Add("@strAssignmentDescription", SqlDbType.VarChar, 500).Value = DBNull.Value;
                }
                if (FilePath != null&&FilePath!=string.Empty)
                {
                    m_command.Parameters.Add("@strFilePath", SqlDbType.VarChar, 150).Value = FilePath;
                }
                else
                {
                    m_command.Parameters.Add("@strFilePath", SqlDbType.VarChar, 150).Value = DBNull.Value;
                }
                if(FileType!=0)
                {
                    m_command.Parameters.Add("@iFileType", SqlDbType.TinyInt).Value = FileType;
                }
                else
                {
                    m_command.Parameters.Add("@iFileType", SqlDbType.TinyInt).Value = DBNull.Value;
                }
               
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool UpdateTestDetails(long TestId, string TestName, string TestDescription, string FilePath, byte FileType)
        {
            bool result = false;
            string strCurrentMethodName = "UpdateTestDetails";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spUpdateTestDetails", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llTestId", SqlDbType.BigInt).Value = TestId;
                m_command.Parameters.Add("@strTestName", SqlDbType.VarChar, 250).Value = TestName;
                m_command.Parameters.Add("@strTestDescription", SqlDbType.VarChar, 500).Value = TestDescription;
                //m_command.Parameters.Add("@strFilePath", SqlDbType.VarChar, 150).Value = FilePath;
                //m_command.Parameters.Add("@iFileType", SqlDbType.TinyInt).Value = FileType;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool DeleteTest(long TestId)
        {
            bool result = false;
            string strCurrentMethodName = "DeleteTest";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spDeleteTest", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llTestId", SqlDbType.BigInt).Value = TestId;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool DeleteTestOfCourse(long TestId)
        {
            bool result = false;
            string strCurrentMethodName = "DeleteTestOfCourse";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spDeleteCourseTest", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llTestId", SqlDbType.BigInt).Value = TestId;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool DeleteIndepenetTest(long TestId)
        {
            bool result = false;
            string strCurrentMethodName = "DeleteIndepenetTest";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spDeleteIndependentTest", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llTestId", SqlDbType.BigInt).Value = TestId;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool ActivateAssignment(long AssignmentId,string ShareCode,string TinyUrl)
        {
            bool result = false;
            string strCurrentMethodName = "ActivateAssignment";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spActivateAssignment", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llAssignmentId", SqlDbType.BigInt).Value = AssignmentId;
                m_command.Parameters.Add("@strShareCode", SqlDbType.VarChar,10).Value = ShareCode;
                m_command.Parameters.Add("@strTinyUrl", SqlDbType.VarChar, 100).Value = TinyUrl;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool DeleteAssignmentOfCourse(long AssignmentId)
        {
            bool result = false;
            string strCurrentMethodName = "DeleteAssignment";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spDeleteAssignmentOfCourse", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llAssignmentId", SqlDbType.BigInt).Value = AssignmentId;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool DeleteMcqQuestionOfAssignment(long QuestionId)
        {
            bool result = false;
            string strCurrentMethodName = "DeleteMcqQuestionOfAssignment";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spDeleteMcqAssignmentQuestion", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llQuestionId", SqlDbType.BigInt).Value = QuestionId;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool DeleteIndexTopic(long TopicId)
        {
            bool result = false;
            string strCurrentMethodName = "DeleteIndeXTopic";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spDeleteTopic", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llTopicId", SqlDbType.BigInt).Value = TopicId;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool UpdateIndexTopic(long TopicId,string TopicName,string TopicDescription,string FilePathMapToServer,byte? FileTypeId)
        {
            bool result = false;
            string strCurrentMethodName = "Cp_spUpdateTopicDetails";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spUpdateTopicDetails", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llTopicId", SqlDbType.BigInt).Value = TopicId;
                m_command.Parameters.Add("@strTopicName", SqlDbType.VarChar, 256).Value = TopicName;
                m_command.Parameters.Add("@strTopicDescription", SqlDbType.VarChar, 4000).Value = TopicDescription;
                if (FilePathMapToServer != null&&FilePathMapToServer!=string.Empty)
                {
                    m_command.Parameters.Add("@strFilePathMapToServer", SqlDbType.VarChar, 100).Value = FilePathMapToServer;
                    
                }
                else
                {
                    m_command.Parameters.Add("@strFilePathMapToServer", SqlDbType.VarChar, 100).Value = DBNull.Value; 
                }
                if(FileTypeId!=null)
                {
                    m_command.Parameters.Add("@iFileTypeId", SqlDbType.TinyInt).Value = FileTypeId;
                }
                else
                {
                    m_command.Parameters.Add("@iFileTypeId", SqlDbType.TinyInt).Value = DBNull.Value;
                }
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool UpdateMcqQuestionOfAssignment(long QuestionId,string QuestionStatement,string Option1,string Option2,string Option3,string Option4,byte CorrectOption)
        {
            bool result = false;
            string strCurrentMethodName = "UpdateMcqQuestionOfAssignment";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spUpdateAssignmentMcqQuestion", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@strQuestionStatement", SqlDbType.VarChar,1000).Value = QuestionStatement;
                m_command.Parameters.Add("@llQuestionId", SqlDbType.BigInt).Value = QuestionId;
                m_command.Parameters.Add("@strOption1", SqlDbType.VarChar, 256).Value = Option1;
                m_command.Parameters.Add("@strOption2", SqlDbType.VarChar, 256).Value = Option2;
                m_command.Parameters.Add("@strOption3", SqlDbType.VarChar, 256).Value = Option3;
                m_command.Parameters.Add("@strOption4", SqlDbType.VarChar, 256).Value = Option4;
                m_command.Parameters.Add("@iCorrectOption", SqlDbType.TinyInt).Value = CorrectOption;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool ActivateTest(long TestId,string ShareCode,string TinyUrl)
        {
            bool result = false;
            string strCurrentMethodName = "ActivateTest";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spActivateTest", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llTestId", SqlDbType.BigInt).Value = TestId;
                m_command.Parameters.Add("@strShareCode", SqlDbType.VarChar,10).Value = ShareCode;
                m_command.Parameters.Add("@strTinyUrl", SqlDbType.VarChar,100).Value = TinyUrl;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool InsertContactFormDetails(string Name,string Email,string PhoneNo,string Subject,string Message)
        {
            bool result = false;
            string strCurrentMethodName = "InsertContactFormDetails";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spInsertContactRequest", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@strName", SqlDbType.VarChar,200).Value = Name;
                m_command.Parameters.Add("@strEmail", SqlDbType.VarChar,200).Value = Email;
                m_command.Parameters.Add("@strPhoneNo", SqlDbType.VarChar,10).Value = PhoneNo;
                m_command.Parameters.Add("@strSubject", SqlDbType.VarChar).Value = Subject;
                m_command.Parameters.Add("@strMessage", SqlDbType.VarChar).Value = Message;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public DataSet GetIndexTopicDetails(long IndexId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetIndexTopicDetails";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetIndexTopicDetails", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llIndexid", SqlDbType.BigInt).Value = IndexId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet GetIndexTopicProgressForStudent(long IndexId,long StudentId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetIndexTopicDetails";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetIndexTopicProgressForStudent", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llIndexId", SqlDbType.BigInt).Value = IndexId;
                m_command.Parameters.Add("@llStudentId", SqlDbType.BigInt).Value = StudentId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet GetIndexDetails(long IndexId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetIndexDetails";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetIndexDetails", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llIndexId", SqlDbType.BigInt).Value = IndexId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet GetMcqTestDetails(long TestId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetMcqTestDetails";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetMcqTestDetails", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llTestId", SqlDbType.BigInt).Value = TestId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet GetMcqtestQuestionDetails(long TestId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetMcqtestQuestionDetails";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetAllQuestionsOfMcqTest", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llTestId", SqlDbType.BigInt).Value = TestId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }

        public bool DeleteIndex(long IndexId)
        {
            bool result= false;
            string strCurrentMethodName = "DeleteIndex";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spDeleteCourseIndex", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llIndexId", SqlDbType.BigInt).Value = IndexId;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool DeleteInpependentAssignment(long AssignmentId)
        {
            bool result = false;
            string strCurrentMethodName = "DeleteInpependentAssignment";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spDeleteIndependentAssignment", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llAssignmentId", SqlDbType.BigInt).Value = AssignmentId;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool UpdateAssignmentDetails(long llAssignmentId,string AssignmentName,string AAssignmentDescription)
        {
            bool result = false;
            string strCurrentMethodName = "UpdateAssignmentDetails";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spUpdateCourseAssignmentDetails", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llAssignmentId", SqlDbType.BigInt).Value = llAssignmentId;
                m_command.Parameters.Add("@strAssignmentName", SqlDbType.VarChar,250).Value = AssignmentName;
                m_command.Parameters.Add("@strAssignmentDescription", SqlDbType.VarChar,500).Value = AAssignmentDescription;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool UpdateFullCourseDetails(long CourseId, string CourseName, string CourseDescription)
        {
            bool result = false;
            string strCurrentMethodName = "UpdateAssignmentDetails";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spUpdateFullCourseDetails", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llCourseId", SqlDbType.BigInt).Value = CourseId;
                m_command.Parameters.Add("@strCourseName", SqlDbType.VarChar, 250).Value = CourseName;
                m_command.Parameters.Add("@strCourseDescription", SqlDbType.VarChar, 500).Value = CourseDescription;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool DeleteMcqQuestion(long QuestionId)
        {
            bool result = false;
            string strCurrentMethodName = "DeleteMcqQuestion";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spDeleteMcqQuestion", m_con);
                m_command.Parameters.Add("@llQuestionId", SqlDbType.BigInt).Value = QuestionId;
                m_command.CommandType = System.Data.CommandType.StoredProcedure;               
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool UpdateMcqTestDetails(long TestId, string TestName, string TestDescroption)
        {
            bool result = false;
            string strCurrentMethodName = "UpdateMcqTestDetails";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spUpdateMcqTestDetails", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llTestId", SqlDbType.BigInt).Value = TestId;
                m_command.Parameters.Add("@strTestName", SqlDbType.VarChar, 250).Value = TestName;
                m_command.Parameters.Add("@strTestDescription", SqlDbType.VarChar, 250).Value = TestDescroption;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool UpdateMcqQuestionDetails(long Questionid, string QuestionStatement, string Option1,string Option2,string Option3,string Option4,
                 byte CorrectOption,int iTimeForQuestion,int iMarksForQuestion)
        {
            bool result = false;
            string strCurrentMethodName = "UpdateMcqQuestionDetails";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spUpdateMcqTestQuestionDetails", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@strQuestionStatement", SqlDbType.VarChar,1000).Value = QuestionStatement;
                m_command.Parameters.Add("@llQuestionId", SqlDbType.BigInt).Value = Questionid;
                m_command.Parameters.Add("@strOption1", SqlDbType.VarChar, 256).Value = Option1;
                m_command.Parameters.Add("@strOption2", SqlDbType.VarChar, 256).Value = Option2;
                m_command.Parameters.Add("@strOption3", SqlDbType.VarChar, 256).Value = Option3;
                m_command.Parameters.Add("@strOption4", SqlDbType.VarChar, 256).Value = Option4;
                m_command.Parameters.Add("@iCorrectOption", SqlDbType.TinyInt).Value = CorrectOption;
                m_command.Parameters.Add("@iTimeForQuestion", SqlDbType.Int).Value = iTimeForQuestion;
                m_command.Parameters.Add("@iMarksForQuestion", SqlDbType.Int).Value = @iMarksForQuestion;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool DeleteMcqTest(long TestId)
        {
            bool result = false;
            string strCurrentMethodName = "DeleteMcqTest";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spDeleteMcqTest", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llTestId", SqlDbType.BigInt).Value = TestId;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool InsertNewTestToCourse(string TestName,string TestDescription,byte TestTypeId,string FilePath,byte? FileTypeId,
                    long CourseId)
        {
            bool result = false;
            string strCurrentMethodName = "InsertNewTestToCourse";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spInsertNewTestToCourse", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@strTestName", SqlDbType.VarChar,250).Value = TestName;
                m_command.Parameters.Add("@strTestDescription", SqlDbType.VarChar, 250).Value = TestDescription;
                if (FilePath == null)
                {
                    m_command.Parameters.Add("@strFilePath", SqlDbType.VarChar, 150).Value = DBNull.Value;
                }
                else
                {
                    m_command.Parameters.Add("@strFilePath", SqlDbType.VarChar, 150).Value=FilePath;
                }
                if (FileTypeId == null || FileTypeId == 0)
                {
                    m_command.Parameters.Add("@iFileTypeId", SqlDbType.TinyInt).Value = DBNull.Value;
                }
                else
                {
                    m_command.Parameters.Add("@iFileTypeId", SqlDbType.TinyInt).Value = FileTypeId;
                }
                m_command.Parameters.Add("@iTestType", SqlDbType.TinyInt).Value = TestTypeId;
                m_command.Parameters.Add("@llCourseId", SqlDbType.BigInt).Value = CourseId;
                m_command.Parameters.Add("@llTestId", SqlDbType.BigInt).Direction = ParameterDirection.Output;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool InsertNewAssignmentToCourse(string AssignmentName, string AssignmentDescription, byte AssignmentTypeId, string FilePath, byte? FileTypeId,
                  long CourseId)
        {
            bool result = false;
            string strCurrentMethodName = "InsertNewAssignmentToCourse";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spInsertNewAssignmentToCourse", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@strAssignmentName", SqlDbType.VarChar, 250).Value = AssignmentName;
                m_command.Parameters.Add("@strAssignmentDescription", SqlDbType.VarChar, 250).Value = AssignmentDescription;
                if (FilePath != null && FilePath != string.Empty) { m_command.Parameters.Add("@strFilePath", SqlDbType.VarChar, 150).Value = FilePath; }
                else { m_command.Parameters.Add("@strFilePath", SqlDbType.VarChar, 150).Value = DBNull.Value; }
                if (FileTypeId != null && FileTypeId != 0) { m_command.Parameters.Add("@iFileTypeId", SqlDbType.TinyInt).Value = FileTypeId; }
                else { m_command.Parameters.Add("@iFileTypeId", SqlDbType.TinyInt).Value = DBNull.Value; ; }
                m_command.Parameters.Add("@iAssignmentType", SqlDbType.TinyInt).Value = AssignmentTypeId;
                m_command.Parameters.Add("@llCourseId", SqlDbType.BigInt).Value = CourseId;
                m_command.Parameters.Add("@llAssignmentId", SqlDbType.BigInt).Direction = ParameterDirection.Output;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool DeleteMcqTestQuestion(long QuestionId)
        {
            bool result = false;
            string strCurrentMethodName = "DeleteMcqTestQuestion";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spDeleteMcqQuestion", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llQuestionId", SqlDbType.VarChar, 250).Value = QuestionId;
             
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public DataSet GetAllAssignmentsForCourse(long CourseId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetAllAssignmentsForCourse";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetAssignmentsOfCourse", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llCourseId", SqlDbType.VarChar, 250).Value = CourseId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);

            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet GetTestsOfCourse(long CourseId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetTestsOfCourse";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetTestOfCourses", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llCourseId", SqlDbType.BigInt).Value = CourseId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);

            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet GetAllQuestionsOfSubjectiveAssignment(long AssignmentId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetAllQuestionsOfSubjectiveAssignment";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetSubjectiveAssignmentQuestions", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llAssignmentId", SqlDbType.BigInt).Value = AssignmentId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public bool InsertSubjectiveAssignmentQuestion(long AssignmentId,string QuestionStatement,string Hint)
        {
            bool result = false;
            string strCurrentMethodName = "InsertSubjectiveAssignmentQuestion";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spInsertSubjectiveAssignmentQuestion", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llAsssignmentId", SqlDbType.BigInt).Value = AssignmentId;
                m_command.Parameters.Add("@strQuestionStatement", SqlDbType.VarChar, 4000).Value = QuestionStatement;
                m_command.Parameters.Add("@strQuestionHint", SqlDbType.VarChar, 4000).Value = Hint;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool UpdateSubjectiveAssignmentQuestion(long QuestionId, string QuestionStatement, string Hint)
        {
            bool result = false;
            string strCurrentMethodName = "UpdateSubjectiveAssignmentQuestion";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spUpdateSubjectiveAssignmentQuestion", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llQuestionId", SqlDbType.BigInt).Value = QuestionId;
                m_command.Parameters.Add("@strQuestionStatement", SqlDbType.VarChar, 4000).Value = QuestionStatement;
                m_command.Parameters.Add("@strQuestionHint", SqlDbType.VarChar, 4000).Value = Hint;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool DeleteSubjectiveAssignmentOfCourse(long AssignmentId)
        {
            bool result = false;
            string strCurrentMethodName = "DeleteSubjectiveAssignmentOfCourse";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spDeleteSubectiveAssignmentOfCourse", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llAssignmentId", SqlDbType.BigInt).Value = AssignmentId;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool DeleteSubjectiveAssignmentQuestion(long QuestionId)
        {
            bool result = false;
            string strCurrentMethodName = "DeleteSubjectiveAssignmentOfCourse";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spDeleteSubjectiveAssignmentQuestion", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llQuestionId", SqlDbType.BigInt).Value = QuestionId;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool RegisterNewStudent(string FirstName,string LastName,string UserId,string HashedPassword,string PhoneNo,
            string PhoneNoVerificationGuid,string EmailIdVerificationGuid)
        {
            bool result = false;
            string strCurrentMethodName = "RegisterNewStudent";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spInsertNewStudent", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@strStudentFirstName", SqlDbType.VarChar,250).Value = FirstName;
                m_command.Parameters.Add("@strStudentLastName", SqlDbType.VarChar, 250).Value = LastName;
                m_command.Parameters.Add("@strSudentUserId", SqlDbType.VarChar, 250).Value = UserId;
                m_command.Parameters.Add("@strHashedPassword", SqlDbType.VarChar, 100).Value = HashedPassword;
                m_command.Parameters.Add("@strPhoneNo", SqlDbType.VarChar, 20).Value = PhoneNo;
                m_command.Parameters.Add("@strPhoneNoVerificationGuid", SqlDbType.VarChar, 100).Value = PhoneNoVerificationGuid;
                m_command.Parameters.Add("@strEmailVarificationGuid", SqlDbType.VarChar, 100).Value = EmailIdVerificationGuid;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool ValidateStudentLogin(string UserId, string HashedPassword,ref long StudentId)
        {
            bool result = false;
            string strCurrentMethodName = "ValidateStudentLogin";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spValidateStudentLogin", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@strUserId", SqlDbType.VarChar, 250).Value = UserId;
                m_command.Parameters.Add("@strPassword", SqlDbType.VarChar, 100).Value = HashedPassword;
                m_command.Parameters.Add("@bIsLoggedIn", SqlDbType.Bit).Direction = ParameterDirection.Output;
                m_command.Parameters.Add("@llStudentId", SqlDbType.BigInt).Direction = ParameterDirection.Output;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = (bool)m_command.Parameters["@bIsLoggedIn"].Value;
                    StudentId = (long)m_command.Parameters["@llStudentId"].Value;
                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool InsertActivityForStudent(string ActivityMessage,long StudentId)
        {
            bool result = false;
            string strCurrentMethodName = "InsertActivityForStudent";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spInsertNewActivity", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@strActivityMessage", SqlDbType.VarChar, 250).Value = ActivityMessage;
                m_command.Parameters.Add("@strActivityMessage", SqlDbType.BigInt).Value = StudentId;
                m_con.Open();
                result = m_command.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool JoinStudentToCourse(long CourseId, long StudentId)
        {
            bool result = false;
            string strCurrentMethodName = "JoinStudentToCourse";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spJoinCourseToStudent", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llCourseId", SqlDbType.BigInt).Value = CourseId;
                m_command.Parameters.Add("@llStudentId", SqlDbType.BigInt).Value = StudentId;
                m_con.Open();
                result = m_command.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public DataSet SearchForCourse(string SerachString, int MaxRowToReturn,int NoOfRowsFetch,int SortType)
        {
            DataSet sDS=new DataSet();
            string strCurrentMethodName = "SerchForCourse";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetSerachResultForCourse", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@strSerarchString", SqlDbType.VarChar,100).Value = SerachString;
                m_command.Parameters.Add("@llMaxRowToReturn", SqlDbType.Int).Value = MaxRowToReturn;
                m_command.Parameters.Add("@iNoOfRowsFetched", SqlDbType.Int).Value = NoOfRowsFetch;
                m_command.Parameters.Add("@iSortType", SqlDbType.Int).Value = SortType;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet SearchForCourseForStudent(string SerachString, int MaxRowToReturn, int NoOfRowsFetch, int SortType,long StudentId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "SearchForCourseForStudent";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetSerachResultForCourseForStudent", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@strSerarchString", SqlDbType.VarChar, 100).Value = SerachString;
                m_command.Parameters.Add("@llMaxRowToReturn", SqlDbType.Int).Value = MaxRowToReturn;
                m_command.Parameters.Add("@iNoOfRowsFetched", SqlDbType.Int).Value = NoOfRowsFetch;
                m_command.Parameters.Add("@iSortType", SqlDbType.Int).Value = SortType;
                m_command.Parameters.Add("@llStudentId", SqlDbType.BigInt).Value = StudentId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet SearchForCourseOfInstructor(string SerachString, int MaxRowToReturn,int InstructorId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "SearchForCourseOfInstructor";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetSerachResultForCourseOfInstructor", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@strSerarchString", SqlDbType.VarChar, 100).Value = SerachString;
                m_command.Parameters.Add("@llMaxRowToReturn", SqlDbType.Int).Value = MaxRowToReturn;
                m_command.Parameters.Add("@iInstructorId", SqlDbType.Int).Value = InstructorId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet SearchForInstructor(string SerachString, int MaxRowToReturn)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "SearchForInstructor";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetSerachResultForInstructor", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@strSerarchString", SqlDbType.VarChar, 100).Value = SerachString;
                m_command.Parameters.Add("@llMaxRowToReturn", SqlDbType.Int).Value = MaxRowToReturn;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet SearchForAssignment(string SerachString, int MaxRowToReturn,long LastFetchedId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "SearchForAssignment";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetSerachResultForAssignment", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@strSerarchString", SqlDbType.VarChar, 100).Value = SerachString;
                m_command.Parameters.Add("@llMaxRowToReturn", SqlDbType.Int).Value = MaxRowToReturn;
                m_command.Parameters.Add("@llLastFetchedId",SqlDbType.BigInt).Value=LastFetchedId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet SearchForAssignmentOfInstructor(string SerachString, int MaxRowToReturn,int InstructorId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "SearchForAssignmentOfInstructor";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetSerachResultForAssignmentOfInstructor", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@strSerarchString", SqlDbType.VarChar, 100).Value = SerachString;
                m_command.Parameters.Add("@llMaxRowToReturn", SqlDbType.Int).Value = MaxRowToReturn;
                m_command.Parameters.Add("@iInstructorId", SqlDbType.Int).Value = InstructorId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet SearchForTest(string SerachString, int MaxRowToReturn,long LastFetchedId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "SearchForTest";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetSerachResultForTest", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@strSerarchString", SqlDbType.VarChar, 100).Value = SerachString;
                m_command.Parameters.Add("@llMaxRowToReturn", SqlDbType.Int).Value = MaxRowToReturn;
                m_command.Parameters.Add("@llLastFetchedId", SqlDbType.BigInt).Value = LastFetchedId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet SearchForTestOfInstructor(string SerachString, int MaxRowToReturn,int InstructorId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "SearchForTestOfInstructor";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetSerachResultForTestOfInstructor", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@strSerarchString", SqlDbType.VarChar, 100).Value = SerachString;
                m_command.Parameters.Add("@llMaxRowToReturn", SqlDbType.Int).Value = MaxRowToReturn;
                m_command.Parameters.Add("@iInstructorId", SqlDbType.Int).Value = InstructorId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet GetStudentDetails(long StudentId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetStudentDetails";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetStudentDetails", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llStudentId", SqlDbType.BigInt).Value = StudentId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet GetJoinedCoursesForStudent(long StudentId,string SearchString,int MaxRowCountToReturn)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetJoinedCoursesForStudent";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetJOinedCoursesForStudent", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llStudentId", SqlDbType.BigInt).Value = StudentId;
                m_command.Parameters.Add("@strSerarchString", SqlDbType.VarChar,100).Value = SearchString;
                m_command.Parameters.Add("@llMaxRowToReturn", SqlDbType.Int).Value = MaxRowCountToReturn;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public bool GetStudentIdFromUserId(string UserId, ref long StudentId)
        {
            bool result = false;
            string strCurrentMethodName = "GetStudentIdFromUserId";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetStudentIdFromUserId", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@strUserId", SqlDbType.VarChar,250).Value = UserId;
                m_command.Parameters.Add("@llStudentId", SqlDbType.BigInt).Direction = ParameterDirection.Output;
                m_con.Open();
                m_command.ExecuteNonQuery();
             
                StudentId = (long)m_command.Parameters["@llStudentId"].Value;
                result = true;


            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool UpdateStudentDetails(string FirstName,string LastName,string AddressLine1,string AddressLine2,string PinCode,
                                          int? StateId,int? CityId,string Gender,long StudentId,string PhoneNo)
        {
            bool result = false;
            string strCurrentMethodName = "GetStudentIdFromUserId";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spUpdateStudentDetails", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llStudentId", SqlDbType.BigInt).Value=StudentId;
                m_command.Parameters.Add("@strFirstName", SqlDbType.VarChar, 250).Value = FirstName;
                m_command.Parameters.Add("@strLastName", SqlDbType.VarChar, 250).Value = LastName;
                m_command.Parameters.Add("@strAddressLine1", SqlDbType.VarChar, 250).Value = AddressLine1;
                m_command.Parameters.Add("@strAddressLine2", SqlDbType.VarChar, 250).Value = AddressLine2;
                m_command.Parameters.Add("@strPineCode", SqlDbType.VarChar,6).Value = PinCode;
                m_command.Parameters.Add("@strPhoneNo", SqlDbType.VarChar, 20).Value = PhoneNo;
                if (StateId==null)
                {
                    m_command.Parameters.Add("@iStateId", SqlDbType.Int).Value = DBNull.Value;
                }
                else
                {
                    m_command.Parameters.Add("@iStateId", SqlDbType.Int).Value = StateId;
                }
                if (CityId == null)
                {
                    m_command.Parameters.Add("@iCityId", SqlDbType.Int).Value = DBNull.Value;
                }
                else
                {
                    m_command.Parameters.Add("@iCityId", SqlDbType.Int).Value = CityId;
                }
                m_command.Parameters.Add("@iGender", SqlDbType.VarChar,10).Value = Gender;
                m_con.Open();
                result = m_command.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool UpdateStudentPassword(long StudentId,string OldHashedPassword,string NewHashedPassword)
        {
            bool result = false;
            string strCurrentMethodName = "UpdateStudentPassword";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spUpdatePasswordForStudent", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@strHasedPasswordOld", SqlDbType.VarChar, 100).Value = OldHashedPassword;
                m_command.Parameters.Add("@strNewPasswordPassword", SqlDbType.VarChar, 100).Value = NewHashedPassword;
                m_command.Parameters.Add("@llStudentId", SqlDbType.BigInt).Value = StudentId;
                m_con.Open();
                result = m_command.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool JoinStudentToInstructor(long StudentId,int InstructorId)
        {
            bool result = false;
            string strCurrentMethodName = "JoinStudentToInstructor";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spJoinStudentToInstructor", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llStudentId", SqlDbType.BigInt).Value = StudentId;
                m_command.Parameters.Add("@iInstructorId", SqlDbType.Int).Value = InstructorId;
                m_con.Open();
                result = m_command.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public DataSet GetAllJoinedInstructorForStudent(long StudentId, string SearchString,int MaxRowCount)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetAllJoinedInstructorForStudent";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetAllJoinedInstructorsForStudent", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llStudentId", SqlDbType.BigInt).Value = StudentId;
                m_command.Parameters.Add("@strSerarchString", SqlDbType.VarChar,100).Value = SearchString;
                m_command.Parameters.Add("@iMaxRowToReturn", SqlDbType.Int).Value = MaxRowCount;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet GetAllAssignmentSubmissionsForStudent(long StudentId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetAllAssignmentSubmissionsForStudent";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetAllAssignmentSubmissionsForStudent", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llStudentId", SqlDbType.BigInt).Value = StudentId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public bool InsertAssignmentResponse(long StudentId, long AssignmentId, DateTime AssignmentStartTime, DateTime AssignmentFinishTime, string Response,
                     int PercentageScore,int TotalNoOfQuestions,ref long SubmissionId)
        {
            bool result = false;
            string strCurrentMethodName = "InsertAssignmentResponse";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spInsertStudentAssignmentResponse", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llStudentId", SqlDbType.BigInt).Value = StudentId;
                m_command.Parameters.Add("@llAssignmentID", SqlDbType.Int).Value = AssignmentId;
                m_command.Parameters.Add("@dtAssignmentStartTime", SqlDbType.DateTime).Value = AssignmentStartTime;
                m_command.Parameters.Add("@dtAssignmentFinishTime", SqlDbType.DateTime).Value = AssignmentFinishTime;
                m_command.Parameters.Add("@strAssignmentResponse", SqlDbType.VarChar,4000).Value = Response;
                m_command.Parameters.Add("@iAssignmentPercentageScore", SqlDbType.Int).Value = PercentageScore;
                m_command.Parameters.Add("@iTotalNoOfQuestions", SqlDbType.Int).Value = TotalNoOfQuestions;
                m_command.Parameters.Add("@llSubmissionId", SqlDbType.BigInt).Direction = ParameterDirection.Output;
                m_con.Open();
                if(m_command.ExecuteNonQuery()>0)
                {
                    result = true;
                    SubmissionId = (long)m_command.Parameters["@llSubmissionId"].Value;
                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool InsertAssignmentFeedback(long SubmissionId, string FeedBack,int Rating)
        {
            bool result = false;
            string strCurrentMethodName = "InsertAssignmentFeedback";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spInsertAssignmentFeedback", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llSubmissionID", SqlDbType.BigInt).Value = SubmissionId;
                m_command.Parameters.Add("@strFeedBack", SqlDbType.VarChar,4000).Value = FeedBack;
                m_command.Parameters.Add("@iRating", SqlDbType.Int).Value = Rating;
                m_con.Open();
                result = m_command.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public DataSet GetAssignmentResponse(long SubmissionId,long StudentId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetAssignmentResponse";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetAssignmentResponse", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llSubmissionID", SqlDbType.BigInt).Value = SubmissionId;
                m_command.Parameters.Add("@llStudentId", SqlDbType.BigInt).Value = StudentId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);

            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public bool InsertTestResponse(long StudentId, long TestId, DateTime TestStartTime, DateTime TestFinishTime, string Response,
                    int PercentageScore, int TotalNoOfQuestions, ref long SubmissionId)
        {
            bool result = false;
            string strCurrentMethodName = "InsertAssignmentResponse";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spInsertStudentTestResponse", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llStudentId", SqlDbType.BigInt).Value = StudentId;
                m_command.Parameters.Add("@llTestId", SqlDbType.Int).Value = TestId;
                m_command.Parameters.Add("@dtTestStartTime", SqlDbType.DateTime).Value = TestStartTime;
                m_command.Parameters.Add("@dtTestFinishTime", SqlDbType.DateTime).Value = TestFinishTime;
                m_command.Parameters.Add("@strTestResponse", SqlDbType.VarChar, 4000).Value = Response;
                m_command.Parameters.Add("@iTestPercentageScore", SqlDbType.Int).Value = PercentageScore;
                m_command.Parameters.Add("@iTotalNoOfQuestions", SqlDbType.Int).Value = TotalNoOfQuestions;
                m_command.Parameters.Add("@llSubmissionId", SqlDbType.BigInt).Direction = ParameterDirection.Output;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = true;
                    SubmissionId = (long)m_command.Parameters["@llSubmissionId"].Value;
                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public DataSet GetTestResponse(long SubmissionId,long StudentId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetTestResponse";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetTestResponse", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llSubmissionID", SqlDbType.BigInt).Value = SubmissionId;
                m_command.Parameters.Add("@llStudentId", SqlDbType.BigInt).Value = StudentId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);

            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet GetAllTestSubmissionsForStudent(long StudentId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetAllTestSubmissionsForStudent";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetAllTestSubmissionsForStudent", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llStudentId", SqlDbType.BigInt).Value = StudentId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet GetStudentHomeDetails(long StudentId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetStudentHomeDetails";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetStudentHomeDetails", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llStudentId", SqlDbType.BigInt).Value = StudentId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet GetStudentJoinedToCourse(long CourseId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetStudentJoinedToCourse";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetAllStudentsJoinedToCourse", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llCourseId", SqlDbType.BigInt).Value = CourseId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet GetAllAssignmentsSubmissions(long AssignmentId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetAllAssignmentsSubmissions";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetAllAssignmentSubissions", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llAssignmentId", SqlDbType.BigInt).Value = AssignmentId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet GetAllTestSubmissions(long StudentId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetAllTestSubmissions";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetAllTestSubissions", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llTestId", SqlDbType.BigInt).Value = StudentId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet GetAllStudentsJoinedToInstructor(int InstructorId,int NoOfRowsFetched,string SearchString)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetAllStudentsJoinedToInstructor";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetAllStudentsJoinedToInstructor", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@iInstructorId", SqlDbType.BigInt).Value = InstructorId;
                m_command.Parameters.Add("@strSearchStudent", SqlDbType.VarChar).Value = SearchString;
                m_command.Parameters.Add("@iNoOfRowsFetched", SqlDbType.Int).Value = NoOfRowsFetched;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public bool InsertNewCourseV2(string CourseName,string CourseDescription,int InstructorId,string AboutCourse,string CourseImagePath,ref long CourseId)
        {
            bool result = false;
            string strCurrentMethodName = "insertNewCourseV2";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spInsertNewCourseV2", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@iInstructorId", SqlDbType.BigInt).Value = InstructorId;
                m_command.Parameters.Add("@strCourseName", SqlDbType.VarChar,250).Value = CourseName;
                m_command.Parameters.Add("@strCourseDescription", SqlDbType.VarChar, 1000).Value = CourseDescription;
                m_command.Parameters.Add("@strAboutCourse", SqlDbType.VarChar, 8000).Value = AboutCourse;
                m_command.Parameters.Add("@strCourseImagePath", SqlDbType.VarChar, 256).Value = CourseImagePath;
                m_command.Parameters.Add("@llCourseId", SqlDbType.BigInt).Direction = ParameterDirection.Output;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = true;
                    CourseId = (long)m_command.Parameters["@llCourseId"].Value;
                }

            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool InsertNewIndexToV2Course(long CourseId,string IndexName,string IndexContetHtml,ref long IndexId)
        {
            bool result = false;
            string strCurrentMethodName = "insertNewCourseV2";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spInsertNewCourseV2", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llCourseId", SqlDbType.BigInt).Value = CourseId;
                m_command.Parameters.Add("@strIndexName", SqlDbType.VarChar, 250).Value = IndexName;
                m_command.Parameters.Add("@strIndexDescription", SqlDbType.VarChar, 8000).Value = IndexContetHtml;
                m_command.Parameters.Add("@llIndexId", SqlDbType.BigInt).Direction = ParameterDirection.Output;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = true;
                    IndexId = (long)m_command.Parameters["IndexId"].Value;
                }

            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool InsertNewTopicToV2Course(long IndexId,string TopicName,string TopicHtml)
        {
            bool result = false;
            string strCurrentMethodName = "insertNewCourseV2";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spInsertNewCourseV2", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llIndexId", SqlDbType.BigInt).Value = IndexId;
                m_command.Parameters.Add("@strTopicName", SqlDbType.VarChar, 250).Value = TopicName;
                m_command.Parameters.Add("@strTopicContentHtml", SqlDbType.VarChar, 8000).Value = TopicHtml;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool InsertNewAlertForInstructor(int InstructorId,string AlertMessage,int AlertTypeId,long? EffectiveContentid,long StudentId)
        {
            bool result = false;
            string strCurrentMethodName = "InsertNewAlertForInstructor";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spInsertNewAlertForInstructor", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@iInstructorId", SqlDbType.BigInt).Value = InstructorId;
                m_command.Parameters.Add("@strAlertMessage", SqlDbType.VarChar, 250).Value = AlertMessage;
                m_command.Parameters.Add("@iAlertTypeId", SqlDbType.Int).Value = AlertTypeId;
                if (EffectiveContentid == null)
                {
                    m_command.Parameters.Add("@llEffectedId", SqlDbType.BigInt).Value = DBNull.Value;
                }
                else
                {
                    m_command.Parameters.Add("@llEffectedId", SqlDbType.BigInt).Value = EffectiveContentid;
                }
                m_command.Parameters.Add("@llStudentId", SqlDbType.BigInt).Value = StudentId;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public DataSet GetAllAlertForInstructor(int InstructorId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetAllAlertForInstructor";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetInstructorAlertMessage", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@iInstructorId", SqlDbType.BigInt).Value = InstructorId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public bool GetInstructorIdByCourseId(long CourseId,ref int InstructorId)
        {
            bool result = false;
            string strCurrentMethodName = "GetInstructorIdByCourseId";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetInstructorIdByCourseId", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llCourseID", SqlDbType.BigInt).Value = CourseId;
                m_command.Parameters.Add("@iInstructorId", SqlDbType.Int).Direction = ParameterDirection.Output;
                m_con.Open();
                m_command.ExecuteNonQuery();
                InstructorId = (int)m_command.Parameters["@iInstructorId"].Value;
                result = true;
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool GetInstructorIdByTestId(long TestId, ref int InstructorId)
        {
            bool result = false;
            string strCurrentMethodName = "GetInstructorIdByTestId";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetInstructorIdByTestId", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llTestid", SqlDbType.BigInt).Value = TestId;
                m_command.Parameters.Add("@iInstructorId", SqlDbType.Int).Direction = ParameterDirection.Output;
                m_con.Open();
                m_command.ExecuteNonQuery();
                InstructorId = (int)m_command.Parameters["@iInstructorId"].Value;
                result = true;
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool GetInstructorIdByAssignmentId(long AssignmentId, ref int InstructorId)
        {
            bool result = false;
            string strCurrentMethodName = "GetInstructorIdByAssignmentId";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetInstructorIdByAssignmentid", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llAssignmentId", SqlDbType.BigInt).Value = AssignmentId;
                m_command.Parameters.Add("@iInstructorId", SqlDbType.Int).Direction = ParameterDirection.Output;
                m_con.Open();
                m_command.ExecuteNonQuery();
                InstructorId = (int)m_command.Parameters["@iInstructorId"].Value;
                result = true;
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool MarkAlertReadForInstructor(long AlertId)
        {
            bool result = false;
            string strCurrentMethodName = "MarkAlertReadForInstructor";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spMarkAlertReadForInstructor", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llAlertId", SqlDbType.BigInt).Value = AlertId;
                m_con.Open();
                if (m_command.ExecuteNonQuery()>0)
                {
                    result = true;
                }
               
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public DataSet CheckStudentHasJoinedTheCourse(long StudentId,long CourseId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "CheckStudentHasJoinedTheCourse";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spCheckStudentHasJoinedTheCourse", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llStudentId", SqlDbType.BigInt).Value = StudentId;
                m_command.Parameters.Add("@llCourseId", SqlDbType.BigInt).Value = CourseId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet CheckStudentHasSubmittedTheTest(long StudentId, long TestId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "CheckStudentHasSubmittedTheTest";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spStudnetHasAlreadySubmittedTheTest", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llStudentId", SqlDbType.BigInt).Value = StudentId;
                m_command.Parameters.Add("@llTestId", SqlDbType.BigInt).Value = TestId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet CheckStudentHasSubmittedTheAssignment(long StudentId, long AssignmentId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "CheckStudentHasSubmittedTheAssignment";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spCheckStudnetHasAlreadySubmittedTheAssignment", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llStudentId", SqlDbType.BigInt).Value = StudentId;
                m_command.Parameters.Add("@llAssignmentId", SqlDbType.BigInt).Value = AssignmentId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet CheckTestResponseIdExistsForStudent(long StudentId, long SubmissionId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "CheckTestResponseIdExistsForStudent";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spCheckTestResponseIdExistsForStudent", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llStudentId", SqlDbType.BigInt).Value = StudentId;
                m_command.Parameters.Add("@llSubmissionId", SqlDbType.BigInt).Value = SubmissionId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet CheckAssignmentResponseIdExistsForStudent(long StudentId, long SubmissionId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "CheckTestResponseIdExistsForStudent";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spCheckAssignmentResponseIdExistsForStudent", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llStudentId", SqlDbType.BigInt).Value = StudentId;
                m_command.Parameters.Add("@llSubmissionId", SqlDbType.BigInt).Value = SubmissionId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet GetInstructorProfileDetails(int InstructorId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetInstructorProfileDetails";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetInstructorProfileDetails", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@iInstructorId", SqlDbType.BigInt).Value = InstructorId;
                
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet CheckTestAccess(long TestId,string AccessCode)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "CheckTestAccess";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spCheckTestAccess", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llTestId", SqlDbType.BigInt).Value = TestId;
                m_command.Parameters.Add("@strAccessCode", SqlDbType.VarChar,10).Value = AccessCode;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet CheckAssignmentAccess(long AssignmentId, string AccessCode)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "CheckAssignmentAccess";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spCheckAssignmentAccess", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llAssignmentId", SqlDbType.BigInt).Value = AssignmentId;
                m_command.Parameters.Add("@strAccessCode", SqlDbType.VarChar, 10).Value = AccessCode;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet GetIndependentAssignmentDetails(long AssignmentId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetIndependentAssignmentDetails";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetIndependentAssignmentDetails", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llAssignmentId", SqlDbType.BigInt).Value = AssignmentId;
                
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet GetIndependentTestDetails(long TestId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetIndependentTestDetails";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetIndependetMcqTestDetails", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llTestId", SqlDbType.BigInt).Value = TestId;

                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet GetTestDetailsWithAccessCode(long TestId,string AccessCode)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetTestDetailsWithAccessCode";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetMcqTestDetailsWithVarificationCode", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llTestId", SqlDbType.BigInt).Value = TestId;
                m_command.Parameters.Add("@strAccessCode", SqlDbType.VarChar,10).Value = AccessCode;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet GetAssignmentDetailsWithAC(long AssignmentId, string AccessCode)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetTestDetailsWithAccessCode";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetIndependetMcqTestDetails", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llAssignmentId", SqlDbType.BigInt).Value = AssignmentId;
                m_command.Parameters.Add("@strAccessCode", SqlDbType.VarChar, 10).Value = AccessCode;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet GetWebsiteAboutDetails()
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetWebsiteAboutDetails";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetHomeAboutDetails", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public bool InsertSmsNotification(int NotificationTypeId,string SmsBody,string ReceiverPhoneNo)
        {
            bool result = false;
            string strCurrentMethodName = "MarkAlertReadForInstructor";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spInsertSmsNotification", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@iSmsNotificationId", SqlDbType.Int).Value = NotificationTypeId;
                m_command.Parameters.Add("@strSmsBody", SqlDbType.VarChar, 4000).Value = SmsBody;
                m_command.Parameters.Add("@strReciverPhoneNo", SqlDbType.VarChar, 20).Value = ReceiverPhoneNo;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = true;
                }

            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public DataSet GetAllNotificationToPrecess(int MaxRetryCount)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetAllNotificationToPrecess";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetAllSmsNotificationToProcess", m_con);
                m_command.Parameters.Add("@iMaxRetryCount", SqlDbType.Int).Value = MaxRetryCount;
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public bool UpdateNotificationStatus(bool Status,long NotificationId)
        {
            bool result = false;
            string strCurrentMethodName = "UpdateNotificationStatus";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spUpdatesSmsNotificationStatus", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@bStatus", SqlDbType.Bit).Value = Status;
                m_command.Parameters.Add("@llSmsNotificationId", SqlDbType.BigInt).Value = NotificationId;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = true;
                }

            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool InsertStudentPasswordRecoveryRequest(string UserId,string Token,string OTP)
        {
            bool result = false;
            string strCurrentMethodName = "InsertStudentPasswordRecoveryRequest";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spInsertStudentPasswordRecoveryRequest", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@strStudentUserId", SqlDbType.VarChar,100).Value = UserId;
                m_command.Parameters.Add("@strToken", SqlDbType.VarChar, 100).Value = Token;
                m_command.Parameters.Add("@strOTP", SqlDbType.VarChar, 100).Value = OTP;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = true;
                }

            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool InsertInstructorPasswordRecoveryRequest(string UserId, string Token, string OTP)
        {
            bool result = false;
            string strCurrentMethodName = "InsertInstructorPasswordRecoveryRequest";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spInsertInstructorPasswordRecoveryRequest", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@strStudentUserId", SqlDbType.VarChar, 100).Value = UserId;
                m_command.Parameters.Add("@strToken", SqlDbType.VarChar, 100).Value = Token;
                m_command.Parameters.Add("@strOTP", SqlDbType.VarChar, 100).Value = OTP;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = true;
                }

            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public DataSet ValidateStudentPasswordRecoveryRequest(string UserId, string Token, string OTP)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetWebsiteAboutDetails";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spValidateUpdatePasswordRequest", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@strStudentUserId", SqlDbType.VarChar, 100).Value = UserId;
                m_command.Parameters.Add("@strToken", SqlDbType.VarChar, 100).Value = Token;
                m_command.Parameters.Add("@strOTP", SqlDbType.VarChar, 100).Value = OTP;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet ValidateInstructorPasswordRecoveryRequest(string UserId, string Token, string OTP)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "ValidateInstructorPasswordRecoveryRequest";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spValidateUpdatePasswordRequestForInstructor", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@strStudentUserId", SqlDbType.VarChar, 100).Value = UserId;
                m_command.Parameters.Add("@strToken", SqlDbType.VarChar, 100).Value = Token;
                m_command.Parameters.Add("@strOTP", SqlDbType.VarChar, 100).Value = OTP;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public bool ChanegPasswordAfterAuthentication(string UserId, string Token, string HashedPassword)
        {
            bool result = false;
            string strCurrentMethodName = "ChanegPasswordAfterAuthentication";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spChangeStudentPasswordAfterOtpValidation", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@strUserId", SqlDbType.VarChar, 100).Value = UserId;
                m_command.Parameters.Add("@strToken", SqlDbType.VarChar, 100).Value = Token;
                m_command.Parameters.Add("@strHashedPassword", SqlDbType.VarChar, 100).Value = HashedPassword;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = true;
                }

            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool ChangePasswordAfterAuthenticationForInstructor(string UserId, string Token, string HashedPassword)
        {
            bool result = false;
            string strCurrentMethodName = "ChangePasswordAfterAuthenticationForInstructor";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spChangeInstructorPasswordAfterOtpValidation", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@strUserId", SqlDbType.VarChar, 100).Value = UserId;
                m_command.Parameters.Add("@strToken", SqlDbType.VarChar, 100).Value = Token;
                m_command.Parameters.Add("@strHashedPassword", SqlDbType.VarChar, 100).Value = HashedPassword;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = true;
                }

            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool MarkOtpVarified(string UserId, string Token)
        {
            bool result = false;
            string strCurrentMethodName = "MarkOtpVarified";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spMarrkOtpVarifiedForStudent", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@strStudentUserId", SqlDbType.VarChar, 100).Value = UserId;
                m_command.Parameters.Add("@strToken", SqlDbType.VarChar, 100).Value = Token;
              
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = true;
                }

            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool MarkPassowordVarificationOtpVarifiedForInstructor(string UserId, string Token)
        {
            bool result = false;
            string strCurrentMethodName = "MarkPassowordVarificationOtpVarifiedForInstructor";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spMarrkOtpVarifiedForInstructor", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@strStudentUserId", SqlDbType.VarChar, 100).Value = UserId;
                m_command.Parameters.Add("@strToken", SqlDbType.VarChar, 100).Value = Token;

                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = true;
                }

            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool ValidatePhoneNoVarificationLinkForStudent(string UserId, string guid)
        {
            bool result = false;
            string strCurrentMethodName = "ValidatePhoneNoVarificationLinkForStudent";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spValidateStudentPhoneNoByLink", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@strUserId", SqlDbType.VarChar, 100).Value = UserId;
                m_command.Parameters.Add("@strGuid", SqlDbType.VarChar, 100).Value = guid;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = true;
                }

            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool ValidatePhoneNoVarificationLinkForInstructor(string UserId, string guid)
        {
            bool result = false;
            string strCurrentMethodName = "ValidatePhoneNoVarificationLinkForInstructor";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spValidateInstructorPhoneNoByLink", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@strUserId", SqlDbType.VarChar, 100).Value = UserId;
                m_command.Parameters.Add("@strGuid", SqlDbType.VarChar, 100).Value = guid;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = true;
                }

            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public DataSet CheckCourseIdExsitsForInstructor(int InstructoId, long CourseId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "CheckCourseIdExsitsForInstructor";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spCheckCourseIdExistsForInstructor", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@iInstructorId", SqlDbType.BigInt).Value = InstructoId;
                m_command.Parameters.Add("@llCourseId", SqlDbType.BigInt).Value = CourseId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet CheckIndexIdExsitsForInstructor(int InstructoId, long IndexId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "CheckIndexIdExsitsForInstructor";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spCheckIndexIdExistsForInstructor", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@iInstructorId", SqlDbType.BigInt).Value = InstructoId;
                m_command.Parameters.Add("@llIndexId", SqlDbType.BigInt).Value = IndexId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet CheckCourseAccess(long CourseId, string AccessCode)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "CheckIndexIdExsitsForInstructor";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spCheckCourseAccess", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llCourseId", SqlDbType.BigInt).Value = CourseId;
                m_command.Parameters.Add("@strAccessCode", SqlDbType.VarChar,20).Value = AccessCode;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet CheckAssignmentIdExistsForAnyCourseForInstructor(long AssignmentId,int InstructorId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "CheckAssignmentIdExistsForAnyCourseForInstructor";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spCheckAssignmentIdExistsForAnyCourseForInstructor", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llAssignmentId", SqlDbType.BigInt).Value = AssignmentId;
                m_command.Parameters.Add("@iIstructorId", SqlDbType.Int).Value = InstructorId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet CheckAssignmentIdExistsForInstructor(long AssignmentId, int InstructorId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "CheckAssignmentIdExistsForInstructor";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spCheckAssignmentIdExistsForInstructor", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llAssignmentId", SqlDbType.BigInt).Value = AssignmentId;
                m_command.Parameters.Add("@iIstructorId", SqlDbType.Int).Value = InstructorId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet CheckTestIdExistsForAnyCourseForInstructor(long Testid, int InstructorId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "CheckTestIdExistsForAnyCourseForInstructor";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spCheckTestIdExistsForAnyCourseForInstructor", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llTestId", SqlDbType.BigInt).Value = Testid;
                m_command.Parameters.Add("@iIstructorId", SqlDbType.Int).Value = InstructorId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet CheckTestIdExistsForInstructor(long Testid, int InstructorId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "CheckTestIdExistsForInstructor";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spCheckTestIdExistsForInstructor", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llTestId", SqlDbType.BigInt).Value = Testid;
                m_command.Parameters.Add("@iIstructorId", SqlDbType.Int).Value = InstructorId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet GetAllQuestionAskForCourseByStudent(long Studentid, long CourseId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetAllQuestionAskForCourseByStudent";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetAllCourseQuestionOfStudent", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llStudentId", SqlDbType.BigInt).Value = Studentid;
                m_command.Parameters.Add("@llCourseId", SqlDbType.BigInt).Value = CourseId;
                
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public bool InsertCompletedTopicforStudent(long TopicId,long StudentId)
        {
            bool result = false;
            string strCurrentMethodName = "InsertCompletedTopicforStudent";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spInsertCompletedTopicForStudent", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llStudentId", SqlDbType.BigInt).Value = StudentId;
                m_command.Parameters.Add("@llTopicId", SqlDbType.BigInt).Value = TopicId;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = true;
                }

            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool InsertCourseQuestionByStudent(long CourseId, long StudentId,string Question)
        {
            bool result = false;
            string strCurrentMethodName = "InsertCourseQuestionByStudent";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spInsrCourseQuestionByStudent", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llStudentId", SqlDbType.BigInt).Value = StudentId;
                m_command.Parameters.Add("@llCourseId", SqlDbType.BigInt).Value = CourseId;
                m_command.Parameters.Add("@strQuestiOn", SqlDbType.VarChar,8000).Value = Question;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = true;
                }

            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool InsertAnswerForCourseQuestion(long QuestionId, int InstructorId, string Answer)
        {
            bool result = false;
            string strCurrentMethodName = "InsertAnswerForCourseQuestion";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spInsrCourseQuestionByStudent", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llQuestionId", SqlDbType.BigInt).Value = QuestionId;
                m_command.Parameters.Add("@iInstructorId", SqlDbType.Int).Value = InstructorId;
                m_command.Parameters.Add("@strAnswer", SqlDbType.VarChar, 8000).Value = Answer;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = true;
                }

            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public long InsertNewClassRoomForInstructor(int InstrcuctorId,string ClassRoomName,string ClassRoomDescription,string BackGroundImageUrl,
            string ClassroomMeetingName,string ThumbnailSmall,string ThumbnailMedium,int NoOfDemoClassrooms)
        {
            long result = -1;
            string strCurrentMethodName = "InsertNewClassRoomForInstructor";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spInsertClassRoom", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@iInstructorId", SqlDbType.Int).Value = InstrcuctorId;
                m_command.Parameters.Add("@llClassRoomId", SqlDbType.BigInt).Direction = ParameterDirection.Output;
                m_command.Parameters.Add("@strClassRoomName", SqlDbType.NVarChar,250).Value = ClassRoomName;
                if(BackGroundImageUrl!=null)
                {
                    m_command.Parameters.Add("@strBackGroundImagePath", SqlDbType.VarChar, 250).Value = BackGroundImageUrl;
                }
                else
                {
                    m_command.Parameters.Add("@strBackGroundImagePath", SqlDbType.VarChar, 250).Value = DBNull.Value;
                }
                if (ThumbnailSmall != null)
                {
                    m_command.Parameters.Add("@strSmallIconPath", SqlDbType.VarChar, 250).Value = ThumbnailSmall;
                }
                else
                {
                    m_command.Parameters.Add("@strSmallIconPath", SqlDbType.VarChar, 250).Value = DBNull.Value;
                }
                if (ThumbnailSmall != null)
                {
                    m_command.Parameters.Add("@strMediumIconPath", SqlDbType.VarChar, 250).Value = ThumbnailMedium;
                }
                else
                {
                    m_command.Parameters.Add("@strMediumIconPath", SqlDbType.VarChar, 250).Value = DBNull.Value;
                }
                m_command.Parameters.Add("@strClassroomMeetingName", SqlDbType.VarChar, 200).Value = ClassroomMeetingName;
                m_command.Parameters.Add("@strClassRoomDescription", SqlDbType.NVarChar, 1000).Value = ClassRoomDescription;
                m_command.Parameters.Add("@iNoOfDemoClassrooms", SqlDbType.Int).Value = NoOfDemoClassrooms;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = (long)m_command.Parameters["@llClassRoomId"].Value;
                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public DataSet GetAllClassRoomForInstrcutor(int InstrcutorId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetAllClassRoomForInstrcutor";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetAllClassRoomForInstructor", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@iInstructorId", SqlDbType.Int).Value = InstrcutorId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet GetClassRoomDetailsForInstructor(int InstrcutorId,long ClassRoomId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetClassRoomDetailsForInstructor";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetClassRoomDetailsForInstructor", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@iInstructorId", SqlDbType.Int).Value = InstrcutorId;
                m_command.Parameters.Add("@llClassRoomId", SqlDbType.BigInt).Value = ClassRoomId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public bool InertNewPostToClassroom(long ClassroomId,string Post)
        {
            bool result = false;
            string strCurrentMethodName = "InertNewPostToClassroom";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spInsertNewPostToClassroom", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@strPost", SqlDbType.NVarChar,4000).Value = Post;
                m_command.Parameters.Add("@llClassroomId", SqlDbType.BigInt).Value = ClassroomId;
                m_con.Open();
                result = m_command.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool InertNewMeetingToClassroom(long ClassroomId, string MeetingName,string MeetingPassword)
        {
            bool result = false;
            string strCurrentMethodName = "InertNewMeetingToClassroom";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spInsertNewMeetingToClassroom", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llClassroomId", SqlDbType.BigInt).Value = ClassroomId;
                m_con.Open();
                result=m_command.ExecuteNonQuery() > 0 ;

            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public DataSet GetMeetingDetailsOfClassroom(long ClassRoomId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetMeetingDetailsOfClassroom";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_GetMeetingDetailsOfClassroom", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llClassRoomId", SqlDbType.BigInt).Value = ClassRoomId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public bool ActivateClassroom(long ClassroomId,string ShareCode,string ShareUrl,
            int ClassroomPublicType,int ClassroomJoiningAmountInPaise, string StartTime,string ArrayOpeningDays,int NofDemoClasses,
            int Category,int Level)
        {
            bool result = false;
            string strCurrentMethodName = "ActivateClassroom";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spActivateClassroom", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llClassroomId", SqlDbType.BigInt).Value = ClassroomId;
                m_command.Parameters.Add("@strShareUrl", SqlDbType.VarChar, 100).Value = ShareUrl;
                m_command.Parameters.Add("@strShareCode", SqlDbType.VarChar, 10).Value = ShareCode;
                m_command.Parameters.Add("@iClassroomPublicType", SqlDbType.Int).Value = ClassroomPublicType;
                m_command.Parameters.Add("@iClassroomChangeInPaise", SqlDbType.Int).Value = ClassroomJoiningAmountInPaise;
                m_command.Parameters.Add("@strClassroomWeekDaysSchedule", SqlDbType.VarChar, 10).Value = ArrayOpeningDays;
                m_command.Parameters.Add("@strClassroomStartTime", SqlDbType.VarChar, 10).Value = StartTime;
                m_command.Parameters.Add("@iNoOfDemoClasses", SqlDbType.Int).Value = NofDemoClasses;
                m_command.Parameters.Add("@iClassroomCategoryId", SqlDbType.Int).Value = Category;
                m_command.Parameters.Add("@iClassroomLevelId", SqlDbType.Int).Value = Level;
                m_con.Open();
                result = m_command.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public DataSet GetClasroomDetails( long ClassRoomId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetClasroomDetails";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetClassRoomDetails", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
               
                m_command.Parameters.Add("@llClassRoomId", SqlDbType.BigInt).Value = ClassRoomId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public bool JoinStudentToClassroom(long ClassroomId,long StudentId)
        {
            bool result = false;
            string strCurrentMethodName = "JoinStudentToClassroom";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spInsertNewStudentToClassroom", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llClassroomId", SqlDbType.BigInt).Value = ClassroomId;
                m_command.Parameters.Add("@llStudentId", SqlDbType.BigInt).Value = StudentId;
                m_con.Open();
                result = m_command.ExecuteNonQuery() > 0;

            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public DataSet GetJoinedClassroomForStudent(long StudentId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetJoinedClassroomForStudent";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetAllClassroomsJoinedForStudent", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;

                m_command.Parameters.Add("@llStudentId", SqlDbType.BigInt).Value = StudentId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public bool JoinStudentToMeeting(long MeetingId, long StudentId)
        {
            bool result = false;
            string strCurrentMethodName = "JoinStudentToMeeting";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spInsertNewStudentToMeeting", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llMeetingId", SqlDbType.BigInt).Value = MeetingId;
                m_command.Parameters.Add("@llStudentId", SqlDbType.BigInt).Value = StudentId;
                m_con.Open();
                result = m_command.ExecuteNonQuery() > 0;

            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool ReportMeetingLeftForStudent(long MeetingId, long StudentId)
        {
            bool result = false;
            string strCurrentMethodName = "JoinStudentToMeeting";
            try
            {
                InitDB();
                m_command = new SqlCommand("ReportMeetingLeftForStudent", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llMeetingId", SqlDbType.BigInt).Value = MeetingId;
                m_command.Parameters.Add("@llStudentId", SqlDbType.BigInt).Value = StudentId;
                m_con.Open();
                result = m_command.ExecuteNonQuery() > 0;

            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool ReportMeetingLeftForHost(long MeetingId)
        {
            bool result = false;
            string strCurrentMethodName = "JoinStudentToMeeting";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_ReportMeetingInActive", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llMeetingId", SqlDbType.BigInt).Value = MeetingId;
               
                m_con.Open();
                result = m_command.ExecuteNonQuery() > 0;

            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public DataSet GetAllMeetingForClassroom(long ClassroomId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetAllMeetingForClassroom";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetAllMeetingsForClassroom", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;

                m_command.Parameters.Add("@llClassroomId", SqlDbType.BigInt).Value = ClassroomId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet GetAllStudentsJoinedToClassroom(long ClassroomId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetAllStudentsJoinedToClassroom";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetAllStudentJoinedToClassroom", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llClassroomId", SqlDbType.BigInt).Value = ClassroomId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet CheckClassroomAccess(long ClassroomId,string AccessCode)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "CheckClassroomAccess";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spValidateClassroomAccess", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llClassroomId", SqlDbType.BigInt).Value = ClassroomId;
                m_command.Parameters.Add("@strAccessCode", SqlDbType.VarChar,20).Value = AccessCode;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet CheckInstructorClassroomAccess(long ClassroomId, int InstructorId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "CheckInstructorClassroomAccess";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spValidateInstructorClassroomAccess", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llClassroomId", SqlDbType.BigInt).Value = ClassroomId;
                m_command.Parameters.Add("@iInstructorId", SqlDbType.Int).Value = InstructorId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet CheckStudentClassroomAccess(long ClassroomId,long StudentId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "CheckStudentClassroomAccess";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spValidateStudentClassroomAccess", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llClassroomId", SqlDbType.BigInt).Value = ClassroomId;
                m_command.Parameters.Add("@llStudentId", SqlDbType.Int).Value = StudentId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet GetAllStudentsJoinedToMeeting(long ClassroomId, long MeetingId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetAllStudentsJoinedToMeeting";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetAllJoinedToClassroomMeeting", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llClassroomId", SqlDbType.BigInt).Value = ClassroomId;
                m_command.Parameters.Add("@llMeetingId", SqlDbType.Int).Value = MeetingId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public bool InsertNewInstructorClassroomMessage(long ClassroomId,string Message)
        {
            bool result = false;
            string strCurrentMethodName = "InsertNewInstructorClassroomMessage";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spInsertInstructorClassroomMessage", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llClassroomId", SqlDbType.BigInt).Value = ClassroomId;
                m_command.Parameters.Add("@strMessage", SqlDbType.NVarChar,4000).Value = Message;
                m_con.Open();
                result = m_command.ExecuteNonQuery() > 0;

            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool InsertNewStudentClassroomMessage(long ClassroomId, string Message,long StudentId)
        {
            bool result = false;
            string strCurrentMethodName = "InsertNewStudentClassroomMessage";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spInsertStudentClassroomMessage", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llClassroomId", SqlDbType.BigInt).Value = ClassroomId;
                m_command.Parameters.Add("@strMessage", SqlDbType.NVarChar, 4000).Value = Message;
                m_command.Parameters.Add("@llStudentId", SqlDbType.BigInt).Value = StudentId;   
                m_con.Open();
                result = m_command.ExecuteNonQuery() > 0;

            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public DataSet GetAllClassroomMessage(long ClassroomId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetAllClassroomMessage";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetAllClassroomMessage", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llClassroomId", SqlDbType.BigInt).Value = ClassroomId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet GetAllClassroomMessageAfterLastMessage(long ClassroomId,long LastMessageId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetAllClassroomMessageAfterLastMessage";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetAllClassroomMessageAfterLastMessage", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llClassroomId", SqlDbType.BigInt).Value = ClassroomId;
                m_command.Parameters.Add("@llLastMessageId", SqlDbType.BigInt).Value = LastMessageId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public bool UpdateClassroomDetails(long ClassroomId, string ClassroomName, string ClassroomDescription,DateTime? classroomStartDate,
            DateTime? classroomRegistrationCloseDate,int NoOfDemoSessions)
        {
            bool result = false;
            string strCurrentMethodName = "UpdateClassroomDetails";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spUpdateClassroomDetails", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llClassroomId", SqlDbType.BigInt).Value = ClassroomId;
                m_command.Parameters.Add("@strClassroomName", SqlDbType.NVarChar, 500).Value = ClassroomName;
                m_command.Parameters.Add("@strCalssroomNameDescription", SqlDbType.NVarChar,2000).Value = ClassroomDescription;
                if(classroomStartDate==null)
                {
                    m_command.Parameters.Add("@dtClassroomRegistrationCloseDate", SqlDbType.DateTime).Value =DBNull.Value;
                }
                else
                {
                    m_command.Parameters.Add("@dtClassroomRegistrationCloseDate", SqlDbType.DateTime).Value = classroomRegistrationCloseDate;
                }
                if(classroomStartDate == null)
                {
                    m_command.Parameters.Add("@dtClassroomStartDate", SqlDbType.DateTime).Value = DBNull.Value;
                }
                else
                {
                    m_command.Parameters.Add("@dtClassroomStartDate", SqlDbType.DateTime).Value = classroomStartDate;
                }
                m_command.Parameters.Add("@iNoOfLiveClasses", SqlDbType.Int).Value = NoOfDemoSessions;
                m_con.Open();
                result = m_command.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool DeleteClassroom(long ClassroomId)
        {
            bool result = false;
            string strCurrentMethodName = "DeleteClassroom";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spDeleteClassroom", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llClassroomId", SqlDbType.BigInt).Value = ClassroomId;
                m_con.Open();
                result = m_command.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool AddNewAssignmentToClassroom(long ClassroomId,long AssignmentId)
        {
            bool result = false;
            string strCurrentMethodName = "AddNewAssignmentToClassroom";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spInsertNewAssignmentToClassroom", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llClassroomId", SqlDbType.BigInt).Value = ClassroomId;
                m_command.Parameters.Add("@llAssignmentId", SqlDbType.BigInt).Value = AssignmentId;
                m_con.Open();
                result = m_command.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public DataSet GetAllClassroomAssignments(long ClassroomId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetAllClassroomAssignments";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetAllClassrooAssignments", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llClassroomId", SqlDbType.BigInt).Value = ClassroomId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public bool DeleteClassroomAssignment(long ClassroomId, long AssignmentId)
        {
            bool result = false;
            string strCurrentMethodName = "DeleteClassroomAssignment";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spDeleteClassroomAssignment", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llClassroomId", SqlDbType.BigInt).Value = ClassroomId;
                m_command.Parameters.Add("@llAssignmentId", SqlDbType.BigInt).Value = AssignmentId;
                m_con.Open();
                result = m_command.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool DeleteClassroomTest(long ClassroomId, long TestId)
        {
            bool result = false;
            string strCurrentMethodName = "DeleteClassroomTest";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spDeleteClassroomTest", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llClassroomId", SqlDbType.BigInt).Value = ClassroomId;
                m_command.Parameters.Add("@llTestId", SqlDbType.BigInt).Value = TestId;
                m_con.Open();
                result = m_command.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool AddNewTestToClassroom(long ClassroomId, long TestId)
        {
            bool result = false;
            string strCurrentMethodName = "AddNewTestToClassroom";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spInsertNewClassroomTest", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llClassroomId", SqlDbType.BigInt).Value = ClassroomId;
                m_command.Parameters.Add("@llTestId", SqlDbType.BigInt).Value = TestId;
                m_con.Open();
                result = m_command.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public DataSet GetAllClassroomTest(long ClassroomId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetAllClassroomTest";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetAllClassroomTest", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llClassroomId", SqlDbType.BigInt).Value = ClassroomId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet GetAllClassroomMeetingForStudent(long ClassroomId,long StudentId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetAllClassroomMeetingForStudent";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetAllClassroomMeetingsForStudent", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llClassroomId", SqlDbType.BigInt).Value = ClassroomId;
                m_command.Parameters.Add("@llStudentId", SqlDbType.BigInt).Value = StudentId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet GetAllClassroomAssignmntForStudent(long ClassroomId, long StudentId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetAllClassroomAssignmntForStudent";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetAllCalssroomAssignmentForStudent", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llClassroomId", SqlDbType.BigInt).Value = ClassroomId;
                m_command.Parameters.Add("@llStudentId", SqlDbType.BigInt).Value = StudentId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet GetAllClassroomAssignmentSubmissionsForStudent(long ClassroomId, long StudentId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetAllClassroomAssignmntForStudent";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetAllClassroomAssignmentSubmissions", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llClassroomId", SqlDbType.BigInt).Value = ClassroomId;
                m_command.Parameters.Add("@llStudentId", SqlDbType.BigInt).Value = StudentId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet GetAllClassroomTestSubmissionsForStudent(long ClassroomId, long StudentId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "Cp_spGetAllClassroomTestSubmissionsForStudent";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetAllClassroomTestSubmissionsForStudent", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llClassroomId", SqlDbType.BigInt).Value = ClassroomId;
                m_command.Parameters.Add("@llStudentId", SqlDbType.BigInt).Value = StudentId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet GetAllClassroomTestForStudent(long ClassroomId, long StudentId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetAllClassroomTestForStudent";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetAllClassroomTestForStudent", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llClassroomId", SqlDbType.BigInt).Value = ClassroomId;
                m_command.Parameters.Add("@llStudentId", SqlDbType.BigInt).Value = StudentId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet GetClassroomHomeDetailsForStudent(long ClassroomId, long StudentId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetAllClassroomTestForStudent";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetClassroomHomeDetailsForStudent", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llClassroomId", SqlDbType.BigInt).Value = ClassroomId;
                m_command.Parameters.Add("@llStudentId", SqlDbType.BigInt).Value = StudentId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet GetInstructorClassroomSearchDetails(int InstructorId,string SearchString)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetInstructorClassroomSearchDetails";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetInstructorClassroomSearchResult", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@strSearchString", SqlDbType.VarChar,250).Value = SearchString;
                    m_command.Parameters.Add("@iInstructorId", SqlDbType.Int).Value = InstructorId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public bool UpdateInstructorProfilePicture(int InstructorId, string Url,string MediumSizeUrl,
            string SmallSizeUrl)
        {
            bool result = false;
            string strCurrentMethodName = "UpdateInstructorProfilePicture";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spUpadteInstructorProfilePicture", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@strPrfileUrl", SqlDbType.VarChar,250).Value = Url;
                m_command.Parameters.Add("@iInstructorId", SqlDbType.Int).Value = InstructorId;
                m_command.Parameters.Add("@strMediumSizeProfileUrl", SqlDbType.VarChar, 250).Value = MediumSizeUrl;
                m_command.Parameters.Add("@strSmallSizeProfileUrl", SqlDbType.VarChar, 250).Value = SmallSizeUrl;
                m_con.Open();
                result = m_command.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool UpdateStudentProfilePicture(long StudentId, string OriginalFile,string SmallThumbnailUrl,
            string MediumThumbnailUrl)
        {
            bool result = false;
            string strCurrentMethodName = "UpdateInstructorProfilePicture";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spUpdateStudentProfilePicture", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@strProfilePictureUrl", SqlDbType.VarChar, 250).Value = OriginalFile;
                m_command.Parameters.Add("@llStudentId", SqlDbType.BigInt).Value = StudentId;
                m_command.Parameters.Add("@strProfilePictureSmallUrl", SqlDbType.VarChar, 250).Value = SmallThumbnailUrl;
                m_command.Parameters.Add("@strPrfoilePictureMediumUrl", SqlDbType.VarChar, 250).Value = MediumThumbnailUrl;
                m_con.Open();
                result = m_command.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public DataSet GetClassroomMeetingDetails( long ClassroomId, long MeetingId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetClassroomMeetingDetails";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetClasssroomMeetingDetails", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llClassroomId", SqlDbType.BigInt).Value = ClassroomId;
                m_command.Parameters.Add("@llMeetingId", SqlDbType.BigInt).Value = MeetingId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public bool InsertClassroomAttachment(long ClassroomId, string AttachmentName,string AttachmentDescription,string AttachmentUrl)
        {
            bool result = false;
            string strCurrentMethodName = "InsertClassroomAttachment";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spInsertClassroomAttachment", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llClassroomId", SqlDbType.BigInt).Value = ClassroomId;
                m_command.Parameters.Add("@strAttachmentName", SqlDbType.VarChar, 250).Value = AttachmentName;
                m_command.Parameters.Add("@strAttachmentDescription", SqlDbType.VarChar, 250).Value = AttachmentDescription;
                m_command.Parameters.Add("@strAttachmentUrl", SqlDbType.VarChar,250).Value = AttachmentUrl;
                m_con.Open();
                result = m_command.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool UpdateClassroomAttachmentDetails(long AttachmentId, string AttachmentName, string AttachmentDescription, string AttachmentUrl)
        {
            bool result = false;
            string strCurrentMethodName = "UpdateClassroomAttachmentDetails";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spUpdateClassroomAttachmentDetails", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llAttachmentId", SqlDbType.BigInt).Value = AttachmentId;
                m_command.Parameters.Add("@strAttachmentName", SqlDbType.VarChar, 250).Value = AttachmentName;
                m_command.Parameters.Add("@strAttachmentDescription", SqlDbType.VarChar, 250).Value = AttachmentDescription;
                m_command.Parameters.Add("@strAttachmentUrl", SqlDbType.VarChar, 250).Value = AttachmentUrl;
                m_con.Open();
                result = m_command.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool DeleteClassroomAttachment(long AttachmentId)
        {
            bool result = false;
            string strCurrentMethodName = "DeleteClassroomAttachment";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spDeleteClassroomAttachment", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llAttachmentId", SqlDbType.BigInt).Value = AttachmentId;
              
                m_con.Open();
                result = m_command.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public DataSet GetClassroomAttachments(long ClassroomId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetClassroomAttachments";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetAllClassroomAttachments", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llClassroomId", SqlDbType.BigInt).Value = ClassroomId;
              
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet GetInstructorProfileDetailsForStudent(int InstructorId,long StudentId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetInstructorProfileDetailsForStudent";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetInstructorProfileDetailsForStudent", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@iInstructorId", SqlDbType.Int).Value = InstructorId;
                m_command.Parameters.Add("@llStudentId", SqlDbType.BigInt).Value = StudentId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public bool InsertClassroomSchedule(long ClassroomId,string ClassroomSchedule)
        {
            bool result = false;
            string strCurrentMethodName = "InsertClassroomSchedule";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spInsertClassroomSchedule", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llClassroomId", SqlDbType.BigInt).Value = ClassroomId;
                m_command.Parameters.Add("@strClassroomScheduleObj", SqlDbType.VarChar,4000).Value = ClassroomSchedule;
                m_con.Open();
                result = m_command.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool UpdateClassroomSchedule(long ClassroomId, string ClassroomSchedule)
        {
            bool result = false;
            string strCurrentMethodName = "UpdateClassroomSchedule";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_sUpdateClassroomSchedule", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llClassroomId", SqlDbType.BigInt).Value = ClassroomId;
                m_command.Parameters.Add("@strClassroomScheduleObj", SqlDbType.VarChar,4000).Value = ClassroomSchedule;
                m_con.Open();
                result = m_command.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public DataSet GetClassroomSchedule(long ClassroomId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetClassroomSchedule";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetClassroomScheduleDetails", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llClassroomId", SqlDbType.Int).Value = ClassroomId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet GetPublicClassroomDetailsForStudent(int NoOfRowsFetched,long StudentId,
            int NoOfRecordsToBeFetched,string QueryString)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetPublicClassroomDetailsForStudent";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetAllPublicClassroom", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@iNoOfRowsFetched", SqlDbType.BigInt).Value = NoOfRowsFetched;
                m_command.Parameters.Add("@llStudentId", SqlDbType.BigInt).Value = StudentId;
                m_command.Parameters.Add("@iNoOfRecordsToBeFetched", SqlDbType.Int).Value = NoOfRecordsToBeFetched;
                m_command.Parameters.Add("@strSearchQuesry", SqlDbType.NVarChar,100).Value = QueryString;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet SearchForCourseForNotLoggedUser(string SerachString, int MaxRowToReturn, int NoOfRowsFetch, int SortType, long StudentId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "SearchForCourseForStudent";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetSerachResultForCourseForNotLoggedInUsers", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@strSerarchString", SqlDbType.VarChar, 100).Value = SerachString;
                m_command.Parameters.Add("@llMaxRowToReturn", SqlDbType.Int).Value = MaxRowToReturn;
                m_command.Parameters.Add("@iNoOfRowsFetched", SqlDbType.Int).Value = NoOfRowsFetch;
                m_command.Parameters.Add("@iSortType", SqlDbType.Int).Value = SortType;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet GetInstructorAcademicRecords(int InstructorId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetInstructorAcademicRecords";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetInstructorAcademicRecords", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@iInsructorId", SqlDbType.Int).Value = InstructorId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public bool UpdateInstructorAcademicRecord(string LinkedInId,string GoogleScholarId,int InstructorId,string SchoolDetails)
        {
            bool result = false;
            string strCurrentMethodName = "UpdateInstructorAcademicRecord";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spUpdateInstructorAcademicsRecords", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@iInsructorId", SqlDbType.Int).Value = InstructorId;
                m_command.Parameters.Add("@strLinkedIn", SqlDbType.VarChar, 100).Value = LinkedInId;
                m_command.Parameters.Add("@strGoogleScholarId", SqlDbType.VarChar, 100).Value = GoogleScholarId;
                m_command.Parameters.Add("@strSchoolDetails", SqlDbType.VarChar).Value = SchoolDetails;
                m_con.Open();
                result = m_command.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool UpdateInstructorBio(int InstructorId,string InstructoBioData)
        {
            bool result = false;
            string strCurrentMethodName = "UpdateInstructorBio";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spUpdateInstructorBio", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@iInsructorId", SqlDbType.Int).Value = InstructorId;
                m_command.Parameters.Add("@strBio", SqlDbType.NVarChar, 1000).Value = InstructoBioData;
                m_con.Open();
                result = m_command.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public DataSet GetClassroomPaymentDetails(long ClassroomId,long StudentId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetClassroomPaymentDetails";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetClassroomPaymentDetais", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llStudentId", SqlDbType.Int).Value = StudentId;
                m_command.Parameters.Add("@llClassroomId", SqlDbType.Int).Value = ClassroomId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public bool CreatePaymentOrder(string OrederId, string CustomerName,string CustomerEmail,string CustomerPhoneNo,
            int AmountInPaise,string CustomerAddress)
        {
            bool result = false;
            string strCurrentMethodName = "CreatePaymentOrder";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spCreateOrderRequest", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@strOrderID", SqlDbType.VarChar,250).Value = OrederId;
                m_command.Parameters.Add("@strCustomerName", SqlDbType.VarChar, 250).Value = CustomerName;
                m_command.Parameters.Add("@strCustomerEmail", SqlDbType.VarChar, 250).Value = CustomerEmail;
                m_command.Parameters.Add("@strCustomerPhone", SqlDbType.VarChar, 250).Value = CustomerPhoneNo;
                m_command.Parameters.Add("@iAmountInPaise", SqlDbType.Int).Value = AmountInPaise;
                if (CustomerAddress != null)
                {
                    m_command.Parameters.Add("@strCustomerAddress", SqlDbType.VarChar, 4000).Value = CustomerAddress;
                }
                else
                {
                    m_command.Parameters.Add("@strCustomerAddress", SqlDbType.VarChar, 4000).Value = DBNull.Value;
                }
                m_con.Open();
                result = m_command.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool InsertRazorPayTxnDetails(string OrederId, string RazorPayPaymentId, string RazorPayOderId, string RazorPaySignature)
        {
            bool result = false;
            string strCurrentMethodName = "CreatePaymentOrder";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spRazorPayTxnDetails", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@strOrderID", SqlDbType.VarChar, 100).Value = OrederId;
                m_command.Parameters.Add("@strRazorPayPaymentId", SqlDbType.VarChar, 250).Value = RazorPayPaymentId;
                m_command.Parameters.Add("@strRazorPayOrderId", SqlDbType.VarChar, 250).Value = RazorPayOderId;
                m_command.Parameters.Add("@strRazorPaySignature", SqlDbType.VarChar, 250).Value = RazorPaySignature;
                m_con.Open();
                result = m_command.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool GetInstructorIdByClassroomId(long ClassroomId, ref int InstructorId)
        {
            bool result = false;
            string strCurrentMethodName = "GetInstructorIdByCourseId";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetInstructorIdByClassroomId", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llClassroomId", SqlDbType.BigInt).Value = ClassroomId;
                m_command.Parameters.Add("@iInstructorId", SqlDbType.Int).Direction = ParameterDirection.Output;
                m_con.Open();
                m_command.ExecuteNonQuery();
                InstructorId = (int)m_command.Parameters["@iInstructorId"].Value;
                result = true;
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool GetCoursePrice(long CourseId, ref int CoursePrice)
        {
            bool result = false;
            string strCurrentMethodName = "GetCoursePrice";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetCoursePaymentDetails", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llCourseId", SqlDbType.BigInt).Value = CourseId;
                m_command.Parameters.Add("@iCoursePrice", SqlDbType.Int).Direction = ParameterDirection.Output;
                m_con.Open();
                m_command.ExecuteNonQuery();
                CoursePrice = (int)m_command.Parameters["@iCoursePrice"].Value;
                result = true;
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public DataSet GetCoursePaymentDetails(long CourseId, long StudentId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetCoursePaymentDetails";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetCoursePaymentDetais", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llStudentId", SqlDbType.BigInt).Value = StudentId;
                m_command.Parameters.Add("@llCourseId", SqlDbType.BigInt).Value = CourseId;

                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet GetInstructorEarnings(int InstructorId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetInstructorEarnings";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetInstructorTotalEarning", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@iInstructorId", SqlDbType.BigInt).Value = InstructorId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet GetMonthwiseInstructorClassroomEarning(int InstructorId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetMonthwisInstructorClassroomEarning";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetInstructorClassroomEarningYearWise", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@iInstructorId", SqlDbType.BigInt).Value = InstructorId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet GetMonthwiseInstructorCourseEarning(int InstructorId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetMonthwiseInstructorCourseEarning";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetInstructorCourseEarningYearWise", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@iInstructorId", SqlDbType.BigInt).Value = InstructorId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public bool InsertNewTestSeries(int InstructorId,string TestSeriesName,string TestSeriesDescription ,string TestSeriesImage,
            ref long TestSeriesId)
        {
            bool result = false;
            string strCurrentMethodName = "InsertNewTestSeries";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spInsertNewTestSeries", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@iInstructorId", SqlDbType.Int).Value = InstructorId;
                m_command.Parameters.Add("@strTestSeriesName", SqlDbType.NVarChar,250).Value = TestSeriesName;
                m_command.Parameters.Add("@strTestSeriesDescription", SqlDbType.NVarChar,4000).Value = TestSeriesDescription;
                m_command.Parameters.Add("@strTestSeriesImage", SqlDbType.VarChar,250).Value = TestSeriesImage;
                m_command.Parameters.Add("@llTestSeriesId", SqlDbType.BigInt).Direction = ParameterDirection.Output;
                m_con.Open();
                m_command.ExecuteNonQuery();
                TestSeriesId = (long)m_command.Parameters["@llTestSeriesId"].Value;
                result = true;
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool ActivateTestSeries(long TestSeriesId,int InstructorId)
        {
            bool result = false;
            string strCurrentMethodName = "ActivateTestSeries";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spActivateTestSeries", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llTestSeriesId", SqlDbType.BigInt).Value = TestSeriesId;
                m_command.Parameters.Add("@iInstructorId", SqlDbType.Int).Value = InstructorId;              
                m_con.Open();
                result = m_command.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool InsertNewTestToTestSeries(long TestSeriesId, long TestId)
        {
            bool result = false;
            string strCurrentMethodName = "InsertNewTestToTestSeries";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spInsertNetTestToTestSeries", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llTestSeriesId", SqlDbType.BigInt).Value = TestSeriesId;
                m_command.Parameters.Add("@llTestId", SqlDbType.Int).Value = TestId;
                m_con.Open();
                result = m_command.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool DeletTestFromTestSeries(long TestSeriesContentId)
        {
            bool result = false;
            string strCurrentMethodName = "DeletTestFromTestSeries";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spDeletTestFromTestSeries", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llTestSeriesContentId", SqlDbType.BigInt).Value = TestSeriesContentId;
                m_con.Open();
                result = m_command.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public DataSet GetTestSearchResultForStudent(long StudentId,string SearchString,int MaxRowToFetch,long LastFetchedId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetTestSearchResultForStudent";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spSearchTestForStudent", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llStudentId", SqlDbType.BigInt).Value = StudentId;
                m_command.Parameters.Add("@strSearchString", SqlDbType.VarChar,100).Value = SearchString;
                m_command.Parameters.Add("@iMaxRowsToFetch", SqlDbType.Int).Value = MaxRowToFetch;
                m_command.Parameters.Add("@llLastTestIdFetched", SqlDbType.BigInt).Value = LastFetchedId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public bool AddNewTestAnonymousTestSubmission(long TestId,string TestAccessCode)
        {
            bool result = false;
            string strCurrentMethodName = "AddNewTestAnonymousTestSubmission";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spInsertNewTestSubmissionForUnLoggedInUser", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llTestId", SqlDbType.BigInt).Value = TestId;
                m_command.Parameters.Add("@strTestAccessCode", SqlDbType.VarChar,100).Value = TestAccessCode;
                m_con.Open();
                result = m_command.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool UpdateClassroomSyllabus(long ClassroomId,string ClassroomSyallabus)
        {
            bool result = false;
            string strCurrentMethodName = "UpdateClassroomSyallabus";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spUpdateClassroomSyallabus", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@strClassroomSchedule", SqlDbType.VarChar,8000).Value = ClassroomSyallabus;
                m_command.Parameters.Add("@llClassroomId", SqlDbType.BigInt).Value = ClassroomId;
                m_con.Open();
                result = m_command.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public DataSet GetClassroomsForHomePage()
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetClassroomsForHomePage";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetClassroomsForHomePage", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet GetClassRoomDetailsForStudent(long ClassRoomId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetClassRoomDetailsForStudent";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetClassRoomDetailsForStudent", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llClassRoomId", SqlDbType.BigInt).Value = ClassRoomId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {

                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public bool InsertEmailSubscriber(string EmailId)
        {
            bool result = false;
            string strCurrentMethodName = "InsertEmailSubscriber";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spInsertNewEmailSubscriber", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@strEmailAddress", SqlDbType.VarChar, 250).Value = EmailId;         
                m_con.Open();
                result = m_command.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool InsertStudentAlert(int AlertTypeId,long StudentId,string TargetUrl)
        {
            bool result = false;
            string strCurrentMethodName = "InsertStudentAlert";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spInsertStudentAlert", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@iAlertTypeId", SqlDbType.Int).Value = AlertTypeId;
                m_command.Parameters.Add("@llStudentId", SqlDbType.BigInt).Value = StudentId;
                m_command.Parameters.Add("@strTargetUrl", SqlDbType.VarChar, 250).Value = TargetUrl;
                m_con.Open();
                result = m_command.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public DataSet GetStudentRecentLiveClassJoin(long StudentId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetStudentRecentLiveClassJoin";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetStudentRecentLiveClasses", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llStudentId", SqlDbType.BigInt).Value = StudentId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {

                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet GetSearchResultForStudent(string SearchString, int MaxRowToReturn, int NoOfRowsFetch, long StudentId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetSearchResultForStudent";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetSearchResultForInstructor", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@strSearchString", SqlDbType.VarChar, 100).Value = SearchString;
                m_command.Parameters.Add("@iNoOfRowsfetched", SqlDbType.Int).Value = NoOfRowsFetch;
                m_command.Parameters.Add("@iMaxRowToReturn", SqlDbType.Int).Value = MaxRowToReturn;
                m_command.Parameters.Add("@llStudentId", SqlDbType.BigInt).Value = StudentId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet GetStudentRecentCourseJoin(long StudentId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetStudentRecentCourseJoin";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetStudentRecentCourseJoin", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llStudentId", SqlDbType.BigInt).Value = StudentId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {

                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public bool UpdateClassroomBackground(long ClassroomId,string OriginalImagePath, string SmallIcon,string MediumIcon)
        {
            bool result = false;
            string strCurrentMethodName = "UpdateClassroomBackground";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spUpdateClassroomBackground", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llClassroomId", SqlDbType.BigInt).Value = ClassroomId;
                m_command.Parameters.Add("@strOriginalImagePath", SqlDbType.VarChar,250).Value = OriginalImagePath;
                m_command.Parameters.Add("@strSmallThumbnailPath", SqlDbType.VarChar, 250).Value = SmallIcon;
                m_command.Parameters.Add("@strMediumThumbnailPath", SqlDbType.VarChar, 250).Value = MediumIcon;
                m_con.Open();
                result = m_command.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool UpdateCourseBackground(long CourseId, string OriginalImagePath, string SmallIcon, string MediumIcon)
        {
            bool result = false;
            string strCurrentMethodName = "UpdateCourseBackground";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spUpdateCourseBackgroundImage", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llCourseId", SqlDbType.BigInt).Value = CourseId;
                m_command.Parameters.Add("@strOriginalImagePath", SqlDbType.VarChar, 250).Value = OriginalImagePath;
                m_command.Parameters.Add("@strSmallThumbnailPath", SqlDbType.VarChar, 250).Value = SmallIcon;
                m_command.Parameters.Add("@strMediumThumbnailPath", SqlDbType.VarChar, 250).Value = MediumIcon;
                m_con.Open();
                result = m_command.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool JoinNewStudent(long StudentStaertedFollowingId,long StudentGettingFollowedId)
        {
            bool result = false;
            string strCurrentMethodName = "JoinNewStudent";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spJoinNewStudent", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llStudentIdStatedFollowed", SqlDbType.BigInt).Value = StudentStaertedFollowingId;
                m_command.Parameters.Add("@llStudentIdGettingFollowed", SqlDbType.BigInt).Value = StudentGettingFollowedId;
                m_con.Open();
                result = m_command.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool UnfollowStudent(long StudentStaertedFollowingId, long StudentGettingFollowedId)
        {
            bool result = false;
            string strCurrentMethodName = "UnfollowStudent";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spUnfollowStudent", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llStudentIdStatedFollowed", SqlDbType.BigInt).Value = StudentStaertedFollowingId;
                m_command.Parameters.Add("@llStudentIdGettingFollowed", SqlDbType.BigInt).Value = StudentGettingFollowedId;
                m_con.Open();
                result = m_command.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool FollowBackStudent(long StudentStaertedFollowingId, long StudentGettingFollowedId)
        {
            bool result = false;
            string strCurrentMethodName = "FollowBackStudent";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spFollowBackStudent", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llStudentIdStatedFollowed", SqlDbType.BigInt).Value = StudentStaertedFollowingId;
                m_command.Parameters.Add("@llStudentIdGettingFollowed", SqlDbType.BigInt).Value = StudentGettingFollowedId;
                m_con.Open();
                result = m_command.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public DataSet GetAllStudentsToFollow(long StudentId,int NoOfRowsFetched,int NoOfRowsToBeFetched,string SearchString)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetAllStudentsToFollow";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetAllStudentsToFollow", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llStudentId", SqlDbType.BigInt).Value = StudentId;
                m_command.Parameters.Add("@iNoOfRowsFetched", SqlDbType.Int).Value = NoOfRowsFetched;
                m_command.Parameters.Add("@iNoOfRowsToBeFetched", SqlDbType.Int).Value = NoOfRowsToBeFetched;
                m_command.Parameters.Add("@strSearchString", SqlDbType.VarChar, 100).Value = SearchString;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {

                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet GetStudentPublicProfileDetails(long StudentId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetStudentPublicProfileDetails";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetStudentPublicProfileDetails", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llStudentId", SqlDbType.BigInt).Value = StudentId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {

                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet GetAllStudentsFollowedByStudent(long StudentId, int NoOfRowsFetched, int NoOfRowsToBeFetched)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetAllStudentsFollowedByStudent";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetAllStudentsFollowedByStudnet", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llStudentId", SqlDbType.BigInt).Value = StudentId;
                m_command.Parameters.Add("@iNoOfRowsFetched", SqlDbType.Int).Value = NoOfRowsFetched;
                m_command.Parameters.Add("@iMaxRowsToBeFetched", SqlDbType.Int).Value = NoOfRowsToBeFetched;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {

                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet GetStudentFriendDetails(long StudentId,long FriendId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetStudentFriendDetails";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetStudentFriendProfileDetails", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llStudentId", SqlDbType.BigInt).Value = StudentId;
                m_command.Parameters.Add("@llFriendId", SqlDbType.BigInt).Value = FriendId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {

                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet CheckStudentFollowingStudent(long StudentId, long StudentToBeFollowedId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "CheckStudentAlreadyFollowingAnyStudent";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spCheckIsStudentAlreadyFolllowingStudent", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llStudentId", SqlDbType.BigInt).Value = StudentId;
                m_command.Parameters.Add("@llStudentIdToBeFollowed", SqlDbType.BigInt).Value = StudentToBeFollowedId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {

                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet GetStudentSelfPublicDetails(long StudentId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetStudentSelfPublicDetails";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetStudentSelfPublicDetail", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llStudentId", SqlDbType.BigInt).Value = StudentId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {

                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public bool UpdateClassroomsingleMeetingDetails(long MeetingId, string VideoUrl,string TopicName,string TopicNotes )
        {
            bool result = false;
            string strCurrentMethodName = "UpdateClassroomsingleMeetingDetails";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spUpdateClassroomMeetingDetails", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llClassroomMeetingId", SqlDbType.BigInt).Value = MeetingId;
                m_command.Parameters.Add("@strVideoUrl", SqlDbType.VarChar,250).Value = VideoUrl;
                m_command.Parameters.Add("@strMeetingTopic", SqlDbType.NVarChar, 250).Value = TopicName;
                m_command.Parameters.Add("@strMeetingNotes", SqlDbType.NVarChar, 1000).Value = TopicNotes;
                m_con.Open();
                result = m_command.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public DataSet GetLiveClassMeetingDetails(long MeetingId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetLiveClassMeetingDetails";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetLiveClassMeetingDetails", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llMeetingId", SqlDbType.BigInt).Value = MeetingId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {

                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet GetClassroomAllMeetingDetailsForStudent(long StudentId, long ClassroomId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetClassroomMeetingDetails";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetAllLiveClassesOfClassroomForStudent", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llStudentId", SqlDbType.BigInt).Value = StudentId;
                m_command.Parameters.Add("@llClassroomId", SqlDbType.BigInt).Value = ClassroomId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {

                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet GetClassroomMeetingDetailsForStudent(long StudentId, long MeetingId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetClassroomMeetingDetailsForStudent";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetClassroomMeetingDetailsForStudent", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llStudentId", SqlDbType.BigInt).Value = StudentId;
                m_command.Parameters.Add("@llMeetingId", SqlDbType.BigInt).Value = MeetingId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {

                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet GetClassroomSyllabus(long ClassroomId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetClassroomSyllabus";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetClassroomSyllabus", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llClassroomId", SqlDbType.BigInt).Value = ClassroomId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {

                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public bool MarkStudentClassroomPaymentSuccessful(long ClassroomId,long StudentId)
        {
            bool result = false;
            string strCurrentMethodName = "MarkStudentClassroomPaymentSuccessful";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spMarkStudentClassroomPaymentSuccessful", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llClassroomId", SqlDbType.BigInt).Value = ClassroomId;
                m_command.Parameters.Add("@llStudentId", SqlDbType.BigInt).Value = StudentId;
                m_con.Open();
                result = m_command.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public DataSet SearchInstructorByUserId(string SearchString)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "SearchInstructorByUserId";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spSearchInstructorByEmailId", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@strSearchEmailString", SqlDbType.VarChar,100).Value = SearchString;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {

                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public bool InsertOrUpdateClassroomFeedbackByStudent(long ClassroomId, long StudentId,string
            FeedbackMessage,int NoOfRatings)
        {
            bool result = false;
            string strCurrentMethodName = "InsertOrUpdateClassroomFeedbackByStudent";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spInsertStudentClassroomFeedback", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llClassroomId", SqlDbType.BigInt).Value = ClassroomId;
                m_command.Parameters.Add("@llStudentId", SqlDbType.BigInt).Value = StudentId;
                m_command.Parameters.Add("@strFeedbackMessage", SqlDbType.NVarChar,500).Value = FeedbackMessage;
                m_command.Parameters.Add("@iNoOfRatings", SqlDbType.Int).Value = NoOfRatings;
                m_con.Open();
                result = m_command.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public DataSet GetAllCoupons()
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetAllCoupons";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetAllCouponDiscounts", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {

                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public bool SendClassroomNotification(long ClassroomId, string NotificationMessage)
        {
            bool result = false;
            string strCurrentMethodName = "SendClassroomNotification";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spSendClassroomNotioticationToAllStudents", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llClassroomId", SqlDbType.BigInt).Value = ClassroomId;
                m_command.Parameters.Add("@strMessage", SqlDbType.VarChar).Value = NotificationMessage;
                m_con.Open();
                result = m_command.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool RegisterNewStudentViaGmail(string GmailId, string FirstName,string LastName,string UserId,string PhoneNo,
            string PhoneNoVarificationGuid, string ProfileUrl)
        {
            bool result = false;
            string strCurrentMethodName = "RegisterNewStudentViaGmail";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spInsertNewStudentThrougGmail", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@strGmailId", SqlDbType.VarChar,100).Value = GmailId;
                m_command.Parameters.Add("@strStudentFirstName", SqlDbType.VarChar,250).Value = FirstName;
                m_command.Parameters.Add("@strStudentLastName", SqlDbType.VarChar, 250).Value = LastName;
                m_command.Parameters.Add("@strSudentUserId", SqlDbType.VarChar, 250).Value = UserId;
                if (PhoneNo == null)
                {
                    m_command.Parameters.Add("@strPhoneNo", SqlDbType.VarChar, 20).Value = "";
                }
                else
                {
                    m_command.Parameters.Add("@strPhoneNo", SqlDbType.VarChar, 20).Value = PhoneNo;
                }
                m_command.Parameters.Add("@strPhoneNoVerificationGuid", SqlDbType.VarChar, 100).Value = PhoneNoVarificationGuid;
                m_command.Parameters.Add("@strProfileUrl", SqlDbType.VarChar, 100).Value = ProfileUrl;
                m_con.Open();
                result = m_command.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public DataSet CheckGmailUserAlreadyExists(string GmailId,string UserId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "CheckGmailUserAlreadyExists";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spCheckGmailUserAlreadyExists", m_con);
                m_command.Parameters.Add("@strGmailId", SqlDbType.VarChar, 100).Value = GmailId;
                m_command.Parameters.Add("@strUserId", SqlDbType.VarChar, 250).Value = UserId;
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {

                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public bool VarifyStudentPhoneNo(string UserId,string Otp,string PhoneNoGuid)
        {
            bool result = false;
            string strCurrentMethodName = "VarifyStudentPhoneNo";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spVarifyPhoneNo", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@strOTP", SqlDbType.VarChar, 10).Value = Otp;
                m_command.Parameters.Add("@strEmail", SqlDbType.VarChar, 100).Value = UserId;
                m_command.Parameters.Add("@strPhoneNoVarificationGuid", SqlDbType.VarChar, 100).Value = PhoneNoGuid;
                m_con.Open();
                result = m_command.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool InsertOtpToVarifyPhoneNoOfStudent(long StudentId, string Otp)
        {
            bool result = false;
            string strCurrentMethodName = "VarifyStudentPhoneNo";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spInsertOtoForStudentPhoneNoVarificationRequest", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@strOTP", SqlDbType.VarChar, 10).Value = Otp;
                m_command.Parameters.Add("@llStudentId", SqlDbType.BigInt).Value = StudentId;
                m_con.Open();
                result = m_command.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool UpdatePhoneNoOfGmailRegStudent(string UserId, string Token,string PhoneNo)
        {
            bool result = false;
            string strCurrentMethodName = "UpdatePhoneNoOfGmailRegStudent";
            try
            {
                InitDB();
                m_command = new SqlCommand("UpdatePhoneNoOfGmailRegisteredStudent", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@strPhoneNo", SqlDbType.VarChar,20).Value = PhoneNo;
                m_command.Parameters.Add("@strUserId", SqlDbType.VarChar,100).Value = UserId;
                m_command.Parameters.Add("@strToken", SqlDbType.VarChar,100).Value = Token;
                m_con.Open();
                result = m_command.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public bool InsertInstructorContactUsDetail(int InstructorId,string Email, string PhoneNo, string Subject, string Message)
        {
            bool result = false;
            string strCurrentMethodName = "InsertContactFormDetails";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spInsertInstructorContactRequest", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@iInstructorId", SqlDbType.Int).Value = InstructorId;
                m_command.Parameters.Add("@strEmail", SqlDbType.VarChar, 100).Value = Email;
                m_command.Parameters.Add("@strPhoneNo", SqlDbType.VarChar, 20).Value = PhoneNo;
                m_command.Parameters.Add("@strSubject", SqlDbType.NVarChar,500).Value = Subject;
                m_command.Parameters.Add("@strMessage", SqlDbType.NVarChar,200).Value = Message;
                m_con.Open();
                if (m_command.ExecuteNonQuery() > 0)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return result;
        }
        public DataSet GetAllClassroomCategories()
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetAllClassroomCategories";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetClassroomAllCategories", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {

                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet GetStudentsJoinedToClassroomForStudent(long ClassroomId, long StudentId,
                                              int MaxRowsToBeFetched,int NoOfRowsFetched)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetStudentsJoinedToClassroomForStudent";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_SpGetAllStudentsJoinedToClassroomForStudent", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llCllassroomId", SqlDbType.BigInt).Value = ClassroomId;
                m_command.Parameters.Add("@llStudentId", SqlDbType.BigInt).Value = StudentId;
                m_command.Parameters.Add("@iNoOfRowsFetched", SqlDbType.Int).Value = NoOfRowsFetched;
                m_command.Parameters.Add("@iNoOfRowsToBeFetched", SqlDbType.Int).Value = MaxRowsToBeFetched;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {
                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet CheckClassroomMeetingOccuredToday(long ClassroomId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "CheckClassroomMeetingOccuredToday";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spCheckClassroomMeetingAlreadyOccurredToday", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llClassroomId", SqlDbType.BigInt).Value = ClassroomId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {

                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet GetAllTrialClassroomMeetingDetailsForStudent(long StudentId, long ClassroomId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetAllTrialClassroomMeetingDetailsForStudent";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetTrialLiveClassesOfClassroomForStudent", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llStudentId", SqlDbType.BigInt).Value = StudentId;
                m_command.Parameters.Add("@llClassroomId", SqlDbType.BigInt).Value = ClassroomId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occurred in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {

                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet GetAllClassroomReviews(long ClassroomId,int NoOfRowsToBeFetched,int NoOfRowsFetched)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetAllClassroomReviews";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetAllReviewsForClassroom", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llClassroomId", SqlDbType.BigInt).Value = ClassroomId;
                m_command.Parameters.Add("@iMaxRowsToReturn", SqlDbType.BigInt).Value = NoOfRowsToBeFetched;
                m_command.Parameters.Add("@iNoOfRowsReturned", SqlDbType.BigInt).Value = NoOfRowsFetched;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occurred in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {

                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
        public DataSet GetAvgRatingForClassroom(long ClassroomId)
        {
            DataSet sDS = new DataSet();
            string strCurrentMethodName = "GetAvgRatingForClassroom";
            try
            {
                InitDB();
                m_command = new SqlCommand("Cp_spGetAvgRatingForClassroom", m_con);
                m_command.CommandType = System.Data.CommandType.StoredProcedure;
                m_command.Parameters.Add("@llClassroomId", SqlDbType.BigInt).Value = ClassroomId;
                m_con.Open();
                SqlDataAdapter sSQLAdpter = new SqlDataAdapter(m_command);
                sSQLAdpter.Fill(sDS);
            }
            catch (Exception ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, ex.ToString());
                m_strLogMessage.Append("Exception occurred in method :" + ex.TargetSite);
                CpLogger.Error(m_strLogMessage);
            }
            finally
            {
                if (m_con != null)
                {

                    m_con.Dispose();
                }
                if (m_command != null)
                {
                    m_command.Dispose();
                }
            }
            return sDS;
        }
    }
}