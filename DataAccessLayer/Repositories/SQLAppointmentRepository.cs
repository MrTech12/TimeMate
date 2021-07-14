using Core.DTOs;
using Core.Entities;
using Core.Errors;
using Core.Repositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DataAccessLayer.Repositories
{
    public class SQLAppointmentRepository : IAppointmentRepository
    {
        private SQLDatabaseRepository SQLDatabaseRepository = new SQLDatabaseRepository();

        public int CreateAppointment(Appointment appointment)
        {
            int appointmentID;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(SQLDatabaseRepository.GetConnectionString()))
                {
                    string query = @"INSERT INTO [Appointment](AgendaID, Name, Starting, Ending) VALUES (@0, @1, @2, @3); SELECT SCOPE_IDENTITY();";

                    sqlConnection.Open();
                    SqlCommand insertCommand = new SqlCommand(query, sqlConnection);

                    insertCommand.Parameters.AddWithValue("0", appointment.AgendaID);
                    insertCommand.Parameters.AddWithValue("1", appointment.AppointmentName);
                    insertCommand.Parameters.AddWithValue("2", appointment.StartDate);
                    insertCommand.Parameters.AddWithValue("3", appointment.EndDate);
                    appointmentID = Convert.ToInt32(insertCommand.ExecuteScalar());
                }
            }
            catch (SqlException exception)
            {
                throw new DatabaseException("Er is op dit moment een probleem met de database.", exception);
            }
            return appointmentID;
        }

        public List<AppointmentDTO> GetAppointments(int accountID)
        {
            List<AppointmentDTO> appointments = new List<AppointmentDTO>();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(SQLDatabaseRepository.GetConnectionString()))
                {
                    string query = @"SELECT app.*, Agenda.Name AS AgendaName, description.*, task.* FROM [Appointment] app
                        LEFT JOIN Appointment_Description description ON app.AppointmentID = description.AppointmentID
                        LEFT JOIN Appointment_Task task ON app.AppointmentID = task.AppointmentID
                        INNER JOIN Agenda ON app.AgendaID = Agenda.AgendaID
                        AND Agenda.AccountID = @0";

                    sqlConnection.Open();
                    SqlCommand selectCommand = new SqlCommand(query, sqlConnection);

                    selectCommand.Parameters.AddWithValue("0", accountID);
                    SqlDataReader dataReader = selectCommand.ExecuteReader();

                    while (dataReader.Read())
                    {
                        AppointmentDTO appointmentModel = new AppointmentDTO();
                        TaskDTO taskDTO = new TaskDTO();
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
                            taskDTO.TaskID = Convert.ToInt32(dataReader["TaskID"]);
                            taskDTO.AppointmentID = Convert.ToInt32(dataReader["AppointmentID"]);
                            taskDTO.TaskName = dataReader["TaskName"].ToString();
                            taskDTO.TaskChecked = Convert.ToBoolean(dataReader["TaskChecked"]);
                            appointmentModel.TaskList.Add(taskDTO);
                        }
                        appointments.Add(appointmentModel);
                    }
                }
            }
            catch (SqlException exception)
            {
                throw new DatabaseException("Er is op dit moment een probleem met de database.", exception);
            }
            return appointments;
        }

        public Job GetWorkHours(int agendaID, List<DateTime> dates)
        {
            Job job = new Job();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(SQLDatabaseRepository.GetConnectionString()))
                {
                    string query = @"SELECT Starting, Ending FROM [Appointment] WHERE AgendaID = @0 
                                    AND Starting >= @1 AND Ending <= @2";

                    sqlConnection.Open();
                    SqlCommand selectCommand = new SqlCommand(query, sqlConnection);

                    selectCommand.Parameters.AddWithValue("0", agendaID);
                    selectCommand.Parameters.AddWithValue("1", dates[0]);
                    selectCommand.Parameters.AddWithValue("2", dates[1]);
                    SqlDataReader dataReader = selectCommand.ExecuteReader();

                    while (dataReader.Read())
                    {
                        job.StartDate.Add(Convert.ToDateTime(dataReader["Starting"]));
                        job.EndDate.Add(Convert.ToDateTime(dataReader["Ending"]));
                    }
                }
            }
            catch (SqlException exception)
            {
                throw new DatabaseException("Er is op dit moment een probleem met de database.", exception);
            }
            return job;
        }
    }
}
