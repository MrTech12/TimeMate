using BusinessLogicLayer.Logic;
using DataAccessLayer.DTO;
using System;
using TimeMateTest.Stubs;
using Xunit;

namespace TimeMateTest.BLL
{
    public class AccountLogicTest
    {
        Account accountLogic;
        AccountDTO accountDTO;

        [Fact]
        public void WrongMailaddressTest()
        {
            string output;
            accountDTO = new AccountDTO() { MailAddress = "test@gmail.com", Password = "test123"};
            accountLogic = new Account(accountDTO, new StubAccountContext(), new StubAgendaContext());

            output = accountLogic.UserLogsIn();

            Assert.Equal("Verkeerd mailadres en/of wachtwoord.", output);
        }

        [Fact]
        public void WrongPasswordTeset()
        {
            string output;
            accountDTO = new AccountDTO() { MailAddress = "bert@gmail.com", Password = "cmck323kc" };
            accountLogic = new Account(accountDTO, new StubAccountContext(), new StubAgendaContext());

            output = accountLogic.UserLogsIn();

            Assert.Equal("Verkeerd mailadres en/of wachtwoord.", output);
        }

        [Fact]
        public void GetAccountIDTest()
        {
            int output;
            accountDTO = new AccountDTO() { MailAddress = "bert@gmail.com", Password = "cmck323kc" };
            accountLogic = new Account(accountDTO, new StubAccountContext(), new StubAgendaContext());

            output = accountLogic.GetActiveAccountID(accountDTO.MailAddress);

            Assert.Equal(0, output);
        }

        [Fact]
        public void CreateAccountWithLowerCasePasswordTest()
        {
            string output;
            accountDTO = new AccountDTO() { FirstName = "Hans", MailAddress = "hans@bing.com", Password = "qwieiwi231@#" };
            accountLogic = new Account(accountDTO, new StubAccountContext(), new StubAgendaContext());

            output = accountLogic.NewAccountValidation();

            Assert.Equal("Het wachtwoord moet een hoofdletter bevatten.", output);
        }

        [Fact]
        public void CreateAccountWithNoSpecialCharactersInPasswordTest()
        {
            string output;
            accountDTO = new AccountDTO() { FirstName = "Hans", MailAddress = "hans@bing.com", Password = "qwiEEWwi231WE" };
            accountLogic = new Account(accountDTO, new StubAccountContext(), new StubAgendaContext());

            output = accountLogic.NewAccountValidation();

            Assert.Equal("Het wachtwoord moet een speciale karakter bevatten.", output);
        }

        [Fact]
        public void CreateAccountWithNoNumbersInPasswordTest()
        {
            string output;
            accountDTO = new AccountDTO() { FirstName = "Hans", MailAddress = "hans@bing.com", Password = "qwieiwieWE@#" };
            accountLogic = new Account(accountDTO, new StubAccountContext(), new StubAgendaContext());

            output = accountLogic.NewAccountValidation();

            Assert.Equal("Het wachtwoord moet een cijfer bevatten.", output);
        }

        [Fact]
        public void CreateAccountWithExistingMailTest()
        {
            string output;
            accountDTO = new AccountDTO() { FirstName = "Bert", MailAddress = "bert@gmail.com", Password = "qwieEW12iwieWE@#" };
            accountLogic = new Account(accountDTO, new StubAccountContext(), new StubAgendaContext());

            output = accountLogic.NewAccountValidation();

            Assert.Equal("Er bestaat al een account met dit mailadres.", output);
        }
    }
}
