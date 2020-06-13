using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DataAccessLayer.DTO;
using DataAccessLayer.Interfaces;

namespace TimeMateTest.Stubs
{
    class StubAgendaContainer : IAgendaContainer
    {
        public int AddAgenda(int accountID, AgendaDTO agendaDTO)
        {
            int agendaID = 0;
            using (StreamWriter streamWriter = new StreamWriter(@"C:\tmp\addAgendaTest.txt"))
            {
                streamWriter.WriteLine(agendaDTO.AgendaName);
                streamWriter.WriteLine(agendaDTO.AgendaColor);
                streamWriter.WriteLine(agendaDTO.NotificationType);
            }          
            return agendaID;
        }

        public void DeleteAgenda(int accountID, int agendaID)
        {
            if (agendaID == 51 && accountID == 12)
            {
                File.WriteAllText(@"C:\tmp\removeAgendaTest.txt", String.Empty);
            }
        }

        public List<AgendaDTO> GetAllAgendas(int accountID)
        {
            List<AgendaDTO> agendaNames = new List<AgendaDTO>();
            if (accountID == 12)
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

        public int GetAgendaID(string agendaName, int accountID)
        {
            int agendaID = 0;
            if (accountID == 15 & agendaName == "Bijbaan")
            {
                agendaID = 1;
            }
            else if (accountID == 25 && agendaName == "Bijbaan")
            {
                agendaID = 2;
            }
            else if (accountID == 30 && agendaName == "Bijbaan")
            {
                agendaID = 3;
            }
            return agendaID;
        }
    }
}
