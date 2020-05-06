using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace DataAccessLayer.Contexts
{
    public class SQLDatabaseContext : IDatabaseContext
    {
        private readonly string connectionString = "Server=mssql.fhict.local;Database=dbi400050;User Id = dbi400050;Password = EuAIC1a!jcW2Hwn$";

        public SqlConnection GetConnection()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            return connection;
        }
    }
}
