using Model.DTO_s;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Error;

namespace DataAccessLayer.Repositories
{
    public class SQLAgendaRepository : IAgendaRepository
    {
        private SQLDatabaseRepository SQLDatabaseRepository = new SQLDatabaseRepository();

        public int CreateAgenda(int accountID, AgendaDTO agendaDTO)
        {
            int agendaID;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(SQLDatabaseRepository.GetConnectionString()))
                {
                    string query = @"INSERT INTO [Agenda](AccountID, Name, Color, NotificationType) VALUES (@0,@1,@2,@3); SELECT SCOPE_IDENTITY();";
                    
                    sqlConnection.Open();
                    SqlCommand insertCommand = new SqlCommand(query, sqlConnection);

                    insertCommand.Parameters.AddWithValue("0", accountID);
                    insertCommand.Parameters.AddWithValue("1", agendaDTO.AgendaName);
                    insertCommand.Parameters.AddWithValue("2", agendaDTO.AgendaColor);
                    insertCommand.Parameters.AddWithValue("3", agendaDTO.NotificationType);
                    agendaID = Convert.ToInt32(insertCommand.ExecuteScalar());
                }
            }
            catch (SqlException exception)
            {
                throw new DatabaseException("Er is op dit moment een probleem met de database.", exception);
            }
            return agendaID;
        }

        public void DeleteAgenda(int accountID, int agendaID)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(SQLDatabaseRepository.GetConnectionString()))
                {
                    string query = @"DELETE FROM [Agenda] WHERE AgendaID = @0 AND AccountID = @1";

                    sqlConnection.Open();
                    SqlCommand deleteCommand = new SqlCommand(query, sqlConnection);

                    deleteCommand.Parameters.AddWithValue("0", agendaID);
                    deleteCommand.Parameters.AddWithValue("1", accountID);
                    deleteCommand.ExecuteNonQuery();
                }
            }
            catch (SqlException exception)
            {
                throw new DatabaseException("Er is op dit moment een probleem met de database.", exception);
            }
        }

        public List<AgendaDTO> GetAgendas(int accountID)
        {
            List<AgendaDTO> agendas = new List<AgendaDTO>();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(SQLDatabaseRepository.GetConnectionString()))
                {
                    string query = @"SELECT a.* FROM [Agenda] a WHERE AccountID = @0";

                    sqlConnection.Open();
                    SqlCommand selectCommand = new SqlCommand(query, sqlConnection);

                    selectCommand.Parameters.AddWithValue("0", accountID);
                    SqlDataReader dataReader = selectCommand.ExecuteReader();

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
                throw new DatabaseException("Er is op dit moment een probleem met de database.", exception);
            }
            return agendas;
        }

        public int GetAgendaID(string agendaName, int accountID)
        {
            int agendaID;
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(SQLDatabaseRepository.GetConnectionString()))
                {
                    string query = @"SELECT AgendaID FROM [Agenda] WHERE Name = @0 AND AccountID = @1";

                    sqlConnection.Open();
                    SqlCommand selectCommand = new SqlCommand(query, sqlConnection);

                    selectCommand.Parameters.AddWithValue("0", agendaName);
                    selectCommand.Parameters.AddWithValue("1", accountID);
                    var resultedAgendaID = selectCommand.ExecuteScalar();

                    if (resultedAgendaID == null)
                    {
                        agendaID = -1;
                    }
                    else
                    {
                        agendaID = Convert.ToInt32(resultedAgendaID);
                    }
                }
            }
            catch (SqlException exception)
            {
                throw new DatabaseException("Er is op dit moment een probleem met de database.", exception);
            }
            return agendaID;
        }
    }
}
