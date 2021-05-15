using Model.DTO_s;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;
using Error;

namespace DataAccessLayer.Repositories
{
    public class SQLAccountRepository : IAccountRepository
    {
        private SQLDatabaseRepository SQLDatabaseRepository = new SQLDatabaseRepository();

        public int GetUserID(string mail)
        {
            int userID;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(SQLDatabaseRepository.GetConnectionString()))
                {
                    string query = @"SELECT AccountID FROM [Account] WHERE Mail = @0";

                    sqlConnection.Open();
                    SqlCommand selectCommand = new SqlCommand(query, sqlConnection);

                    selectCommand.Parameters.AddWithValue("0", mail);
                    var resultedAccountID = selectCommand.ExecuteScalar();

                    if (resultedAccountID == null)
                    {
                        userID = -1;
                    }
                    else
                    {
                        userID = Convert.ToInt32(resultedAccountID);
                    }
                }
            }
            catch (SqlException exception)
            {
                throw new DatabaseException("Er is op dit moment een probleem met de database.", exception);
            }
            return userID;
        }

        public string GetFirstName(string mail)
        {
            string firstName;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(SQLDatabaseRepository.GetConnectionString()))
                {
                    string query = @"SELECT FirstName FROM [Account] WHERE Mail = @0";

                    sqlConnection.Open();
                    SqlCommand selectCommand = new SqlCommand(query, sqlConnection);

                    selectCommand.Parameters.AddWithValue("0", mail);
                    var resultedFirstName = selectCommand.ExecuteScalar();

                    if (resultedFirstName == null)
                    {
                        firstName = null;
                    }
                    else
                    {
                        firstName = resultedFirstName.ToString();
                    }
                }
            }
            catch (SqlException exception)
            {
                throw new DatabaseException("Er is op dit moment een probleem met de database.", exception);
            }
            return firstName;
        }

        public string GetPasswordHash(string mail)
        {
            string passwordHash;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(SQLDatabaseRepository.GetConnectionString()))
                {
                    string query = @"SELECT Password FROM [Account] WHERE Mail = @0";

                    sqlConnection.Open();
                    SqlCommand selectCommand = new SqlCommand(query, sqlConnection);
                    
                    selectCommand.Parameters.AddWithValue("0", mail);
                    var resultedPasswordHash = selectCommand.ExecuteScalar();

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
                throw new DatabaseException("Er is op dit moment een probleem met de database.", exception);
            }
            return passwordHash;
        }

        public int CreateAccount(AccountDTO accountDTO)
        {
            int accountID;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(SQLDatabaseRepository.GetConnectionString()))
                {
                    string query = @"INSERT INTO [Account](FirstName, Mail, Password) Values (@0,@1,@2); SELECT SCOPE_IDENTITY();";

                    sqlConnection.Open();
                    SqlCommand insertCommand = new SqlCommand(query, sqlConnection);

                    insertCommand.Parameters.AddWithValue("0", accountDTO.FirstName);
                    insertCommand.Parameters.AddWithValue("1", accountDTO.Mail);
                    insertCommand.Parameters.AddWithValue("2", accountDTO.Password);
                    accountID = Convert.ToInt32(insertCommand.ExecuteScalar());
                }
            }
            catch (SqlException exception)
            {
                throw new DatabaseException("Er is op dit moment een probleem met de database.", exception);
            }
            return accountID;
        }
    }
}
