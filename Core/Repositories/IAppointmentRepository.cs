using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Repositories
{
    public interface IAppointmentRepository
    {
        int CreateAppointment(Appointment appointment);

        List<Appointment> GetAppointments(int accountID);

        Job GetWorkHours(int agendaID, List<DateTime> dates);
    }
}