using DataAccessLayer.DTO;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Logic
{
    public class NormalAppointment
    {
        private INormalAppointmentContainer _normalAppointmentContext;

        public NormalAppointment(INormalAppointmentContainer normalAppointmentContext)
        {
            this._normalAppointmentContext = normalAppointmentContext;
        }

        public string RetrieveDescription(int appointmentID)
        {
            return _normalAppointmentContext.GetDescription(appointmentID);
        }
    }
}
