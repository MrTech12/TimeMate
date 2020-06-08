using DataAccessLayer.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Interfaces
{
    public interface INormalAppointmentContext
    {
        void AddDescription(AppointmentDTO appointmentDTO);

        string GetDescription(int appointmentID);
    }
}
