using BusinessLogicLayer.Logic;
using DataAccessLayer.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TimeMateTest.Stubs;
using Xunit;

namespace TimeMateTest.BLL
{
    public class AccountRegisterTest
    {
        private Account account;
        private AccountDTO accountDTO;
        private string filePathAccount = @"C:\tmp\CreateAccountTest.txt";
        private string filePathJob = @"C:\tmp\addWorkPayDetails.txt";

        [Fact]
        public void CreateAccountNoJob()
        {
            string[] file;
            accountDTO = new AccountDTO();
            accountDTO.FirstName = "Hans";
            accountDTO.Mail = "sina1240@gmail.com";
            accountDTO.Password = "QWEwieiwi231@#";
            account = new Account(accountDTO, new StubAccountContainer(), new StubAgendaContainer(), new StubJobContainer(), new StubSenderContainer());
            
            account.CreateAccount();

            file = File.ReadAllLines(filePathAccount);
            File.Delete(filePathAccount);

            Assert.Equal("Hans", file[0]);
            Assert.Equal("sina1240@gmail.com", file[1]);
            Assert.Equal("6", file[3]);
        }

        [Fact]
        public void CreateAccountJob()
        {
            string[] fileAccount;
            string[] filePayDetails;
            accountDTO = new AccountDTO();
            accountDTO.FirstName = "Hans";
            accountDTO.Mail = "sina1242@gmail.com";
            accountDTO.Password = "QWEwieiwi231@#";
            accountDTO.JobCount = 1;
            accountDTO.JobHourlyWage.Add(1.20);
            accountDTO.JobDayType.Add("Doordeweeks");
            account = new Account(accountDTO, new StubAccountContainer(), new StubAgendaContainer(), new StubJobContainer(), new StubSenderContainer());
            
            account.CreateAccount();

            fileAccount = File.ReadAllLines(filePathAccount);
            filePayDetails = File.ReadAllLines(filePathJob);
            File.Delete(filePathAccount);
            File.Delete(filePathJob);

            Assert.Equal("Hans", fileAccount[0]);
            Assert.Equal("sina1242@gmail.com", fileAccount[1]);
            Assert.Equal("14", fileAccount[3]);
            Assert.Equal("1,2", filePayDetails[0]);
        }

        [Fact]
        public void CreateAccounSendMail()
        {
            string[] file;
            string[] output;
            accountDTO = new AccountDTO();
            accountDTO.FirstName = "Intel";
            accountDTO.Mail = "intel12@gmail.com";
            accountDTO.Password = "QWEwieiwi231@#";
            account = new Account(accountDTO, new StubAccountContainer(), new StubAgendaContainer(), new StubJobContainer(), new StubSenderContainer());
            
            output = account.NewAccountValidation();

            file = File.ReadAllLines(filePathAccount);
            File.Delete(filePathAccount);

            Assert.Equal("39", output[0]);
            Assert.Equal("Intel", output[1]);
            Assert.Equal("Intel", file[0]);
            Assert.Equal("intel12@gmail.com", file[1]);
            Assert.Equal("39", file[3]);
            Assert.Equal("Een mail is verstuurd naar intel12@gmail.com", file[4]);
        }

        [Fact]
        public void CreateEmptyAccount()
        {
            string[] output;
            accountDTO = new AccountDTO();
            account = new Account(accountDTO, new StubAccountContainer(), new StubAgendaContainer());

            output = account.NewAccountValidation();

            Assert.Equal("Er is niks ingevuld", output[0]);
        }

        [Fact]
        public void CreateAccountLowercasePassword()
        {
            string[] output;
            accountDTO = new AccountDTO() { FirstName = "Hans", Mail = "hans@bing.com", Password = "qwieiwi231@#" };
            account = new Account(accountDTO, new StubAccountContainer(), new StubAgendaContainer());
            
            output = account.NewAccountValidation();

            Assert.Equal("Het wachtwoord moet een hoofdletter bevatten.", output[0]);
        }

        [Fact]
        public void CreateAccountNoSpecialCharactersPassword()
        {
            string[] output;
            accountDTO = new AccountDTO() { FirstName = "Hans", Mail = "hans@bing.com", Password = "qwiEEWwi231WE" };
            account = new Account(accountDTO, new StubAccountContainer(), new StubAgendaContainer());
            
            output = account.NewAccountValidation();

            Assert.Equal("Het wachtwoord moet een speciale karakter bevatten.", output[0]);
        }

        [Fact]
        public void CreateAccountNoNumbersPassword()
        {
            string[] output;
            accountDTO = new AccountDTO() { FirstName = "Hans", Mail = "hans@bing.com", Password = "qwieiwieWE@#" };
            account = new Account(accountDTO, new StubAccountContainer(), new StubAgendaContainer());
            
            output = account.NewAccountValidation();

            Assert.Equal("Het wachtwoord moet een cijfer bevatten.", output[0]);
        }

        [Fact]
        public void CreateAccounExistingMail()
        {
            string[] output;
            accountDTO = new AccountDTO() { FirstName = "Bert", Mail = "bert@gmail.com", Password = "qwieEW12iwieWE@#" };
            account = new Account(accountDTO, new StubAccountContainer(), new StubAgendaContainer());
            
            output = account.NewAccountValidation();

            Assert.Equal("Er bestaat al een account met dit mailadres.", output[0]);
        }

        [Fact]
        public void CreateAccountIncorrectMail()
        {
            string[] output;
            accountDTO = new AccountDTO() { FirstName = "Bert", Mail = "bert@gmail.", Password = "qwieEW12iwieWE@#" };
            account = new Account(accountDTO, new StubAccountContainer(), new StubAgendaContainer());
            
            output = account.NewAccountValidation();

            Assert.Equal("Het emailadres is niet geldig.", output[0]);
        }
    }
}
