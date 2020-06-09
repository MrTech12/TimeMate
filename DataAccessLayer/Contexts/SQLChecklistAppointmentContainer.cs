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
        private SQLDatabaseContainer SQLDatabaseContainer = new SQLDatabaseContainer();

        public void AddTask(AppointmentDTO appointmentDTO)
        {
            try
            {
                using (SqlConnection databaseConn = new SqlConnection(SQLDatabaseContainer.GetConnectionString()))
                {
                    string query = @"INSERT INTO [Task](AppointmentID, Task_name, Task_checked) VALUES (@0, @1, @2)";

                    databaseConn.Open();
                    SqlCommand insertQuery = new SqlCommand(query, databaseConn);

                    foreach (var item in appointmentDTO.ChecklistDTOs)
                    {
                        insertQuery.Parameters.Clear();
                        insertQuery.Parameters.AddWithValue("0", appointmentDTO.AppointmentID);
                        insertQuery.Parameters.AddWithValue("1", item.TaskName);
                        insertQuery.Parameters.AddWithValue("2", false);
                        insertQuery.ExecuteNonQuery();
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
                using (SqlConnection databaseConn = new SqlConnection(SQLDatabaseContainer.GetConnectionString()))
                {
                    string query = @"SELECT TaskID, Task_name FROM [Task] WHERE AppointmentID = @0";

                    databaseConn.Open();
                    SqlCommand selectQuery = new SqlCommand(query, databaseConn);

                    selectQuery.Parameters.AddWithValue("0", appointmentID);
                    SqlDataReader dataReader = selectQuery.ExecuteReader();

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
            bool taskStatus;
            try
            {
                using (SqlConnection databaseConn = new SqlConnection(SQLDatabaseContainer.GetConnectionString()))
                {
                    string query = @"SELECT Task_checked FROM [Task] WHERE TaskID = @0";

                    databaseConn.Open();
                    SqlCommand selectQuery = new SqlCommand(query, databaseConn);

                    selectQuery.Parameters.AddWithValue("0", taskID);
                    taskStatus = Convert.ToBoolean(selectQuery.ExecuteScalar());
                }
            }
            catch (SqlException exception)
            {
                throw new Exception("Er is op dit moment een probleem met de database.", exception);
            }
            return taskStatus;
        }

        public void CheckOffTask(int taskID)
        {
            try
            {
                using (SqlConnection databaseConn = new SqlConnection(SQLDatabaseContainer.GetConnectionString()))
                {
                    string query = @"UPDATE [Task] SET Task_checked=@0 WHERE TaskID = @1";

                    databaseConn.Open();
                    SqlCommand updateQuery = new SqlCommand(query, databaseConn);

                    updateQuery.Parameters.AddWithValue("0", true);
                    updateQuery.Parameters.AddWithValue("1", taskID);
                    updateQuery.ExecuteScalar();
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
                using (SqlConnection databaseConn = new SqlConnection(SQLDatabaseContainer.GetConnectionString()))
                {
                    string querry = @"UPDATE [Task] SET Task_checked=@0 WHERE TaskID = @1";

                    databaseConn.Open();
                    SqlCommand updateQuery = new SqlCommand(querry, databaseConn);

                    updateQuery.Parameters.AddWithValue("0", false);
                    updateQuery.Parameters.AddWithValue("1", taskID);
                    updateQuery.ExecuteScalar();
                }
            }
            catch (SqlException exception)
            {
                throw new Exception("Er is op dit moment een probleem met de database.", exception);
            }
        }
    }
}
