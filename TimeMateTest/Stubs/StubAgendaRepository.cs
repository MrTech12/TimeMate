using Core.Entities;
using Core.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TimeMateTest.Stubs
{
    class StubAgendaRepository : IAgendaRepository
    {
        public int CreateAgenda(Agenda agenda)
        {
            using (StreamWriter streamWriter = new StreamWriter(@"C:\tmp\addAgendaTest.txt"))
            {
                streamWriter.WriteLine(agenda.AgendaName);
                streamWriter.WriteLine(agenda.AgendaColor);
            }          
            return 0;
        }

        public void DeleteAgenda(Agenda agenda)
        {
            if (agenda.AgendaID == 51 && agenda.AccountID == 12)
            {
                File.WriteAllText(@"C:\tmp\removeAgendaTest.txt", String.Empty);
            }
        }

        public List<Agenda> GetAgendas(int accountID)
        {
            List<Agenda> agendaNames = new List<Agenda>();
            if (accountID == 12)
            {
                Agenda agenda1 = new Agenda();
                agenda1.AgendaID = 0;
                agenda1.AgendaName = "Work";
                agendaNames.Add(agenda1);

                Agenda agenda2 = new Agenda();
                agenda2.AgendaID = 1;
                agenda2.AgendaName = "Personal";
                agendaNames.Add(agenda2);
            }
            return agendaNames;
        }

        public int GetWorkAgendaID(int accountID)
        {
            int agendaID = 0;
            if (accountID == 15)
            {
                agendaID = 1;
            }
            else if (accountID == 25)
            {
                agendaID = 2;
            }
            else if (accountID == 30)
            {
                agendaID = 3;
            }
            return agendaID;
        }
    }
}
