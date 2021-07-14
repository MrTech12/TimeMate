using Core.Entities;
using Core.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TimeMateTest.Stubs
{
    class StubNormalAppointmentRepository : INormalAppointmentRepository
    {
        public void CreateDescription(Description description)
        {
            if (description.AppointmentID == 60)
            {
                using (StreamWriter streamWriter = File.AppendText(@"C:\tmp\addAppointmentTest.txt"))
                {
                    streamWriter.WriteLine(description.DescriptionName);
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
