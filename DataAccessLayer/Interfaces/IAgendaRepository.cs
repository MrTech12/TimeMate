using Model.DTO_s;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Interfaces
{
    public interface IAgendaRepository
    {
        int CreateAgenda(int accountID, AgendaDTO agendaDTO);

        void DeleteAgenda(int accountID, int agendaID);

        List<AgendaDTO> GetAgendas(int accountID);

        int GetWorkAgendaID(int accountID);
    }
}
