using DataAccessLayer.DTO;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Logic
{
    public class NormalAppointment : Appointment
    {
        private INormalAppointmentContainer _nAppointmentContext;

        public NormalAppointment(AppointmentDTO appointmentDTO, INormalAppointmentContainer normalAppointmentContext) : base(appointmentDTO)
        {
            this._nAppointmentContext = normalAppointmentContext;
        }

        public string RetrieveDescription(AppointmentDTO appointmentDTO)
        {
            return _nAppointmentContext.GetDescription(appointmentDTO.AppointmentID);
        }

        /// <summary>
        /// Rename an appointment.
        /// </summary>
        public override void RenameAppointment(AppointmentDTO appointmentDTO)
        {
            throw new NotImplementedException();
        }
    }
}
