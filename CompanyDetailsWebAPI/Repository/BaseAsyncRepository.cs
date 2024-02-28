using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace CompanyDetailsWebAPI.Repository
{
    public class BaseAsyncRepository
    {
        private string sqlConnectionString;
        private string databaseType;

        public BaseAsyncRepository(IConfiguration configuration)
        {
            sqlConnectionString = configuration.GetConnectionString("SqlConnection");
            databaseType = configuration["DbInfo:DbType"];
        }

        internal DbConnection SqlConnection
        {
            get
            {
                switch (databaseType)
                {
                    case "SqlServer":
                        return new SqlConnection(sqlConnectionString);
                    default:
                        return new SqlConnection(sqlConnectionString);
                }
            }
        }
    }
}
