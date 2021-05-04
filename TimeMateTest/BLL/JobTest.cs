using BusinessLogicLayer.Logic;
using Model.DTO_s;
using System;
using System.Collections.Generic;
using TimeMateTest.Stubs;
using Xunit;

namespace TimeMateTest.BLL
{
    public class JobTest
    {
        private Job job;

        [Fact]
        public void NoJob()
        {
            JobDTO jobDTO = new JobDTO();
            job = new Job(new StubJobRepository(), new StubAgendaRepository(), new StubAppointmentRepository());

            jobDTO = job.RetrieveJobDetails(-5);

            Assert.Equal(0, jobDTO.WeeklyPay);
            Assert.Equal(0,jobDTO.WeeklyHours);
        }

        [Fact]
        public void WorkdayJob()
        {
            JobDTO jobDTO = new JobDTO();
            job = new Job(new StubJobRepository(), new StubAgendaRepository(), new StubAppointmentRepository());

            jobDTO = job.RetrieveJobDetails(15);

            Assert.Equal(43.958333333333336, jobDTO.WeeklyPay);
            Assert.Equal(3.5166666666666666, jobDTO.WeeklyHours);
        }

        [Fact]
        public void WeekendJob()
        {
            JobDTO jobDTO = new JobDTO();
            job = new Job(new StubJobRepository(), new StubAgendaRepository(), new StubAppointmentRepository());

            jobDTO = job.RetrieveJobDetails(25);

            Assert.Equal(206.805, jobDTO.WeeklyPay);
            Assert.Equal(13.516666666666666, jobDTO.WeeklyHours);
        }

        [Fact]
        public void TwoWorkHoursForWorkdays()
        {
            double output;
            job = new Job(new StubJobRepository(), new StubAgendaRepository(), new StubAppointmentRepository());

            output = job.RetrieveWorkdayHours(6);

            Assert.Equal(12.00, output);
        }

        [Fact]
        public void ZeroWorkHoursForWorkdays()
        {
            double output;
            job = new Job(new StubJobRepository(), new StubAgendaRepository(), new StubAppointmentRepository());

            output = job.RetrieveWorkdayHours(7);

            Assert.Equal(0.00, output);
        }

        [Fact]
        public void TwoWorkHoursForWeekend()
        {
            double output;
            job = new Job(new StubJobRepository(), new StubAgendaRepository(), new StubAppointmentRepository());

            output = job.RetrieveWeekendHours(8);

            Assert.Equal(16.333333333333336, output);
        }

        [Fact]
        public void ZeroWorkHoursForWeekend()
        {
            double output;
            job = new Job(new StubJobRepository(), new StubAgendaRepository(), new StubAppointmentRepository());

            output = job.RetrieveWeekendHours(9);

            Assert.Equal(0.00, output);
        }

        [Fact]
        public void FourAndHalfWorkedHours()
        {
            double output;
            job = new Job(new StubJobRepository(), new StubAgendaRepository(), new StubAppointmentRepository());
            JobDTO jobDTO = new JobDTO();
            jobDTO.StartDate.Add(DateTime.Parse("2020-06-01 14:00:00"));
            jobDTO.EndDate.Add(DateTime.Parse("2020-06-01 18:20:00"));

            output = job.CalculateWorkedHours(jobDTO);

            Assert.Equal(4.333333333333333, output);
        }

        [Fact]
        public void ZeroWorkedHours()
        {
            double output;
            job = new Job(new StubJobRepository(), new StubAgendaRepository(), new StubAppointmentRepository());
            JobDTO jobDTO = new JobDTO();

            output = job.CalculateWorkedHours(jobDTO);

            Assert.Equal(0.00, output);
        }

        [Fact]
        public void FirstDayOfWeekSix()
        {
            DateTime output;
            DateTime weekSixThursday = DateTime.Parse("2020-02-06");
            job = new Job(new StubJobRepository(), new StubAgendaRepository(), new StubAppointmentRepository());

            output = job.GetFirstDayOfWeek(weekSixThursday);

            Assert.Equal(DateTime.Parse("2020-02-03"), output);
        }

        [Fact]
        public void FirstDayOfWeekFour()
        {
            DateTime output;
            DateTime weekFourSunday = DateTime.Parse("2020-01-26");
            job = new Job(new StubJobRepository(), new StubAgendaRepository(), new StubAppointmentRepository());

            output = job.GetFirstDayOfWeek(weekFourSunday);

            Assert.Equal(DateTime.Parse("2020-01-20"), output);
        }

        [Fact]
        public void NoFirstDay()
        {
            DateTime output;
            DateTime empty = new DateTime();
            job = new Job(new StubJobRepository(), new StubAgendaRepository(), new StubAppointmentRepository());

            output = job.GetFirstDayOfWeek(empty);

            Assert.Equal(DateTime.Parse("0001-1-1"), output);
        }
    }
}
