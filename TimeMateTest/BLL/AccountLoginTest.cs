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
        public void LoggingIn()
        {
            string[] output;
            accountDTO = new AccountDTO() { Mail = "bert@gmail.com", Password = "qoe2ieiwiir" };
            account = new Account(accountDTO, new StubAccountContainer(), new StubAgendaContainer());

            output = account.LoggingIn();

            Assert.Equal("0", output[0]);
        }

        [Fact]
        public void EmptyCredentails()
        {
            string[] output;
            accountDTO = new AccountDTO() { Mail = "", Password = "" };
            account = new Account(accountDTO, new StubAccountContainer(), new StubAgendaContainer());

            output = account.LoggingIn();

            Assert.Null(output[0]);
        }

        [Fact]
        public void WrongMail()
        {
            string[] output;
            accountDTO = new AccountDTO() { Mail = "test@gmail.com", Password = "test123"};
            account = new Account(accountDTO, new StubAccountContainer(), new StubAgendaContainer());

            output = account.LoggingIn();

            Assert.Null(output[0]);
        }

        [Fact]
        public void NoMail()
        {
            string[] output;
            accountDTO = new AccountDTO() { Mail = "", Password = "test123" };
            account = new Account(accountDTO, new StubAccountContainer(), new StubAgendaContainer());

            output = account.LoggingIn();

            Assert.Null(output[0]);
        }

        [Fact]
        public void IncompleteMail()
        {
            string[] output;
            accountDTO = new AccountDTO() { Mail = "test@gmail.", Password = "test123" };
            account = new Account(accountDTO, new StubAccountContainer(), new StubAgendaContainer());

            output = account.LoggingIn();

            Assert.Null(output[0]);
        }

        [Fact]
        public void WrongPassword()
        {
            string[] output;
            accountDTO = new AccountDTO() { Mail = "bert@gmail.com", Password = "cmck323kc" };
            account = new Account(accountDTO, new StubAccountContainer(), new StubAgendaContainer());

            output = account.LoggingIn();

            Assert.Null(output);
        }

        [Fact]
        public void NoPassword()
        {
            string[] output;
            accountDTO = new AccountDTO() { Mail = "bert@gmail.com", Password = "" };
            account = new Account(accountDTO, new StubAccountContainer(), new StubAgendaContainer());

            output = account.LoggingIn();

            Assert.Null(output);
        }
    }
}
