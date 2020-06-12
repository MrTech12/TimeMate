using DataAccessLayer.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Interfaces
{
    public interface IAgendaContainer
    {
        int AddAgenda(int accountID, AgendaDTO agendaDTO);

        void DeleteAgenda(int accountID, int agendaID);

        List<AgendaDTO> GetAllAgendas(int accountID);

        List<DateTime> GetWorkdayHours(int agendaID);

        List<DateTime> GetWeekendHours(int agendaID);
    }
}
