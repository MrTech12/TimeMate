using System;
using System.Collections.Generic;
using System.Text;
using DataAccessLayer.DTO;
using DataAccessLayer.Interfaces;

namespace TimeMateTest.Stubs
{
    class StubAgendaContext : IAgendaContext
    {
        public int AddNewAgenda(AgendaDTO agendaDTO, AccountDTO accountDTO)
        {
            int agendaID = 0;
            if (accountDTO.AccountID == 12)
            {
                agendaDTO.AgendaID = 12;
            }
            return agendaID;
        }

        public void AddNewJobAgenda(AgendaDTO agendaDTO, AccountDTO accountDTO)
        {
            int jobID;
            if (agendaDTO.AgendaID == 12)
            {
                jobID = 12;
            }
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
            List<string> agendaNames = new List<string>();
            if (accountDTO.AccountID == 12)
            {
                agendaNames.Add("Work");
                agendaNames.Add("Personal");
            }

            return agendaNames;
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
