using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Repositories
{
    public interface INormalAppointmentRepository
    {
        void CreateDescription(Description description);

        string GetDescription(int appointmentID);
    }
}