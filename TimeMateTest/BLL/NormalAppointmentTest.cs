using BusinessLogicLayer.Logic;
using DataAccessLayer.DTO;
using System;
using TimeMateTest.Stubs;
using Xunit;

namespace TimeMateTest.BLL
{
    public class NormalAppointmentTest
    {
        private AppointmentDTO appointmentDTO = new AppointmentDTO();
        private AccountDTO accountDTO;
        private NormalAppointment normalAppointment;

        [Fact]
        public void GetDescriptionTest()
        {
            appointmentDTO.AppointmentID = 24;
            normalAppointment = new NormalAppointment(appointmentDTO, new StubNormalAppointmentContext());

            var output = normalAppointment.RetrieveDescription(appointmentDTO);

            Assert.Equal("Dit is een beschrijving", output);
        }
    }
}
