using System;
using System.Collections.Generic;
using System.Text;
using DataAccessLayer.DTO;
using DataAccessLayer.Interfaces;

namespace TimeMateTest.Stubs
{
    class StubChecklistAppointmentContext : IChecklistAppointmentContext
    {
        public void AddTask(int appointmentID, string taskName)
        {
            string qwerty;
            if (appointmentID == 48 && taskName == "Get inspiration")
            {
                qwerty = "nothing";
            }
        }

        public void CheckOffTask(AppointmentDTO appointmentDTO)
        {
            throw new NotImplementedException();
        }

        public AppointmentDTO GetTask(int appointmentIndex)
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
