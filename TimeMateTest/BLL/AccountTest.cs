using BusinessLogicLayer.Logic;
using DataAccessLayer.DTO;
using System;
using System.Collections.Generic;
using TimeMateTest.Stubs;
using Xunit;

namespace TimeMateTest.BLL
{
    public class AccountTest
    {
        private Account account;
        private AccountDTO accountDTO;

        [Fact]
        public void LoggingInTest()
        {
            string output;
            accountDTO = new AccountDTO() { MailAddress = "bert@gmail.com", Password = "qoe2ieiwiir" };
            account = new Account(accountDTO, new StubAccountContext(), new StubAgendaContext());

            output = account.UserLogsIn();

            Assert.Equal("0", output);
        }

        [Fact]
        public void EmptyLoginTest()
        {
            string output;
            accountDTO = new AccountDTO() { MailAddress = "", Password = "" };
            account = new Account(accountDTO, new StubAccountContext(), new StubAgendaContext());

            output = account.UserLogsIn();

            Assert.Equal("Verkeerd mailadres en/of wachtwoord.", output);
        }

        [Fact]
        public void WrongMailaddressTest()
        {
            string output;
            accountDTO = new AccountDTO() { MailAddress = "test@gmail.com", Password = "test123"};
            account = new Account(accountDTO, new StubAccountContext(), new StubAgendaContext());

            output = account.UserLogsIn();

            Assert.Equal("Verkeerd mailadres en/of wachtwoord.", output);
        }

        [Fact]
        public void EmptyMailTest()
        {
            string output;
            accountDTO = new AccountDTO() { MailAddress = "", Password = "test123" };
            account = new Account(accountDTO, new StubAccountContext(), new StubAgendaContext());

            output = account.UserLogsIn();

            Assert.Equal("Verkeerd mailadres en/of wachtwoord.", output);
        }

        [Fact]
        public void IncompleteMailTest()
        {
            string output;
            accountDTO = new AccountDTO() { MailAddress = "test@gmail.", Password = "test123" };
            account = new Account(accountDTO, new StubAccountContext(), new StubAgendaContext());

            output = account.UserLogsIn();

            Assert.Equal("Verkeerd mailadres en/of wachtwoord.", output);
        }

        [Fact]
        public void WrongPasswordTeset()
        {
            string output;
            accountDTO = new AccountDTO() { MailAddress = "bert@gmail.com", Password = "cmck323kc" };
            account = new Account(accountDTO, new StubAccountContext(), new StubAgendaContext());

            output = account.UserLogsIn();

            Assert.Equal("Verkeerd mailadres en/of wachtwoord.", output);
        }

        [Fact]
        public void EmptyPasswordTest()
        {
            string output;
            accountDTO = new AccountDTO() { MailAddress = "bert@gmail.com", Password = "" };
            account = new Account(accountDTO, new StubAccountContext(), new StubAgendaContext());

            output = account.UserLogsIn();

            Assert.Equal("Verkeerd mailadres en/of wachtwoord.", output);
        }

        [Fact]
        public void CreateAccountWithNoJobTest()
        {
            string output;
            accountDTO = new AccountDTO();
            accountDTO.FirstName = "Hans";
            accountDTO.MailAddress = "sina1240@gmail.com";
            accountDTO.Password = "QWEwieiwi231@#";

            account = new Account(accountDTO, new StubAccountContext(), new StubAgendaContext());

            output = account.NewAccountValidation();

            Assert.Equal("6", output);
        }

        [Fact]
        public void CreateAccountWithJobTest()
        {
            string output;
            accountDTO = new AccountDTO();
            accountDTO.FirstName = "Hans";
            accountDTO.MailAddress = "sina1240@gmail.com";
            accountDTO.Password = "QWEwieiwi231@#";
            accountDTO.JobCount = 1;
            accountDTO.JobHourlyWage.Add(1.20);
            accountDTO.JobDayType.Add("Doordeweeks");
            accountDTO.AllocatedHours = 2;

            account = new Account(accountDTO, new StubAccountContext(), new StubAgendaContext());

            output = account.NewAccountValidation();

            Assert.Equal("6", output);
        }

