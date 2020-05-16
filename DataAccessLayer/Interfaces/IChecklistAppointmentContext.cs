using DataAccessLayer.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Interfaces
{
    public interface IChecklistAppointmentContext
    {
        void AddTask(int appointmentID, string taskName);

        void CheckOffTask(AppointmentDTO appointmentDTO);
    }
}
