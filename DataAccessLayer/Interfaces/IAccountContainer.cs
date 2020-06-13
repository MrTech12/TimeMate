using DataAccessLayer.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Interfaces
{
    public interface IAccountContainer
    {
        int GetUserID(string mail);

        string GetFirstName(string mail);

        string SearchForPasswordHash(string mail);

        int CreateAccount(AccountDTO AccountDTO);
    }
}
