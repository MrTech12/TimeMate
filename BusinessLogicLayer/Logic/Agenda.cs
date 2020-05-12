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
        private IAgendaContext _agendaContext;
        private IAccountContext _accountContext;
        private INormalAppointmentContext _nAppointmentContext;
        private IChecklistAppointmentContext _cAppointmentContext;
        private AccountDTO accountDTO = new AccountDTO();
        private AppointmentDTO appointmentDTO = new AppointmentDTO();

        private string messageToUser;

        public Agenda(AccountDTO accountDTO, IAgendaContext agendaContext)
        {
            this._agendaContext = agendaContext;
            this.accountDTO = accountDTO;
        }

        public Agenda(AccountDTO accountDTO, IAgendaContext agendaContext, INormalAppointmentContext nAppointmentContext, IChecklistAppointmentContext checklistAppointmentContext)
        {
            this._agendaContext = agendaContext;
            this.accountDTO = accountDTO;
            this._nAppointmentContext = nAppointmentContext;
            this._cAppointmentContext = checklistAppointmentContext;
        }

        public Agenda(AccountDTO accountDTO, AppointmentDTO appointmentDTO , IAgendaContext agendaContext, INormalAppointmentContext nAppointmentContext, IChecklistAppointmentContext checklistAppointmentContext)
        {
            this._agendaContext = agendaContext;
            this.appointmentDTO = appointmentDTO;
            this.accountDTO = accountDTO;
            this._nAppointmentContext = nAppointmentContext;
            this._cAppointmentContext = checklistAppointmentContext;
        }

        /// <summary>
        /// Rename an agenda
        /// </summary>
        public void RenameAgenda(string agendaName)
        {
            AgendaDTO agendaDTO = new AgendaDTO();
            agendaDTO.AgendaID = _agendaContext.GetAgendaID(agendaName, accountDTO);
            _agendaContext.RenameAgenda(agendaDTO.AgendaID, accountDTO);
        }

        /// <summary>
        /// Get all appointments of the current user.
        /// </summary>
        /// <returns></returns>
        public List<AppointmentDTO> RetrieveAppointments()
        {
            List<AppointmentDTO> appointmentModel = new List<AppointmentDTO>();
            appointmentModel = _agendaContext.GetAllAppointments(accountDTO);
            appointmentModel.OrderBy(x => x.StartDate);

            return appointmentModel;
        }

        /// <summary>
        /// Create appointment with description.
        /// </summary>
        public string CreateNAppointment(AppointmentDTO appointmentDTO, string agendaName)
        {
            bool emptyAppointmentName = appointmentDTO.AppointmentName == "";
            if (emptyAppointmentName)
            {
                messageToUser = "U heeft niet een afspraaknaam ingevuld.";
            }
            else
            {
                AgendaDTO agendaDTO = new AgendaDTO();
                agendaDTO.AgendaID = _agendaContext.GetAgendaID(agendaName, accountDTO);
                _nAppointmentContext.AddNormalAppointment(appointmentDTO, agendaDTO.AgendaID);
            }
            return messageToUser;
        }

        /// <summary>
        /// Create appointment with checklist.
        /// </summary>
        public string CreateCAppointment(AppointmentDTO appointmentDTO, string agendaName)
        {
            bool emptyAppointmentName = appointmentDTO.AppointmentName == "";
            if (emptyAppointmentName)
            {
                messageToUser = "U heeft niet een afspraaknaam ingevuld.";
            }
            else
            {
                AgendaDTO agendaDTO = new AgendaDTO();
                agendaDTO.AgendaID = _agendaContext.GetAgendaID(agendaName, accountDTO);
                _cAppointmentContext.AddChecklistAppointment(appointmentDTO, agendaDTO.AgendaID);
            }
            return messageToUser;
        }

        public void DeleteAppointment(string appointmentName)
        {
            if (appointmentDTO.ChecklistItemName[0] != null)
            {
                AgendaDTO agendaDTO = new AgendaDTO();
                agendaDTO.AgendaID = _agendaContext.GetAgendaID(appointmentName, accountDTO);
                appointmentDTO.AppointmentID = _cAppointmentContext.GetChecklistAppointmentID(appointmentDTO, agendaDTO.AgendaID);
                _cAppointmentContext.DeleteChecklistAppointment(appointmentDTO.AppointmentID, agendaDTO.AgendaID);
            }
            else
            {
                AgendaDTO agendaDTO = new AgendaDTO();
                agendaDTO.AgendaID = _agendaContext.GetAgendaID(appointmentName, accountDTO);
                appointmentDTO.AppointmentID = _nAppointmentContext.GetNormalAppointmentID(appointmentDTO, agendaDTO.AgendaID);
                _nAppointmentContext.DeleteNormalAppointment(appointmentDTO.AppointmentID, agendaDTO.AgendaID);
            }
        }
    }
}
