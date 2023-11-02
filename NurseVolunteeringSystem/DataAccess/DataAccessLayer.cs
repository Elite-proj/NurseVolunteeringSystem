using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using NurseVolunteeringSystem.Models.ViewModels;
using NurseVolunteeringSystem.Models;
using NurseVolunteeringSystem.Areas.Manager.Models;
using NurseVolunteeringSystem.Password;

namespace NurseVolunteeringSystem.DataAccess
{
    public class DataAccessLayer
    {
        public readonly IConfiguration _configuration;

        public DataAccessLayer(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        SqlConnection dbconn;
        SqlCommand dbComm;
        SqlDataAdapter dbAdapter;
        DataTable dt;

        #region Admin Area

        public int RegisterManager(ManagerViewModel manager)
        {
            string connString = _configuration.GetConnectionString("connString");
            dbconn = new SqlConnection(connString);

            try
            {
                dbconn.Open();
            }
            catch
            {

            }

            dbComm = new SqlCommand("sp_RegisterManager", dbconn);
            dbComm.CommandType = CommandType.StoredProcedure;

            manager.UserType = "O";

            dbComm.Parameters.AddWithValue("@Username", manager.Username);
            dbComm.Parameters.AddWithValue("@EmailAddress", manager.Email);
            dbComm.Parameters.AddWithValue("@Password", PasswordEncryption.ConvertToEncrypt(manager.Password));
            dbComm.Parameters.AddWithValue("@ContactNo", manager.ContactNo);
            dbComm.Parameters.AddWithValue("@UserType", manager.UserType);
           

            int x = dbComm.ExecuteNonQuery();
            dbconn.Close();

            return x;
        }

        public int RegisterNurse(NurseViewModel nurse)
        {
            string connString = _configuration.GetConnectionString("connString");
            dbconn = new SqlConnection(connString);

            try
            {
                dbconn.Open();
            }
            catch
            {

            }

            dbComm = new SqlCommand("sp_RegisterNurse", dbconn);
            dbComm.CommandType = CommandType.StoredProcedure;

            nurse.UserType = "N";

            dbComm.Parameters.AddWithValue("@Username", nurse.Username);
            dbComm.Parameters.AddWithValue("@FirstName", nurse.FirstName);
            dbComm.Parameters.AddWithValue("@Surname", nurse.Surname);
            dbComm.Parameters.AddWithValue("@EmailAddress", nurse.Email);
            dbComm.Parameters.AddWithValue("@Password", PasswordEncryption.ConvertToEncrypt(nurse.Password));
            dbComm.Parameters.AddWithValue("@ContactNo", nurse.ContactNo);
            dbComm.Parameters.AddWithValue("@GenderID", nurse.GenderID);
            
            dbComm.Parameters.AddWithValue("@UserType", nurse.UserType);


            int x = dbComm.ExecuteNonQuery();
            dbconn.Close();

            return x;
        }

        public int RegisterPatient(PatientViewModel patient)
        {
            string connString = _configuration.GetConnectionString("connString");
            dbconn = new SqlConnection(connString);

            try
            {
                dbconn.Open();
            }
            catch
            {

            }

            dbComm = new SqlCommand("sp_RegisterPatient", dbconn);
            dbComm.CommandType = CommandType.StoredProcedure;

            patient.UserType = "P";

            dbComm.Parameters.AddWithValue("@Username", patient.Username);
            dbComm.Parameters.AddWithValue("@FirstName", patient.FirstName);
            dbComm.Parameters.AddWithValue("@Surname", patient.Surname);
            dbComm.Parameters.AddWithValue("@EmailAddress", patient.Email);
            dbComm.Parameters.AddWithValue("@Password", PasswordEncryption.ConvertToEncrypt(patient.Password));
            dbComm.Parameters.AddWithValue("@AddressLine1", patient.AddressLine1);
            dbComm.Parameters.AddWithValue("@AddressLine2", patient.AddressLine2);
            dbComm.Parameters.AddWithValue("@SuburbID", patient.SuburbID);
            dbComm.Parameters.AddWithValue("@ContactNo", patient.ContactNo);
            dbComm.Parameters.AddWithValue("@GenderID", patient.GenderID);
            dbComm.Parameters.AddWithValue("@DateOfBirth", patient.DateOfBirth.ToString());
            dbComm.Parameters.AddWithValue("@EmergencyContactPerson", patient.EmergencyContactPerson);
            dbComm.Parameters.AddWithValue("@EmergencyContactNo", patient.EmergencyContactNumber);
            dbComm.Parameters.AddWithValue("@IDNumber", patient.IDNumber);
            dbComm.Parameters.AddWithValue("@UserType", patient.UserType);


            int x = dbComm.ExecuteNonQuery();
            dbconn.Close();

            return x;
        }
        public DataTable Login(LoginViewModel user)
        {
            string connString = _configuration.GetConnectionString("connString");
            dbconn = new SqlConnection(connString);

            try
            {
                dbconn.Open();
            }
            catch
            {

            }

            dbComm = new SqlCommand("sp_UserLogin", dbconn);
            dbComm.CommandType = CommandType.StoredProcedure;

            dbComm.Parameters.AddWithValue("@Username", user.Username);
            dbComm.Parameters.AddWithValue("@Password", PasswordEncryption.ConvertToEncrypt(user.Password));

            dbAdapter = new SqlDataAdapter(dbComm);
            dt = new DataTable();
            dbAdapter.Fill(dt);
            dbconn.Close();
            return dt;
        }

        public int UpdateManager(UpdateManagerVM manager)
        {
            string connString = _configuration.GetConnectionString("connString");
            dbconn = new SqlConnection(connString);

            try
            {
                dbconn.Open();
            }
            catch
            {

            }

            dbComm = new SqlCommand("sp_UpdateManager", dbconn);
            dbComm.CommandType = CommandType.StoredProcedure;

           
            dbComm.Parameters.AddWithValue("@Email", manager.Email);
            dbComm.Parameters.AddWithValue("@ContactNo", manager.ContactNo);
            dbComm.Parameters.AddWithValue("@UserID", manager.UserID);


            int x = dbComm.ExecuteNonQuery();
            dbconn.Close();

            return x;
        }

        public int UpdateNurse(UpdateNurseVM nurse)
        {
            string connString = _configuration.GetConnectionString("connString");
            dbconn = new SqlConnection(connString);

            try
            {
                dbconn.Open();
            }
            catch
            {

            }

            dbComm = new SqlCommand("sp_UpdateNurse", dbconn);
            dbComm.CommandType = CommandType.StoredProcedure;

            

           
            dbComm.Parameters.AddWithValue("@FirstName", nurse.FirstName);
            dbComm.Parameters.AddWithValue("@Surname", nurse.Surname);
            dbComm.Parameters.AddWithValue("@Email", nurse.Email);
            dbComm.Parameters.AddWithValue("@ContactNo", nurse.ContactNo);
            dbComm.Parameters.AddWithValue("@GenderID", nurse.GenderID);
            
            dbComm.Parameters.AddWithValue("@UserID", nurse.UserID);


            int x = dbComm.ExecuteNonQuery();
            dbconn.Close();

            return x;
        }

        public DataTable ListManagers()
        {
            string connString = _configuration.GetConnectionString("connString");
            dbconn = new SqlConnection(connString);

            try
            {
                dbconn.Open();
            }
            catch
            {

            }

            dbComm = new SqlCommand("sp_SelectManagers", dbconn);
            dbComm.CommandType = CommandType.StoredProcedure;

            dbAdapter = new SqlDataAdapter(dbComm);
            dt = new DataTable();
            dbAdapter.Fill(dt);
            dbconn.Close();
            return dt;
        }
        public DataTable ListNurses()
        {
            string connString = _configuration.GetConnectionString("connString");
            dbconn = new SqlConnection(connString);

            try
            {
                dbconn.Open();
            }
            catch
            {

            }

            dbComm = new SqlCommand("sp_SelectNurses", dbconn);
            dbComm.CommandType = CommandType.StoredProcedure;

            dbAdapter = new SqlDataAdapter(dbComm);
            dt = new DataTable();
            dbAdapter.Fill(dt);
            dbconn.Close();
            return dt;
        }

        public DataTable GetManagerByID(int id)
        {
            string connString = _configuration.GetConnectionString("connString");
            dbconn = new SqlConnection(connString);

            try
            {
                dbconn.Open();
            }
            catch
            {

            }

            dbComm = new SqlCommand("sp_SelectManagerByID", dbconn);
            dbComm.CommandType = CommandType.StoredProcedure;

            dbComm.Parameters.AddWithValue("@UserID", id);

            dbAdapter = new SqlDataAdapter(dbComm);
            dt = new DataTable();
            dbAdapter.Fill(dt);
            dbconn.Close();
            return dt;
        }
        public DataTable GetNurseByID(int id)
        {
           
            string connString = _configuration.GetConnectionString("connString");
            dbconn = new SqlConnection(connString);

            try
            {
                dbconn.Open();
            }
            catch
            {

            }

            dbComm = new SqlCommand("sp_SelectNurseByID", dbconn);
            dbComm.CommandType = CommandType.StoredProcedure;

            dbComm.Parameters.AddWithValue("@UserID", id);

            dbAdapter = new SqlDataAdapter(dbComm);
            dt = new DataTable();
            dbAdapter.Fill(dt);
            dbconn.Close();
            return dt;


        }

        public int DeleteUser(User user)
        {
            string connString = _configuration.GetConnectionString("connString");
            dbconn = new SqlConnection(connString);

            try
            {
                dbconn.Open();
            }
            catch
            {

            }

            dbComm = new SqlCommand("DeleteUsers", dbconn);
            dbComm.CommandType = CommandType.StoredProcedure;

            dbComm.Parameters.AddWithValue("@UserID", user.UserID);


            int x = dbComm.ExecuteNonQuery();
            dbconn.Close();

            return x;
        }

        public DataTable SearchVisitsByDates(DateRangeVM date)
        {
            string connString = _configuration.GetConnectionString("connString");
            dbconn = new SqlConnection(connString);

            try
            {
                dbconn.Open();
            }
            catch
            {

            }

            dbComm = new SqlCommand("spSeachVisitsByDate", dbconn);
            dbComm.CommandType = CommandType.StoredProcedure;

            dbComm.Parameters.AddWithValue("@MinDate", date.MinDate);
            dbComm.Parameters.AddWithValue("@MaxDate", date.MaxDate);
            dbComm.Parameters.AddWithValue("@ContractID", date.contractID);
            

            dbAdapter = new SqlDataAdapter(dbComm);
            dt = new DataTable();
            dbAdapter.Fill(dt);
            dbconn.Close();
            return dt;
        }

        public DataTable SearchContractsByDates(DateRangeVM date)
        {
            string connString = _configuration.GetConnectionString("connString");
            dbconn = new SqlConnection(connString);

            try
            {
                dbconn.Open();
            }
            catch
            {

            }

            dbComm = new SqlCommand("sp_SearchCareContractByDates", dbconn);
            dbComm.CommandType = CommandType.StoredProcedure;

            dbComm.Parameters.AddWithValue("@MinDate", date.MinDate);
            dbComm.Parameters.AddWithValue("@MaxDate", date.MaxDate);


            dbAdapter = new SqlDataAdapter(dbComm);
            dt = new DataTable();
            dbAdapter.Fill(dt);
            dbconn.Close();
            return dt;
        }


        #endregion

        #region Update personal info 
        public int UpdateNursePersonalInfo(UpdateNurseViewModel nurse)
        {
            string connString = _configuration.GetConnectionString("connString");
            dbconn = new SqlConnection(connString);

            try
            {
                dbconn.Open();
            }
            catch
            {

            }

            dbComm = new SqlCommand("sp_UpdateNursePersonalInfo", dbconn);
            dbComm.CommandType = CommandType.StoredProcedure;

           

           
            dbComm.Parameters.AddWithValue("@FirstName", nurse.FirstName);
            dbComm.Parameters.AddWithValue("@Surname", nurse.Surname);
            dbComm.Parameters.AddWithValue("@EmailAddress", nurse.Email);
            dbComm.Parameters.AddWithValue("@ContactNo", nurse.ContactNo);
            dbComm.Parameters.AddWithValue("@GenderID", nurse.GenderID);
            dbComm.Parameters.AddWithValue("@NurseID", nurse.NurseID);


            int x = dbComm.ExecuteNonQuery();
            dbconn.Close();

            return x;
        }
        public int UpdatePatientPersonalInfo(UpdatePatientViewModel patient)
        {
            string connString = _configuration.GetConnectionString("connString");
            dbconn = new SqlConnection(connString);

            try
            {
                dbconn.Open();
            }
            catch
            {

            }

            dbComm = new SqlCommand("sp_PatientUpdatePersonalInfo", dbconn);
            dbComm.CommandType = CommandType.StoredProcedure;

           

           
            dbComm.Parameters.AddWithValue("@FirstName", patient.FirstName);
            dbComm.Parameters.AddWithValue("@Surname", patient.Surname);
            dbComm.Parameters.AddWithValue("@EmailAddress", patient.Email);
            dbComm.Parameters.AddWithValue("@AddressLine1", patient.AddressLine1);
            dbComm.Parameters.AddWithValue("@AddressLine2", patient.AddressLine2);
            dbComm.Parameters.AddWithValue("@SuburbID", patient.SuburbID);
            dbComm.Parameters.AddWithValue("@ContactNo", patient.ContactNo);
            dbComm.Parameters.AddWithValue("@GenderID", patient.GenderID);
            dbComm.Parameters.AddWithValue("@DateOfBirth", patient.DateOfBirth.ToString());
            dbComm.Parameters.AddWithValue("@EmergencyContactPerson", patient.EmergencyContactPerson);
            dbComm.Parameters.AddWithValue("@EmergencyContactNo", patient.EmergencyContactNumber);
            dbComm.Parameters.AddWithValue("@IDNumber", patient.IDNumber);
            dbComm.Parameters.AddWithValue("@PatientID", patient.PatientID);


            int x = dbComm.ExecuteNonQuery();
            dbconn.Close();

            return x;
        }
        public DataTable GetPatientByID(int id)
        {

            string connString = _configuration.GetConnectionString("connString");
            dbconn = new SqlConnection(connString);

            try
            {
                dbconn.Open();
            }
            catch
            {

            }

            dbComm = new SqlCommand("sp_SelectPatientByID", dbconn);
            dbComm.CommandType = CommandType.StoredProcedure;

            dbComm.Parameters.AddWithValue("@UserID", id);

            dbAdapter = new SqlDataAdapter(dbComm);
            dt = new DataTable();
            dbAdapter.Fill(dt);
            dbconn.Close();
            return dt;


        }
        public int UpdateUserPassword(UpdateUserPassword user)
        {
            string connString = _configuration.GetConnectionString("connString");
            dbconn = new SqlConnection(connString);

            try
            {
                dbconn.Open();
            }
            catch
            {

            }

            dbComm = new SqlCommand("sp_ChangeUserPassword", dbconn);
            dbComm.CommandType = CommandType.StoredProcedure;




            
            dbComm.Parameters.AddWithValue("@Password", PasswordEncryption.ConvertToEncrypt(user.Password));
            dbComm.Parameters.AddWithValue("@UserID", user.UserID);


            int x = dbComm.ExecuteNonQuery();
            dbconn.Close();

            return x;
        }

        public DataTable GetUserByID(int id)
        {

            string connString = _configuration.GetConnectionString("connString");
            dbconn = new SqlConnection(connString);

            try
            {
                dbconn.Open();
            }
            catch
            {

            }

            dbComm = new SqlCommand("sp_SelectUserByID", dbconn);
            dbComm.CommandType = CommandType.StoredProcedure;

            dbComm.Parameters.AddWithValue("@UserID", id);

            dbAdapter = new SqlDataAdapter(dbComm);
            dt = new DataTable();
            dbAdapter.Fill(dt);
            dbconn.Close();
            return dt;


        }

        #endregion
    }
}
