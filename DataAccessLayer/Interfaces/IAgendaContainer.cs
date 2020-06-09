using DataAccessLayer.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Interfaces
{
    public interface IAgendaContainer
    {
        int AddAgenda(AgendaDTO agendaDTO, int accountID);

        void AddPayDetails(int agendaID, AccountDTO accountDTO);

        void DeleteAgenda(int agendaID, int accountID);

        List<AgendaDTO> GetAllAgendas(int accountID);

        List<DateTime> GetWorkdayHours(int agendaIndex);

        List<DateTime> GetWeekendHours(int agendaIndex);
    }
}
