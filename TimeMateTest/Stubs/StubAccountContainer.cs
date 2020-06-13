using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DataAccessLayer.DTO;
using DataAccessLayer.Interfaces;

namespace TimeMateTest.Stubs
{
    class StubAccountContainer : IAccountContainer
    {
        public int GetUserID(string mail)
        {
            int accountID = 0;
            if (mail == "bert@gmail.com")
            {
                accountID = 0;
            }
            return accountID;
        }

        public string GetFirstName(string mail)
        {
            return "test";
        }

        public string SearchForPasswordHash(string mail)
        {
            string returnMessage = null;
            if (mail == "test@gmail.com")
            {
                returnMessage = null;
            }
            else if (mail == "bert@gmail.com")
            {
                returnMessage = "$2b$10$s.mU04TYPOSCHn.BOh3iIehsM5sGUyvoAGoXgOojrTxtEF/5aptLS";
            }
            return returnMessage;
        }

        public int CreateAccount(AccountDTO accountDTO)
        {
            int accountID = 0;
            if (accountDTO.Mail == "sina1240@gmail.com")
            {
                using (StreamWriter streamWriter = new StreamWriter(@"C:\tmp\CreateAccountTest.txt"))
                {
                    streamWriter.WriteLine(6);
                    streamWriter.WriteLine(accountDTO.FirstName);
                    streamWriter.WriteLine(accountDTO.Mail);
                    streamWriter.WriteLine(accountDTO.Password);
                }
                accountID = 6;
            }
            else if (accountDTO.Mail == "sina1242@gmail.com")
            {
                using (StreamWriter streamWriter = new StreamWriter(@"C:\tmp\CreateAccountTest.txt"))
                {
                    streamWriter.WriteLine(14);
                    streamWriter.WriteLine(accountDTO.FirstName);
                    streamWriter.WriteLine(accountDTO.Mail);
                    streamWriter.WriteLine(accountDTO.Password);
                    streamWriter.WriteLine(accountDTO.JobHourlyWage[0]);
                    streamWriter.WriteLine(accountDTO.JobDayType[0]);
                }
                accountID = 14;
            }
            return accountID;
        }
    }
}
