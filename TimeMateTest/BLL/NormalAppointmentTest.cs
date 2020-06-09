﻿using BusinessLogicLayer.Logic;
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
        public void GetDescriptionTest()
        {
            int appointmentID = 24;
            normalAppointment = new NormalAppointment(new StubNormalAppointmentContext());

            var output = normalAppointment.RetrieveDescription(appointmentID);

            Assert.Equal("Dit is een beschrijving", output);
        }
    }
}
