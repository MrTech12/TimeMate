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

        public ChecklistAppointment(AppointmentDTO appointmentDTO, IChecklistAppointmentContext checklistAppointmentContext) : base(appointmentDTO)
        {
            this._cAppointmentContext = checklistAppointmentContext;
        }

        public AppointmentDTO RetrieveTasks(int appointmentIndex)
        {
            AppointmentDTO appointmentDTO = new AppointmentDTO();
            appointmentDTO = _cAppointmentContext.GetTasks(appointmentIndex);
            return appointmentDTO;
        }

        public override void RenameAppointment(AppointmentDTO appointmentDTO)
        {
            throw new NotImplementedException();
        }
    }
}
