using System.Data.SqlClient;

namespace POS360_API.Configs
{
    public sealed class ApiManager
    {

        private ApiManager() { }
        private static ApiManager instance = null;
        public static ApiManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ApiManager();
                }
                return instance;
            }
        }


        public SqlConnectionStringBuilder GetConnectionString()
        {

            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = @"DESKTOP-IMGD37M\SQLEXPRESS";  
            builder.UserID = "Hirushan";         
            builder.Password = "hirushan";  
            builder.InitialCatalog = "Suwastha_DB";
            return builder;
        }
    }


    enum APIResponse
    {
        Success,
        Fail
    }
}
