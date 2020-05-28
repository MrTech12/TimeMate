﻿using DataAccessLayer.DTO;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
namespace TimeMateTest.Stubs
{
    class StubAppointmentContext : IAppointmentContext
    {
        public int AddAppointment(AppointmentDTO appointmentDTO, int agendaIndex)
        {
            int appointmentID = 0;

            using (StreamWriter streamWriter = new StreamWriter("C:\\tmp\\addAppointmentTest.txt"))
            {
                streamWriter.WriteLine(appointmentDTO.AppointmentName);
                streamWriter.WriteLine(appointmentDTO.StartDate);
                streamWriter.WriteLine(appointmentDTO.EndDate);
                streamWriter.WriteLine(appointmentDTO.AgendaName);
                appointmentID = 60;
            }
            return appointmentID;
        }

        public void DeleteAppointment(int appointmentIndex, int agendaIndex)
        {
            throw new NotImplementedException();
        }

        public int GetAppointmentID(AppointmentDTO appointmentDTO, int agendaIndex)
        {
            int appointmentID = 0;
            if (appointmentDTO.AppointmentName == "Shopping" && agendaIndex == 36)
            {
                appointmentID = 8;
            }
            else if (appointmentDTO.AppointmentName == null && agendaIndex == 0)
            {
                appointmentID = -1;
            }
            else if (appointmentDTO.AppointmentName == "Reorder cables" && agendaIndex == 2)
            {
                appointmentID = 8;
            }
            else if (appointmentDTO.AppointmentName == "Create 3D render" && agendaIndex == 2)
            {
                appointmentID = 48;
            }
            return appointmentID;
        }

        public void RenameAppointment(int appointmentIndex, int agendaIndex)
        {
            throw new NotImplementedException();
        }
    }
}
