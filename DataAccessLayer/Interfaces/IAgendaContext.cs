using DataAccessLayer.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Interfaces
{
    public interface IAgendaContext
    {
        void AddNewAgenda(AgendaDTO agendaDTO, AccountDTO accountDTO);

        void AddNewJobAgenda(AgendaDTO agendaDTO, AccountDTO accountDTO);

        void RenameAgenda(int agendaIndex, AccountDTO accountDTO);

        void DeleteAgenda(int AgendaIndex, AccountDTO accountDTO);

        List<string> GetAgendaNamesFromDB(AccountDTO accountDTO);

        int GetAgendaID(string agendaName, AccountDTO accountDTO);

        List<AppointmentDTO> GetAllAppointments(AccountDTO accountDTO);

        List<DateTime> GetWorkdayHours(int agendaIndex);

        List<DateTime> GetWeekendHours(int agendaIndex);
    }
}
