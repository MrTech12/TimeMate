using DataAccessLayer.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Interfaces
{
    public interface INormalAppointmentContext
    {
        void AddNormalAppointment(AppointmentDTO appointmentDTO, int agendaIndex);

        int GetNormalAppointmentID(AppointmentDTO appointmentDTO, int agendaIndex);

        void DeleteNormalAppointment(int normalAppointmentIndex, int agendaIndex);

        void RenameNormalAppointment(int normalAppointmentIndex, int agendaIndex);
    }
}
