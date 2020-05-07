using System;
using System.Collections.Generic;
using System.Text;
using DataAccessLayer.DTO;
using DataAccessLayer.Interfaces;

namespace TimeMateTest.Stubs
{
    class StubAgendaContext : IAgendaContext
    {
        public void AddNewAgenda(AgendaDTO agendaDTO, AccountDTO accountDTO)
        {
            throw new NotImplementedException();
        }

        public void AddNewJobAgenda(AgendaDTO agendaDTO, AccountDTO accountDTO)
        {
            throw new NotImplementedException();
        }

        public void DeleteAgenda(int AgendaIndex, AccountDTO accountDTO)
        {
            throw new NotImplementedException();
        }

        public int GetAgendaID(string agendaName, AccountDTO accountDTO)
        {
            throw new NotImplementedException();
        }

        public List<string> GetAgendaNamesFromDB(AccountDTO accountDTO)
        {
            throw new NotImplementedException();
        }

        public List<AppointmentDTO> GetAllAppointments(AccountDTO accountDTO)
        {
            throw new NotImplementedException();
        }

        public List<DateTime> GetWeekendHours(int agendaIndex)
        {
            throw new NotImplementedException();
        }

        public List<DateTime> GetWorkdayHours(int agendaIndex)
        {
            throw new NotImplementedException();
        }

        public void RenameAgenda(int agendaIndex, AccountDTO accountDTO)
        {
            throw new NotImplementedException();
        }
    }
}
