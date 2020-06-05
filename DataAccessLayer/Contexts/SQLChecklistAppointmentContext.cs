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
        /// Add task(s) of an appointment to the database.
        /// </summary>
        public void AddTask(AppointmentDTO appointmentDTO)
        {
            try
            {
                using (SqlConnection databaseConn = new SqlConnection(SQLDatabaseContext.GetConnection()))
                {
                    databaseConn.Open();

                    SqlCommand insertQuerry = new SqlCommand(@"INSERT INTO [Task](AppointmentID, Task_name, Task_checked) VALUES (@0, @1, @2)", databaseConn);

                    foreach (var item in appointmentDTO.ChecklistDTOs)
                    {
                        insertQuerry.Parameters.Clear();
                        insertQuerry.Parameters.AddWithValue("0", appointmentDTO.AppointmentID);
                        insertQuerry.Parameters.AddWithValue("1", item.TaskName);
                        insertQuerry.Parameters.AddWithValue("2", false);
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

        public List<ChecklistDTO> GetTasks(AppointmentDTO appointmentDTO)
        {
            List<ChecklistDTO> checklists = new List<ChecklistDTO>();
            try
            {
                using (SqlConnection databaseConn = new SqlConnection(SQLDatabaseContext.GetConnection()))
                {
                    databaseConn.Open();

                    SqlCommand insertQuerry = new SqlCommand(@"SELECT TaskID, Task_name FROM [Task] WHERE AppointmentID = @0", databaseConn);

                    insertQuerry.Parameters.AddWithValue("0", appointmentDTO.AppointmentID);

                    SqlDataReader dataReader = insertQuerry.ExecuteReader();

                    while (dataReader.Read())
                    {
                        ChecklistDTO checklistDTO = new ChecklistDTO();
                        checklistDTO.TaskID = Convert.ToInt32(dataReader["TaskID"]);
                        checklistDTO.TaskName = dataReader["Task_name"].ToString();
                        checklists.Add(checklistDTO);
                    }
                }
            }
            catch (SqlException)
            {
                //Display the error.
                throw;
            }
            return checklists;
        }

        public bool GetTaskStatus(int taskID)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Check off a task in the db.
        /// </summary>
        public void CheckOffTask(int taskID)
        {
            throw new NotImplementedException();
        }

        public void RevertCheckOffTask(int taskID)
        {
            throw new NotImplementedException();
        }
    }
}
