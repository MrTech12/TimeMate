using BusinessLogicLayer.Logic;
using Model.DTO_s;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TimeMateTest.Stubs;
using Xunit;
using Error;

namespace TimeMateTest.BLL
{
    public class AccountRegisterTest
    {
        private Account account;
        private Agenda agenda;
        private StubJobRepository _stubJobRepository = new StubJobRepository();
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
            account = new Account(accountDTO, new StubAccountRepository(), new StubSender());
            
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
            accountDTO.JobHourlyWage.Add(1.20);
            accountDTO.JobDayType.Add("Doordeweeks");
            AgendaDTO workAgendaDTO = new AgendaDTO() { AgendaName = "Bijbaan", AgendaColor = "#FF0000", NotificationType = "Nee" };
            account = new Account(accountDTO, new StubAccountRepository(), new StubSender());
            agenda = new Agenda(accountDTO, new StubAgendaRepository());

            account.CreateAccount();
            agenda.AddAgenda(workAgendaDTO);
            _stubJobRepository.CreatePayDetails(accountDTO);
            

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
            accountDTO = new AccountDTO();
            accountDTO.FirstName = "Intel";
            accountDTO.Mail = "intel12@gmail.com";
            accountDTO.Password = "QWEwieiwi231@#";
            account = new Account(accountDTO, new StubAccountRepository(), new StubSender());

            account.CreateAccount();

            file = File.ReadAllLines(filePathAccount);
            File.Delete(filePathAccount);

            Assert.Equal("Intel", file[0]);
            Assert.Equal("intel12@gmail.com", file[1]);
            Assert.Equal("39", file[3]);
            Assert.Equal("Een mail is verstuurd naar intel12@gmail.com", file[4]);
        }

        [Fact]
        public void CreateAccounExistingMail()
        {
            string output;
            accountDTO = new AccountDTO() { FirstName = "Bert", Mail = "bert@gmail.com", Password = "qwieEW12iwieWE@#" };
            account = new Account(accountDTO, new StubAccountRepository(), new StubSender());
            
            output = account.CheckExistingMail();

            Assert.Equal("Er bestaat al een account met dit mailadres.", output);
        }

        [Fact]
        public void CreateAccountNoMail()
        {
            accountDTO = new AccountDTO();
            account = new Account(accountDTO, new StubAccountRepository(), new StubSender());

            Action action = () => account.CheckExistingMail();

            Exception exception = Assert.Throws<AccountException>(action);

            Assert.Equal("Het mailadres voor een nieuw account is niet geleverd.", exception.Message);
        }

        [Fact]
        public void CreateAccountNoPassword()
        {
            accountDTO = new AccountDTO();
            account = new Account(accountDTO, new StubAccountRepository(), new StubSender());

            Action action = () => account.CreateAccount();

            Exception exception = Assert.Throws<AccountException>(action);

            Assert.Equal("Het wachtwoord voor een nieuw account is niet geleverd.", exception.Message);
        }
    }
}
