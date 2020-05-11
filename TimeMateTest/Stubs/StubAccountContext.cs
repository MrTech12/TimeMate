using System;
using System.Collections.Generic;
using System.Text;
using DataAccessLayer.DTO;
using DataAccessLayer.Interfaces;

namespace TimeMateTest.Stubs
{
    class StubAccountContext : IAccountContext
    {
        string returnMessage;
        public string GetUserID(string mail)
        {
            string accountID = null;
            if (mail == "bert@gmail.com")
            {
                accountID = "0";
            }
            return accountID;
        }

        public void RegisterNewUser(AccountDTO AccountDTO)
        {
            throw new NotImplementedException();
        }

        public string SearchForPasswordHash(string mail)
        {
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
