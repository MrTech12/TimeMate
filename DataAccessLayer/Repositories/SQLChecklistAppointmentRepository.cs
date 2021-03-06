using Core.Entities;
using Core.Errors;
using Core.Repositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DataAccessLayer.Repositories
{
    public class SQLChecklistAppointmentRepository : IChecklistAppointmentRepository
    {
        private SQLDatabaseRepository SQLDatabaseRepository = new SQLDatabaseRepository();

        public void CreateTask(List<Task> tasks)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(SQLDatabaseRepository.GetConnectionString()))
                {
                    string query = @"INSERT INTO [Appointment_Task](AppointmentID, TaskName, TaskChecked) VALUES (@0, @1, @2)";

                    sqlConnection.Open();
                    SqlCommand insertCommand = new SqlCommand(query, sqlConnection);

                    foreach (var item in tasks)
                    {
                        insertCommand.Parameters.Clear();
                        insertCommand.Parameters.AddWithValue("0", item.AppointmentID);
                        insertCommand.Parameters.AddWithValue("1", item.TaskName);
                        insertCommand.Parameters.AddWithValue("2", false);
                        insertCommand.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException exception)
            {
                throw new DatabaseException("Er is op dit moment een probleem met de database.", exception);
            }
        }

        public Dictionary<int, string> GetTasks(int appointmentID)
        {
            Dictionary<int, string> tasksDict = new Dictionary<int, string>();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(SQLDatabaseRepository.GetConnectionString()))
                {
                    string query = @"SELECT TaskID, TaskName FROM [Appointment_Task] WHERE AppointmentID = @0";

                    sqlConnection.Open();
                    SqlCommand selectCommand = new SqlCommand(query, sqlConnection);

                    selectCommand.Parameters.AddWithValue("0", appointmentID);
                    SqlDataReader dataReader = selectCommand.ExecuteReader();

                    while (dataReader.Read())
                    {
                        tasksDict.Add(Convert.ToInt32(dataReader["TaskID"]), dataReader["TaskName"].ToString());
                    }
                }
            }
            catch (SqlException exception)
            {
                throw new DatabaseException("Er is op dit moment een probleem met de database.", exception);
            }
            return tasksDict;
        }

        public bool GetTaskStatus(int taskID)
        {
            bool taskStatus;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(SQLDatabaseRepository.GetConnectionString()))
                {
                    string query = @"SELECT TaskChecked FROM [Appointment_Task] WHERE TaskID = @0";

                    sqlConnection.Open();
                    SqlCommand selectCommand = new SqlCommand(query, sqlConnection);

                    selectCommand.Parameters.AddWithValue("0", taskID);
                    taskStatus = Convert.ToBoolean(selectCommand.ExecuteScalar());
                }
            }
            catch (SqlException exception)
            {
                throw new DatabaseException("Er is op dit moment een probleem met de database.", exception);
            }
            return taskStatus;
        }

        public void UpdateTaskStatus(int taskID, bool status)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(SQLDatabaseRepository.GetConnectionString()))
                {
                    string query = @"UPDATE [Appointment_Task] SET TaskChecked=@0 WHERE TaskID = @1";

                    sqlConnection.Open();
                    SqlCommand updateCommand = new SqlCommand(query, sqlConnection);

                    updateCommand.Parameters.AddWithValue("0", status);
                    updateCommand.Parameters.AddWithValue("1", taskID);
                    updateCommand.ExecuteScalar();
                }
            }
            catch (SqlException exception)
            {
                throw new DatabaseException("Er is op dit moment een probleem met de database.", exception);
            }
        }
    }
}
