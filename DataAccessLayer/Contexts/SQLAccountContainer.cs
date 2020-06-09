using DataAccessLayer.DTO;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace DataAccessLayer.Contexts
{
    public class SQLAccountContainer : IAccountContainer
    {
        private SQLDatabaseContainer SQLDatabaseContext = new SQLDatabaseContainer();
        private string databaseOutput;

        /// <summary>
        /// Get the account ID from the database.
        /// </summary>
        public string GetUserID(string mail)
        {
            try
            {
                using (SqlConnection databaseConn = new SqlConnection(SQLDatabaseContext.GetConnection()))
                {
                    databaseConn.Open();
                    SqlCommand selectQuerry = new SqlCommand(@"SELECT AccountID FROM [Account] WHERE Mail = @0", databaseConn);

                    selectQuerry.Parameters.AddWithValue("0", mail);
                    var resultedAccountID = selectQuerry.ExecuteScalar();

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

        /// <summary>
        /// Get the password hash from the database.
        /// </summary>
        public string SearchForPasswordHash(string mail)
        {
            try
            {
                using (SqlConnection databaseConn = new SqlConnection(SQLDatabaseContext.GetConnection()))
                {
                    databaseConn.Open();
                    SqlCommand selectQuerry = new SqlCommand(@"SELECT Password FROM [Account] WHERE Mail = @0", databaseConn);

                    selectQuerry.Parameters.AddWithValue("0", mail);
                    var resultedPasswordHash = selectQuerry.ExecuteScalar();

                    if (resultedPasswordHash == null)
                    {
                        databaseOutput = null;
                    }
                    else
                    {
                        databaseOutput = resultedPasswordHash.ToString();
                    }
                }
            }
            catch (SqlException exception)
            {
                throw new Exception("Er is op dit moment een probleem met de database.", exception);
            }
            return databaseOutput;
        }

        /// <summary>
        /// Insert an account into the database.
        /// </summary>
        public int CreateAccount(AccountDTO accountDTO)
        {
            int accountID;
            try
            {
                using (SqlConnection databaseConn = new SqlConnection(SQLDatabaseContext.GetConnection()))
                {
                    databaseConn.Open();
                    SqlCommand insertQuerry = new SqlCommand(@"INSERT INTO [Account](First_name, Mail, Password) 
                                                            Values (@0,@1,@2); SELECT SCOPE_IDENTITY();", databaseConn);

                    insertQuerry.Parameters.AddWithValue("0", accountDTO.FirstName);
                    insertQuerry.Parameters.AddWithValue("1", accountDTO.Mail);
                    insertQuerry.Parameters.AddWithValue("2", accountDTO.Password);

                    accountID = Convert.ToInt32(insertQuerry.ExecuteScalar());
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
