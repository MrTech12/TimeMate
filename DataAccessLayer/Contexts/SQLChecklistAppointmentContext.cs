﻿using DataAccessLayer.DTO;
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
        public void AddTask(int appointmentID, string taskName)
        {
            try
            {
                using (SqlConnection databaseConn = new SqlConnection(SQLDatabaseContext.GetConnection()))
                {
                    databaseConn.Open();

                    SqlCommand insertQuerry = new SqlCommand("INSERT INTO [Task](AppointmentID, Task_name, Task_checked) VALUES (@0, @1, @2)", databaseConn);

                    insertQuerry.Parameters.AddWithValue("0", appointmentID);
                    insertQuerry.Parameters.AddWithValue("1", taskName);
                    insertQuerry.Parameters.AddWithValue("2", false);
                    insertQuerry.ExecuteNonQuery();
                }
            }
            catch (SqlException)
            {
                //Display the error.
                throw;
            }
        }

        public AppointmentDTO GetTask(int appointmentIndex)
        {
            AppointmentDTO appointmentDTO = new AppointmentDTO();
            try
            {
                using (SqlConnection databaseConn = new SqlConnection(SQLDatabaseContext.GetConnection()))
                {
                    databaseConn.Open();

                    SqlCommand insertQuerry = new SqlCommand("SELECT Task_name, Task_checked FROM [Task] WHERE AppointmentID = @0", databaseConn);

                    insertQuerry.Parameters.AddWithValue("0", appointmentIndex);

                    SqlDataReader dataReader = insertQuerry.ExecuteReader();

                    while (dataReader.Read())
                    {
                        AppointmentDTO appointmentModel = new AppointmentDTO();
                        string taskName = dataReader["Task_name"].ToString();
                        bool taskComplete = Convert.ToBoolean(dataReader["Task_checked"]);
                        appointmentDTO.ChecklistItemName.Add(taskName);
                        appointmentDTO.ChecklistItemChecked.Add(taskComplete);
                    }
                }
            }
            catch (SqlException)
            {
                //Display the error.
                throw;
            }
            return appointmentDTO;
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
