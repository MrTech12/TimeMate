using DataAccessLayer.DTO;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Logic
{
    public class ChecklistAppointmentLogic : AppointmentLogic
    {
        private IChecklistAppointmentContext cAppointmentContext;
        private string messageToUser;

        public ChecklistAppointmentLogic(IAgendaContext agendaContextInput, AppointmentDTO appointmentDTOInput, IChecklistAppointmentContext checklistAppointmentContextInput) : base(appointmentDTOInput, agendaContextInput)
        {
            this.cAppointmentContext = checklistAppointmentContextInput;
        }

        public void CheckingOffTask(string taskName, string appointmentName)
        {

        }

        public override void DeleteAppointment(string appointmentName)
        {
            AgendaDTO agendaDTO = new AgendaDTO();
            agendaDTO.AgendaID = agendaContext.GetAgendaID(appointmentName, accountDTO);
            appointmentDTO.AppointmentID = cAppointmentContext.GetChecklistAppointmentID(appointmentDTO, agendaDTO.AgendaID);
            cAppointmentContext.DeleteChecklistAppointment(appointmentDTO.AppointmentID, agendaDTO.AgendaID);
        }

        public override void RenameAppointment(string appointmentName)
        {
            throw new NotImplementedException();
        }
    }
}
