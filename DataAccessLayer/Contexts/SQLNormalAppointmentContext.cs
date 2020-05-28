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
        /// Add a description of an appointment to the db.
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
                    insertQuerry.Parameters.AddWithValue("1", appointmentDTO.Description);

                    insertQuerry.ExecuteNonQuery();
                }
            }
            catch (SqlException)
            {
                //Display the error.
                throw;
            }
        }

        public string GetDescription(int appointmentID)
        {
            string description;
            try
            {
                using (SqlConnection databaseConn = new SqlConnection(SQLDatabaseContext.GetConnection()))
                {
                    databaseConn.Open();
                    SqlCommand insertQuerry = new SqlCommand(@"SELECT Details FROM [Appointment_Details] WHERE AppointmentID = @0", databaseConn);

                    insertQuerry.Parameters.AddWithValue("0", appointmentID);

                    description = Convert.ToString(insertQuerry.ExecuteScalar());
                }
            }
            catch (SqlException)
            {
                //Display the error.
                throw;
            }
            return description;
        }
    }
}
