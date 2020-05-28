using DataAccessLayer.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Interfaces
{
    public interface IAccountContext
    {
        string GetUserID(string mail);

        string SearchForPasswordHash(string mail);

        int CreateNewAccount(AccountDTO AccountDTO);
    }
}
