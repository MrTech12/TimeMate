using BusinessLogicLayer.Logic;
using DataAccessLayer.Contexts;
using DataAccessLayer.DTO;
using System;
using System.Collections.Generic;
using System.IO;
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

            output = account.LoggingIn();

            Assert.Equal("0", output);
        }

        [Fact]
        public void EmptyLoginTest()
        {
            string output;
            accountDTO = new AccountDTO() { MailAddress = "", Password = "" };
            account = new Account(accountDTO, new StubAccountContext(), new StubAgendaContext());

            output = account.LoggingIn();

            Assert.Equal("Verkeerd mailadres en/of wachtwoord.", output);
        }

        [Fact]
        public void WrongMailaddressTest()
        {
            string output;
            accountDTO = new AccountDTO() { MailAddress = "test@gmail.com", Password = "test123"};
            account = new Account(accountDTO, new StubAccountContext(), new StubAgendaContext());

            output = account.LoggingIn();

            Assert.Equal("Verkeerd mailadres en/of wachtwoord.", output);
        }

        [Fact]
        public void EmptyMailTest()
        {
            string output;
            accountDTO = new AccountDTO() { MailAddress = "", Password = "test123" };
            account = new Account(accountDTO, new StubAccountContext(), new StubAgendaContext());

            output = account.LoggingIn();

            Assert.Equal("Verkeerd mailadres en/of wachtwoord.", output);
        }

        [Fact]
        public void IncompleteMailTest()
        {
            string output;
            accountDTO = new AccountDTO() { MailAddress = "test@gmail.", Password = "test123" };
            account = new Account(accountDTO, new StubAccountContext(), new StubAgendaContext());

            output = account.LoggingIn();

            Assert.Equal("Verkeerd mailadres en/of wachtwoord.", output);
        }

        [Fact]
        public void WrongPasswordTeset()
        {
            string output;
            accountDTO = new AccountDTO() { MailAddress = "bert@gmail.com", Password = "cmck323kc" };
            account = new Account(accountDTO, new StubAccountContext(), new StubAgendaContext());

            output = account.LoggingIn();

            Assert.Equal("Verkeerd mailadres en/of wachtwoord.", output);
        }

        [Fact]
        public void EmptyPasswordTest()
        {
            string output;
            accountDTO = new AccountDTO() { MailAddress = "bert@gmail.com", Password = "" };
            account = new Account(accountDTO, new StubAccountContext(), new StubAgendaContext());

            output = account.LoggingIn();

            Assert.Equal("Verkeerd mailadres en/of wachtwoord.", output);
        }

        [Fact]
        public void CreateAccountNoJobTest()
        {
            string output;
            accountDTO = new AccountDTO();
            accountDTO.FirstName = "Hans";
            accountDTO.MailAddress = "sina1240@gmail.com";
            accountDTO.Password = "QWEwieiwi231@#";

            account = new Account(accountDTO, new StubAccountContext(), new StubAgendaContext(), new StubSenderContext());

            output = account.NewAccountValidation();

            Assert.Equal("6", output);
        }

        [Fact]
        public void CreateAccountJobTest()
        {
            string output;
            accountDTO = new AccountDTO();
            accountDTO.FirstName = "Hans";
            accountDTO.MailAddress = "sina1240@gmail.com";
            accountDTO.Password = "QWEwieiwi231@#";
            accountDTO.JobCount = 1;
            accountDTO.JobHourlyWage.Add(1.20);
            accountDTO.JobDayType.Add("Doordeweeks");

            account = new Account(accountDTO, new StubAccountContext(), new StubAgendaContext(), new StubSenderContext());

            output = account.NewAccountValidation();

            Assert.Equal("6", output);
        }

        [Fact]
        public void CreateAccountLowerCasePasswordTest()
        {
            string output;
            accountDTO = new AccountDTO() { FirstName = "Hans", MailAddress = "hans@bing.com", Password = "qwieiwi231@#" };
            account = new Account(accountDTO, new StubAccountContext(), new StubAgendaContext());

            output = account.NewAccountValidation();

            Assert.Equal("Het wachtwoord moet een hoofdletter bevatten.", output);
        }

        [Fact]
        public void CreateAccountNoSpecialCharactersInPasswordTest()
        {
            string output;
            accountDTO = new AccountDTO() { FirstName = "Hans", MailAddress = "hans@bing.com", Password = "qwiEEWwi231WE" };
            account = new Account(accountDTO, new StubAccountContext(), new StubAgendaContext());

            output = account.NewAccountValidation();

            Assert.Equal("Het wachtwoord moet een speciale karakter bevatten.", output);
        }

        [Fact]
        public void CreateAccountNoNumbersInPasswordTest()
        {
            string output;
            accountDTO = new AccountDTO() { FirstName = "Hans", MailAddress = "hans@bing.com", Password = "qwieiwieWE@#" };
            account = new Account(accountDTO, new StubAccountContext(), new StubAgendaContext());

            output = account.NewAccountValidation();

            Assert.Equal("Het wachtwoord moet een cijfer bevatten.", output);
        }

        [Fact]
        public void CreateAccounExistingMailTest()
        {
            string output;
            accountDTO = new AccountDTO() { FirstName = "Bert", MailAddress = "bert@gmail.com", Password = "qwieEW12iwieWE@#" };
            account = new Account(accountDTO, new StubAccountContext(), new StubAgendaContext());

            output = account.NewAccountValidation();

            Assert.Equal("Er bestaat al een account met dit mailadres.", output);
        }

        [Fact]
        public void CreateAccountIncorrectMailTest()
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
            AgendaDTO agendaDTO = new AgendaDTO() { AgendaName = "Homework", AgendaColor = "#0x0000", Notification = "Nee" };
            
            account.CreateAgenda(agendaDTO);
            string[] file = File.ReadAllLines(@"C:\tmp\addAgendaTest.txt");
            File.Delete(@"C:\tmp\addAgendaTest.txt");

            Assert.Contains("Homework", file[0]);
            Assert.Contains("#0x0000", file[1]);
        }

        [Fact]
        public void CreateAgendaTest2()
        {
            accountDTO = new AccountDTO() { AccountID = 12 };
            account = new Account(accountDTO, new StubAccountContext(), new StubAgendaContext());
            Agenda agenda = new Agenda(accountDTO, new StubAgendaContext());
            AgendaDTO agendaDTO = new AgendaDTO() { AgendaName = "Skype", AgendaColor = "#15F560", Notification = "Nee" };

            account.CreateAgenda(agendaDTO);
            string[] file = File.ReadAllLines(@"C:\tmp\addAgendaTest.txt");
            File.Delete(@"C:\tmp\addAgendaTest.txt");

            Assert.Contains("Skype", file[0]);
            Assert.Contains("#15F560", file[1]);
        }

        [Fact]
        public void CreateWorkAgendaTest()
        {
            accountDTO = new AccountDTO() { AccountID = 12 };
            account = new Account(accountDTO, new StubAccountContext(), new StubAgendaContext());
            Agenda agenda = new Agenda(accountDTO, new StubAgendaContext());

            account.CreateWorkAgenda();
            string[] file = File.ReadAllLines(@"C:\tmp\addWorkAgendaTest.txt");
            File.Delete(@"C:\tmp\addWorkAgendaTest.txt");

            Assert.Contains("Bijbaan", file[0]);
            Assert.Contains("#FF0000", file[1]);
        }

        [Fact]
        public void GetAgendaNamesTest()
        {
            List<AgendaDTO> output = new List<AgendaDTO>();
            accountDTO = new AccountDTO() { AccountID = 12 };
            account = new Account(accountDTO, new StubAccountContext(), new StubAgendaContext());

            output = account.RetrieveAgendas();

            Assert.Contains("Work", output[0].AgendaName);
            Assert.True(output.Count == 2);
        }

        [Fact]
        public void RemoveAgenda()
        {
            accountDTO = new AccountDTO() { AccountID = 12 };
            AgendaDTO agendaDTO = new AgendaDTO() { AgendaID = 51, AgendaName = "qwerty", AgendaColor = "#0X2312", Notification = "Nee"};
            Account account = new Account(accountDTO, new StubAccountContext(), new StubAgendaContext());

            using (StreamWriter streamWriter = new StreamWriter(@"C:\tmp\removeAgendaTest.txt"))
            {
                streamWriter.WriteLine(agendaDTO.AgendaID);
                streamWriter.WriteLine(agendaDTO.AgendaName);
                streamWriter.WriteLine(agendaDTO.AgendaColor);
                streamWriter.WriteLine(agendaDTO.Notification);
            }

            account.DeleteAgenda(agendaDTO.AgendaID);

            string[] file = File.ReadAllLines(@"C:\tmp\removeAgendaTest.txt");
            File.Delete(@"C:\tmp\removeAgendaTest.txt");

            Assert.Equal("System.String[]", file.ToString());

        }
    }
}
