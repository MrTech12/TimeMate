using BusinessLogicLayer.Logic;
using DataAccessLayer.DTO;
using System;
using TimeMateTest.Stubs;
using Xunit;

namespace TimeMateTest.BLL
{
    public class AccountTest
    {
        Account account;
        AccountDTO accountDTO;

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
        public void WrongPasswordTeset()
        {
            string output;
            accountDTO = new AccountDTO() { MailAddress = "bert@gmail.com", Password = "cmck323kc" };
            account = new Account(accountDTO, new StubAccountContext(), new StubAgendaContext());

            output = account.UserLogsIn();

            Assert.Equal("Verkeerd mailadres en/of wachtwoord.", output);
        }

        [Fact]
        public void GetAccountIDTest()
        {
            int output;
            accountDTO = new AccountDTO() { MailAddress = "bert@gmail.com", Password = "cmck323kc" };
            account = new Account(accountDTO, new StubAccountContext(), new StubAgendaContext());

            output = account.GetActiveAccountID(accountDTO.MailAddress);

            Assert.Equal(0, output);
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
    }
}
