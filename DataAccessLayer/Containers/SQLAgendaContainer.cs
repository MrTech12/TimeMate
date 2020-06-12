using DataAccessLayer.DTO;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DataAccessLayer.Containers
{
    public class SQLAgendaContainer : IAgendaContainer
    {
        private SQLDatabaseContainer SQLDatabaseContainer = new SQLDatabaseContainer();

        public int AddAgenda(int accountID, AgendaDTO agendaDTO)
        {
            int agendaID;
            try
            {
                using (SqlConnection databaseConn = new SqlConnection(SQLDatabaseContainer.GetConnectionString()))
                {
                    string query = @"INSERT INTO [Agenda](AccountID, Name, Color, NotificationType) VALUES (@0,@1,@2,@3); SELECT SCOPE_IDENTITY();";
                    
                    databaseConn.Open();
                    SqlCommand insertQuery = new SqlCommand(query, databaseConn);

                    insertQuery.Parameters.AddWithValue("0", accountID);
                    insertQuery.Parameters.AddWithValue("1", agendaDTO.AgendaName);
                    insertQuery.Parameters.AddWithValue("2", agendaDTO.AgendaColor);
                    insertQuery.Parameters.AddWithValue("3", agendaDTO.NotificationType);
                    agendaID = Convert.ToInt32(insertQuery.ExecuteScalar());
                }
            }
            catch (SqlException exception)
            {
                throw new Exception("Er is op dit moment een probleem met de database.", exception);
            }
            return agendaID;
        }

        public void DeleteAgenda(int accountID, int agendaID)
        {
            try
            {
                using (SqlConnection databaseConn = new SqlConnection(SQLDatabaseContainer.GetConnectionString()))
                {
                    string query = @"DELETE FROM [Agenda] WHERE AgendaID = @0 AND AccountID = @1";

                    databaseConn.Open();
                    SqlCommand deleteQuery = new SqlCommand(query, databaseConn);

                    deleteQuery.Parameters.AddWithValue("0", agendaID);
                    deleteQuery.Parameters.AddWithValue("1", accountID);
                    deleteQuery.ExecuteNonQuery();
                }
            }
            catch (SqlException exception)
            {
                throw new Exception("Er is op dit moment een probleem met de database.", exception);
            }
        }

        public List<AgendaDTO> GetAllAgendas(int accountID)
        {
            List<AgendaDTO> agendas = new List<AgendaDTO>();
            try
            {
                using (SqlConnection databaseConn = new SqlConnection(SQLDatabaseContainer.GetConnectionString()))
                {
                    string query = @"SELECT a.* FROM [Agenda] a WHERE AccountID = @0";

                    databaseConn.Open();
                    SqlCommand insertQuery = new SqlCommand(query, databaseConn);

                    insertQuery.Parameters.AddWithValue("0", accountID);
                    SqlDataReader dataReader = insertQuery.ExecuteReader();

                    while (dataReader.Read())
                    {
                        AgendaDTO agendaDTO = new AgendaDTO();
                        agendaDTO.AgendaID = Convert.ToInt32(dataReader["AgendaID"]);
                        agendaDTO.AgendaName = dataReader["Name"].ToString();
                        agendaDTO.AgendaColor = dataReader["Color"].ToString();
                        agendas.Add(agendaDTO);
                    }
                }
            }
            catch (SqlException exception)
            {
                throw new Exception("Er is op dit moment een probleem met de database.", exception);
            }
            return agendas;
        }

        public string GetAgendaID(string agendaName, int accountID)
        {
            string agendaID;
            try
            {
                using (SqlConnection databaseConn = new SqlConnection(SQLDatabaseContainer.GetConnectionString()))
                {
                    string query = @"SELECT AgendaID FROM [Agenda] WHERE Name = @0 AND AccountID = @1";

                    databaseConn.Open();
                    SqlCommand selectQuery = new SqlCommand(query, databaseConn);

                    selectQuery.Parameters.AddWithValue("0", agendaName);
                    selectQuery.Parameters.AddWithValue("1", accountID);
                    var resultedAgendaID = selectQuery.ExecuteScalar();

                    if (resultedAgendaID == null)
                    {
                        agendaID = null;
                    }
                    else
                    {
                        agendaID = resultedAgendaID.ToString();
                    }
                }
            }
            catch (SqlException exception)
            {
                throw new Exception("Er is op dit moment een probleem met de database.", exception);
            }
            return agendaID;
        }
    }
}
