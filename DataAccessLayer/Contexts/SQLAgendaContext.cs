﻿using DataAccessLayer.DTO;
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
        /// Add a new agenda into the database.
        /// </summary>
        /// <returns></returns>
        public int AddNewAgenda(AgendaDTO agendaDTO, AccountDTO accountDTO)
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
        /// Add a new agenda for work into the database.
        /// </summary>
        /// <returns></returns>
        public void AddNewJobAgenda(AgendaDTO newAgendaDTO, AccountDTO accountDTO)
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
                            SqlCommand insertQuerry = new SqlCommand(@"INSERT INTO [Job](AgendaID, Hourly_wage_buss, Hourly_wage_week, Allowed_hours) 
                                                                    Values (@0,@1,@2,@3)", databaseConn);
                            insertQuerry.Parameters.AddWithValue("0", newAgendaDTO.AgendaID);
                            insertQuerry.Parameters.AddWithValue("1", accountDTO.JobHourlyWage[i]);
                            insertQuerry.Parameters.AddWithValue("2", "0.00");
                            insertQuerry.Parameters.AddWithValue("3", accountDTO.AllocatedHours);
                            insertQuerry.ExecuteNonQuery();
                        }
                        else if (accountDTO.JobDayType[i] == "Weekend" && accountDTO.JobHourlyWage[i] != 0)
                        {
                            SqlCommand insertQuerry = new SqlCommand(@"INSERT INTO [Job](AgendaID, Hourly_wage_buss, Hourly_wage_week, Allowed_hours) 
                                                                    Values (@0,@1,@2,@3)", databaseConn);
                            insertQuerry.Parameters.AddWithValue("0", newAgendaDTO.AgendaID);
                            insertQuerry.Parameters.AddWithValue("1", "0.00");
                            insertQuerry.Parameters.AddWithValue("2", accountDTO.JobHourlyWage[i]);
                            insertQuerry.Parameters.AddWithValue("3", accountDTO.AllocatedHours);
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
        /// Get all the agenda names that the account has.
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
        /// Get the agendaID of an agenda.
        /// </summary>
        public int GetAgendaID(string agendaNameInput, AccountDTO accountDTO)
        {
            int AgendaID;
            try
            {
                using (SqlConnection databaseConn = new SqlConnection(SQLDatabaseContext.GetConnection()))
                {
                    databaseConn.Open();
                    SqlCommand insertQuerry = new SqlCommand(@"SELECT AgendaID FROM [Agenda] WHERE AccountID = @0 AND Name = @1", databaseConn);

                    insertQuerry.Parameters.AddWithValue("0", accountDTO.AccountID);
                    insertQuerry.Parameters.AddWithValue("1", agendaNameInput);

                    var result = insertQuerry.ExecuteScalar();
                    AgendaID = Convert.ToInt32(result); //Store agendaID.
                }
            }
            catch (SqlException)
            {
                //Display the error.
                throw;
            }
            return AgendaID;
        }

        /// <summary>
        /// Get all appointments of the user, from the database.
        /// </summary>
        /// <returns></returns>
        public List<AppointmentDTO> GetAllAppointments(AccountDTO accountDTO)
        {
            List<AppointmentDTO> AppointmentsFromAccount = new List<AppointmentDTO>();

            try
            {
                using (SqlConnection databaseConn = new SqlConnection(SQLDatabaseContext.GetConnection()))
                {
                    databaseConn.Open();
                    SqlCommand insertQuerry = new SqlCommand
                        (@"SELECT Appointment.Name, Appointment.Starting, Appointment.Ending, Agenda.Name AS AgendaName, 
                        Agenda.AgendaID AS AgendaID FROM [Appointment] INNER JOIN Agenda ON Appointment.AgendaID = Agenda.AgendaID 
                        AND Agenda.AccountID = @0", databaseConn);

                    insertQuerry.Parameters.AddWithValue("0", accountDTO.AccountID);
                    SqlDataReader dataReader = insertQuerry.ExecuteReader();

                    while (dataReader.Read())
                    {
                        AppointmentDTO appointmentModel = new AppointmentDTO();
                        appointmentModel.AppointmentName = dataReader["Name"].ToString();
                        appointmentModel.StartDate = Convert.ToDateTime(dataReader["Starting"]);
                        appointmentModel.EndDate = Convert.ToDateTime(dataReader["Ending"]);
                        appointmentModel.AgendaName = dataReader["AgendaName"].ToString();
                        appointmentModel.AgendaID = Convert.ToInt32(dataReader["AgendaID"]);
                        AppointmentsFromAccount.Add(appointmentModel);
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }
            return AppointmentsFromAccount;
        }

        public List<DateTime> GetWorkdayHours(int agendaIndex)
        {
            throw new NotImplementedException();
        }

        public List<DateTime> GetWeekendHours(int agendaIndex)
        {
            throw new NotImplementedException();
        }
    }
}
