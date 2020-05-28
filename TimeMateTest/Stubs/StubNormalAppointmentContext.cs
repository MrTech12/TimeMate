using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DataAccessLayer.DTO;
using DataAccessLayer.Interfaces;

namespace TimeMateTest.Stubs
{
    class StubNormalAppointmentContext : INormalAppointmentContext
    {
        public void AddDescription(AppointmentDTO appointmentDTO)
        {
            if (appointmentDTO.AppointmentName == "Reorder cables")
            {
                using (StreamWriter streamWriter = File.AppendText("C:\\tmp\\addAppointmentTest.txt"))
                {
                    streamWriter.WriteLine(appointmentDTO.Description);
                }
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
