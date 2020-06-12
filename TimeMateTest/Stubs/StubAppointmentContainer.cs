using DataAccessLayer.DTO;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
namespace TimeMateTest.Stubs
{
    class StubAppointmentContainer : IAppointmentContainer
    {
        public int AddAppointment(AppointmentDTO appointmentDTO)
        {
            int appointmentID = 0;

            using (StreamWriter streamWriter = new StreamWriter(@"C:\tmp\addAppointmentTest.txt"))
            {
                streamWriter.WriteLine(appointmentDTO.AppointmentName);
                streamWriter.WriteLine(appointmentDTO.StartDate);
                streamWriter.WriteLine(appointmentDTO.EndDate);
                streamWriter.WriteLine(appointmentDTO.AgendaName);
                appointmentID = 60;
            }
            return appointmentID;
        }

        public List<AppointmentDTO> GetAppointments(int accountID)
        {
            List<AppointmentDTO> appointmentDTO = new List<AppointmentDTO>();
            if (accountID == 12)
            {
                AppointmentDTO appointment1 = new AppointmentDTO();
                appointment1.AppointmentID = 1;
                appointment1.AppointmentName = "Walk the dog";
                appointment1.StartDate = DateTime.Now.AddHours(3);
                appointmentDTO.Add(appointment1);

                AppointmentDTO appointment2 = new AppointmentDTO();
                appointment2.AppointmentID = 2;
                appointment2.AppointmentName = "Do the dishes";
                appointment2.StartDate = DateTime.Now.AddHours(2);
                appointmentDTO.Add(appointment2);

                AppointmentDTO appointment3 = new AppointmentDTO();
                appointment3.AppointmentID = 3;
                appointment3.AppointmentName = "Sleep for 7 hours";
                appointment3.StartDate = DateTime.Now.AddHours(7);
                appointmentDTO.Add(appointment3);
            }
            else if (accountID == 42)
            {
                AppointmentDTO appointment1 = new AppointmentDTO();
                ChecklistDTO checklist1 = new ChecklistDTO() { TaskName = "Walk the dog", TaskID = 1, AppointmentID = 1 };
                ChecklistDTO checklist2 = new ChecklistDTO() { TaskName = "Paint the birdhouse", TaskID = 2, AppointmentID = 1 };
                ChecklistDTO checklist3 = new ChecklistDTO() { TaskName = "Buy a poster", TaskID = 3, AppointmentID = 1 };
                appointment1.AppointmentID = 1;
                appointment1.AppointmentName = "Maintenancy Saturday";
                appointment1.StartDate = DateTime.Now.AddHours(3);
                appointment1.ChecklistDTOs.Add(checklist1);
                appointment1.ChecklistDTOs.Add(checklist2);
                appointment1.ChecklistDTOs.Add(checklist3);
                appointmentDTO.Add(appointment1);

                AppointmentDTO appointment2 = new AppointmentDTO();
                ChecklistDTO checklist4 = new ChecklistDTO() { TaskName = "Listen to music", TaskID = 4, AppointmentID = 2 };
                ChecklistDTO checklist5 = new ChecklistDTO() { TaskName = "Make coffee", TaskID = 5, AppointmentID = 2 };
                appointment2.AppointmentID = 2;
                appointment2.AppointmentName = "Relax Sunday";
                appointment2.StartDate = DateTime.Now.AddHours(2);
                appointment2.ChecklistDTOs.Add(checklist4);
                appointment2.ChecklistDTOs.Add(checklist5);
                appointmentDTO.Add(appointment2);
            }
            else if (accountID == 54)
            {
                AppointmentDTO appointment1 = new AppointmentDTO();
                appointment1.AppointmentID = 1;
                appointment1.AppointmentName = "Look up info about render servers.";
                appointment1.StartDate = DateTime.Now.AddHours(3);
                appointment1.DescriptionDTO.Description = "The render servers must support Blender.";
                appointmentDTO.Add(appointment1);
            }
            return appointmentDTO;
        }

        public JobDTO GetWorkHours(int agendaID, List<DateTime> weekDates)
        {
            throw new NotImplementedException();
        }
    }
}
