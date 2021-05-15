﻿using BusinessLogicLayer.Logic;
using Model.DTO_s;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TimeMateTest.Stubs;
using Xunit;

namespace TimeMateTest.BLL
{
    public class AgendaTest
    {
        private Agenda agenda;
        private AccountDTO accountDTO;
        private StubJobRepository _stubJobRepository = new StubJobRepository();
        private string filePathAgendaCreation = @"C:\tmp\addAgendaTest.txt";
        private string filePathWorkDetails = @"C:\tmp\addWorkPayDetails.txt";
        private string filePathAgendaDeletion = @"C:\tmp\removeAgendaTest.txt";

        [Fact]
        public void CreateAgenda1()
        {
            accountDTO = new AccountDTO() { AccountID = 12 };
            agenda = new Agenda(accountDTO, new StubAgendaRepository());
            AgendaDTO agendaDTO = new AgendaDTO() { AgendaName = "Homework", AgendaColor = "#0x0000", NotificationType = "Nee" };

            agenda.AddAgenda(agendaDTO);

            string[] file = File.ReadAllLines(filePathAgendaCreation);
            File.Delete(filePathAgendaCreation);

            Assert.Contains("Homework", file[0]);
            Assert.Contains("#0x0000", file[1]);
        }

        [Fact]
        public void CreateAgenda2()
        {
            accountDTO = new AccountDTO() { AccountID = 12 };
            agenda = new Agenda(accountDTO, new StubAgendaRepository());
            AgendaDTO agendaDTO = new AgendaDTO() { AgendaName = "Skype", AgendaColor = "#15F560", NotificationType = "Nee" };

            agenda.AddAgenda(agendaDTO);

            string[] file = File.ReadAllLines(filePathAgendaCreation);
            File.Delete(filePathAgendaCreation);

            Assert.Contains("Skype", file[0]);
            Assert.Contains("#15F560", file[1]);
        }

        [Fact]
        public void CreateNoAgenda()
        {
            accountDTO = new AccountDTO() { AccountID = 12 };
            agenda = new Agenda(accountDTO, new StubAgendaRepository());
            AgendaDTO agendaDTO = new AgendaDTO() { AgendaName = "", AgendaColor = "", NotificationType = "" };

            agenda.AddAgenda(agendaDTO);

            string[] file = File.ReadAllLines(filePathAgendaCreation);
            File.Delete(filePathAgendaCreation);

            Assert.Contains("", file[0]);
            Assert.Contains("", file[1]);
        }

        [Fact]
        public void CreateWorkAgendaWithPayDetails1()
        {
            AgendaDTO workAgendaDTO = new AgendaDTO() { AgendaName = "Bijbaan", AgendaColor = "#FF0000", NotificationType = "Nee" };
            accountDTO = new AccountDTO() { AccountID = 12 };
            accountDTO.JobHourlyWage.Add(12.23);
            accountDTO.JobDayType.Add("Doordeweeks");
            agenda = new Agenda(accountDTO, new StubAgendaRepository());

            agenda.AddAgenda(workAgendaDTO);
            _stubJobRepository.CreatePayDetails(accountDTO);

            string[] fileAgenda = File.ReadAllLines(filePathAgendaCreation);
            string[] filePay = File.ReadAllLines(filePathWorkDetails);
            File.Delete(filePathAgendaCreation);
            File.Delete(filePathWorkDetails);

            Assert.Contains("Bijbaan", fileAgenda[0]);
            Assert.Contains("#FF0000", fileAgenda[1]);
            Assert.Contains("12,23", filePay[0]);
            Assert.Contains("Doordeweeks", filePay[1]);
            Assert.True(filePay.Length == 2);
        }

        [Fact]
        public void CreateWorkAgendaWithPayDetails2()
        {
            AgendaDTO workAgendaDTO = new AgendaDTO() { AgendaName = "Bijbaan", AgendaColor = "#FF0000", NotificationType = "Nee" };
            accountDTO = new AccountDTO() { AccountID = 56 };
            accountDTO.JobHourlyWage.Add(12.23);
            accountDTO.JobDayType.Add("Doordeweeks");
            accountDTO.JobHourlyWage.Add(10);
            accountDTO.JobDayType.Add("Weekend");
            agenda = new Agenda(accountDTO, new StubAgendaRepository());

            agenda.AddAgenda(workAgendaDTO);
            _stubJobRepository.CreatePayDetails(accountDTO);

            string[] fileAgenda = File.ReadAllLines(filePathAgendaCreation);
            string[] filePay = File.ReadAllLines(filePathWorkDetails);
            File.Delete(filePathAgendaCreation);
            File.Delete(filePathWorkDetails);

            Assert.Contains("Bijbaan", fileAgenda[0]);
            Assert.Contains("#FF0000", fileAgenda[1]);
            Assert.Contains("12,23", filePay[0]);
            Assert.Contains("Doordeweeks", filePay[1]);
            Assert.Contains("10", filePay[2]);
            Assert.Contains("Weekend", filePay[3]);
            Assert.True(filePay.Length == 4);
        }

