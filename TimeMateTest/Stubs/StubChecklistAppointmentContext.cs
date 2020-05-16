using System;
using System.Collections.Generic;
using System.Text;
using DataAccessLayer.DTO;
using DataAccessLayer.Interfaces;

namespace TimeMateTest.Stubs
{
    class StubChecklistAppointmentContext : IChecklistAppointmentContext
    {
        public void AddTask(AppointmentDTO appointmentDTO)
        {
            throw new NotImplementedException();
        }

        public void CheckOffTask(AppointmentDTO appointmentDTO)
        {
            throw new NotImplementedException();
        }
    }
}
