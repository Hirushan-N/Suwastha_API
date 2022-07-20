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
            builder.DataSource = @"SQL5080.site4now.net";  
            builder.UserID = "db_a8a62e_suwastha_admin";         
            builder.Password = "Suwastha2022#";  
            builder.InitialCatalog = "db_a8a62e_suwastha";
            return builder;
        }
    }


    enum APIResponse
    {
        Success,
        Fail
    }
}
