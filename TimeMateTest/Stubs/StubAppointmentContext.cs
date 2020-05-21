using DataAccessLayer.DTO;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
namespace TimeMateTest.Stubs
{
    class StubAppointmentContext : IAppointmentContext
    {
        public int AddAppointment(AppointmentDTO appointmentDTO, int agendaIndex)
        {
            throw new NotImplementedException();
        }

        public void DeleteAppointment(int appointmentIndex, int agendaIndex)
        {
            throw new NotImplementedException();
        }

        public int GetAppointmentID(AppointmentDTO appointmentDTO, int agendaIndex)
        {
            throw new NotImplementedException();
        }

        public void RenameAppointment(int appointmentIndex, int agendaIndex)
        {
            throw new NotImplementedException();
        }
    }
}
