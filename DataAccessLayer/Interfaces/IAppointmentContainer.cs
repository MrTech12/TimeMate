using DataAccessLayer.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Interfaces
{
    public interface IAppointmentContainer
    {
        int AddAppointment(AppointmentDTO appointmentDTO);

        List<AppointmentDTO> GetAppointments(int accountID);
    }
}
