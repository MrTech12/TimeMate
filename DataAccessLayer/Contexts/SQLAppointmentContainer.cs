using DataAccessLayer.DTO;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DataAccessLayer.Contexts
{
    public class SQLAppointmentContainer : IAppointmentContainer
    {
        private SQLDatabaseContainer SQLDatabaseContainer = new SQLDatabaseContainer();

        public int AddAppointment(AppointmentDTO appointmentDTO)
        {
            int appointmentID;
            try
            {
                using (SqlConnection databaseConn = new SqlConnection(SQLDatabaseContainer.GetConnectionString()))
                {
                    string query = @"INSERT INTO [Appointment](AgendaID, Name, Starting, Ending) VALUES (@0, @1, @2, @3); SELECT SCOPE_IDENTITY();";

                    databaseConn.Open();
                    SqlCommand insertQuery = new SqlCommand(query, databaseConn);

                    insertQuery.Parameters.AddWithValue("0", appointmentDTO.AgendaID);
                    insertQuery.Parameters.AddWithValue("1", appointmentDTO.AppointmentName);
                    insertQuery.Parameters.AddWithValue("2", appointmentDTO.StartDate);
                    insertQuery.Parameters.AddWithValue("3", appointmentDTO.EndDate);
                    appointmentID = Convert.ToInt32(insertQuery.ExecuteScalar());
                }
            }
            catch (SqlException exception)
            {
                throw new Exception("Er is op dit moment een probleem met de database.", exception);
            }
            return appointmentID;
        }

        public List<AppointmentDTO> GetAppointments(int accountID)
        {
            List<AppointmentDTO> appointments = new List<AppointmentDTO>();
            try
            {
                using (SqlConnection databaseConn = new SqlConnection(SQLDatabaseContainer.GetConnectionString()))
                {
                    string query = @"SELECT app.*, Agenda.Name AS AgendaName, description.*, task.* FROM [Appointment] app
                        LEFT JOIN Appointment_Description description ON app.AppointmentID = description.AppointmentID
                        LEFT JOIN Appointment_Task task ON app.AppointmentID = task.AppointmentID
                        INNER JOIN Agenda ON app.AgendaID = Agenda.AgendaID
                        AND Agenda.AccountID = @0";

                    databaseConn.Open();
                    SqlCommand insertQuery = new SqlCommand(query, databaseConn);

                    insertQuery.Parameters.AddWithValue("0", accountID);
                    SqlDataReader dataReader = insertQuery.ExecuteReader();

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

                        if (dataReader["Description"] != DBNull.Value)
                        {
                            appointmentModel.DescriptionDTO.Description = dataReader["Description"].ToString();
                        }
                        else if (dataReader["TaskID"] != DBNull.Value)
                        {
                            checklistDTO.TaskID = Convert.ToInt32(dataReader["TaskID"]);
                            checklistDTO.AppointmentID = Convert.ToInt32(dataReader["AppointmentID"]);
                            checklistDTO.TaskName = dataReader["TaskName"].ToString();
                            checklistDTO.TaskChecked = Convert.ToBoolean(dataReader["TaskChecked"]);
                        }
                        appointmentModel.ChecklistDTOs.Add(checklistDTO);
                        appointments.Add(appointmentModel);
                    }
                }
            }
            catch (SqlException exception)
            {
                throw new Exception("Er is op dit moment een probleem met de database.", exception);
            }
            return appointments;
        }
    }
}
