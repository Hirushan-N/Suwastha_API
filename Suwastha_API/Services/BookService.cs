using POS360_API.Configs;
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

        #region Read Books
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
                    command.CommandText = "SP_Book_ReadAll";

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ClinicBook clinicBook = new ClinicBook();
                                clinicBook.Id = Convert.ToInt32(reader["Id"]);
                                clinicBook.BookID = reader["BookID"].ToString();
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
                            responseObj.RenponseBody = clinicBooks;
                        }
                        else
                        {
                            responseObj.Status = APIResponse.Fail.ToString();
                            responseObj.RenponseBody = null;
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
