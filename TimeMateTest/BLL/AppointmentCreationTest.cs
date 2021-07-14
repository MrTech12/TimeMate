using BusinessLogicLayer.Logic;
using Core.DTOs;
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
        private NormalAppointmentService normalAppointmentService;
        private ChecklistAppointmentService checklistAppointmentService;
        private AppointmentDTO appointmentDTO;
        private string filePath = @"C:\tmp\addAppointmentTest.txt";

        [Fact]
        public void NormalAppointmentNoDescription()
        {
            normalAppointmentService = new NormalAppointmentService(new StubAppointmentRepository(), new StubNormalAppointmentRepository());

            appointmentDTO = new AppointmentDTO()
            {
                AppointmentName = "Reorder cables",
                StartDate = DateTime.Now.AddHours(2),
                EndDate = DateTime.Now.AddHours(4),
                AgendaName = "Firefox"
            };

            normalAppointmentService.AddNormalAppointment(appointmentDTO);

            string[] file = File.ReadAllLines(filePath);
            File.Delete(filePath);

            Assert.Contains("Reorder cables", file[0]);
            Assert.Contains("Firefox", file[3]);
            Assert.True(file.Length == 4);
        }

        [Fact]
        public void ChecklistAppointmenNoChecklist()
        {
            checklistAppointmentService = new ChecklistAppointmentService(new StubAppointmentRepository(), new StubChecklistAppointmentRepository());

            appointmentDTO = new AppointmentDTO();
            appointmentDTO.AppointmentID = 60;
            appointmentDTO.AppointmentName = "Create 3D render";
            appointmentDTO.StartDate = DateTime.Now.AddHours(3);
            appointmentDTO.EndDate = DateTime.Now.AddHours(4);
            appointmentDTO.AgendaName = "Firefox";

            checklistAppointmentService.AddChecklistAppointment(appointmentDTO);

            string[] file = File.ReadAllLines(@"C:\tmp\addAppointmentTest.txt");
            File.Delete(@"C:\tmp\addAppointmentTest.txt");

            Assert.Contains("Create 3D render", file[0]);
            Assert.Contains("Firefox", file[3]);
            Assert.True(file.Length == 4);
        }
    }
}
