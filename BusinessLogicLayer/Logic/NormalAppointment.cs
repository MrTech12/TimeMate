using DataAccessLayer.DTO;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Logic
{
    public class NormalAppointment
    {
        private INormalAppointmentContainer _normalAppointmentContainer;

        public NormalAppointment(INormalAppointmentContainer normalAppointmentContainer)
        {
            this._normalAppointmentContainer = normalAppointmentContainer;
        }

        public string RetrieveDescription(int appointmentID)
        {
            return _normalAppointmentContainer.GetDescription(appointmentID);
        }
    }
}
