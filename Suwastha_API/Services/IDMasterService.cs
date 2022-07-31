using Suwastha_API.Configs;
using Suwastha_API.Models;
using System.Data;
using System.Data.SqlClient;

namespace Suwastha_API.Services
{
    public class IDMasterService
    {
        #region Local Variables 
        private SqlCommand command;
        private SqlTransaction transaction;
        private ResponseResult responseObj;
        #endregion

        #region Read 
        public IDMaster Read(string TableName)
        {
            IDMaster iDMaster = new IDMaster();
            using (SqlConnection connection = new SqlConnection(ApiManager.Instance.GetConnectionString().ConnectionString))
            {
                try
                {
                    connection.Open();
                    transaction = connection.BeginTransaction();
                    command = new SqlCommand();
                    command.Connection = connection;
                    command.Transaction = transaction;

                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 0;
                    command.CommandText = "SP_IDMaster_Read";
                    command.Parameters.AddWithValue("TableName",TableName);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                iDMaster.Id = Convert.ToInt32(reader["Id"]);
                                iDMaster.TableName = reader["TableName"].ToString();
                                iDMaster.LastID = reader["LastID"].ToString();
                            }

                        }

                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return iDMaster;
        }
        #endregion

    }
}
