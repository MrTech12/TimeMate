using DataAccessLayer.DTO;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
namespace TimeMateTest.Stubs
{
    class StubAppointmentContext : IAppointmentContext
    {
        public int AddAppointment(AppointmentDTO appointmentDTO)
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

        public List<AppointmentDTO> GetAllAppointments(AccountDTO accountDTO)
        {
            List<AppointmentDTO> appointmentDTO = new List<AppointmentDTO>();
            if (accountDTO.AccountID == 12)
            {
                AppointmentDTO appointment1 = new AppointmentDTO();
                appointment1.AppointmentName = "Walk the dog";
                appointment1.StartDate = DateTime.Now.AddHours(3);
                appointmentDTO.Add(appointment1);

                AppointmentDTO appointment2 = new AppointmentDTO();
                appointment2.AppointmentName = "Do the dishes";
                appointment2.StartDate = DateTime.Now.AddHours(2);
                appointmentDTO.Add(appointment2);

                AppointmentDTO appointment3 = new AppointmentDTO();
                appointment3.AppointmentName = "Sleep for 7 hours";
                appointment3.StartDate = DateTime.Now.AddHours(7);
                appointmentDTO.Add(appointment3);
            }
            return appointmentDTO;
        }

        public int GetAppointmentID(AppointmentDTO appointmentDTO)
        {
            int appointmentID = 0;
            if (appointmentDTO.AppointmentName == "Shopping" && appointmentDTO.AgendaID == 36)
            {
                appointmentID = 8;
            }
            else if (appointmentDTO.AppointmentName == null && appointmentDTO.AgendaID == 0)
            {
                appointmentID = -1;
            }
            else if (appointmentDTO.AppointmentName == "Reorder cables" && appointmentDTO.AgendaID == 2)
            {
                appointmentID = 8;
            }
            else if (appointmentDTO.AppointmentName == "Create 3D render" && appointmentDTO.AgendaID == 2)
            {
                appointmentID = 48;
            }
            return appointmentID;
        }
    }
}
