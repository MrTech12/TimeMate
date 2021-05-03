using BusinessLogicLayer.Logic;
using DataAccessLayer.DTO;
using System;
using System.IO;
using TimeMateTest.Stubs;
using Xunit;

namespace TimeMateTest.BLL
{
    public class NormalAppointmentTest
    {
        private NormalAppointment normalAppointment;
        private AppointmentDTO appointmentDTO;
        private string filePath = @"C:\tmp\addAppointmentTest.txt";

        [Fact]
        public void CreateNormalAppointment()
        {
            normalAppointment = new NormalAppointment(new StubAppointmentRepository(), new StubNormalAppointmentRepository());
            DescriptionDTO descriptionDTO = new DescriptionDTO() { Description = "This is <b> a </b> test." };

            appointmentDTO = new AppointmentDTO()
            {
                AppointmentName = "Reorder cables",
                StartDate = DateTime.Now.AddHours(2),
                EndDate = DateTime.Now.AddHours(4),
                DescriptionDTO = descriptionDTO,
                AgendaName = "Firefox"
            };

            normalAppointment.CreateNormalAppointment(appointmentDTO);

            string[] appointmentFile = File.ReadAllLines(filePath);
            File.Delete(filePath);

            Assert.Contains("Reorder cables", appointmentFile[0]);
            Assert.Contains("This is <b> a </b> test.", appointmentFile[4]);
            Assert.True(appointmentFile.Length == 5);
        }

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
