﻿using DataAccessLayer.DTO;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DataAccessLayer.Contexts
{
    public class SQLNormalAppointmentContext : INormalAppointmentContext
    {
        private readonly string sqlConnection;
        private readonly SQLDatabaseContext SQLDatabaseContext;

        public SQLNormalAppointmentContext(SQLDatabaseContext sqlDatabaseContext)
        {
            sqlConnection = sqlDatabaseContext.ConnectiongString;
        }

        /// <summary>
        /// Add a normal appointment to an agenda.
        /// </summary>
        public void AddNormalAppointment(AppointmentDTO appointmentDTO, int agendaIndex)
        {
            try
            {
                using (SqlConnection databaseConn = new SqlConnection(sqlConnection))
                {
                    databaseConn.Open();
                    SqlCommand insertQuerry = new SqlCommand("INSERT INTO [Normal_Appointment](AgendaID, Name, Starting, Ending, Details) VALUES (@0, @1, @2, @3, @4)", databaseConn);

                    insertQuerry.Parameters.AddWithValue("0", agendaIndex);
                    insertQuerry.Parameters.AddWithValue("1", appointmentDTO.AppointmentName);
                    insertQuerry.Parameters.AddWithValue("2", appointmentDTO.StartDate);
                    insertQuerry.Parameters.AddWithValue("3", appointmentDTO.EndDate);
                    insertQuerry.Parameters.AddWithValue("4", appointmentDTO.Description);

                    insertQuerry.ExecuteNonQuery();
                }
            }
            catch (SqlException)
            {
                //Display the error.
                throw;
            }
        }

        public void DeleteNormalAppointment(int normalAppointmentIndex, int agendaIndex)
        {
            throw new NotImplementedException();
        }

        public int GetNormalAppointmentID(AppointmentDTO appointmentDTO, int agendaIndex)
        {
            throw new NotImplementedException();
        }

        public void RenameNormalAppointment(int normalAppointmentIndex, int agendaIndex)
        {
            throw new NotImplementedException();
        }
    }
}
