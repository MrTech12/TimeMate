using DataAccessLayer.DTO;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DataAccessLayer.Contexts
{
    public class SQLAgendaContext : IAgendaContext
    {
        private SQLDatabaseContext SQLDatabaseContext = new SQLDatabaseContext();

        /// <summary>
        /// Add an agenda into the database.
        /// </summary>
        /// <returns></returns>
        public int AddAgenda(AgendaDTO agendaDTO, AccountDTO accountDTO)
        {
            int agendaID = 0;
            try
            {
                using (SqlConnection databaseConn = new SqlConnection(SQLDatabaseContext.GetConnection()))
                {
                    databaseConn.Open();
                    SqlCommand insertQuerry = new SqlCommand(@"INSERT INTO [Agenda](AccountID, Name, Color, Notification_type) 
                                                            VALUES (@0,@1,@2,@3); SELECT SCOPE_IDENTITY();", databaseConn);

                    insertQuerry.Parameters.AddWithValue("0", accountDTO.AccountID);
                    insertQuerry.Parameters.AddWithValue("1", agendaDTO.AgendaName);
                    insertQuerry.Parameters.AddWithValue("2", agendaDTO.AgendaColor);
                    insertQuerry.Parameters.AddWithValue("3", agendaDTO.Notification);

                    agendaID = Convert.ToInt32(insertQuerry.ExecuteScalar());
                }
            }
            catch (SqlException)
            {
                //Display the error.
                throw;
            }
            return agendaID;
        }

        /// <summary>
        /// Add the pay details into the agenda.
        /// </summary>
        /// <returns></returns>
        public void AddPayDetails(AgendaDTO newAgendaDTO, AccountDTO accountDTO)
        {
            try
            {
                using (SqlConnection databaseConn = new SqlConnection(SQLDatabaseContext.GetConnection()))
                {
                    databaseConn.Open();

                    for (int i = 0; i < accountDTO.JobHourlyWage.Count; i++)
                    {
                        if (accountDTO.JobDayType[i] == "Doordeweeks" && accountDTO.JobHourlyWage[i] != 0)
                        {
                            SqlCommand insertQuerry = new SqlCommand(@"INSERT INTO [Job](AgendaID, Hourly_wage_buss, Hourly_wage_week) 
                                                                    Values (@0,@1,@2)", databaseConn);
                            insertQuerry.Parameters.AddWithValue("0", newAgendaDTO.AgendaID);
                            insertQuerry.Parameters.AddWithValue("1", accountDTO.JobHourlyWage[i]);
                            insertQuerry.Parameters.AddWithValue("2", "0.00");
                            insertQuerry.ExecuteNonQuery();
                        }
                        else if (accountDTO.JobDayType[i] == "Weekend" && accountDTO.JobHourlyWage[i] != 0)
                        {
                            SqlCommand insertQuerry = new SqlCommand(@"INSERT INTO [Job](AgendaID, Hourly_wage_buss, Hourly_wage_week) 
                                                                    Values (@0,@1,@2)", databaseConn);
                            insertQuerry.Parameters.AddWithValue("0", newAgendaDTO.AgendaID);
                            insertQuerry.Parameters.AddWithValue("1", "0.00");
                            insertQuerry.Parameters.AddWithValue("2", accountDTO.JobHourlyWage[i]);
                            insertQuerry.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }
        }

        /// <summary>
        /// Delete an agenda from the db.
        /// </summary>
        public void DeleteAgenda(int AgendaIndexInput, AccountDTO accountDTO)
        {
            try
            {
                using (SqlConnection databaseConn = new SqlConnection(SQLDatabaseContext.GetConnection()))
                {
                    databaseConn.Open();
                    SqlCommand deleteQuerry = new SqlCommand(@"DELETE FROM [Agenda] WHERE AgendaID = @0 AND AccountID = @1", databaseConn);

                    deleteQuerry.Parameters.AddWithValue("0", AgendaIndexInput);
                    deleteQuerry.Parameters.AddWithValue("1", accountDTO.AccountID);

                    deleteQuerry.ExecuteNonQuery();
                }
            }
            catch (SqlException)
            {
                throw;
            }
        }

        /// <summary>
        /// Rename an agenda from the db.
        /// </summary>
        public void RenameAgenda(int AgendaIndexInput, AccountDTO accountDTO)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get all the agenda details that the current account has.
        /// </summary>
        /// <returns></returns>
        public List<AgendaDTO> GetAllAgendas(AccountDTO accountDTO)
        {
            List<AgendaDTO> agendas = new List<AgendaDTO>();
            try
            {
                using (SqlConnection databaseConn = new SqlConnection(SQLDatabaseContext.GetConnection()))
                {
                    databaseConn.Open();
                    SqlCommand insertQuerry = new SqlCommand(@"SELECT a.* FROM [Agenda] a WHERE AccountID = @0", databaseConn);

                    insertQuerry.Parameters.AddWithValue("0", accountDTO.AccountID);

                    SqlDataReader dataReader = insertQuerry.ExecuteReader();

                    while (dataReader.Read())
                    {
                        AgendaDTO agendaDTO = new AgendaDTO();
                        agendaDTO.AgendaID = Convert.ToInt32(dataReader["AgendaID"]);
                        agendaDTO.AgendaName = dataReader["Name"].ToString();
                        agendas.Add(agendaDTO);
                    }
                }
            }
            catch (SqlException)
            {
                //Display the error.
                throw;
            }
            return agendas;
        }

        /// <summary>
        /// 
        /// </summary>
        public List<DateTime> GetWorkdayHours(int agendaIndex)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        public List<DateTime> GetWeekendHours(int agendaIndex)
        {
            throw new NotImplementedException();
        }
    }
}
