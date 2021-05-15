using Model.DTO_s;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Interfaces
{
    public interface IAppointmentRepository
    {
        int CreateAppointment(AppointmentDTO appointmentDTO);

        List<AppointmentDTO> GetAppointments(int accountID);

        JobDTO GetWorkHours(int agendaID, List<DateTime> dates);
    }
}
