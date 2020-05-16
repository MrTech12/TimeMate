using DataAccessLayer.DTO;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DataAccessLayer.Contexts
{
    public class SQLChecklistAppointmentContext : IChecklistAppointmentContext
    {
        private SQLDatabaseContext SQLDatabaseContext = new SQLDatabaseContext();

        /// <summary>
        /// Add task(s) of an appointment to the db.
        /// </summary>
        /// <param name="appointmentDTO"></param>
        public void AddTask(AppointmentDTO appointmentDTO)
        {
            try
            {
                using (SqlConnection databaseConn = new SqlConnection(SQLDatabaseContext.GetConnection()))
                {
                    databaseConn.Open();

                    SqlCommand insertQuerry = new SqlCommand("INSERT INTO [Task](AppointmentID, Task_name, Task_checked) VALUES (@0, @1, @2)", databaseConn);

                    insertQuerry.Parameters.AddWithValue("0", appointmentDTO.AppointmentID);
                    insertQuerry.Parameters.AddWithValue("2", false);

                    foreach (var item in appointmentDTO.ChecklistItemName)
                    {
                        insertQuerry.Parameters.AddWithValue("1", item);
                        insertQuerry.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException)
            {
                //Display the error.
                throw;
            }
        }

        /// <summary>
        /// Check off a task in the db.
        /// </summary>
        public void CheckOffTask(AppointmentDTO appointmentDTO)
        {
            throw new NotImplementedException();
        }
    }
}
