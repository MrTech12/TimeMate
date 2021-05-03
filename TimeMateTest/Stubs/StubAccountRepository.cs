using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DataAccessLayer.DTO;
using DataAccessLayer.Interfaces;

namespace TimeMateTest.Stubs
{
    class StubAccountRepository : IAccountRepository
    {
        public int GetUserID(string mail)
        {
            int accountID = -1;
            if (mail == "bert@gmail.com")
            {
                accountID = 0;
            }
            return accountID;
        }

        public string GetFirstName(string mail)
        {
            string firstName = null;
            if (mail == "intel12@gmail.com")
            {
                firstName = "Intel";
            }
            return firstName;
        }

        public string GetPasswordHash(string mail)
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
            using (StreamWriter streamWriter = new StreamWriter(@"C:\tmp\CreateAccountTest.txt"))
            {
                streamWriter.WriteLine(accountDTO.FirstName);
                streamWriter.WriteLine(accountDTO.Mail);
                streamWriter.WriteLine(accountDTO.Password);

                if (accountDTO.Mail == "sina1240@gmail.com")
                {
                    streamWriter.WriteLine(6);
                    accountID = 6;
                }
                else if (accountDTO.Mail == "sina1242@gmail.com")
                {
                    streamWriter.WriteLine(14);
                    accountID = 14;
                }
                else if (accountDTO.Mail == "intel12@gmail.com")
                {
                    streamWriter.WriteLine(39);
                    accountID = 39;
                }
            }
            return accountID;
        }
    }
}
