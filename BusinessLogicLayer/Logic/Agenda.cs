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
        private IAppointmentContainer _appointmentContainer;
        private INormalAppointmentContainer _normalAppointmentContainer;
        private IChecklistAppointmentContainer _checklistAppointmentContainer;

        private AccountDTO accountDTO = new AccountDTO();

        public Agenda(AccountDTO accountDTO, IAppointmentContainer appointmentContainer)
        {
            this.accountDTO = accountDTO;
            this._appointmentContainer = appointmentContainer;
        }

        public Agenda(AccountDTO accountDTO, IAppointmentContainer appointmentContainer , INormalAppointmentContainer normalAppointmentContainer)
        {
            this.accountDTO = accountDTO;
            this._appointmentContainer = appointmentContainer;
            this._normalAppointmentContainer = normalAppointmentContainer;
        }

        public Agenda(AccountDTO accountDTO, IAppointmentContainer appointmentContainer, IChecklistAppointmentContainer checklistAppointmentContainer)
        {
            this.accountDTO = accountDTO;
            this._appointmentContainer = appointmentContainer;
            this._checklistAppointmentContainer = checklistAppointmentContainer;
        }

        public List<AppointmentDTO> RetrieveAppointments()
        {
            List<ChecklistDTO> checklists = new List<ChecklistDTO>();
            var appointments = _appointmentContainer.GetAllAppointments(accountDTO);

            List<AppointmentDTO> sortedAppointments = appointments.OrderBy(x => x.StartDate).GroupBy(x => x.AppointmentID).Select(g => g.First()).ToList();

            for (int i = 0; i < appointments.Count; i++)
            {
                ChecklistDTO checklist = new ChecklistDTO();
                if (appointments[i].ChecklistDTOs[0].TaskName != null)
                {
                    checklist = appointments[i].ChecklistDTOs[0];
                    checklists.Add(checklist);
                }
            }

            if (checklists.Capacity != 0)
            {
                for (int i = 0; i < sortedAppointments.Count; i++)
                {
                    sortedAppointments[i].ChecklistDTOs.RemoveAt(0);
                }

                foreach (var item in sortedAppointments)
                {
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

        /// <summary>
        /// Create appointment with description.
        /// </summary>
        public void CreateNormalAppointment(AppointmentDTO appointmentDTO)
        {
            appointmentDTO.AppointmentID = _appointmentContainer.AddAppointment(appointmentDTO);

            if (appointmentDTO.DescriptionDTO.Description != null)
            {
                _normalAppointmentContainer.AddDescription(appointmentDTO);
            }
        }

        /// <summary>
        /// Create appointment with checklist.
        /// </summary>
        public void CreateChecklistAppointment(AppointmentDTO appointmentDTO)
        {
            appointmentDTO.AppointmentID = _appointmentContainer.AddAppointment(appointmentDTO);

            _checklistAppointmentContainer.AddTask(appointmentDTO);
        }
    }
}
