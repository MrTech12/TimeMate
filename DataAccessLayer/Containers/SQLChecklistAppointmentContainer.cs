using DataAccessLayer.DTO;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DataAccessLayer.Containers
{
    public class SQLChecklistAppointmentContainer : IChecklistAppointmentContainer
    {
        private SQLDatabaseContainer SQLDatabaseContainer = new SQLDatabaseContainer();

        public void AddTask(AppointmentDTO appointmentDTO)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(SQLDatabaseContainer.GetConnectionString()))
                {
                    string query = @"INSERT INTO [Appointment_Task](AppointmentID, TaskName, TaskChecked) VALUES (@0, @1, @2)";

                    sqlConnection.Open();
                    SqlCommand insertQuery = new SqlCommand(query, sqlConnection);

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
                using (SqlConnection sqlConnection = new SqlConnection(SQLDatabaseContainer.GetConnectionString()))
                {
                    string query = @"SELECT TaskID, TaskName FROM [Appointment_Task] WHERE AppointmentID = @0";

                    sqlConnection.Open();
                    SqlCommand selectQuery = new SqlCommand(query, sqlConnection);

                    selectQuery.Parameters.AddWithValue("0", appointmentID);
                    SqlDataReader dataReader = selectQuery.ExecuteReader();

                    while (dataReader.Read())
                    {
                        ChecklistDTO checklistDTO = new ChecklistDTO();
                        checklistDTO.TaskID = Convert.ToInt32(dataReader["TaskID"]);
                        checklistDTO.TaskName = dataReader["TaskName"].ToString();
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
                using (SqlConnection sqlConnection = new SqlConnection(SQLDatabaseContainer.GetConnectionString()))
                {
                    string query = @"SELECT TaskChecked FROM [Appointment_Task] WHERE TaskID = @0";

                    sqlConnection.Open();
                    SqlCommand selectQuery = new SqlCommand(query, sqlConnection);

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

        public void CheckOffTask(int taskID, bool status)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(SQLDatabaseContainer.GetConnectionString()))
                {
                    string query = @"UPDATE [Appointment_Task] SET TaskChecked=@0 WHERE TaskID = @1";

                    sqlConnection.Open();
                    SqlCommand updateQuery = new SqlCommand(query, sqlConnection);

                    updateQuery.Parameters.AddWithValue("0", status);
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
