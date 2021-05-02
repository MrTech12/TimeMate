using DataAccessLayer.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Interfaces
{
    public interface IAppointmentRepository
    {
        int AddAppointment(AppointmentDTO appointmentDTO);

        List<AppointmentDTO> GetAppointments(int accountID);

        JobDTO GetWorkHours(int agendaID, List<DateTime> dates);
    }
}
