using DataAccessLayer.DTO;
using DataAccessLayer.Exceptions;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DataAccessLayer.Repositories
{
    public class SQLNormalAppointmentRepository : INormalAppointmentRepository
    {
        private SQLDatabaseRepository SQLDatabaseRepository = new SQLDatabaseRepository();

        public void AddDescription(AppointmentDTO appointmentDTO)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(SQLDatabaseRepository.GetConnectionString()))
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
                throw new DatabaseException("Er is op dit moment een probleem met de database.", exception);
            }
        }

        public string GetDescription(int appointmentID)
        {
            string description;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(SQLDatabaseRepository.GetConnectionString()))
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
                throw new DatabaseException("Er is op dit moment een probleem met de database.", exception);
            }
            return description;
        }
    }
}
