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
        public int AddAppointment(AppointmentDTO appointmentDTO, int agendaIndex)
        {
            int appointmentID = 0;
            try
            {
                using (SqlConnection databaseConn = new SqlConnection(SQLDatabaseContext.GetConnection()))
                {
                    databaseConn.Open();
                    SqlCommand insertQuerry = new SqlCommand(@"INSERT INTO [Appointment](AgendaID, Name, Starting, Ending)
                                                            VALUES (@0, @1, @2, @3); SELECT SCOPE_IDENTITY();", databaseConn);

                    insertQuerry.Parameters.AddWithValue("0", agendaIndex);
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

        public int GetAppointmentID(AppointmentDTO appointmentDTO, int agendaIndex)
        {
            int appointmentID = 0;
            try
            {
                using (SqlConnection databaseConn = new SqlConnection(SQLDatabaseContext.GetConnection()))
                {
                    databaseConn.Open();
                    SqlCommand insertQuerry = new SqlCommand(@"SELECT AppointmentID FROM [Appointment] WHERE AgendaID = @0 AND 
                                                            (Name = @1 AND Starting = @2);", databaseConn);

                    insertQuerry.Parameters.AddWithValue("0", agendaIndex);
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
