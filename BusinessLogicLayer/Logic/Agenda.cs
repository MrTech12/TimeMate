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
        private AccountDTO accountDTO = new AccountDTO();

        private IAgendaContext _agendaContext;
        private IAppointmentContext _appointmentContext;
        private INormalAppointmentContext _nAppointmentContext;
        private IChecklistAppointmentContext _cAppointmentContext;

        public Agenda(AccountDTO accountDTO, IAgendaContext agendaContext)
        {
            this._agendaContext = agendaContext;
            this.accountDTO = accountDTO;
        }

        public Agenda(AccountDTO accountDTO, IAgendaContext agendaContext, IAppointmentContext appointmentContext)
        {
            this.accountDTO = accountDTO;
            this._agendaContext = agendaContext;
            this._appointmentContext = appointmentContext;
        }

        public Agenda(AccountDTO accountDTO, IAgendaContext agendaContext, IAppointmentContext appointmentContext , INormalAppointmentContext nAppointmentContext)
        {
            this.accountDTO = accountDTO;
            this._agendaContext = agendaContext;
            this._appointmentContext = appointmentContext;
            this._nAppointmentContext = nAppointmentContext;
        }

        public Agenda(AccountDTO accountDTO, IAgendaContext agendaContext, IAppointmentContext appointmentContext, IChecklistAppointmentContext cAppointmentContext)
        {
            this.accountDTO = accountDTO;
            this._agendaContext = agendaContext;
            this._appointmentContext = appointmentContext;
            this._cAppointmentContext = cAppointmentContext;
        }

        public int RetrieveAppointmentID(AppointmentDTO appointmentDTO)
        {
            int ID = _appointmentContext.GetAppointmentID(appointmentDTO);
            return ID;
        }

        /// <summary>
        /// Retrieve all appointments that belong to the current actor.
        /// </summary>
        /// <returns></returns>
        public List<AppointmentDTO> RetrieveAppointments()
        {
            List<ChecklistDTO> checklists = new List<ChecklistDTO>();
            var appointments = _appointmentContext.GetAllAppointments(accountDTO);

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
            appointmentDTO.AppointmentID = _appointmentContext.AddAppointment(appointmentDTO);

            if (appointmentDTO.DescriptionDTO.Description != null)
            {
                _nAppointmentContext.AddDescription(appointmentDTO);
            }
        }

        /// <summary>
        /// Create appointment with checklist.
        /// </summary>
        public void CreateChecklistAppointment(AppointmentDTO appointmentDTO)
        {
            appointmentDTO.AppointmentID = _appointmentContext.AddAppointment(appointmentDTO);

            _cAppointmentContext.AddTask(appointmentDTO);
        }
    }
}
