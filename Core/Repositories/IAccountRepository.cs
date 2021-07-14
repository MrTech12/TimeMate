using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Repositories
{
    public interface IAccountRepository
    {
        int GetUserID(string mail);

        string GetFirstName(string mail);

        string GetPasswordHash(string mail);

        int CreateAccount(Account account);
    }
}