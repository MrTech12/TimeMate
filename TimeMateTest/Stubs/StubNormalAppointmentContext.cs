using System;
using System.Collections.Generic;
using System.Text;
using DataAccessLayer.DTO;
using DataAccessLayer.Interfaces;

namespace TimeMateTest.Stubs
{
    class StubNormalAppointmentContext : INormalAppointmentContext
    {
        public void AddDescription(AppointmentDTO appointmentDTO)
        {
            throw new NotImplementedException();
        }

        public string GetDescription(int appointmentID)
        {
            string description = null;

            if (appointmentID == 12)
            {
                description = "This needs to happen.";
            }
            return description;
        }
    }
}
