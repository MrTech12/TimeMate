using BusinessLogicLayer.Logic;
using DataAccessLayer.DTO;
using System;
using TimeMateTest.Stubs;
using Xunit;

namespace TimeMateTest.BLL
{
    public class AccountLogicTest
    {
        AccountLogic accountLogic;
        AccountDTO accountDTO;

        [Fact]
        public void UserLogsInWithWrongCredentials()
        {
            string output;
            accountDTO = new AccountDTO() { MailAddress = "test@gmail.com", Password = "test123"};
            accountLogic = new AccountLogic(accountDTO, new StubAccountContext(), new StubAgendaContext());

            output = accountLogic.UserLogsIn();

            Assert.Equal("Er bestaat geen account met deze gegevens.", output);
        }
    }
}
