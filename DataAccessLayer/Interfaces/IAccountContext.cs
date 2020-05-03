using DataAccessLayer.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Interfaces
{
    public interface IAccountContext
    {
        int GetUserID(string mail);

        string SearchForPasswordHash(string mail);

        string SearchUserByMail(string mail);

        void RegisterNewUser(AccountDTO AccountDTO);
    }
}
