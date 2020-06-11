using BusinessLogicLayer.Logic;
using DataAccessLayer.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using TimeMateTest.Stubs;
using Xunit;

namespace TimeMateTest.BLL
{
    public class AccountRegisterTest
    {
        private Account account;
        private AccountDTO accountDTO;

        [Fact]
        public void CreateAccountNoJobTest()
        {
            string[] output;
            accountDTO = new AccountDTO();
            accountDTO.FirstName = "Hans";
            accountDTO.Mail = "sina1240@gmail.com";
            accountDTO.Password = "QWEwieiwi231@#";

            account = new Account(accountDTO, new StubAccountContext(), new StubAgendaContext(), new StubSenderContext());

            output = account.NewAccountValidation();

            Assert.Equal("6", output[0]);
        }

        [Fact]
        public void CreateAccountJobTest()
        {
            string[] output;
            accountDTO = new AccountDTO();
            accountDTO.FirstName = "Hans";
            accountDTO.Mail = "sina1240@gmail.com";
            accountDTO.Password = "QWEwieiwi231@#";
            accountDTO.JobCount = 1;
            accountDTO.JobHourlyWage.Add(1.20);
            accountDTO.JobDayType.Add("Doordeweeks");

            account = new Account(accountDTO, new StubAccountContext(), new StubAgendaContext(), new StubSenderContext());

            output = account.NewAccountValidation();

            Assert.Equal("6", output[0]);
        }

        [Fact]
        public void CreateAccountLowercasePasswordTest()
        {
            string[] output;
            accountDTO = new AccountDTO() { FirstName = "Hans", Mail = "hans@bing.com", Password = "qwieiwi231@#" };
            account = new Account(accountDTO, new StubAccountContext(), new StubAgendaContext());

            output = account.NewAccountValidation();

            Assert.Equal("Het wachtwoord moet een hoofdletter bevatten.", output[0]);
        }

        [Fact]
        public void CreateAccountNoSpecialCharactersPasswordTest()
        {
            string[] output;
            accountDTO = new AccountDTO() { FirstName = "Hans", Mail = "hans@bing.com", Password = "qwiEEWwi231WE" };
            account = new Account(accountDTO, new StubAccountContext(), new StubAgendaContext());

            output = account.NewAccountValidation();

            Assert.Equal("Het wachtwoord moet een speciale karakter bevatten.", output[0]);
        }

        [Fact]
        public void CreateAccountNoNumbersPasswordTest()
        {
            string[] output;
            accountDTO = new AccountDTO() { FirstName = "Hans", Mail = "hans@bing.com", Password = "qwieiwieWE@#" };
            account = new Account(accountDTO, new StubAccountContext(), new StubAgendaContext());

            output = account.NewAccountValidation();

            Assert.Equal("Het wachtwoord moet een cijfer bevatten.", output[0]);
        }

        [Fact]
        public void CreateAccounExistingMailTest()
        {
            string[] output;
            accountDTO = new AccountDTO() { FirstName = "Bert", Mail = "bert@gmail.com", Password = "qwieEW12iwieWE@#" };
            account = new Account(accountDTO, new StubAccountContext(), new StubAgendaContext());

            output = account.NewAccountValidation();

            Assert.Equal("Er bestaat al een account met dit mailadres.", output[0]);
        }

        [Fact]
        public void CreateAccountIncorrectMailTest()
        {
            string[] output;
            accountDTO = new AccountDTO() { FirstName = "Bert", Mail = "bert@gmail.", Password = "qwieEW12iwieWE@#" };
            account = new Account(accountDTO, new StubAccountContext(), new StubAgendaContext());

            output = account.NewAccountValidation();

            Assert.Equal("Het emailadres is niet geldig.", output[0]);
        }
    }
}
