using BusinessLogicLayer.Logic;
using DataAccessLayer.DTO;
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
            job = new Job(new StubJobContainer(), new StubAgendaContainer(), new StubAppointmentContainer());

            jobDTO = job.RetrieveJobDetails(-5);

            Assert.Equal(0, jobDTO.WeeklyPay);
            Assert.Null(jobDTO.WeeklyHours);
        }

        [Fact]
        public void WorkdayJob()
        {
            JobDTO jobDTO = new JobDTO();
            job = new Job(new StubJobContainer(), new StubAgendaContainer(), new StubAppointmentContainer());

            jobDTO = job.RetrieveJobDetails(15);

            Assert.Equal(44, jobDTO.WeeklyPay);
            Assert.Equal("3,52", jobDTO.WeeklyHours);
        }

        [Fact]
        public void WeekendJob()
        {
            JobDTO jobDTO = new JobDTO();
            job = new Job(new StubJobContainer(), new StubAgendaContainer(), new StubAppointmentContainer());

            jobDTO = job.RetrieveJobDetails(25);

            Assert.Equal(206.856, jobDTO.WeeklyPay);
            Assert.Equal("13,52", jobDTO.WeeklyHours);
        }

        [Fact]
        public void TwoWorkHoursForWorkdays()
        {
            string output;
            job = new Job(new StubJobContainer(), new StubAgendaContainer(), new StubAppointmentContainer());

            output = job.RetrieveWorkdayHours(6);

            Assert.Equal("12,00", output);
        }

        [Fact]
        public void ZeroWorkHoursForWorkdays()
        {
            string output;
            job = new Job(new StubJobContainer(), new StubAgendaContainer(), new StubAppointmentContainer());

            output = job.RetrieveWorkdayHours(7);

            Assert.Equal("0,00", output);
        }

        [Fact]
        public void TwoWorkHoursForWeekend()
        {
            string output;
            job = new Job(new StubJobContainer(), new StubAgendaContainer(), new StubAppointmentContainer());

            output = job.RetrieveWeekendHours(8);

            Assert.Equal("16,33", output);
        }

        [Fact]
        public void ZeroWorkHoursForWeekend()
        {
            string output;
            job = new Job(new StubJobContainer(), new StubAgendaContainer(), new StubAppointmentContainer());

            output = job.RetrieveWeekendHours(9);

            Assert.Equal("0,00", output);
        }

        [Fact]
        public void FourAndHalfWorkedHours()
        {
            string output;
            job = new Job(new StubJobContainer(), new StubAgendaContainer(), new StubAppointmentContainer());
            JobDTO jobDTO = new JobDTO();
            jobDTO.StartDate.Add(DateTime.Parse("2020-06-01 14:00:00"));
            jobDTO.EndDate.Add(DateTime.Parse("2020-06-01 18:20:00"));

            output = job.CalculateWorkedHours(jobDTO);

            Assert.Equal("4,33", output);
        }

        [Fact]
        public void ZeroWorkedHours()
        {
            string output;
            job = new Job(new StubJobContainer(), new StubAgendaContainer(), new StubAppointmentContainer());
            JobDTO jobDTO = new JobDTO();

            output = job.CalculateWorkedHours(jobDTO);

            Assert.Equal("0,00", output);
        }

        [Fact]
        public void FirstDayOfWeekSix()
        {
            DateTime output;
            DateTime weekSix = DateTime.Parse("2020-02-06");
            job = new Job(new StubJobContainer(), new StubAgendaContainer(), new StubAppointmentContainer());

            output = job.GetFirstDayOfWeek(weekSix);

            Assert.Equal(DateTime.Parse("2020-02-03"), output);
        }

        [Fact]
        public void NoFirstDay()
        {
            DateTime output;
            DateTime empty = new DateTime();
            job = new Job(new StubJobContainer(), new StubAgendaContainer(), new StubAppointmentContainer());

            output = job.GetFirstDayOfWeek(empty);

            Assert.Equal(DateTime.Parse("0001-1-1"), output);
        }
    }
}
