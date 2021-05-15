using Model.DTO_s;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Interfaces
{
    public interface INormalAppointmentRepository
    {
        void CreateDescription(AppointmentDTO appointmentDTO);

        string GetDescription(int appointmentID);
    }
}