        [Fact]
        public void CreateOnlyWorkAgenda()
        {
            AgendaDTO workAgendaDTO = new AgendaDTO() { AgendaName = "Bijbaan", AgendaColor = "#FF0000", NotificationType = "Nee" };
            accountDTO = new AccountDTO() { AccountID = 23 };
            agenda = new Agenda(accountDTO, new StubAgendaRepository());

            agenda.AddAgenda(workAgendaDTO);
            _stubJobRepository.CreatePayDetails(accountDTO);

            string[] fileAgenda = File.ReadAllLines(filePathAgendaCreation);
            string[] filePay = File.ReadAllLines(filePathWorkDetails);
            File.Delete(filePathAgendaCreation);
            File.Delete(filePathWorkDetails);

            Assert.Contains("Bijbaan", fileAgenda[0]);
            Assert.Contains("#FF0000", fileAgenda[1]);
            Assert.True(filePay.Length == 0);
        }

        [Fact]
        public void GetAgendaNames()
        {
            List<AgendaDTO> output = new List<AgendaDTO>();
            accountDTO = new AccountDTO() { AccountID = 12 };
            agenda = new Agenda(accountDTO, new StubAgendaRepository());

            output = agenda.RetrieveAgendas();

            Assert.Contains("Work", output[0].AgendaName);
            Assert.True(output.Count == 2);
        }

        [Fact]
        public void GetZeroAgendaNames()
        {
            List<AgendaDTO> output = new List<AgendaDTO>();
            accountDTO = new AccountDTO() { AccountID = 128 };
            agenda = new Agenda(accountDTO, new StubAgendaRepository());

            output = agenda.RetrieveAgendas();

            Assert.True(output.Count == 0);
        }

        [Fact]
        public void RemoveAgenda()
        {
            accountDTO = new AccountDTO() { AccountID = 12 };
            AgendaDTO agendaDTO = new AgendaDTO() { AgendaID = 51, AgendaName = "qwerty", AgendaColor = "#0X2312", NotificationType = "Nee" };
            agenda = new Agenda(accountDTO, new StubAgendaRepository());

            using (StreamWriter streamWriter = new StreamWriter(filePathAgendaDeletion))
            {
                streamWriter.WriteLine(agendaDTO.AgendaID);
                streamWriter.WriteLine(agendaDTO.AgendaName);
                streamWriter.WriteLine(agendaDTO.AgendaColor);
                streamWriter.WriteLine(agendaDTO.NotificationType);
            }

            agenda.DeleteAgenda(agendaDTO.AgendaID);

            string[] file = File.ReadAllLines(filePathAgendaDeletion);
            File.Delete(filePathAgendaDeletion);

            Assert.Equal("System.String[]", file.ToString());

        }

        [Fact]
        public void RemoveNoAgenda()
        {
            accountDTO = new AccountDTO() { AccountID = 12 };
            AgendaDTO agendaDTO = new AgendaDTO() { AgendaID = 51, AgendaName = "qwerty", AgendaColor = "#0X2312", NotificationType = "Nee" };
            agenda = new Agenda(accountDTO, new StubAgendaRepository());

            using (StreamWriter streamWriter = new StreamWriter(filePathAgendaDeletion))
            {
                streamWriter.WriteLine(agendaDTO.AgendaID);
                streamWriter.WriteLine(agendaDTO.AgendaName);
                streamWriter.WriteLine(agendaDTO.AgendaColor);
                streamWriter.WriteLine(agendaDTO.NotificationType);
            }

            agenda.DeleteAgenda(-5);

            string[] file = File.ReadAllLines(filePathAgendaDeletion);
            File.Delete(filePathAgendaDeletion);

            Assert.Equal("51", file[0]);
            Assert.Equal("qwerty", file[1]);
        }
    }
}
