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
        private IAgendaContext AgendaContext;
        private IAccountContext AccountContext;
        private INormalAppointmentContext NAppointmentContext;
        private IChecklistAppointmentContext CAppointmentContext;
        private AccountDTO accountDTO = new AccountDTO();

        private List<string> agendaFromUser = new List<string>();
        private string messageToUser;

        public AgendaLogic(AccountDTO accountDTO, IAgendaContext agendaContext)
        {
            this.AgendaContext = agendaContext;
            this.accountDTO = accountDTO;
        }

        public AgendaLogic(AccountDTO accountDTOInput, IAgendaContext agendaContextInput, INormalAppointmentContext nAppointmentContextInput, IChecklistAppointmentContext checklistAppointmentContextInput)
        {
            this.AgendaContext = agendaContextInput;
            this.accountDTO = accountDTOInput;
            this.NAppointmentContext = nAppointmentContextInput;
            this.CAppointmentContext = checklistAppointmentContextInput;
        }

        /// <summary>
        /// Rename an agenda
        /// </summary>
        public void RenameAgenda(string agendaName)
        {
            AgendaDTO agendaDTO = new AgendaDTO();
            agendaDTO.AgendaID = AgendaContext.GetAgendaID(agendaName, accountDTO);
            AgendaContext.RenameAgenda(agendaDTO.AgendaID, accountDTO);
        }

        /// <summary>
        /// Remove an agenda
        /// </summary>
        public void RemoveAgenda(string agendaName)
        {
            AgendaDTO agendaDTO = new AgendaDTO();
            agendaDTO.AgendaID = AgendaContext.GetAgendaID(agendaName, accountDTO);
            AgendaContext.DeleteAgenda(agendaDTO.AgendaID, accountDTO);
        }

        /// <summary>
        /// Get the agenda names of the current user.
        /// </summary>
        /// <returns></returns>
        public List<string> GetAgendaNames()
        {
            agendaFromUser = AgendaContext.GetAgendaNamesFromDB(accountDTO);
            return agendaFromUser;
        }

        /// <summary>
        /// Get all appointments of the current user.
        /// </summary>
        /// <returns></returns>
        public List<AppointmentDTO> RetrieveAppointments()
        {
            List<AppointmentDTO> appointmentModel = new List<AppointmentDTO>();
            appointmentModel = AgendaContext.GetAllAppointments(accountDTO);
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
                agendaDTO.AgendaID = AgendaContext.GetAgendaID(agendaName, accountDTO);
                NAppointmentContext.AddNormalAppointment(appointmentDTO, agendaDTO.AgendaID);
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
                agendaDTO.AgendaID = AgendaContext.GetAgendaID(agendaName, accountDTO);
                CAppointmentContext.AddChecklistAppointment(appointmentDTO, agendaDTO.AgendaID);
            }
            return messageToUser;
        }
    }
}
