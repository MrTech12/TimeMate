using BusinessLogicLayer.Logic;
using DataAccessLayer.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using TimeMateTest.Stubs;
using Xunit;

namespace TimeMateTest.BLL
{
    public class AccountLoginTest
    {
        private Account account;
        private AccountDTO accountDTO;

        [Fact]
        public void LoggingInTest()
        {
            string output;
            accountDTO = new AccountDTO() { Mail = "bert@gmail.com", Password = "qoe2ieiwiir" };
            account = new Account(accountDTO, new StubAccountContext(), new StubAgendaContext());

            output = account.LoggingIn();

            Assert.Equal("0", output);
        }

        [Fact]
        public void EmptyLoginTest()
        {
            string output;
            accountDTO = new AccountDTO() { Mail = "", Password = "" };
            account = new Account(accountDTO, new StubAccountContext(), new StubAgendaContext());

            output = account.LoggingIn();

            Assert.Equal("Verkeerd mailadres en/of wachtwoord.", output);
        }

        [Fact]
        public void WrongMailaddressTest()
        {
            string output;
            accountDTO = new AccountDTO() { Mail = "test@gmail.com", Password = "test123"};
            account = new Account(accountDTO, new StubAccountContext(), new StubAgendaContext());

            output = account.LoggingIn();

            Assert.Equal("Verkeerd mailadres en/of wachtwoord.", output);
        }

        [Fact]
        public void EmptyMailTest()
        {
            string output;
            accountDTO = new AccountDTO() { Mail = "", Password = "test123" };
            account = new Account(accountDTO, new StubAccountContext(), new StubAgendaContext());

            output = account.LoggingIn();

            Assert.Equal("Verkeerd mailadres en/of wachtwoord.", output);
        }

        [Fact]
        public void IncompleteMailTest()
        {
            string output;
            accountDTO = new AccountDTO() { Mail = "test@gmail.", Password = "test123" };
            account = new Account(accountDTO, new StubAccountContext(), new StubAgendaContext());

            output = account.LoggingIn();

            Assert.Equal("Verkeerd mailadres en/of wachtwoord.", output);
        }

        [Fact]
        public void WrongPasswordTeset()
        {
            string output;
            accountDTO = new AccountDTO() { Mail = "bert@gmail.com", Password = "cmck323kc" };
            account = new Account(accountDTO, new StubAccountContext(), new StubAgendaContext());

            output = account.LoggingIn();

            Assert.Equal("Verkeerd mailadres en/of wachtwoord.", output);
        }

        [Fact]
        public void EmptyPasswordTest()
        {
            string output;
            accountDTO = new AccountDTO() { Mail = "bert@gmail.com", Password = "" };
            account = new Account(accountDTO, new StubAccountContext(), new StubAgendaContext());

            output = account.LoggingIn();

            Assert.Equal("Verkeerd mailadres en/of wachtwoord.", output);
        }
    }
}
