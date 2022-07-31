using Suwastha_API.Configs;
using Suwastha_API.Models;
using System.Data;
using System.Data.SqlClient;

namespace Suwastha_API.Services
{
    public class ClinicBookService
    {
        #region Local Variables 
        private SqlCommand command;
        private SqlTransaction transaction;
        private ResponseResult responseObj;
        #endregion

        #region C and U ClinicBook
        public ResponseResult Save(ClinicBook clinicBook)
        {
            using (SqlConnection connection = new SqlConnection(ApiManager.Instance.GetConnectionString().ConnectionString))
            {
                try
                {
                    List<ClinicBook> clinicBooks = new List<ClinicBook>();
                    responseObj = new ResponseResult();
                    connection.Open();
                    transaction = connection.BeginTransaction();
                    command = new SqlCommand();
                    command.Connection = connection;
                    command.Transaction = transaction;

                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 0;
                    command.CommandText = "SP_ClinicBook_Save";

                    command.Parameters.AddWithValue("@ClinicBookID", clinicBook.ClinicBookID);
                    command.Parameters.AddWithValue("@PatientID", clinicBook.PatientID);
                    command.Parameters.AddWithValue("@ClinicID", clinicBook.ClinicID);
                    command.Parameters.AddWithValue("@NextDate", clinicBook.NextDate);
                    command.Parameters.AddWithValue("@CreatedDoctor", clinicBook.CreatedDoctor);
                    command.Parameters.AddWithValue("@CreatedSection", clinicBook.CreatedSection);
                    command.Parameters.AddWithValue("@ReferenceLetter", clinicBook.ReferenceLetter);
                    command.Parameters.AddWithValue("@ClosingNote", clinicBook.ClosingNote);
                    command.Parameters.AddWithValue("@IsActive", clinicBook.IsActive);
                    command.Parameters.AddWithValue("@IsDeleted", clinicBook.IsDeleted);
                    command.Parameters.AddWithValue("@CreatedDate", clinicBook.CreatedDate);
                    command.Parameters.AddWithValue("@UpdatedDate", clinicBook.UpdatedDate);
                    int rowCount = command.ExecuteNonQuery();
                    transaction.Commit();
                    command.Dispose();
                    connection.Close();

                    if (rowCount<1)
                    {
                        transaction.Rollback();
                        responseObj.Status = APIResponse.Fail.ToString();
                        responseObj.Message = "Unsucceeded";
                    }
                    else
                    {
                        responseObj.Status = APIResponse.Success.ToString();
                        responseObj.Body = clinicBook;
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

        #region Delete ClinicBook
        public ResponseResult Delete(ClinicBook clinicBook)
        {
            clinicBook.IsActive = false;
            clinicBook.IsDeleted = true;
            ResponseResult responseObj = this.Save(clinicBook);
            responseObj.Body = null;
            responseObj.Message = "Deleted";
            return responseObj;
        } 
        #endregion

        #region Read ClinicBook
        public ResponseResult Read()
        {
            using (SqlConnection connection = new SqlConnection(ApiManager.Instance.GetConnectionString().ConnectionString))
            {
                try
                {
                    List<ClinicBook> clinicBooks = new List<ClinicBook>();
                    responseObj = new ResponseResult();
                    connection.Open();
                    transaction = connection.BeginTransaction();
                    command = new SqlCommand();
                    command.Connection = connection;
                    command.Transaction = transaction;

                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 0;
                    command.CommandText = "SP_ClinicBook_ReadAll";

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ClinicBook clinicBook = new ClinicBook();
                                clinicBook.Id = Convert.ToInt32(reader["Id"]);
                                clinicBook.ClinicBookID = reader["ClinicBookID"].ToString();
                                clinicBook.PatientID = reader["PatientID"].ToString();
                                clinicBook.ClinicID = reader["ClinicID"].ToString();
                                clinicBook.NextDate = Convert.ToDateTime(reader["NextDate"]);
                                clinicBook.CreatedDoctor = reader["CreatedDoctor"].ToString();
                                clinicBook.CreatedSection = reader["CreatedSection"].ToString();
                                clinicBook.ReferenceLetter = Convert.ToString(reader["ReferenceLetter"]);
                                clinicBook.ClosingNote = Convert.ToString(reader["ClosingNote"]);
                                clinicBook.IsActive = Convert.ToBoolean(reader["IsActive"]);
                                clinicBook.IsDeleted = Convert.ToBoolean(reader["IsDeleted"]);
                                clinicBook.CreatedDate = Convert.ToDateTime(reader["CreatedDate"]);
                                if (!Convert.IsDBNull(reader["UpdatedDate"]))
                                {
                                    clinicBook.UpdatedDate = Convert.ToDateTime(reader["UpdatedDate"]);
                                }
                                else
                                {
                                    clinicBook.UpdatedDate = null;
                                }

                                clinicBooks.Add(clinicBook);

                            }
                            responseObj.Status = APIResponse.Success.ToString();
                            responseObj.Body = clinicBooks;
                        }
                        else
                        {
                            responseObj.Status = APIResponse.Fail.ToString();
                            responseObj.Body = null;
                            responseObj.Message = "Books Not Found";
                        }

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

    }
}
