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
            if (agendaDTO.AgendaID == 12 && accountDTO.AccountID == 12)
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
            int agendaID = 0;
            if (agendaName == "Personal" && accountDTO.AccountID == 12)
            {
                agendaID = 2;
            }
            else if (agendaName == null && accountDTO.AccountID == 12)
            {
                agendaID = -1;
            }
            else if (agendaName == "Homework" && accountDTO.AccountID == 12)
            {
                agendaID = 24;
            }
            else if (agendaName == "Bijbaan" && accountDTO.AccountID == 12)
            {
                agendaID = 4;
            }

            return agendaID;
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
