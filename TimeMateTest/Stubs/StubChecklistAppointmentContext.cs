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
        public void AddTask(int appointmentID, string taskName)
        {
            if (appointmentID == 60)
            {
                using (StreamWriter streamWriter = File.AppendText("C:\\tmp\\addAppointmentTest.txt"))
                {
                    streamWriter.WriteLine(taskName);
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
