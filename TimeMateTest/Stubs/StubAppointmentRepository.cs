using Core.Entities;
using Core.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
namespace TimeMateTest.Stubs
{
    class StubAppointmentRepository : IAppointmentRepository
    {
        public int CreateAppointment(Appointment appointment)
        {
            int appointmentID = 0;
            using (StreamWriter streamWriter = new StreamWriter(@"C:\tmp\addAppointmentTest.txt"))
            {
                streamWriter.WriteLine(appointment.AppointmentName);
                streamWriter.WriteLine(appointment.StartDate);
                streamWriter.WriteLine(appointment.EndDate);
                streamWriter.WriteLine(appointment.AgendaName);
                appointmentID = 60;
            }
            return appointmentID;
        }

        public List<Appointment> GetAppointments(int accountID)
        {
            List<Appointment> appointments = new List<Appointment>();
            if (accountID == 12)
            {
                Task task = new Task() { TaskName = null, TaskID = 0, AppointmentID = 0 };

                Appointment appointment1 = new Appointment();
                appointment1.AppointmentID = 1;
                appointment1.AppointmentName = "Walk the dog";
                appointment1.StartDate = DateTime.Now.AddHours(3);
                appointment1.TaskList.Add(task);
                appointments.Add(appointment1);

                Appointment appointment2 = new Appointment();
                appointment2.AppointmentID = 2;
                appointment2.AppointmentName = "Do the dishes";
                appointment2.StartDate = DateTime.Now.AddHours(2);
                appointment2.TaskList.Add(task);
                appointments.Add(appointment2);

                Appointment appointment3 = new Appointment();
                appointment3.AppointmentID = 3;
                appointment3.AppointmentName = "Sleep for 7 hours";
                appointment3.StartDate = DateTime.Now.AddHours(7);
                appointment3.TaskList.Add(task);
                appointments.Add(appointment3);
            }
            else if (accountID == 42)
            {
                Task task1 = new Task() { TaskName = "Paint the birdhouse", TaskID = 0, AppointmentID = 1 };
                Appointment appointment1 = new Appointment();
                appointment1.AppointmentID = 1;
                appointment1.AppointmentName = "Maintenancy Saturday";
                appointment1.StartDate = DateTime.Now.AddHours(3);
                appointment1.TaskList.Add(task1);
                appointments.Add(appointment1);

                Task task2 = new Task() { TaskName = "Buy a poster", TaskID = 1, AppointmentID = 2 };
                Appointment appointment2 = new Appointment();
                appointment2.AppointmentID = 2;
                appointment2.AppointmentName = "Relax Sunday";
                appointment2.StartDate = DateTime.Now.AddHours(2);
                appointment2.TaskList.Add(task2);
                appointments.Add(appointment2);
            }
            else if (accountID == 54)
            {
                Task task = new Task() { TaskName = null, TaskID = 0, AppointmentID = 0 };
                Appointment appointment1 = new Appointment();
                appointment1.AppointmentID = 1;
                appointment1.AppointmentName = "Look up info about render servers.";
                appointment1.StartDate = DateTime.Now.AddHours(3);
                appointment1.Description.DescriptionName = "The render servers must support Blender.";
                appointment1.TaskList.Add(task);
                appointments.Add(appointment1);
            }
            return appointments;
        }

        public Job GetWorkHours(int agendaID, List<DateTime> dates)
        {
            Job job = new Job();
            if (agendaID == 1)
            {
                job.StartDate.Add(DateTime.Parse("2020-05-04 14:00:00"));
                job.EndDate.Add(DateTime.Parse("2020-05-04 16:00:00"));
                job.StartDate.Add(DateTime.Parse("2020-05-05 16:03:00"));
                job.EndDate.Add(DateTime.Parse("2020-05-05 17:34:00"));
            }
            else if (agendaID == 2)
            {
                job.StartDate.Add(DateTime.Parse("2020-05-09 14:00:00"));
                job.EndDate.Add(DateTime.Parse("2020-05-09 21:00:00"));
                job.StartDate.Add(DateTime.Parse("2020-05-10 09:03:00"));
                job.EndDate.Add(DateTime.Parse("2020-05-10 15:34:00"));
            }
            else if (agendaID == 3)
            {
                job.StartDate.Add(DateTime.Parse("2020-05-11 14:00:00"));
                job.EndDate.Add(DateTime.Parse("2020-05-11 21:00:00"));
                job.StartDate.Add(DateTime.Parse("2020-05-16 23:03:00"));
                job.EndDate.Add(DateTime.Parse("2020-05-17 02:04:00"));
            }
            else if (agendaID == 6)
            {
                job.StartDate.Add(DateTime.Parse("2020-05-04 14:00:00"));
                job.EndDate.Add(DateTime.Parse("2020-05-04 20:00:00"));
                job.StartDate.Add(DateTime.Parse("2020-05-05 08:00:00"));
                job.EndDate.Add(DateTime.Parse("2020-05-05 14:00:00"));
            }
            else if (agendaID == 8)
            {
                job.StartDate.Add(DateTime.Parse("2020-05-04 10:00:00"));
                job.EndDate.Add(DateTime.Parse("2020-05-04 18:20:00"));
                job.StartDate.Add(DateTime.Parse("2020-05-05 08:00:00"));
                job.EndDate.Add(DateTime.Parse("2020-05-05 16:00:00"));
            }
            return job;
        }
    }
}
