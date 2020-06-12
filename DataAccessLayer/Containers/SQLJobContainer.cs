using DataAccessLayer.DTO;
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
                using (SqlConnection databaseConn = new SqlConnection(SQLDatabaseContainer.GetConnectionString()))
                {
                    string query = @"INSERT INTO [Job](AccountID, HourlyWageBuss, HourlyWageWeek) Values (@0,@1,@2)";

                    databaseConn.Open();
                    SqlCommand insertQuery = new SqlCommand(query, databaseConn);

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
                throw new Exception("Er is op dit moment een probleem met de database.", exception);
            }
        }

        /// <summary>
        /// Get the pay for business days from the database.
        /// </summary>
        public double GetWorkdayPay(AccountDTO accountDTO, JobDTO jobDTO)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get the pay for the weekend from the database.
        /// </summary>
        public double GetWeekendPay(AccountDTO accountDTO, JobDTO jobDTO)
        {
            throw new NotImplementedException();
        }
    }
}
