using System;
using System.Collections.Generic;
using System.Text;
using DataAccessLayer.DTO;
using DataAccessLayer.Interfaces;

namespace TimeMateTest.Stubs
{
    class StubAccountContext : IAccountContainer
    {
        public string GetUserID(string mail)
        {
            string accountID = null;
            if (mail == "bert@gmail.com")
            {
                accountID = "0";
            }
            return accountID;
        }

        public string GetFirstName(string mail)
        {
            return "test";
        }

        public int CreateAccount(AccountDTO AccountDTO)
        {
            int accountID = 0;
            if (AccountDTO.Mail == "sina1240@gmail.com")
            {
                accountID = 6;
            }
            return accountID;
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
    }
}
