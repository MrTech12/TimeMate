using DataAccessLayer.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Interfaces
{
    public interface IAgendaRepository
    {
        int AddAgenda(int accountID, AgendaDTO agendaDTO);

        void DeleteAgenda(int accountID, int agendaID);

        List<AgendaDTO> GetAllAgendas(int accountID);

        int GetAgendaID(string agendaName, int accountID);
    }
}
