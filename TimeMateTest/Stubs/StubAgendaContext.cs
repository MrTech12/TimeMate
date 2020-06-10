﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DataAccessLayer.DTO;
using DataAccessLayer.Interfaces;

namespace TimeMateTest.Stubs
{
    class StubAgendaContext : IAgendaContainer
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

        public void AddPayDetails(int agendaID, AccountDTO accountDTO)
        {
            using (StreamWriter streamWriter = new StreamWriter(@"C:\tmp\addWorkPayDetails.txt"))
            {
                streamWriter.WriteLine(accountDTO.JobHourlyWage[0]);
                streamWriter.WriteLine(accountDTO.JobDayType[0]);
            }
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

        public List<DateTime> GetWeekendHours(int agendaIndex)
        {
            throw new NotImplementedException();
        }

        public List<DateTime> GetWorkdayHours(int agendaIndex)
        {
            throw new NotImplementedException();
        }
    }
}
