using DataAccessLayer.DTO;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Logic
{
    public class ChecklistAppointment : Appointment
    {
        private IChecklistAppointmentContext _cAppointmentContext;
        private string messageToUser;

        public ChecklistAppointment(IAgendaContext agendaContextInput, AppointmentDTO appointmentDTOInput, IChecklistAppointmentContext checklistAppointmentContextInput) : base(appointmentDTOInput, agendaContextInput)
        {
            this._cAppointmentContext = checklistAppointmentContextInput;
        }

        public void CheckingOffTask(string taskName, string appointmentName)
        {

        }

        public override void DeleteAppointment(string appointmentName)
        {
            AgendaDTO agendaDTO = new AgendaDTO();
            agendaDTO.AgendaID = _agendaContext.GetAgendaID(appointmentName, accountDTO);
            appointmentDTO.AppointmentID = _cAppointmentContext.GetChecklistAppointmentID(appointmentDTO, agendaDTO.AgendaID);
            _cAppointmentContext.DeleteChecklistAppointment(appointmentDTO.AppointmentID, agendaDTO.AgendaID);
        }

        public override void RenameAppointment(string appointmentName)
        {
            throw new NotImplementedException();
        }
    }
}
