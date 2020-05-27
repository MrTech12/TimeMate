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
            int appointmentID;
            if (appointmentDTO.AppointmentName == "This is <b> a </b> test.")
            {
                appointmentID = 366;
            }
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
