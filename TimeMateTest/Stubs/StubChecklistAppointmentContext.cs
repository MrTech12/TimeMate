using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DataAccessLayer.DTO;
using DataAccessLayer.Interfaces;

namespace TimeMateTest.Stubs
{
    class StubChecklistAppointmentContext : IChecklistAppointmentContext
    {
        public void AddTask(AppointmentDTO appointmentDTO)
        {
            if (appointmentDTO.AppointmentID == 60)
            {
                using (StreamWriter streamWriter = File.AppendText("C:\\tmp\\addAppointmentTest.txt"))
                {
                    streamWriter.WriteLine(appointmentDTO.ChecklistItemName[0]);
                }
            }

        }

        public void CheckOffTask(AppointmentDTO appointmentDTO)
        {
            throw new NotImplementedException();
        }

        public AppointmentDTO GetTasks(int appointmentIndex)
        {
            AppointmentDTO appointmentDTO = new AppointmentDTO();
            if (appointmentIndex == 6)
            {
                appointmentDTO.ChecklistItemName.Add("Water the plants.");
                appointmentDTO.ChecklistItemName.Add("Mow the lawn");
                appointmentDTO.ChecklistItemName.Add("Collect grapes");
            }
            return appointmentDTO;
        }
    }
}
