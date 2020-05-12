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

        public ChecklistAppointment(IAgendaContext agendaContextInput, AppointmentDTO appointmentDTOInput, IChecklistAppointmentContext checklistAppointmentContext) : base(appointmentDTOInput, agendaContextInput)
        {
            this._cAppointmentContext = checklistAppointmentContext;
        }

        public void CheckingOffTask(string taskName, string appointmentName)
        {

        }

        public override void RenameAppointment(string appointmentName)
        {
            throw new NotImplementedException();
        }
    }
}
