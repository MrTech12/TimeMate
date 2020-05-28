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
        /// Add an appointment to the db.
        /// </summary>
        /// <param name="appointmentDTO"></param>
        /// <param name="agendaIndex"></param>
        /// <returns></returns>
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

        public int GetAppointmentID(AppointmentDTO appointmentDTO)
        {
            int appointmentID = 0;
            try
            {
                using (SqlConnection databaseConn = new SqlConnection(SQLDatabaseContext.GetConnection()))
                {
                    databaseConn.Open();
                    SqlCommand insertQuerry = new SqlCommand(@"SELECT AppointmentID FROM [Appointment] WHERE AgendaID = @0 AND 
                                                            (Name = @1 AND Starting = @2);", databaseConn);

                    insertQuerry.Parameters.AddWithValue("0", appointmentDTO.AgendaID);
                    insertQuerry.Parameters.AddWithValue("1", appointmentDTO.AppointmentName);
                    insertQuerry.Parameters.AddWithValue("2", appointmentDTO.StartDate);

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

        public void DeleteAppointment(int appointmentIndex, int agendaIndex)
        {
            throw new NotImplementedException();
        }

        public void RenameAppointment(int appointmentIndex, int agendaIndex)
        {
            throw new NotImplementedException();
        }
    }
}
