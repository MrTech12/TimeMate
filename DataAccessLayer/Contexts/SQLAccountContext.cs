using Dapper;
using DataAccessLayer.DTO;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;

namespace DataAccessLayer.Contexts
{
    public class SQLAccountContext : IAccountContext
    {
        private string databaseOutput;

        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(ConfigurationManager.ConnectionStrings["fontysMS"].ConnectionString);
            }
        }

        /// <summary>
        /// Getting the account ID from the database.
        /// </summary>
        public int GetUserID(string mail)
        {
            int accountID;

            try
            {
                using (IDbConnection databaseConn = Connection)
                {
                    string querry = "SELECT AccountID FROM [Account] WHERE Mail = @mail";
                    databaseConn.Open();
                    var resultedAccountID = databaseConn.Query(querry, new { mail = mail });
                    accountID = Convert.ToInt32(resultedAccountID); //Storing the accountID.
                }
            }
            catch (SqlException)
            {
                //Display the error.
                throw;
            }
            return accountID;
        }

        /// <summary>
        /// Getting the password hash from the database.
        /// </summary>
        public string SearchForPasswordHash(string mail)
        {
            try
            {
                using (IDbConnection databaseConn = Connection)
                {
                    string querry = "SELECT Password FROM [Account] WHERE Mail = @mail";
                    databaseConn.Open();
                    var resultedPasswordHash = databaseConn.Query(querry, new { mail = mail });

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
            catch (SqlException)
            {
                throw;
            }
            return databaseOutput;
        }

        /// <summary>
        /// Search for a mailaddress in the database.
        /// </summary>
        public string SearchUserByMail(string mail)
        {
            try
            {
                using (IDbConnection databaseConn = Connection)
                {
                    string querry = "SELECT AccountID FROM [Account] WHERE Mail = @mail";
                    databaseConn.Open();
                    var resultedAccountID = databaseConn.Query(querry, new { mail = mail });

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
            catch (SqlException)
            {
                //Display the error.
                throw;
            }
            return databaseOutput;
        }

        /// <summary>
        /// Inserting a new account into the database.
        /// </summary>
        /// <returns></returns>
        public void RegisterNewUser(AccountDTO accountDTO)
        {
            try
            {
                using (IDbConnection databaseConn = Connection)
                {
                    string querry = "INSERT INTO [Account](First_name, Mail, Password) Values (@FirstName,@MailAddress,@Password)";
                    databaseConn.Open();
                    var resultedAccountID = databaseConn.Execute(querry, accountDTO);
                }
            }
            catch (SqlException)
            {
                //Display the error.
                throw;
            }
        }
    }
}
