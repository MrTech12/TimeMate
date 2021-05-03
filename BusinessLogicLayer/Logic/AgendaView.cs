using DataAccessLayer.DTO;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogicLayer.Logic
{
    public class AgendaView
    {
        private IAppointmentRepository _appointmentRepository;
        private AccountDTO accountDTO = new AccountDTO();

        public AgendaView(AccountDTO accountDTO, IAppointmentRepository appointmentRepository)
        {
            this.accountDTO = accountDTO;
            _appointmentRepository = appointmentRepository;
        }

        public List<AppointmentDTO> RetrieveAppointments()
        {
            List<ChecklistDTO> checklists = new List<ChecklistDTO>();
            var appointments = _appointmentRepository.GetAppointments(accountDTO.AccountID);

            for (int i = 0; i < appointments.Count; i++)
            {
                ChecklistDTO checklist = new ChecklistDTO();
                if (appointments[i].ChecklistDTOs.Count != 0)
                {
                    checklist = appointments[i].ChecklistDTOs[0];
                    checklists.Add(checklist);
                }
            }

            List<AppointmentDTO> sortedAppointments = appointments.OrderBy(x => x.StartDate).GroupBy(x => x.AppointmentID).Select(g => g.First()).ToList();

            if (checklists.Count != 0)
            {
                foreach (var item in sortedAppointments)
                {
                    item.ChecklistDTOs.RemoveAt(0);
                    for (int i = 0; i < checklists.Count; i++)
                    {
                        if (item.AppointmentID == checklists[i].AppointmentID)
                        {
                            item.ChecklistDTOs.Add(checklists[i]);
                        }
                    }
                }
            }
            return sortedAppointments;
        }
    }
}
