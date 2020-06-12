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
    public class AccountAgendaTest
    {
        private Account account;
        private AccountDTO accountDTO;

        [Fact]
        public void CreateAgenda1()
        {
            accountDTO = new AccountDTO() { AccountID = 12 };
            account = new Account(accountDTO, new StubAgendaContainer());
            AgendaDTO agendaDTO = new AgendaDTO() { AgendaName = "Homework", AgendaColor = "#0x0000", NotificationType = "Nee" };

            account.CreateAgenda(agendaDTO);
            string[] file = File.ReadAllLines(@"C:\tmp\addAgendaTest.txt");
            File.Delete(@"C:\tmp\addAgendaTest.txt");

            Assert.Contains("Homework", file[0]);
            Assert.Contains("#0x0000", file[1]);
        }

        [Fact]
        public void CreateAgenda2()
        {
            accountDTO = new AccountDTO() { AccountID = 12 };
            account = new Account(accountDTO, new StubAgendaContainer());
            AgendaDTO agendaDTO = new AgendaDTO() { AgendaName = "Skype", AgendaColor = "#15F560", NotificationType = "Nee" };

            account.CreateAgenda(agendaDTO);
            string[] file = File.ReadAllLines(@"C:\tmp\addAgendaTest.txt");
            File.Delete(@"C:\tmp\addAgendaTest.txt");

            Assert.Contains("Skype", file[0]);
            Assert.Contains("#15F560", file[1]);
        }

        [Fact]
        public void CreateWorkAgenda1()
        {
            accountDTO = new AccountDTO() { AccountID = 12 };
            accountDTO.JobHourlyWage.Add(12.23);
            accountDTO.JobDayType.Add("Doordeweeks");
            account = new Account(accountDTO, new StubAgendaContainer(), new StubJobContainer());

            account.CreateWorkAgenda();
            string[] fileAgenda = File.ReadAllLines(@"C:\tmp\addAgendaTest.txt");
            string[] filePay = File.ReadAllLines(@"C:\tmp\addWorkPayDetails.txt");

            File.Delete(@"C:\tmp\addAgendaTest.txt");
            File.Delete(@"C:\tmp\addWorkPayDetails.txt");

            Assert.Contains("Bijbaan", fileAgenda[0]);
            Assert.Contains("#FF0000", fileAgenda[1]);
            Assert.Contains("12,23", filePay[0]);
            Assert.Contains("Doordeweeks", filePay[1]);
            Assert.True(filePay.Length == 2);
        }

        [Fact]
        public void CreateWorkAgenda2()
        {
            accountDTO = new AccountDTO() { AccountID = 56 };
            accountDTO.JobHourlyWage.Add(12.23);
            accountDTO.JobDayType.Add("Doordeweeks");
            accountDTO.JobHourlyWage.Add(10);
            accountDTO.JobDayType.Add("Weekend");
            account = new Account(accountDTO, new StubAgendaContainer(), new StubJobContainer());

            account.CreateWorkAgenda();
            string[] fileAgenda = File.ReadAllLines(@"C:\tmp\addAgendaTest.txt");
            string[] filePay = File.ReadAllLines(@"C:\tmp\addWorkPayDetails.txt");

            File.Delete(@"C:\tmp\addAgendaTest.txt");
            File.Delete(@"C:\tmp\addWorkPayDetails.txt");

            Assert.Contains("Bijbaan", fileAgenda[0]);
            Assert.Contains("#FF0000", fileAgenda[1]);
            Assert.Contains("12,23", filePay[0]);
            Assert.Contains("Doordeweeks", filePay[1]);
            Assert.Contains("10", filePay[2]);
            Assert.Contains("Weekend", filePay[3]);
            Assert.True(filePay.Length == 4);
        }

        [Fact]
        public void GetAgendaNames()
        {
            List<AgendaDTO> output = new List<AgendaDTO>();
            accountDTO = new AccountDTO() { AccountID = 12 };
            account = new Account(accountDTO, new StubAccountContainer(), new StubAgendaContainer());

            output = account.RetrieveAgendas();

            Assert.Contains("Work", output[0].AgendaName);
            Assert.True(output.Count == 2);
        }

        [Fact]
        public void GetZeroAgendaNames()
        {
            List<AgendaDTO> output = new List<AgendaDTO>();
            accountDTO = new AccountDTO() { AccountID = 128 };
            account = new Account(accountDTO, new StubAccountContainer(), new StubAgendaContainer());

            output = account.RetrieveAgendas();

            Assert.True(output.Count == 0);
        }

        [Fact]
        public void RemoveAgenda()
        {
            accountDTO = new AccountDTO() { AccountID = 12 };
            AgendaDTO agendaDTO = new AgendaDTO() { AgendaID = 51, AgendaName = "qwerty", AgendaColor = "#0X2312", NotificationType = "Nee" };
            Account account = new Account(accountDTO, new StubAccountContainer(), new StubAgendaContainer());

            using (StreamWriter streamWriter = new StreamWriter(@"C:\tmp\removeAgendaTest.txt"))
            {
                streamWriter.WriteLine(agendaDTO.AgendaID);
                streamWriter.WriteLine(agendaDTO.AgendaName);
                streamWriter.WriteLine(agendaDTO.AgendaColor);
                streamWriter.WriteLine(agendaDTO.NotificationType);
            }

            account.DeleteAgenda(agendaDTO.AgendaID);

            string[] file = File.ReadAllLines(@"C:\tmp\removeAgendaTest.txt");
            File.Delete(@"C:\tmp\removeAgendaTest.txt");

            Assert.Equal("System.String[]", file.ToString());

        }

        [Fact]
        public void RemoveNoAgenda()
        {
            accountDTO = new AccountDTO() { AccountID = 12 };
            AgendaDTO agendaDTO = new AgendaDTO() { AgendaID = 51, AgendaName = "qwerty", AgendaColor = "#0X2312", NotificationType = "Nee" };
            Account account = new Account(accountDTO, new StubAccountContainer(), new StubAgendaContainer());

            using (StreamWriter streamWriter = new StreamWriter(@"C:\tmp\removeAgendaTest.txt"))
            {
                streamWriter.WriteLine(agendaDTO.AgendaID);
                streamWriter.WriteLine(agendaDTO.AgendaName);
                streamWriter.WriteLine(agendaDTO.AgendaColor);
                streamWriter.WriteLine(agendaDTO.NotificationType);
            }

            account.DeleteAgenda(-5);

            string[] file = File.ReadAllLines(@"C:\tmp\removeAgendaTest.txt");
            File.Delete(@"C:\tmp\removeAgendaTest.txt");

            Assert.Equal("51", file[0]);
            Assert.Equal("qwerty", file[1]);
        }
    }
}
