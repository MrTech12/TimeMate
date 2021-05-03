using System;
using System.Collections.Generic;
using System.Text;
using TimeMate.Models;
using TimeMate.Services;
using Xunit;

namespace TimeMateTest.Service
{
    public class AppointmentServiceTest
    {
        AppointmentService appointmentService = new AppointmentService();
        AppointmentViewModel viewModel;

        [Fact]
        public void ValidateEarlyEndDate()
        {
            viewModel = new AppointmentViewModel() { StartDate = DateTime.Now.AddDays(-5), EndDate = DateTime.Now.AddDays(-10) };

            bool validDates = appointmentService.ValidateDates(viewModel);

            Assert.False(validDates);
        }

        [Fact]
        public void ValidateLaterStartDate()
        {
            viewModel = new AppointmentViewModel() { StartDate = DateTime.Now.AddDays(10), EndDate = DateTime.Now.AddDays(-10) };

            bool validDates = appointmentService.ValidateDates(viewModel);

            Assert.False(validDates);
        }

        [Fact]
        public void ValidateNormalDate()
        {
            viewModel = new AppointmentViewModel() { StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(10) };

            bool validDates = appointmentService.ValidateDates(viewModel);

            Assert.True(validDates);
        }
    }
}
