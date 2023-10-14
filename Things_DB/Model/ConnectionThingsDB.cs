using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Things_DB
{
    internal class ConnectionThingsDB
    {
        public SqlConnection OpenDBConnection() 
        {
            string connectionString = ConfigurationManager.ConnectionStrings["LocalConnection"].ConnectionString;
            SqlConnection connection=new SqlConnection(connectionString);
            connection.Open();
            return connection;
        }
    }
}
