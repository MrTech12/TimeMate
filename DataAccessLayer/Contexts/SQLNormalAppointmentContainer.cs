using DataAccessLayer.DTO;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DataAccessLayer.Contexts
{
    public class SQLNormalAppointmentContainer : INormalAppointmentContainer
    {
        private SQLDatabaseContainer SQLDatabaseContext = new SQLDatabaseContainer();

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

        public string GetDescription(int appointmentID)
        {
            string description;
            try
            {
                using (SqlConnection databaseConn = new SqlConnection(SQLDatabaseContext.GetConnection()))
                {
                    databaseConn.Open();
                    SqlCommand selectQuerry = new SqlCommand("SELECT Details FROM [Appointment_Details] WHERE AppointmentID = @0", databaseConn);

                    selectQuerry.Parameters.AddWithValue("0", appointmentID);

                    description = Convert.ToString(selectQuerry.ExecuteScalar());
                }
            }
            catch (SqlException exception)
            {
                throw new Exception("Er is op dit moment een probleem met de database.", exception);
            }
            return description;
        }
    }
}