        [Fact]
        public void CreateAccountWithLowerCasePasswordTest()
        {
            string output;
            accountDTO = new AccountDTO() { FirstName = "Hans", MailAddress = "hans@bing.com", Password = "qwieiwi231@#" };
            account = new Account(accountDTO, new StubAccountContext(), new StubAgendaContext());

            output = account.NewAccountValidation();

            Assert.Equal("Het wachtwoord moet een hoofdletter bevatten.", output);
        }

        [Fact]
        public void CreateAccountWithNoSpecialCharactersInPasswordTest()
        {
            string output;
            accountDTO = new AccountDTO() { FirstName = "Hans", MailAddress = "hans@bing.com", Password = "qwiEEWwi231WE" };
            account = new Account(accountDTO, new StubAccountContext(), new StubAgendaContext());

            output = account.NewAccountValidation();

            Assert.Equal("Het wachtwoord moet een speciale karakter bevatten.", output);
        }

        [Fact]
        public void CreateAccountWithNoNumbersInPasswordTest()
        {
            string output;
            accountDTO = new AccountDTO() { FirstName = "Hans", MailAddress = "hans@bing.com", Password = "qwieiwieWE@#" };
            account = new Account(accountDTO, new StubAccountContext(), new StubAgendaContext());

            output = account.NewAccountValidation();

            Assert.Equal("Het wachtwoord moet een cijfer bevatten.", output);
        }

        [Fact]
        public void CreateAccountWithExistingMailTest()
        {
            string output;
            accountDTO = new AccountDTO() { FirstName = "Bert", MailAddress = "bert@gmail.com", Password = "qwieEW12iwieWE@#" };
            account = new Account(accountDTO, new StubAccountContext(), new StubAgendaContext());

            output = account.NewAccountValidation();

            Assert.Equal("Er bestaat al een account met dit mailadres.", output);
        }

        [Fact]
        public void CreateAccountWithIncorrectMailTest()
        {
            string output;
            accountDTO = new AccountDTO() { FirstName = "Bert", MailAddress = "bert@gmail.", Password = "qwieEW12iwieWE@#" };
            account = new Account(accountDTO, new StubAccountContext(), new StubAgendaContext());

            output = account.NewAccountValidation();

            Assert.Equal("Het emailadres is niet geldig.", output);
        }

        [Fact]
        public void CreateAgendaTest()
        {
            accountDTO = new AccountDTO() { AccountID = 12 };
            account = new Account(accountDTO, new StubAccountContext(), new StubAgendaContext());
            Agenda agenda = new Agenda(accountDTO, new StubAgendaContext());
            AgendaDTO agendaDTO = new AgendaDTO() { AgendaName = "Homework", AgendaColor = "0x0000", Notification = "Nee" };
            
            string before = Convert.ToString(agenda.GetAgendaID(null));
            account.CreateAgenda(agendaDTO);
            string after = Convert.ToString(agenda.GetAgendaID("Homework"));

            Assert.Equal("-1", before);
            Assert.Equal("24", after);
        }

        [Fact]
        public void CreateWorkAgendaTest()
        {
            accountDTO = new AccountDTO() { AccountID = 12 };
            account = new Account(accountDTO, new StubAccountContext(), new StubAgendaContext());
            Agenda agenda = new Agenda(accountDTO, new StubAgendaContext());

            string before = Convert.ToString(agenda.GetAgendaID(null));
            account.CreateWorkAgenda();
            string after = Convert.ToString(agenda.GetAgendaID("Bijbaan"));

            Assert.Equal("-1", before);
            Assert.Equal("4", after);
        }

        [Fact]
        public void GetAgendaNamesTest()
        {
            List<AgendaDTO> output = new List<AgendaDTO>();
            accountDTO = new AccountDTO() { AccountID = 12 };
            account = new Account(accountDTO, new StubAccountContext(), new StubAgendaContext());

            output = account.GetAgendaNames();

            Assert.Contains("Work", output[0].AgendaName);
            Assert.True(output.Count == 2);
        }
    }
}
