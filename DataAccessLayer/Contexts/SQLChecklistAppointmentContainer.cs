using DataAccessLayer.DTO;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DataAccessLayer.Contexts
{
    public class SQLChecklistAppointmentContainer : IChecklistAppointmentContainer
    {
        private SQLDatabaseContainer SQLDatabaseContext = new SQLDatabaseContainer();

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
            catch (SqlException exception)
            {
                throw new Exception("Er is op dit moment een probleem met de database.", exception);
            }
        }

        public List<ChecklistDTO> GetTasks(int appointmentID)
        {
            List<ChecklistDTO> checklists = new List<ChecklistDTO>();
            try
            {
                using (SqlConnection databaseConn = new SqlConnection(SQLDatabaseContext.GetConnection()))
                {
                    databaseConn.Open();

                    SqlCommand selectQuerry = new SqlCommand(@"SELECT TaskID, Task_name FROM [Task] WHERE AppointmentID = @0", databaseConn);

                    selectQuerry.Parameters.AddWithValue("0", appointmentID);

                    SqlDataReader dataReader = selectQuerry.ExecuteReader();

                    while (dataReader.Read())
                    {
                        ChecklistDTO checklistDTO = new ChecklistDTO();
                        checklistDTO.TaskID = Convert.ToInt32(dataReader["TaskID"]);
                        checklistDTO.TaskName = dataReader["Task_name"].ToString();
                        checklists.Add(checklistDTO);
                    }
                }
            }
            catch (SqlException exception)
            {
                throw new Exception("Er is op dit moment een probleem met de database.", exception);
            }
            return checklists;
        }

        public bool GetTaskStatus(int taskID)
        {
            bool taskStatus = false;
            try
            {
                using (SqlConnection databaseConn = new SqlConnection(SQLDatabaseContext.GetConnection()))
                {
                    databaseConn.Open();

                    SqlCommand selectQuerry = new SqlCommand(@"SELECT Task_checked FROM [Task] WHERE TaskID = @0", databaseConn);

                    selectQuerry.Parameters.AddWithValue("0", taskID);

                    taskStatus = Convert.ToBoolean(selectQuerry.ExecuteScalar());
                }
            }
            catch (SqlException exception)
            {
                throw new Exception("Er is op dit moment een probleem met de database.", exception);
            }
            return taskStatus;
        }

        /// <summary>
        /// Check off a task in the db.
        /// </summary>
        public void CheckOffTask(int taskID)
        {
            try
            {
                using (SqlConnection databaseConn = new SqlConnection(SQLDatabaseContext.GetConnection()))
                {
                    databaseConn.Open();

                    SqlCommand updateQuerry = new SqlCommand(@"UPDATE [Task] SET Task_checked=@0 WHERE TaskID = @1", databaseConn);

                    updateQuerry.Parameters.AddWithValue("0", true);
                    updateQuerry.Parameters.AddWithValue("1", taskID);

                    updateQuerry.ExecuteScalar();
                }
            }
            catch (SqlException exception)
            {
                throw new Exception("Er is op dit moment een probleem met de database.", exception);
            }
        }

        public void RevertCheckOffTask(int taskID)
        {
            try
            {
                using (SqlConnection databaseConn = new SqlConnection(SQLDatabaseContext.GetConnection()))
                {
                    databaseConn.Open();

                    SqlCommand updateQuerry = new SqlCommand(@"UPDATE [Task] SET Task_checked=@0 WHERE TaskID = @1", databaseConn);

                    updateQuerry.Parameters.AddWithValue("0", false);
                    updateQuerry.Parameters.AddWithValue("1", taskID);

                    updateQuerry.ExecuteScalar();
                }
            }
            catch (SqlException exception)
            {
                throw new Exception("Er is op dit moment een probleem met de database.", exception);
            }
        }
    }
}
