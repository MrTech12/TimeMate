using DataAccessLayer.DTO;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Logic
{
    public class AgendaLogic
    {
        private IAgendaContext agendaContext;
        private IAccountContext accountContext;
        private INormalAppointmentContext nAppointmentContext;
        private IChecklistAppointmentContext cAppointmentContext;
        private AccountDTO accountDTO = new AccountDTO();

        private List<string> agendaFromUser = new List<string>();
        private string messageToUser;

        public AgendaLogic(AccountDTO accountDTOInput, IAgendaContext agendaContextInput)
        {
            this.agendaContext = agendaContextInput;
            this.accountDTO = accountDTOInput;
        }

        public AgendaLogic(AccountDTO accountDTOInput, IAgendaContext agendaContextInput, INormalAppointmentContext nAppointmentContextInput, IChecklistAppointmentContext checklistAppointmentContextInput)
        {
            this.agendaContext = agendaContextInput;
            this.accountDTO = accountDTOInput;
            this.nAppointmentContext = nAppointmentContextInput;
            this.cAppointmentContext = checklistAppointmentContextInput;
        }

        /// <summary>
        /// Rename an agenda
        /// </summary>
        public void RenameAgenda(string agendaName)
        {
            AgendaDTO agendaDTO = new AgendaDTO();
            agendaDTO.AgendaID = agendaContext.GetAgendaID(agendaName, accountDTO);
            agendaContext.RenameAgenda(agendaDTO.AgendaID, accountDTO);
        }

        /// <summary>
        /// Remove an agenda
        /// </summary>
        public void RemoveAgenda(string agendaName)
        {
            AgendaDTO agendaDTO = new AgendaDTO();
            agendaDTO.AgendaID = agendaContext.GetAgendaID(agendaName, accountDTO);
            agendaContext.DeleteAgenda(agendaDTO.AgendaID, accountDTO);
        }

        /// <summary>
        /// Get the agenda names of the current user.
        /// </summary>
        /// <returns></returns>
        public List<string> GetAgendaNames()
        {
            agendaFromUser = agendaContext.GetAgendaNamesFromDB(accountDTO);
            return agendaFromUser;
        }

        /// <summary>
        /// Get all appointments of the current user.
        /// </summary>
        /// <returns></returns>
        public List<AppointmentDTO> RetrieveAppointments()
        {
            List<AppointmentDTO> appointmentModel = new List<AppointmentDTO>();
            appointmentModel = agendaContext.GetAllAppointments(accountDTO);

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
                agendaDTO.AgendaID = agendaContext.GetAgendaID(agendaName, accountDTO);
                nAppointmentContext.AddNormalAppointment(appointmentDTO, agendaDTO.AgendaID);
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
                agendaDTO.AgendaID = agendaContext.GetAgendaID(agendaName, accountDTO);
                cAppointmentContext.AddChecklistAppointment(appointmentDTO, agendaDTO.AgendaID);
            }
            return messageToUser;
        }
    }
}
