using DataAccessLayer.DTO;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DataAccessLayer.Contexts
{
    public class SQLChecklistAppointmentContext : IChecklistAppointmentContext
    {
        private string databaseOutput;

        private SQLDatabaseContext SQLDatabaseContext = new SQLDatabaseContext();

        public void AddChecklistAppointment(AppointmentDTO appointmentDTO, int agendaIndex)
        {
            try
            {
                using (SqlConnection databaseConn = new SqlConnection(SQLDatabaseContext.GetConnection()))
                {
                    databaseConn.Open();
                    SqlCommand insertQuerry = new SqlCommand("INSERT INTO [Checklist_Appointment](AgendaID, Name, Starting, Ending) VALUES (@0, @1, @2, @3)", databaseConn);

                    insertQuerry.Parameters.AddWithValue("0", agendaIndex);
                    insertQuerry.Parameters.AddWithValue("1", appointmentDTO.AppointmentName);
                    insertQuerry.Parameters.AddWithValue("2", appointmentDTO.StartDate);
                    insertQuerry.Parameters.AddWithValue("3", appointmentDTO.EndDate);

                    insertQuerry.ExecuteNonQuery();

                    SqlCommand selectQuerry;
                    selectQuerry = new SqlCommand("SELECT CAppointmentID FROM [Checklist_Appointment] WHERE AgendaID = @0 AND Name = @1", databaseConn);
                    selectQuerry.Parameters.AddWithValue("0", agendaIndex);
                    selectQuerry.Parameters.AddWithValue("1", appointmentDTO.AppointmentName);
                    var resultedAppointmentID = selectQuerry.ExecuteScalar();

                    insertQuerry = new SqlCommand("INSERT INTO [Task](CAppointmentID, Task_name, Task_checked) VALUES (@0, @1, @2)", databaseConn);

                    foreach (var item in appointmentDTO.ChecklistItemName)
                    {
                        insertQuerry.Parameters.AddWithValue("0", resultedAppointmentID);
                        insertQuerry.Parameters.AddWithValue("1", item);
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

        public void CheckOffTask(AppointmentDTO appointmentDTO)
        {
            throw new NotImplementedException();
        }

        public void DeleteChecklistAppointment(int normalAppointmentIndex, int agendaIndex)
        {
            throw new NotImplementedException();
        }

        public int GetChecklistAppointmentID(AppointmentDTO appointmentDTO, int agendaIndex)
        {
            throw new NotImplementedException();
        }

        public void RenameChecklistAppointment(int normalAppointmentIndex, int agendaIndex)
        {
            throw new NotImplementedException();
        }
    }
}
