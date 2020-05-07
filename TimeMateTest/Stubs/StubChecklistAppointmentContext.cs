using System;
using System.Collections.Generic;
using System.Text;
using DataAccessLayer.DTO;
using DataAccessLayer.Interfaces;

namespace TimeMateTest.Stubs
{
    class StubChecklistAppointmentContext : IChecklistAppointmentContext
    {
        public void AddChecklistAppointment(AppointmentDTO appointmentDTO, int agendaIndex)
        {
            throw new NotImplementedException();
        }

        public void CheckOffTask(AppointmentDTO appointmentDTO)
        {
            throw new NotImplementedException();
        }

        public void DeleteChecklistAppointment(int normalAppointmentIndex, int agendaIndex)
        {
            throw new NotImplementedException();
        }

        public int GetChecklistAppointmentID(AppointmentDTO appointmentDTO, int agendaIndex)
        {
            throw new NotImplementedException();
        }

        public void RenameChecklistAppointment(int normalAppointmentIndex, int agendaIndex)
        {
            throw new NotImplementedException();
        }
    }
}
