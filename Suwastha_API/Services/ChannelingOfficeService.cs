using Suwastha_API.Configs;
using Suwastha_API.Models;
using System.Data;
using System.Data.SqlClient;

namespace Suwastha_API.Services
{
    public class ChannelingOfficeService
    {
        #region Local Variables 
        private SqlCommand command;
        private SqlTransaction transaction;
        private ResponseResult responseObj;
        #endregion

        #region C and U 
        public ResponseResult Save(ChannelingOffice channelingOffice)
        {
            using (SqlConnection connection = new SqlConnection(ApiManager.Instance.GetConnectionString().ConnectionString))
            {
                try
                {
                    responseObj = new ResponseResult();
                    connection.Open();
                    transaction = connection.BeginTransaction();
                    command = new SqlCommand();
                    command.Connection = connection;
                    command.Transaction = transaction;

                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 0;
                    command.CommandText = "SP_ChannelingOffice_Save";

                    command.Parameters.AddWithValue("@ClinicBookID", channelingOffice.ChannelingOfficeID);
                    command.Parameters.AddWithValue("@PatientID", channelingOffice.Name);
                    command.Parameters.AddWithValue("@ClinicID", channelingOffice.Title);
                    command.Parameters.AddWithValue("@NextDate", channelingOffice.Description);
                    command.Parameters.AddWithValue("@CreatedDoctor", channelingOffice.getEncryptedPassword());
                    command.Parameters.AddWithValue("@CreatedSection", channelingOffice.ConsultantID);
                    command.Parameters.AddWithValue("@ReferenceLetter", channelingOffice.ConsultantName);
                    command.Parameters.AddWithValue("@ClosingNote", channelingOffice.DeputyConsultantID);
                    command.Parameters.AddWithValue("@ClosingNote", channelingOffice.DeputyConsultantName);
                    command.Parameters.AddWithValue("@IsActive", channelingOffice.IsActive);
                    command.Parameters.AddWithValue("@IsDeleted", channelingOffice.IsDeleted);
                    command.Parameters.AddWithValue("@CreatedDate", channelingOffice.CreatedDate);
                    command.Parameters.AddWithValue("@UpdatedDate", channelingOffice.UpdatedDate);
                    int rowCount = command.ExecuteNonQuery();
                    transaction.Commit();
                    command.Dispose();
                    connection.Close();

                    if (rowCount < 1)
                    {
                        transaction.Rollback();
                        responseObj.Status = APIResponse.Fail.ToString();
                        responseObj.Message = "Unsucceeded";
                    }
                    else
                    {
                        responseObj.Status = APIResponse.Success.ToString();
                        responseObj.Body = channelingOffice;
                        responseObj.Message = "Succeeded";
                    }


                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    responseObj.Status = APIResponse.Fail.ToString();
                    responseObj.Message = ex.Message;
                }
            }
            return responseObj;
        }
        #endregion

        #region Delete 
        public ResponseResult Delete(ChannelingOffice channelingOffice)
        {
            channelingOffice.IsActive = false;
            channelingOffice.IsDeleted = true;
            ResponseResult responseObj = this.Save(channelingOffice);
            responseObj.Body = null;
            responseObj.Message = "Deleted";
            return responseObj;
        }
        #endregion

        #region Read for login
        public ResponseResult Login(LoginRequest loginRequest)
        {
            using (SqlConnection connection = new SqlConnection(ApiManager.Instance.GetConnectionString().ConnectionString))
            {
                try
                {
                    ChannelingOffice channelingOffice = new ChannelingOffice();
                    responseObj = new ResponseResult();
                    connection.Open();
                    transaction = connection.BeginTransaction();
                    command = new SqlCommand();
                    command.Connection = connection;
                    command.Transaction = transaction;

                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 0;
                    command.CommandText = "SP_ChannelingOffice_Login";
                    command.Parameters.AddWithValue("@ChannelingOfficeID", loginRequest.User);
                    command.Parameters.AddWithValue("@Password", loginRequest.Key);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                channelingOffice.Id = Convert.ToInt32(reader["Id"]);
                                channelingOffice.ChannelingOfficeID = Convert.ToString(reader["ChannelingOfficeID"]);
                                channelingOffice.Name = Convert.ToString(reader["Name"]);
                                channelingOffice.Title = Convert.ToString(reader["Title"]);
                                channelingOffice.Description = Convert.ToString(reader["Description"]);
                                //password did not returned
                                channelingOffice.ConsultantID = reader["ConsultantID"].ToString();
                                channelingOffice.ConsultantName = reader["ConsultantName"].ToString();
                                channelingOffice.DeputyConsultantID = Convert.ToString(reader["DeputyConsultantID"]);
                                channelingOffice.DeputyConsultantName = Convert.ToString(reader["DeputyConsultantName"]);
                                channelingOffice.IsActive = Convert.ToBoolean(reader["IsActive"]);
                                channelingOffice.IsDeleted = Convert.ToBoolean(reader["IsDeleted"]);
                                channelingOffice.CreatedDate = Convert.ToDateTime(reader["CreatedDate"]);
                                if (!Convert.IsDBNull(reader["UpdatedDate"]))
                                {
                                    channelingOffice.UpdatedDate = Convert.ToDateTime(reader["UpdatedDate"]);
                                }
                                else
                                {
                                    channelingOffice.UpdatedDate = null;
                                }

                            }
                            responseObj.Status = APIResponse.Success.ToString();
                            responseObj.Body = channelingOffice;
                            responseObj.Message = "Login Successfull";

                        }
                        else
                        {
                            responseObj.Status = APIResponse.Fail.ToString();
                            responseObj.Body = null;
                            responseObj.Message = "Invalid Login";
                        }

                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    responseObj.Status = APIResponse.Fail.ToString();
                    responseObj.Message = ex.Message;
                    responseObj.Message = "Invalid Login";

                }
            }
            return responseObj;
        }
        #endregion
    }
}
