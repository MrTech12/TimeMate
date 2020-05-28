﻿using DataAccessLayer.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Interfaces
{
    public interface IAppointmentContext
    {
        int AddAppointment(AppointmentDTO appointmentDTO);

        int GetAppointmentID(AppointmentDTO appointmentDTO);

        List<AppointmentDTO> GetAllAppointments(AccountDTO accountDTO);

        void DeleteAppointment(int appointmentIndex, int agendaIndex);

        void RenameAppointment(int appointmentIndex, int agendaIndex);
    }
}
