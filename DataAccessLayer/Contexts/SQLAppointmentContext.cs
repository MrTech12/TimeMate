using DataAccessLayer.DTO;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DataAccessLayer.Contexts
{
    public class SQLAppointmentContext : IAppointmentContext
    {
        private SQLDatabaseContext SQLDatabaseContext = new SQLDatabaseContext();

        /// <summary>
        /// Add an appointment to the database.
        /// </summary>
        public int AddAppointment(AppointmentDTO appointmentDTO)
        {
            int appointmentID = 0;
            try
            {
                using (SqlConnection databaseConn = new SqlConnection(SQLDatabaseContext.GetConnection()))
                {
                    databaseConn.Open();
                    SqlCommand insertQuerry = new SqlCommand(@"INSERT INTO [Appointment](AgendaID, Name, Starting, Ending)
                                                            VALUES (@0, @1, @2, @3); SELECT SCOPE_IDENTITY();", databaseConn);

                    insertQuerry.Parameters.AddWithValue("0", appointmentDTO.AgendaID);
                    insertQuerry.Parameters.AddWithValue("1", appointmentDTO.AppointmentName);
                    insertQuerry.Parameters.AddWithValue("2", appointmentDTO.StartDate);
                    insertQuerry.Parameters.AddWithValue("3", appointmentDTO.EndDate);

                    appointmentID = Convert.ToInt32(insertQuerry.ExecuteScalar());
                }
            }
            catch (SqlException)
            {
                //Display the error.
                throw;
            }
            return appointmentID;
        }

        /// <summary>
        /// Get all appointments of the current account, from the database.
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
                        (@"SELECT app.*, Agenda.Name AS AgendaName, description.*, task.* FROM [Appointment] app
                        LEFT JOIN Appointment_Details description ON app.AppointmentID = description.AppointmentID
                        LEFT JOIN Task task ON app.AppointmentID = task.AppointmentID
                        INNER JOIN Agenda ON app.AgendaID = Agenda.AgendaID
                        AND Agenda.AccountID = @0", databaseConn);

                    insertQuerry.Parameters.AddWithValue("0", accountDTO.AccountID);
                    SqlDataReader dataReader = insertQuerry.ExecuteReader();

                    while (dataReader.Read())
                    {
                        AppointmentDTO appointmentModel = new AppointmentDTO();
                        ChecklistDTO checklistDTO = new ChecklistDTO();
                        appointmentModel.AppointmentID = Convert.ToInt32(dataReader["AppointmentID"]);
                        appointmentModel.AppointmentName = dataReader["Name"].ToString();
                        appointmentModel.StartDate = Convert.ToDateTime(dataReader["Starting"]);
                        appointmentModel.EndDate = Convert.ToDateTime(dataReader["Ending"]);
                        appointmentModel.AgendaName = dataReader["AgendaName"].ToString();
                        appointmentModel.AgendaID = Convert.ToInt32(dataReader["AgendaID"]);

                        if (dataReader["Details"] != DBNull.Value)
                        {
                            appointmentModel.DescriptionDTO.Description = dataReader["Details"].ToString();
                        }

                        if (dataReader["TaskID"] != DBNull.Value)
                        {
                            checklistDTO.TaskID = Convert.ToInt32(dataReader["TaskID"]);
                            checklistDTO.AppointmentID = Convert.ToInt32(dataReader["AppointmentID"]);
                            checklistDTO.TaskName = dataReader["Task_name"].ToString();
                            checklistDTO.TaskChecked = Convert.ToBoolean(dataReader["Task_checked"]);
                        }

                        appointmentModel.ChecklistDTOs.Add(checklistDTO);
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
    }
}
