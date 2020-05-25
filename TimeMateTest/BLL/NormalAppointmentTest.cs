using BusinessLogicLayer.Logic;
using DataAccessLayer.DTO;
using System;
using TimeMateTest.Stubs;
using Xunit;

namespace TimeMateTest.BLL
{
    public class NormalAppointmentTest
    {
        private AccountDTO accountDTO;
        private NormalAppointment normalAppointment;

        [Fact]
        public void RetrieveDescriptionTest()
        {
            string output;
            AppointmentDTO appointmentDTO = new AppointmentDTO() { AppointmentID = 12 };
            normalAppointment = new NormalAppointment(appointmentDTO, new StubNormalAppointmentContext());

            output = normalAppointment.RetrieveDescription(appointmentDTO.AppointmentID);

            Assert.Equal("This needs to happen.", output);
        }
    }
}
