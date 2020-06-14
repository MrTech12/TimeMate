using DataAccessLayer.DTO;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DataAccessLayer.Containers
{
    public class SQLNormalAppointmentContainer : INormalAppointmentContainer
    {
        private SQLDatabaseContainer SQLDatabaseContainer = new SQLDatabaseContainer();

        public void AddDescription(AppointmentDTO appointmentDTO)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(SQLDatabaseContainer.GetConnectionString()))
                {
                    string query = @"INSERT INTO [Appointment_Description](AppointmentID, Description) VALUES (@0, @1)";

                    sqlConnection.Open();
                    SqlCommand insertQuery = new SqlCommand(query, sqlConnection);

                    insertQuery.Parameters.AddWithValue("0", appointmentDTO.AppointmentID);
                    insertQuery.Parameters.AddWithValue("1", appointmentDTO.DescriptionDTO.Description);
                    insertQuery.ExecuteNonQuery();
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
                using (SqlConnection sqlConnection = new SqlConnection(SQLDatabaseContainer.GetConnectionString()))
                {
                    string query = @"SELECT Description FROM [Appointment_Description] WHERE AppointmentID = @0";

                    sqlConnection.Open();
                    SqlCommand selectQuery = new SqlCommand(query, sqlConnection);

                    selectQuery.Parameters.AddWithValue("0", appointmentID);
                    description = Convert.ToString(selectQuery.ExecuteScalar());
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
