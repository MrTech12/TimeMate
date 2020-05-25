using BusinessLogicLayer.Logic;
using DataAccessLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public void GetAgendaID()
        {
            int output;
            accountDTO = new AccountDTO(){ AccountID = 12};
            agenda = new Agenda(accountDTO, new StubAgendaContext());

            output = agenda.GetAgendaID("Personal");
            Assert.Equal(12, output);
        }

        [Fact]
        public void GetAppointmentID()
        {
            int output;
            AppointmentDTO appointmentDTO = new AppointmentDTO() { AppointmentName = "Shopping"};
            accountDTO = new AccountDTO() { AccountID = 12 };
            agenda = new Agenda(accountDTO, new StubAgendaContext(), new StubAppointmentContext());

            output = agenda.GetAppointmentID(appointmentDTO, 12);
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
    }
}
