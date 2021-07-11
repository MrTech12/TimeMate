using Core.DTOs;
using Core.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TimeMateTest.Stubs
{
    class StubNormalAppointmentRepository : INormalAppointmentRepository
    {
        public void CreateDescription(AppointmentDTO appointmentDTO)
        {
            if (appointmentDTO.AppointmentName == "Reorder cables")
            {
                using (StreamWriter streamWriter = File.AppendText(@"C:\tmp\addAppointmentTest.txt"))
                {
                    streamWriter.WriteLine(appointmentDTO.DescriptionDTO.Description);
                }
            }
        }

        public string GetDescription(int appointmentID)
        {
            string description = null;
            if (appointmentID == 24)
            {
                description = "Dit is een beschrijving";
            }
            return description;
        }
    }
}
