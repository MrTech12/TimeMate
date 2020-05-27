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

        public ChecklistAppointment(AppointmentDTO appointmentDTO, IChecklistAppointmentContext checklistAppointmentContext) : base(appointmentDTO)
        {
            this._cAppointmentContext = checklistAppointmentContext;
        }

        public AppointmentDTO RetrieveTask(int appointmentIndex)
        {
            AppointmentDTO appointmentDTO = new AppointmentDTO();
            appointmentDTO = _cAppointmentContext.GetTask(appointmentIndex);
            return appointmentDTO;
        }

        public override void RenameAppointment(AppointmentDTO appointmentDTO)
        {
            throw new NotImplementedException();
        }
    }
}
