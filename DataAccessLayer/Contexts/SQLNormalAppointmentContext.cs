﻿using DataAccessLayer.DTO;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Contexts
{
    public class SQLNormalAppointmentContext : INormalAppointmentContext
    {
        public void AddNormalAppointment(AppointmentDTO appointmentDTO, int agendaIndex)
        {
            throw new NotImplementedException();
        }

        public void DeleteNormalAppointment(int normalAppointmentIndex, int agendaIndex)
        {
            throw new NotImplementedException();
        }

        public int GetNormalAppointmentID(AppointmentDTO appointmentDTO, int agendaIndex)
        {
            throw new NotImplementedException();
        }

        public void RenameNormalAppointment(int normalAppointmentIndex, int agendaIndex)
        {
            throw new NotImplementedException();
        }
    }
}
