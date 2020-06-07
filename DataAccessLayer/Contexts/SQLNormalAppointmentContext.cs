using DataAccessLayer.DTO;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DataAccessLayer.Contexts
{
    public class SQLNormalAppointmentContext : INormalAppointmentContext
    {
        private SQLDatabaseContext SQLDatabaseContext = new SQLDatabaseContext();

        /// <summary>
        /// Add a description of an appointment to the database.
        /// </summary>
        /// <param name="appointmentDTO"></param>
        public void AddDescription(AppointmentDTO appointmentDTO)
        {
            try
            {
                using (SqlConnection databaseConn = new SqlConnection(SQLDatabaseContext.GetConnection()))
                {
                    databaseConn.Open();
                    SqlCommand insertQuerry = new SqlCommand(@"INSERT INTO [Appointment_Details](AppointmentID, Details) VALUES (@0, @1)", databaseConn);

                    insertQuerry.Parameters.AddWithValue("0", appointmentDTO.AppointmentID);
                    insertQuerry.Parameters.AddWithValue("1", appointmentDTO.DescriptionDTO.Description);

                    insertQuerry.ExecuteNonQuery();
                }
            }
            catch (SqlException exception)
            {
                throw new Exception("Er is op dit moment een probleem met de database.", exception);
            }
        }
    }
}
