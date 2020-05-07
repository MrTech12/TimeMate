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
        public int GetUserID(string mail)
        {
            throw new NotImplementedException();
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

            return returnMessage;
        }

        public string SearchUserByMail(string mail)
        {
            throw new NotImplementedException();
        }
    }
}
