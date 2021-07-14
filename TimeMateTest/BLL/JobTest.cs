using BusinessLogicLayer.Logic;
using Core.DTOs;
using System;
using System.Collections.Generic;
using TimeMateTest.Stubs;
using Xunit;

namespace TimeMateTest.BLL
{
    public class JobTest
    {
        private JobService jobService;

        [Fact]
        public void NoJob()
        {
            JobDTO jobDTO = new JobDTO();
            jobService = new JobService(new StubJobRepository(), new StubAgendaRepository(), new StubAppointmentRepository());

            jobDTO = jobService.RetrieveJobDetails(-5);

            Assert.Equal(0, jobDTO.WeeklyPay);
            Assert.Equal(0,jobDTO.WeeklyHours);
        }

        [Fact]
        public void WorkdayJob()
        {
            JobDTO jobDTO = new JobDTO();
            jobService = new JobService(new StubJobRepository(), new StubAgendaRepository(), new StubAppointmentRepository());

            jobDTO = jobService.RetrieveJobDetails(15);

            Assert.Equal(43.958333333333336, jobDTO.WeeklyPay);
            Assert.Equal(3.5166666666666666, jobDTO.WeeklyHours);
        }

        [Fact]
        public void WeekendJob()
        {
            JobDTO jobDTO = new JobDTO();
            jobService = new JobService(new StubJobRepository(), new StubAgendaRepository(), new StubAppointmentRepository());

            jobDTO = jobService.RetrieveJobDetails(25);

            Assert.Equal(206.805, jobDTO.WeeklyPay);
            Assert.Equal(13.516666666666666, jobDTO.WeeklyHours);
        }

        [Fact]
        public void TwoWorkHoursForWorkdays()
        {
            double output;
            jobService = new JobService(new StubJobRepository(), new StubAgendaRepository(), new StubAppointmentRepository());

            output = jobService.RetrieveWorkdayHours(6);

            Assert.Equal(12.00, output);
        }

        [Fact]
        public void ZeroWorkHoursForWorkdays()
        {
            double output;
            jobService = new JobService(new StubJobRepository(), new StubAgendaRepository(), new StubAppointmentRepository());

            output = jobService.RetrieveWorkdayHours(7);

            Assert.Equal(0.00, output);
        }

        [Fact]
        public void TwoWorkHoursForWeekend()
        {
            double output;
            jobService = new JobService(new StubJobRepository(), new StubAgendaRepository(), new StubAppointmentRepository());

            output = jobService.RetrieveWeekendHours(8);

            Assert.Equal(16.333333333333336, output);
        }

        [Fact]
        public void ZeroWorkHoursForWeekend()
        {
            double output;
            jobService = new JobService(new StubJobRepository(), new StubAgendaRepository(), new StubAppointmentRepository());

            output = jobService.RetrieveWeekendHours(9);

            Assert.Equal(0.00, output);
        }

        [Fact]
        public void FourAndHalfWorkedHours()
        {
            double output;
            jobService = new JobService(new StubJobRepository(), new StubAgendaRepository(), new StubAppointmentRepository());
            JobDTO jobDTO = new JobDTO();
            jobDTO.StartDate.Add(DateTime.Parse("2020-06-01 14:00:00"));
            jobDTO.EndDate.Add(DateTime.Parse("2020-06-01 18:20:00"));

            output = jobService.CalculateWorkedHours(jobDTO);

            Assert.Equal(4.333333333333333, output);
        }

        [Fact]
        public void ZeroWorkedHours()
        {
            double output;
            jobService = new JobService(new StubJobRepository(), new StubAgendaRepository(), new StubAppointmentRepository());
            JobDTO jobDTO = new JobDTO();

            output = jobService.CalculateWorkedHours(jobDTO);

            Assert.Equal(0.00, output);
        }

        [Fact]
        public void FirstDayOfWeekSix()
        {
            DateTime output;
            DateTime weekSixThursday = DateTime.Parse("2020-02-06");
            jobService = new JobService(new StubJobRepository(), new StubAgendaRepository(), new StubAppointmentRepository());

            output = jobService.GetFirstDayOfWeek(weekSixThursday);

            Assert.Equal(DateTime.Parse("2020-02-03"), output);
        }

        [Fact]
        public void FirstDayOfWeekFour()
        {
            DateTime output;
            DateTime weekFourSunday = DateTime.Parse("2020-01-26");
            jobService = new JobService(new StubJobRepository(), new StubAgendaRepository(), new StubAppointmentRepository());

            output = jobService.GetFirstDayOfWeek(weekFourSunday);

            Assert.Equal(DateTime.Parse("2020-01-20"), output);
        }

        [Fact]
        public void NoFirstDay()
        {
            DateTime output;
            DateTime empty = new DateTime();
            jobService = new JobService(new StubJobRepository(), new StubAgendaRepository(), new StubAppointmentRepository());

            output = jobService.GetFirstDayOfWeek(empty);

            Assert.Equal(DateTime.Parse("0001-1-1"), output);
        }
    }
}
