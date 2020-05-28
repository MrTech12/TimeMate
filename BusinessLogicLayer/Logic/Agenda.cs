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
        /// Get all appointments of the current user.
        /// </summary>
        /// <returns></returns>
        public List<AppointmentDTO> RetrieveAppointments()
        {
            List<AppointmentDTO> appointmentModel = new List<AppointmentDTO>();
            appointmentModel = _appointmentContext.GetAllAppointments(accountDTO);
            appointmentModel = appointmentModel.OrderBy(x => x.StartDate).ToList();

            return appointmentModel;
        }

        /// <summary>
        /// Create appointment with description.
        /// </summary>
        public void CreateNormalAppointment(AppointmentDTO appointmentDTO)
        {
            appointmentDTO.AppointmentID = _appointmentContext.AddAppointment(appointmentDTO);

            if (appointmentDTO.Description != null)
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
