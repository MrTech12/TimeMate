using System;
using System.Collections.Generic;
using System.IO;
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

            using (StreamWriter streamWriter = new StreamWriter("C:\\tmp\\addagendaTest.txt"))
            {
                streamWriter.WriteLine(agendaDTO.AgendaName);
                streamWriter.WriteLine(agendaDTO.AgendaColor);
                streamWriter.WriteLine(agendaDTO.Notification);
            }
           
            return agendaID;
        }

        public void AddJobAgenda(AgendaDTO agendaDTO, AccountDTO accountDTO)
        {
            if (accountDTO.AccountID == 12)
            {
                using (StreamWriter streamWriter = new StreamWriter("C:\\tmp\\addworkagendaTest.txt"))
                {
                    streamWriter.WriteLine(agendaDTO.AgendaName);
                    streamWriter.WriteLine(agendaDTO.AgendaColor);
                    streamWriter.WriteLine(agendaDTO.Notification);
                }
            }
        }

        public void DeleteAgenda(int AgendaIndex, AccountDTO accountDTO)
        {
            throw new NotImplementedException();
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

        public List<AppointmentDTO> GetAllAppointments(AccountDTO accountDTO)
        {
            List<AppointmentDTO> appointmentDTO = new List<AppointmentDTO>();
            if (accountDTO.AccountID == 12)
            {
                AppointmentDTO appointment1 = new AppointmentDTO();
                appointment1.AppointmentName = "Walk the dog";
                appointment1.StartDate = DateTime.Now.AddHours(3);
                appointmentDTO.Add(appointment1);

                AppointmentDTO appointment2 = new AppointmentDTO();
                appointment2.AppointmentName = "Do the dishes";
                appointment2.StartDate = DateTime.Now.AddHours(2);
                appointmentDTO.Add(appointment2);

                AppointmentDTO appointment3 = new AppointmentDTO();
                appointment3.AppointmentName = "Sleep for 7 hours";
                appointment3.StartDate = DateTime.Now.AddHours(7);
                appointmentDTO.Add(appointment3);
            }
            return appointmentDTO;
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
