using DataAccessLayer.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Interfaces
{
    public interface IChecklistAppointmentContext
    {
        void AddTask(AppointmentDTO appointmentDTO);

        void CheckOffTask(AppointmentDTO appointmentDTO);
    }
}
