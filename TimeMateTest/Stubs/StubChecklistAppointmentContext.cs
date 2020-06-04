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
                    streamWriter.WriteLine(appointmentDTO.ChecklistDTOs[0].TaskName);
                }
            }
        }

        public void CheckOffTask(AppointmentDTO appointmentDTO)
        {
            throw new NotImplementedException();
        }

        public List<ChecklistDTO> GetTasks(AppointmentDTO appointmentDTO)
        {
            throw new NotImplementedException();
        }
    }
}
