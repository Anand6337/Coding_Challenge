using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanManagementSystem.Util
{
    public static class DBConnectionUtil
    {
        public static SqlConnection GetDBConn()
        {
            string connectionString = "Server=localhost;Database=LoanManagementSystemDB;Integrated Security=True;"; 
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            return connection;
        }
    }
}