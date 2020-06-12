using BusinessLogicLayer.Logic;
using DataAccessLayer.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TimeMateTest.Stubs;
using Xunit;

namespace TimeMateTest.BLL
{
    public class AppointmentCreationTest
    {
        private Agenda agenda;
        private AppointmentDTO appointmentDTO;

        [Fact]
        public void NormalAppointmentTest()
        {
            agenda = new Agenda(new StubAppointmentContainer(), new StubNormalAppointmentContainer());
            DescriptionDTO descriptionDTO = new DescriptionDTO() { Description = "This is <b> a </b> test." };

            appointmentDTO = new AppointmentDTO()
            {
                AppointmentName = "Reorder cables",
                StartDate = DateTime.Now.AddHours(2),
                EndDate = DateTime.Now.AddHours(4),
                DescriptionDTO = descriptionDTO,
                AgendaName = "Firefox"
            };

            agenda.CreateNormalAppointment(appointmentDTO);
            string[] file = File.ReadAllLines(@"C:\tmp\addAppointmentTest.txt");
            File.Delete(@"C:\tmp\addAppointmentTest.txt");

            Assert.Contains("Reorder cables", file[0]);
            Assert.Contains("This is <b> a </b> test.", file[4]);
        }

        [Fact]
        public void NormalAppointmentNoDescriptionTest()
        {
            agenda = new Agenda(new StubAppointmentContainer(), new StubNormalAppointmentContainer());

            appointmentDTO = new AppointmentDTO()
            {
                AppointmentName = "Reorder cables",
                StartDate = DateTime.Now.AddHours(2),
                EndDate = DateTime.Now.AddHours(4),
                AgendaName = "Firefox"
            };

            agenda.CreateNormalAppointment(appointmentDTO);
            string[] file = File.ReadAllLines(@"C:\tmp\addAppointmentTest.txt");
            File.Delete(@"C:\tmp\addAppointmentTest.txt");

            Assert.Contains("Reorder cables", file[0]);
            Assert.Contains("Firefox", file[3]);
        }

        [Fact]
        public void ChecklistAppointmentTest()
        {
            agenda = new Agenda(new StubAppointmentContainer(), new StubChecklistAppointmentContainer());
            ChecklistDTO checklistDTO = new ChecklistDTO() { TaskName = "Get inspiration" };

            appointmentDTO = new AppointmentDTO();
            appointmentDTO.AppointmentName = "Create 3D render";
            appointmentDTO.StartDate = DateTime.Now.AddHours(3);
            appointmentDTO.EndDate = DateTime.Now.AddHours(4);
            appointmentDTO.AgendaName = "Firefox";
            appointmentDTO.ChecklistDTOs.Add(checklistDTO);

            agenda.CreateChecklistAppointment(appointmentDTO);
            string[] file = File.ReadAllLines(@"C:\tmp\addAppointmentTest.txt");
            File.Delete(@"C:\tmp\addAppointmentTest.txt");

            Assert.Contains("Create 3D render", file[0]);
            Assert.Contains("Get inspiration", file[4]);
        }

        [Fact]
        public void ChecklistAppointmenNoChecklistTest()
        {
            agenda = new Agenda(new StubAppointmentContainer(), new StubChecklistAppointmentContainer());

            appointmentDTO = new AppointmentDTO();
            appointmentDTO.AppointmentName = "Create 3D render";
            appointmentDTO.StartDate = DateTime.Now.AddHours(3);
            appointmentDTO.EndDate = DateTime.Now.AddHours(4);
            appointmentDTO.AgendaName = "Firefox";

            agenda.CreateChecklistAppointment(appointmentDTO);
            string[] file = File.ReadAllLines(@"C:\tmp\addAppointmentTest.txt");
            File.Delete(@"C:\tmp\addAppointmentTest.txt");

            Assert.Contains("Create 3D render", file[0]);
            Assert.Contains("Firefox", file[3]);
        }
    }
}
