using DataAccessLayer.DTO;
using DataAccessLayer.Exceptions;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DataAccessLayer.Containers
{
    public class SQLJobContainer : IJobContainer
    {
        private SQLDatabaseContainer SQLDatabaseContainer = new SQLDatabaseContainer();

        public void AddPayDetails(AccountDTO accountDTO)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(SQLDatabaseContainer.GetConnectionString()))
                {
                    string query = @"INSERT INTO [Job](AccountID, HourlyWageBuss, HourlyWageWeek) Values (@0,@1,@2)";

                    sqlConnection.Open();
                    SqlCommand insertQuery = new SqlCommand(query, sqlConnection);

                    for (int i = 0; i < accountDTO.JobHourlyWage.Count; i++)
                    {
                        insertQuery.Parameters.Clear();
                        insertQuery.Parameters.AddWithValue("0", accountDTO.AccountID);

                        if (accountDTO.JobDayType[i] == "Doordeweeks" && accountDTO.JobHourlyWage[i] != 0)
                        {
                            insertQuery.Parameters.AddWithValue("1", accountDTO.JobHourlyWage[i]);
                            insertQuery.Parameters.AddWithValue("2", DBNull.Value);
                        }
                        else if (accountDTO.JobDayType[i] == "Weekend" && accountDTO.JobHourlyWage[i] != 0)
                        {
                            insertQuery.Parameters.AddWithValue("1", DBNull.Value);
                            insertQuery.Parameters.AddWithValue("2", accountDTO.JobHourlyWage[i]);
                        }
                        insertQuery.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException exception)
            {
                throw new DatabaseException("Er is op dit moment een probleem met de database.", exception);
            }
        }

        public double GetWorkdayPayWage(int accountID)
        {
            double wage = 0;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(SQLDatabaseContainer.GetConnectionString()))
                {
                    string query = @"SELECT HourlyWageBuss FROM [Job] WHERE AccountID = @0";

                    sqlConnection.Open();
                    SqlCommand selectQuery = new SqlCommand(query, sqlConnection);

                    selectQuery.Parameters.AddWithValue("0", accountID);
                    SqlDataReader dataReader = selectQuery.ExecuteReader();

                    while (dataReader.Read())
                    {
                        if (dataReader["HourlyWageBuss"] != DBNull.Value)
                        {
                            wage = Convert.ToDouble(dataReader["HourlyWageBuss"]);
                        }
                    }
                }
            }
            catch (SqlException exception)
            {
                throw new DatabaseException("Er is op dit moment een probleem met de database.", exception);
            }
            return wage;
        }

        public double GetWeekendPayWage(int accountID)
        {
            double wage = 0;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(SQLDatabaseContainer.GetConnectionString()))
                {
                    string query = @"SELECT HourlyWageWeek FROM [Job] WHERE AccountID = @0";

                    sqlConnection.Open();
                    SqlCommand selectQuery = new SqlCommand(query, sqlConnection);

                    selectQuery.Parameters.AddWithValue("0", accountID);
                    SqlDataReader dataReader = selectQuery.ExecuteReader();

                    while (dataReader.Read())
                    {
                        if (dataReader["HourlyWageWeek"] != DBNull.Value)
                        {
                            wage = Convert.ToDouble(dataReader["HourlyWageWeek"]);
                        }
                    }
                }
            }
            catch (SqlException exception)
            {
                throw new DatabaseException("Er is op dit moment een probleem met de database.", exception);
            }
            return wage;
        }
    }
}
