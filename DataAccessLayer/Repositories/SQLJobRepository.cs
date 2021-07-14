using Core.Entities;
using Core.Errors;
using Core.Repositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DataAccessLayer.Repositories
{
    public class SQLJobRepository : IJobRepository
    {
        private SQLDatabaseRepository SQLDatabaseRepository = new SQLDatabaseRepository();

        public void CreatePayDetails(Account account)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(SQLDatabaseRepository.GetConnectionString()))
                {
                    string query = @"INSERT INTO [Job](AccountID, HourlyWageBuss, HourlyWageWeek) Values (@0,@1,@2)";

                    sqlConnection.Open();
                    SqlCommand insertCommand = new SqlCommand(query, sqlConnection);

                    for (int i = 0; i < account.JobHourlyWage.Count; i++)
                    {
                        insertCommand.Parameters.Clear();
                        insertCommand.Parameters.AddWithValue("0", account.AccountID);

                        if (account.JobDayType[i] == "Doordeweeks" && account.JobHourlyWage[i] != 0)
                        {
                            insertCommand.Parameters.AddWithValue("1", account.JobHourlyWage[i]);
                            insertCommand.Parameters.AddWithValue("2", DBNull.Value);
                        }
                        else if (account.JobDayType[i] == "Weekend" && account.JobHourlyWage[i] != 0)
                        {
                            insertCommand.Parameters.AddWithValue("1", DBNull.Value);
                            insertCommand.Parameters.AddWithValue("2", account.JobHourlyWage[i]);
                        }
                        insertCommand.ExecuteNonQuery();
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
            double payWage = 0;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(SQLDatabaseRepository.GetConnectionString()))
                {
                    string query = @"SELECT HourlyWageBuss FROM [Job] WHERE AccountID = @0";

                    sqlConnection.Open();
                    SqlCommand selectCommand = new SqlCommand(query, sqlConnection);

                    selectCommand.Parameters.AddWithValue("0", accountID);
                    SqlDataReader dataReader = selectCommand.ExecuteReader();

                    while (dataReader.Read())
                    {
                        if (dataReader["HourlyWageBuss"] != DBNull.Value)
                        {
                            payWage = Convert.ToDouble(dataReader["HourlyWageBuss"]);
                        }
                    }
                }
            }
            catch (SqlException exception)
            {
                throw new DatabaseException("Er is op dit moment een probleem met de database.", exception);
            }
            return payWage;
        }

        public double GetWeekendPayWage(int accountID)
        {
            double payWage = 0;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(SQLDatabaseRepository.GetConnectionString()))
                {
                    string query = @"SELECT HourlyWageWeek FROM [Job] WHERE AccountID = @0";

                    sqlConnection.Open();
                    SqlCommand selectCommand = new SqlCommand(query, sqlConnection);

                    selectCommand.Parameters.AddWithValue("0", accountID);
                    SqlDataReader dataReader = selectCommand.ExecuteReader();

                    while (dataReader.Read())
                    {
                        if (dataReader["HourlyWageWeek"] != DBNull.Value)
                        {
                            payWage = Convert.ToDouble(dataReader["HourlyWageWeek"]);
                        }
                    }
                }
            }
            catch (SqlException exception)
            {
                throw new DatabaseException("Er is op dit moment een probleem met de database.", exception);
            }
            return payWage;
        }
    }
}
