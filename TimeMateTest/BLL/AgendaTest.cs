using BusinessLogicLayer.Logic;
using DataAccessLayer.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using TimeMateTest.Stubs;
using Xunit;

namespace TimeMateTest.BLL
{
    public class AgendaTest
    {
        private Agenda agenda;
        private AccountDTO accountDTO;

        [Fact]
        public void RetrieveAppointmentsTest()
        {
            List<AppointmentDTO> output = new List<AppointmentDTO>();
            accountDTO = new AccountDTO() { AccountID = 12 };
            agenda = new Agenda(accountDTO, new StubAppointmentContainer());

            output = agenda.RetrieveAppointments();

            Assert.Contains("Walk the dog", output[1].AppointmentName);
            Assert.True(output.Count == 3);
        }

        [Fact]
        public void RetrieveAppointmentsWithTasksTest()
        {
            List<AppointmentDTO> output = new List<AppointmentDTO>();
            accountDTO = new AccountDTO() { AccountID = 42 };
            agenda = new Agenda(accountDTO, new StubAppointmentContainer());

            output = agenda.RetrieveAppointments();

            Assert.Contains("Relax Sunday", output[0].AppointmentName);
            Assert.Contains("Listen to music", output[0].ChecklistDTOs[1].TaskName);
            Assert.Contains("Paint the birdhouse", output[1].ChecklistDTOs[0].TaskName);
            Assert.True(output.Count == 2);
        }

        [Fact]
        public void RetrieveAppointmentsWithDescriptionTest()
        {
            List<AppointmentDTO> output = new List<AppointmentDTO>();
            accountDTO = new AccountDTO() { AccountID = 54 };
            agenda = new Agenda(accountDTO, new StubAppointmentContainer());

            output = agenda.RetrieveAppointments();

            Assert.Contains("Look up info about render servers.", output[0].AppointmentName);
            Assert.Contains("The render servers must support Blender.", output[0].DescriptionDTO.Description);
            Assert.True(output.Count == 1);
        }
    }
}
