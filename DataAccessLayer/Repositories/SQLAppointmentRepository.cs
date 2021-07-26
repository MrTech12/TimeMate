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

        public List<Appointment> GetAppointments(int accountID)
        {
            List<Appointment> appointments = new List<Appointment>();
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
                        Appointment appointment = new Appointment();
                        Task task = new Task();
                        appointment.AppointmentID = Convert.ToInt32(dataReader["AppointmentID"]);
                        appointment.AppointmentName = dataReader["Name"].ToString();
                        appointment.StartDate = Convert.ToDateTime(dataReader["Starting"]);
                        appointment.EndDate = Convert.ToDateTime(dataReader["Ending"]);
                        appointment.AgendaName = dataReader["AgendaName"].ToString();
                        appointment.AgendaID = Convert.ToInt32(dataReader["AgendaID"]);

                        if (dataReader["Description"] != DBNull.Value)
                        {
                            appointment.Description.DescriptionName = dataReader["Description"].ToString();
                        }
                        else if (dataReader["TaskID"] != DBNull.Value)
                        {
                            task.TaskID = Convert.ToInt32(dataReader["TaskID"]);
                            task.AppointmentID = Convert.ToInt32(dataReader["AppointmentID"]);
                            task.TaskName = dataReader["TaskName"].ToString();
                            task.TaskChecked = Convert.ToBoolean(dataReader["TaskChecked"]);
                            appointment.TaskList.Add(task);
                        }
                        appointments.Add(appointment);
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
