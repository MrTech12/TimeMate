using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DataAccessLayer.DTO;
using DataAccessLayer.Interfaces;

namespace TimeMateTest.Stubs
{
    class StubAgendaContext : IAgendaContext
    {
        public int AddAgenda(AgendaDTO agendaDTO, AccountDTO accountDTO)
        {
            int agendaID = 0;

            using (StreamWriter streamWriter = new StreamWriter("C:\\tmp\\addAgendaTest.txt"))
            {
                streamWriter.WriteLine(agendaDTO.AgendaName);
                streamWriter.WriteLine(agendaDTO.AgendaColor);
                streamWriter.WriteLine(agendaDTO.Notification);
            }          
            return agendaID;
        }

        public void AddPayDetails(AgendaDTO agendaDTO, AccountDTO accountDTO)
        {
            using (StreamWriter streamWriter = new StreamWriter("C:\\tmp\\addWorkAgendaTest.txt"))
            {
                streamWriter.WriteLine(agendaDTO.AgendaName);
                streamWriter.WriteLine(agendaDTO.AgendaColor);
                streamWriter.WriteLine(agendaDTO.Notification);
            }
        }

        public void DeleteAgenda(int AgendaID, AccountDTO accountDTO)
        {
            if (AgendaID == 51 && accountDTO.AccountID == 12)
            {
                File.WriteAllText("C:\\tmp\\removeAgendaTest.txt", String.Empty);
            }
        }

        public List<AgendaDTO> GetAllAgendas(AccountDTO accountDTO)
        {
            List<AgendaDTO> agendaNames = new List<AgendaDTO>();
            if (accountDTO.AccountID == 12)
            {
                AgendaDTO agenda1 = new AgendaDTO();
                agenda1.AgendaID = 0;
                agenda1.AgendaName = "Work";
                agendaNames.Add(agenda1);

                AgendaDTO agenda2 = new AgendaDTO();
                agenda2.AgendaID = 1;
                agenda2.AgendaName = "Personal";
                agendaNames.Add(agenda2);
            }

            return agendaNames;
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
