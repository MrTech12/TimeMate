using BusinessLogicLayer.Logic;
using DataAccessLayer.DTO;
using System;
using System.Collections.Generic;
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
        public void GetAppointmentID()
        {
            int output;
            AppointmentDTO appointmentDTO = new AppointmentDTO() { AppointmentName = "Shopping"};
            accountDTO = new AccountDTO() { AccountID = 12 };
            agenda = new Agenda(accountDTO, new StubAgendaContext(), new StubAppointmentContext());

            output = agenda.GetAppointmentID(appointmentDTO, 36);

            Assert.Equal(8, output);
        }

        [Fact]
        public void RetrieveAppointmentsTest()
        {
            List<AppointmentDTO> output = new List<AppointmentDTO>();
            accountDTO = new AccountDTO() { AccountID = 12 };
            agenda = new Agenda(accountDTO, new StubAgendaContext());

            output = agenda.RetrieveAppointments();

            Assert.Contains("Walk the dog", output[1].AppointmentName);
            Assert.True(output.Count == 3);
        }

        [Fact]
        public void CreateNAppointmentTest()
        {
            accountDTO = new AccountDTO() { AccountID = 12 };
            agenda = new Agenda(accountDTO, new StubAgendaContext(), new StubAppointmentContext(), new StubNormalAppointmentContext());
            AppointmentDTO appointmentDTOFake = new AppointmentDTO() { AppointmentName = null, StartDate = DateTime.Today };

            appointmentDTO = new AppointmentDTO()
            {
                AppointmentName = "Reorder cables",
                StartDate = DateTime.Now.AddHours(2),
                EndDate = DateTime.Now.AddHours(4),
                Description = "This is <b> a </b> test.",
                AgendaID = 2
            };

            string before = Convert.ToString(agenda.GetAppointmentID(appointmentDTOFake, 0));
            agenda.CreateNormalAppointment(appointmentDTO);
            string after = Convert.ToString(agenda.GetAppointmentID(appointmentDTO, 2));

            Assert.Equal("-1", before);
            Assert.Equal("8", after);
        }

        [Fact]
        public void CreateCAppointmentTest()
        {
            accountDTO = new AccountDTO() { AccountID = 12 };
            agenda = new Agenda(accountDTO, new StubAgendaContext(), new StubAppointmentContext(), new StubChecklistAppointmentContext());
            AppointmentDTO appointmentDTOFake = new AppointmentDTO() { AppointmentName = null, StartDate = DateTime.Today };

            appointmentDTO = new AppointmentDTO();
            appointmentDTO.AppointmentName = "Create 3D render";
            appointmentDTO.StartDate = DateTime.Now.AddHours(3);
            appointmentDTO.EndDate = DateTime.Now.AddHours(4);
            appointmentDTO.AgendaID = 2;
            appointmentDTO.ChecklistItemName.Add("Get inspiration");

            string before = Convert.ToString(agenda.GetAppointmentID(appointmentDTOFake, 0));
            agenda.CreateChecklistAppointment(appointmentDTO);
            string after = Convert.ToString(agenda.GetAppointmentID(appointmentDTO, 2));

            Assert.Equal("-1", before);
            Assert.Equal("48", after);
        }
    }
}
