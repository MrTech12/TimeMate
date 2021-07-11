using Core.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Repositories
{
    public interface INormalAppointmentRepository
    {
        void CreateDescription(AppointmentDTO appointmentDTO);

        string GetDescription(int appointmentID);
    }
}