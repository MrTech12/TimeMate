using DataAccessLayer.DTO;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;

namespace DataAccessLayer.Containers
{
    public class SQLAccountContainer : IAccountContainer
    {
        private SQLDatabaseContainer SQLDatabaseContainer = new SQLDatabaseContainer();

        public string GetUserID(string mail)
        {
            string databaseOutput;
            try
            {
                using (SqlConnection databaseConn = new SqlConnection(SQLDatabaseContainer.GetConnectionString()))
                {
                    string query = @"SELECT AccountID FROM [Account] WHERE Mail = @0";

                    databaseConn.Open();
                    SqlCommand selectQuery = new SqlCommand(query, databaseConn);

                    selectQuery.Parameters.AddWithValue("0", mail);
                    var resultedAccountID = selectQuery.ExecuteScalar();

                    if (resultedAccountID == null)
                    {
                        databaseOutput = null;
                    }
                    else
                    {
                        databaseOutput = resultedAccountID.ToString();
                    }
                }
            }
            catch (SqlException exception)
            {
                throw new Exception("Er is op dit moment een probleem met de database.", exception);
            }
            return databaseOutput;
        }

        public string GetFirstName(string mail)
        {
            string firstName;
            try
            {
                using (SqlConnection databaseConn = new SqlConnection(SQLDatabaseContainer.GetConnectionString()))
                {
                    string query = @"SELECT FirstName FROM [Account] WHERE Mail = @0";

                    databaseConn.Open();
                    SqlCommand selectQuery = new SqlCommand(query, databaseConn);

                    selectQuery.Parameters.AddWithValue("0", mail);
                    var resultedUserName = selectQuery.ExecuteScalar();

                    if (resultedUserName == null)
                    {
                        firstName = null;
                    }
                    else
                    {
                        firstName = resultedUserName.ToString();
                    }
                }
            }
            catch (SqlException exception)
            {
                throw new Exception("Er is op dit moment een probleem met de database.", exception);
            }
            return firstName;
        }

        public string SearchForPasswordHash(string mail)
        {
            string passwordHash;
            try
            {
                using (SqlConnection databaseConn = new SqlConnection(SQLDatabaseContainer.GetConnectionString()))
                {
                    string query = @"SELECT Password FROM [Account] WHERE Mail = @0";

                    databaseConn.Open();
                    SqlCommand selectQuery = new SqlCommand(query, databaseConn);
                    
                    selectQuery.Parameters.AddWithValue("0", mail);
                    var resultedPasswordHash = selectQuery.ExecuteScalar();

                    if (resultedPasswordHash == null)
                    {
                        passwordHash = null;
                    }
                    else
                    {
                        passwordHash = resultedPasswordHash.ToString();
                    }
                }
            }
            catch (SqlException exception)
            {
                throw new Exception("Er is op dit moment een probleem met de database.", exception);
            }
            return passwordHash;
        }

        public int CreateAccount(AccountDTO accountDTO)
        {
            int accountID;
            try
            {
                using (SqlConnection databaseConn = new SqlConnection(SQLDatabaseContainer.GetConnectionString()))
                {
                    string query = @"INSERT INTO [Account](FirstName, Mail, Password) Values (@0,@1,@2); SELECT SCOPE_IDENTITY();";

                    databaseConn.Open();
                    SqlCommand insertQuery = new SqlCommand(query, databaseConn);

                    insertQuery.Parameters.AddWithValue("0", accountDTO.FirstName);
                    insertQuery.Parameters.AddWithValue("1", accountDTO.Mail);
                    insertQuery.Parameters.AddWithValue("2", accountDTO.Password);
                    accountID = Convert.ToInt32(insertQuery.ExecuteScalar());
                }
            }
            catch (SqlException exception)
            {
                throw new Exception("Er is op dit moment een probleem met de database.", exception);
            }
            return accountID;
        }
    }
}
