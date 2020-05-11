using DataAccessLayer.DTO;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace BusinessLogicLayer.Logic
{
    public class AgendaLogic
    {
        private IAgendaContext _agendaContext;
        private IAccountContext _accountContext;
        private INormalAppointmentContext _nAppointmentContext;
        private IChecklistAppointmentContext _cAppointmentContext;
        private AccountDTO accountDTO = new AccountDTO();

        private List<string> agendaFromUser = new List<string>();
        private string messageToUser;

        public AgendaLogic(AccountDTO accountDTO, IAgendaContext agendaContext)
        {
            this._agendaContext = agendaContext;
            this.accountDTO = accountDTO;
        }

        public AgendaLogic(AccountDTO accountDTOInput, IAgendaContext agendaContextInput, INormalAppointmentContext nAppointmentContextInput, IChecklistAppointmentContext checklistAppointmentContextInput)
        {
            this._agendaContext = agendaContextInput;
            this.accountDTO = accountDTOInput;
            this._nAppointmentContext = nAppointmentContextInput;
            this._cAppointmentContext = checklistAppointmentContextInput;
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
        /// Remove an agenda
        /// </summary>
        public void RemoveAgenda(string agendaName)
        {
            AgendaDTO agendaDTO = new AgendaDTO();
            agendaDTO.AgendaID = _agendaContext.GetAgendaID(agendaName, accountDTO);
            _agendaContext.DeleteAgenda(agendaDTO.AgendaID, accountDTO);
        }

        /// <summary>
        /// Get the agenda names of the current user.
        /// </summary>
        /// <returns></returns>
        public List<string> GetAgendaNames()
        {
            agendaFromUser = _agendaContext.GetAgendaNamesFromDB(accountDTO);
            return agendaFromUser;
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
    }
}
