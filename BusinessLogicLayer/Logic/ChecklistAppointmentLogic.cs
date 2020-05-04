using DataAccessLayer.DTO;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Logic
{
    public class ChecklistAppointmentLogic : AppointmentLogic
    {
        private IChecklistAppointmentContext CAppointmentContext;
        private string messageToUser;

        public ChecklistAppointmentLogic(IAgendaContext agendaContextInput, AppointmentDTO appointmentDTOInput, IChecklistAppointmentContext checklistAppointmentContextInput) : base(appointmentDTOInput, agendaContextInput)
        {
            this.CAppointmentContext = checklistAppointmentContextInput;
        }

        public void CheckingOffTask(string taskName, string appointmentName)
        {

        }

        public override void DeleteAppointment(string appointmentName)
        {
            AgendaDTO agendaDTO = new AgendaDTO();
            agendaDTO.AgendaID = AgendaContext.GetAgendaID(appointmentName, accountDTO);
            appointmentDTO.AppointmentID = CAppointmentContext.GetChecklistAppointmentID(appointmentDTO, agendaDTO.AgendaID);
            CAppointmentContext.DeleteChecklistAppointment(appointmentDTO.AppointmentID, agendaDTO.AgendaID);
        }

        public override void RenameAppointment(string appointmentName)
        {
            throw new NotImplementedException();
        }
    }
}
