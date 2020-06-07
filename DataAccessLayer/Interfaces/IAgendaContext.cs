using DataAccessLayer.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Interfaces
{
    public interface IAgendaContext
    {
        int AddAgenda(AgendaDTO agendaDTO, AccountDTO accountDTO);

        void AddPayDetails(AgendaDTO agendaDTO, AccountDTO accountDTO);

        void DeleteAgenda(int AgendaIndex, AccountDTO accountDTO);

        List<AgendaDTO> GetAllAgendas(AccountDTO accountDTO);

        List<DateTime> GetWorkdayHours(int agendaIndex);

        List<DateTime> GetWeekendHours(int agendaIndex);
    }
}
