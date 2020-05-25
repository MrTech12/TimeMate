using BusinessLogicLayer.Logic;
using DataAccessLayer.DTO;
using System;
using TimeMateTest.Stubs;
using Xunit;

namespace TimeMateTest.BLL
{
    public class AgendaTest
    {
        private Agenda agenda;
        private AccountDTO accountDTO;
        private AgendaDTO agendaDTO;

        [Fact]
        public void GetAgendaID()
        {
            int agendaID;
            accountDTO = new AccountDTO(){ AccountID = 12};
            agenda = new Agenda(accountDTO, new StubAgendaContext());

            agendaID = agenda.GetAgendaID("Personal");
            Assert.Equal(12, agendaID);
        }

        [Fact]
        public void GetAppointmentID()
        {
            int appointmentID;
            AppointmentDTO appointmentDTO = new AppointmentDTO() { AppointmentName = "Shopping"};

            accountDTO = new AccountDTO() { AccountID = 12 };
            agenda = new Agenda(accountDTO, new StubAgendaContext(), new StubAppointmentContext());

            appointmentID = agenda.GetAppointmentID(appointmentDTO, 12);
            Assert.Equal(8, appointmentID);
        }

        [Fact]
        public void RetrieveAppointmentsTest()
        {

        }
    }
}
