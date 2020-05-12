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
        private string messageToUser;

        public NormalAppointment(IAgendaContext agendaContextInput, AppointmentDTO appointmentDTOInput, INormalAppointmentContext normalAppointmentContext) : base(appointmentDTOInput, agendaContextInput)
        {
            this._nAppointmentContext = normalAppointmentContext;
        }

        public override void RenameAppointment(string appointmentName)
        {
            throw new NotImplementedException();
        }
    }
}
