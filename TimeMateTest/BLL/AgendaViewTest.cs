using BusinessLogicLayer.Logic;
using Core.DTOs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using TimeMateTest.Stubs;
using Xunit;

namespace TimeMateTest.BLL
{
    public class AgendaViewTest
    {
        private AgendaView agendaView;
        private AccountDTO accountDTO;

        [Fact]
        public void RetrieveNoAppointments()
        {
            List<AppointmentDTO> output = new List<AppointmentDTO>();
            accountDTO = new AccountDTO() { AccountID = -5 };
            agendaView = new AgendaView(accountDTO, new StubAppointmentRepository());

            output = agendaView.RetrieveAppointments();

            Assert.True(output.Count == 0);
        }

        [Fact]
        public void RetrieveAppointments()
        {
            List<AppointmentDTO> output = new List<AppointmentDTO>();
            accountDTO = new AccountDTO() { AccountID = 12 };
            agendaView = new AgendaView(accountDTO, new StubAppointmentRepository());

            output = agendaView.RetrieveAppointments();

            Assert.Contains("Walk the dog", output[1].AppointmentName);
            Assert.True(output.Count == 3);
        }

        [Fact]
        public void RetrieveAppointmentsWithTasks()
        {
            List<AppointmentDTO> output = new List<AppointmentDTO>();
            accountDTO = new AccountDTO() { AccountID = 42 };
            agendaView = new AgendaView(accountDTO, new StubAppointmentRepository());

            output = agendaView.RetrieveAppointments();

            Assert.Contains("Relax Sunday", output[0].AppointmentName);
            Assert.Contains("Listen to music", output[0].TaskList[1].TaskName);
            Assert.Contains("Paint the birdhouse", output[1].TaskList[0].TaskName);
            Assert.True(output.Count == 2);
        }

        [Fact]
        public void RetrieveAppointmentsWithDescription()
        {
            List<AppointmentDTO> output = new List<AppointmentDTO>();
            accountDTO = new AccountDTO() { AccountID = 54 };
            agendaView = new AgendaView(accountDTO, new StubAppointmentRepository());

            output = agendaView.RetrieveAppointments();

            Assert.Contains("Look up info about render servers.", output[0].AppointmentName);
            Assert.Contains("The render servers must support Blender.", output[0].DescriptionDTO.Description);
            Assert.True(output.Count == 1);
        }
    }
}
