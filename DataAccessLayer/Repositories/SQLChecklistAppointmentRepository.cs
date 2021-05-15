using Model.DTO_s;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Error;

namespace DataAccessLayer.Repositories
{
    public class SQLChecklistAppointmentRepository : IChecklistAppointmentRepository
    {
        private SQLDatabaseRepository SQLDatabaseRepository = new SQLDatabaseRepository();

        public void CreateTask(AppointmentDTO appointmentDTO)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(SQLDatabaseRepository.GetConnectionString()))
                {
                    string query = @"INSERT INTO [Appointment_Task](AppointmentID, TaskName, TaskChecked) VALUES (@0, @1, @2)";

                    sqlConnection.Open();
                    SqlCommand insertCommand = new SqlCommand(query, sqlConnection);

                    foreach (var item in appointmentDTO.TaskList)
                    {
                        insertCommand.Parameters.Clear();
                        insertCommand.Parameters.AddWithValue("0", appointmentDTO.AppointmentID);
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

        public List<TaskDTO> GetTasks(int appointmentID)
        {
            List<TaskDTO> taskList = new List<TaskDTO>();
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
                        TaskDTO taskDTO = new TaskDTO();
                        taskDTO.TaskID = Convert.ToInt32(dataReader["TaskID"]);
                        taskDTO.TaskName = dataReader["TaskName"].ToString();
                        taskList.Add(taskDTO);
                    }
                }
            }
            catch (SqlException exception)
            {
                throw new DatabaseException("Er is op dit moment een probleem met de database.", exception);
            }
            return taskList;
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
