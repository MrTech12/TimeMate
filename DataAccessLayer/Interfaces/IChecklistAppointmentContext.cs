using DataAccessLayer.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Interfaces
{
    public interface IChecklistAppointmentContext
    {
        void AddChecklistAppointment(AppointmentDTO appointmentDTO, int agendaIndex);

        int GetChecklistAppointmentID(AppointmentDTO appointmentDTO, int agendaIndex);

        void DeleteChecklistAppointment(int normalAppointmentIndex, int agendaIndex);

        void RenameChecklistAppointment(int normalAppointmentIndex, int agendaIndex);

        void CheckOffTask(AppointmentDTO appointmentDTO);
    }
}
