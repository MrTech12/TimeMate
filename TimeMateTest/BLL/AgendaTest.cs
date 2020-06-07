using BusinessLogicLayer.Logic;
using DataAccessLayer.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using TimeMateTest.Stubs;
using Xunit;

namespace TimeMateTest.BLL
{
    public class AgendaTest
    {
        private Agenda agenda;
        private AccountDTO accountDTO;
        private AppointmentDTO appointmentDTO;

        [Fact]
        public void RetrieveAppointmentsTest()
        {
            List<AppointmentDTO> output = new List<AppointmentDTO>();
            accountDTO = new AccountDTO() { AccountID = 12 };
            agenda = new Agenda(accountDTO, new StubAgendaContext(), new StubAppointmentContext());

            output = agenda.RetrieveAppointments();

            Assert.Contains("Walk the dog", output[1].AppointmentName);
            Assert.True(output.Count == 3);
        }


        [Fact]
        public void CreateNormalAppointmentTest()
        {
            accountDTO = new AccountDTO() { AccountID = 12 };
            agenda = new Agenda(accountDTO, new StubAgendaContext(), new StubAppointmentContext(), new StubNormalAppointmentContext());
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
        public void CreateChecklistAppointmentTest()
        {
            accountDTO = new AccountDTO() { AccountID = 12 };
            agenda = new Agenda(accountDTO, new StubAgendaContext(), new StubAppointmentContext(), new StubChecklistAppointmentContext());
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
    }
}
