﻿using BusinessLogicLayer.Logic;
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
        public void EmptyCredentailsTest()
        {
            string output;
            accountDTO = new AccountDTO() { Mail = "", Password = "" };
            account = new Account(accountDTO, new StubAccountContext(), new StubAgendaContext());

            output = account.LoggingIn();

            Assert.Null(output);
        }

        [Fact]
        public void WrongMailTest()
        {
            string output;
            accountDTO = new AccountDTO() { Mail = "test@gmail.com", Password = "test123"};
            account = new Account(accountDTO, new StubAccountContext(), new StubAgendaContext());

            output = account.LoggingIn();

            Assert.Null(output);
        }

        [Fact]
        public void NoMailTest()
        {
            string output;
            accountDTO = new AccountDTO() { Mail = "", Password = "test123" };
            account = new Account(accountDTO, new StubAccountContext(), new StubAgendaContext());

            output = account.LoggingIn();

            Assert.Null(output);
        }

        [Fact]
        public void IncompleteMailTest()
        {
            string output;
            accountDTO = new AccountDTO() { Mail = "test@gmail.", Password = "test123" };
            account = new Account(accountDTO, new StubAccountContext(), new StubAgendaContext());

            output = account.LoggingIn();

            Assert.Null(output);
        }

        [Fact]
        public void WrongPasswordTeset()
        {
            string output;
            accountDTO = new AccountDTO() { Mail = "bert@gmail.com", Password = "cmck323kc" };
            account = new Account(accountDTO, new StubAccountContext(), new StubAgendaContext());

            output = account.LoggingIn();

            Assert.Null(output);
        }

        [Fact]
        public void NoPasswordTest()
        {
            string output;
            accountDTO = new AccountDTO() { Mail = "bert@gmail.com", Password = "" };
            account = new Account(accountDTO, new StubAccountContext(), new StubAgendaContext());

            output = account.LoggingIn();

            Assert.Null(output);
        }
    }
}