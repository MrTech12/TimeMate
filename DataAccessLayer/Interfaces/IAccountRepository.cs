using Model.DTO_s;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Interfaces
{
    public interface IAccountRepository
    {
        int GetUserID(string mail);

        string GetFirstName(string mail);

        string GetPasswordHash(string mail);

        int CreateAccount(AccountDTO AccountDTO);
    }
}
