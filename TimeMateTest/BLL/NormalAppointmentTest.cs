using BusinessLogicLayer.Logic;
using DataAccessLayer.DTO;
using System;
using TimeMateTest.Stubs;
using Xunit;

namespace TimeMateTest.BLL
{
    public class NormalAppointmentTest
    {
        private NormalAppointment normalAppointment;

        [Fact]
        public void GetDescription()
        {
            int appointmentID = 24;
            normalAppointment = new NormalAppointment(new StubNormalAppointmentRepository());

            string output = normalAppointment.RetrieveDescription(appointmentID);

            Assert.Equal("Dit is een beschrijving", output);
        }

        [Fact]
        public void GetNoDescription()
        {
            int appointmentID = 0;
            normalAppointment = new NormalAppointment(new StubNormalAppointmentRepository());

            string output = normalAppointment.RetrieveDescription(appointmentID);

            Assert.Null(output);
        }
    }
}
