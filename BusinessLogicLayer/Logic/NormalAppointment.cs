using DataAccessLayer.DTO;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Logic
{
    public class NormalAppointment : Appointment
    {
        private INormalAppointmentContext _nAppointmentContext;

        public NormalAppointment(AppointmentDTO appointmentDTO, INormalAppointmentContext normalAppointmentContext) : base(appointmentDTO)
        {
            this._nAppointmentContext = normalAppointmentContext;
        }

        public override void RenameAppointment(AppointmentDTO appointmentDTO)
        {
            throw new NotImplementedException();
        }

        public string RetrieveDescription(int appointmentID)
        {
            string description = _nAppointmentContext.GetDescription(appointmentID);
            return description;
        }
    }
}
