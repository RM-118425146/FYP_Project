using System;
using Microsoft.Data.SqlClient;


namespace FYP_Project.Helpers
{
    public class DbHelper
    {
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=EsportsData;");
        }
    }
}
