using DataAccessLayer.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Interfaces
{
    public interface INormalAppointmentRepository
    {
        void AddDescription(AppointmentDTO appointmentDTO);

        string GetDescription(int appointmentID);
    }
}
