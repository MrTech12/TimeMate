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
        private string dbConnectionstring = "Server = mssql.fhict.local; Database=dbi400050; User Id = dbi400050; Password = EuAIC1a!jcW2Hwn$";
        private SqlCommand deleteQuerry;

        private AgendaDTO agendaDTO = new AgendaDTO();

        private List<string> agendaNames = new List<string>();

        /// <summary>
        /// Add a new agenda into the database.
        /// </summary>
        /// <returns></returns>
        public void AddNewAgenda(AgendaDTO agendaDTO, AccountDTO accountDTO)
        {
            try
            {
                using (SqlConnection databaseConn = new SqlConnection(dbConnectionstring))
                {
                    databaseConn.Open();

                    SqlCommand insertQuerry;
                    insertQuerry = new SqlCommand("INSERT INTO [Agenda](AccountID, Name, Color, Notification_type, Enabled)  VALUES (@0,@1,@2,@3,@4)", databaseConn);

                    insertQuerry.Parameters.AddWithValue("0", accountDTO.AccountID);
                    insertQuerry.Parameters.AddWithValue("1", agendaDTO.AgendaName);
                    insertQuerry.Parameters.AddWithValue("2", agendaDTO.AgendaColor);
                    insertQuerry.Parameters.AddWithValue("3", agendaDTO.Notification);
                    insertQuerry.Parameters.AddWithValue("4", true);

                    insertQuerry.ExecuteNonQuery();
                }
            }
            catch (SqlException)
            {
                //Display the error.
                throw;
            }
        }

        /// <summary>
        /// Add a new agenda for work into the database.
        /// </summary>
        /// <returns></returns>
        public void AddNewJobAgenda(AgendaDTO newAgendaDTO, AccountDTO accountDTO)
        {
            try
            {
                using (SqlConnection databaseConn = new SqlConnection(dbConnectionstring))
                {
                    databaseConn.Open();

                    SqlCommand insertQuerry;
                    insertQuerry = new SqlCommand("INSERT INTO [Agenda](AccountID, Name, Color, Enabled) Values (@0,@1,@2,@3)", databaseConn);
                    insertQuerry.Parameters.AddWithValue("0", accountDTO.AccountID);
                    insertQuerry.Parameters.AddWithValue("1", newAgendaDTO.AgendaName);
                    insertQuerry.Parameters.AddWithValue("2", newAgendaDTO.AgendaColor);
                    insertQuerry.Parameters.AddWithValue("3", true);
                    insertQuerry.ExecuteNonQuery();

                    SqlCommand selectQuerry;
                    selectQuerry = new SqlCommand("SELECT AgendaID FROM [Agenda] WHERE AccountID = @0", databaseConn);
                    selectQuerry.Parameters.AddWithValue("0", accountDTO.AccountID);
                    var resultedAgendaID = selectQuerry.ExecuteScalar();

                    if (accountDTO.Job1DayType == "Doordeweeks")
                    {
                        insertQuerry = new SqlCommand("INSERT INTO [Job](AgendaID, Hourly_wage_buss, Hourly_wage_week, Allowed_hours) Values (@0,@1,@2,@3)", databaseConn);
                        insertQuerry.Parameters.AddWithValue("0", resultedAgendaID);
                        insertQuerry.Parameters.AddWithValue("1", accountDTO.Job1HourlyWage);
                        insertQuerry.Parameters.AddWithValue("2", "0.00");
                        insertQuerry.Parameters.AddWithValue("3", accountDTO.AllocatedHours);
                        insertQuerry.ExecuteNonQuery();
                    }
                    else if (accountDTO.Job1DayType == "Weekend")
                    {
                        insertQuerry = new SqlCommand("INSERT INTO [Job](AgendaID, Hourly_wage_buss, Hourly_wage_week, Allowed_hours) Values (@0,@1,@2,@3)", databaseConn);
                        insertQuerry.Parameters.AddWithValue("0", resultedAgendaID);
                        insertQuerry.Parameters.AddWithValue("1", "0.00");
                        insertQuerry.Parameters.AddWithValue("2", accountDTO.Job1HourlyWage);
                        insertQuerry.Parameters.AddWithValue("3", accountDTO.AllocatedHours);
                        insertQuerry.ExecuteNonQuery();
                    }
                    else if (accountDTO.Job2DayType == "Doordeweeks")
                    {
                        insertQuerry = new SqlCommand("INSERT INTO [Job](AgendaID, Hourly_wage_buss, Hourly_wage_week, Allowed_hours) Values (@0,@1,@2,@3)", databaseConn);
                        insertQuerry.Parameters.AddWithValue("0", resultedAgendaID);
                        insertQuerry.Parameters.AddWithValue("1", accountDTO.Job2HourlyWage);
                        insertQuerry.Parameters.AddWithValue("2", "0.00");
                        insertQuerry.Parameters.AddWithValue("3", accountDTO.AllocatedHours);
                        insertQuerry.ExecuteNonQuery();
                    }
                    else if (accountDTO.Job2DayType == "Weekend")
                    {
                        insertQuerry = new SqlCommand("INSERT INTO [Job](AgendaID, Hourly_wage_buss, Hourly_wage_week, Allowed_hours) Values (@0,@1,@2,@3)", databaseConn);
                        insertQuerry.Parameters.AddWithValue("0", resultedAgendaID);
                        insertQuerry.Parameters.AddWithValue("1", "0.00");
                        insertQuerry.Parameters.AddWithValue("2", accountDTO.Job2HourlyWage);
                        insertQuerry.Parameters.AddWithValue("3", accountDTO.AllocatedHours);
                        insertQuerry.ExecuteNonQuery();
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
                using (SqlConnection databaseConn = new SqlConnection(dbConnectionstring))
                {
                    databaseConn.Open();

                    deleteQuerry = new SqlCommand("DELETE FROM [Agenda] WHERE AgendaID = @0 AND AccountID = @1", databaseConn);

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
        public List<string> GetAgendaNamesFromDB(AccountDTO accountDTO)
        {
            try
            {
                using (SqlConnection databaseConn = new SqlConnection(dbConnectionstring))
                {
                    databaseConn.Open();

                    SqlCommand insertQuerry;
                    insertQuerry = new SqlCommand("SELECT Name FROM [Agenda] WHERE AccountID = @0", databaseConn);

                    insertQuerry.Parameters.AddWithValue("0", accountDTO.AccountID);

                    SqlDataReader dataReader = insertQuerry.ExecuteReader();

                    while (dataReader.Read())
                    {
                        for (int i = 0; i < dataReader.FieldCount; i++)
                        {
                            agendaNames.Add(dataReader.GetValue(i).ToString());
                        }
                    }
                }
            }
            catch (SqlException)
            {
                //Display the error.
                throw;
            }
            return agendaNames;
        }

        /// <summary>
        /// Get the agendaID of an account.
        /// </summary>
        public int GetAgendaID(string agendaNameInput, AccountDTO accountDTO)
        {
            int AgendaID;
            try
            {
                using (SqlConnection databaseConn = new SqlConnection(dbConnectionstring))
                {
                    databaseConn.Open();

                    SqlCommand insertQuerry;
                    insertQuerry = new SqlCommand("SELECT AgendaID FROM [Agenda] WHERE AccountID = @0 AND Name = @1", databaseConn);

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
                using (SqlConnection databaseConn = new SqlConnection(dbConnectionstring))
                {
                    databaseConn.Open();

                    SqlCommand insertQuerry;
                    insertQuerry = new SqlCommand
                        ("SELECT Normal_Appointment.Name, Normal_Appointment.Starting," +
                        "Normal_Appointment.Ending, Normal_Appointment.Details, Agenda.Name AS AgendaName " +
                        "FROM [Normal_Appointment]" +
                        "INNER JOIN Agenda ON Normal_Appointment.AgendaID = Agenda.AgendaID " +
                        "AND Agenda.AccountID = @0; ", databaseConn);

                    insertQuerry.Parameters.AddWithValue("0", accountDTO.AccountID);
                    SqlDataReader dataReader = insertQuerry.ExecuteReader();

                    while (dataReader.Read())
                    {
                        AppointmentDTO appointmentModel = new AppointmentDTO();
                        appointmentModel.AppointmentName = dataReader["Name"].ToString();
                        appointmentModel.StartDate = Convert.ToDateTime(dataReader["Starting"]);
                        appointmentModel.EndDate = Convert.ToDateTime(dataReader["Ending"]);
                        appointmentModel.AgendaName = Convert.ToString(dataReader["AgendaName"]);
                        appointmentModel.Description = Convert.ToString(dataReader["Details"]);
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
