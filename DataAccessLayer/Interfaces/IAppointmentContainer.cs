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

        JobDTO GetHoursForWorkdayJob(int agendaID, List<DateTime> weekDates);

        JobDTO GetHoursForWeekendJob(int agendaID, List<DateTime> weekendDates);
    }
}
