using DataAccessLayer.DTO;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace BusinessLogicLayer.Logic
{
    public class Agenda
    {
        private IAppointmentRepository _appointmentContainer;
        private INormalAppointmentRepository _normalAppointmentContainer;
        private IChecklistAppointmentRepository _checklistAppointmentContainer;

        private AccountDTO accountDTO = new AccountDTO();

        public Agenda(AccountDTO accountDTO, IAppointmentRepository appointmentContainer)
        {
            this.accountDTO = accountDTO;
            this._appointmentContainer = appointmentContainer;
        }

        public Agenda(IAppointmentRepository appointmentContainer , INormalAppointmentRepository normalAppointmentContainer)
        {
            this._appointmentContainer = appointmentContainer;
            this._normalAppointmentContainer = normalAppointmentContainer;
        }

        public Agenda(IAppointmentRepository appointmentContainer, IChecklistAppointmentRepository checklistAppointmentContainer)
        {
            this._appointmentContainer = appointmentContainer;
            this._checklistAppointmentContainer = checklistAppointmentContainer;
        }

        public List<AppointmentDTO> RetrieveAppointments()
        {
            List<ChecklistDTO> checklists = new List<ChecklistDTO>();
            var appointments = _appointmentContainer.GetAppointments(accountDTO.AccountID);

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

        public void CreateNormalAppointment(AppointmentDTO appointmentDTO)
        {
            appointmentDTO.AppointmentID = _appointmentContainer.AddAppointment(appointmentDTO);

            if (appointmentDTO.DescriptionDTO.Description != null)
            {
                _normalAppointmentContainer.AddDescription(appointmentDTO);
            }
        }

        public void CreateChecklistAppointment(AppointmentDTO appointmentDTO)
        {
            appointmentDTO.AppointmentID = _appointmentContainer.AddAppointment(appointmentDTO);

            if (appointmentDTO.ChecklistDTOs.Count != 0)
            {
                _checklistAppointmentContainer.AddTask(appointmentDTO);
            }
        }
    }
}
