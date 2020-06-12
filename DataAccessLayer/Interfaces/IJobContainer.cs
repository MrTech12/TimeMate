using DataAccessLayer.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Interfaces
{
    public interface IJobContainer
    {
        void AddPayDetails(AccountDTO accountDTO);

        double GetWorkdayPayRate(int accountID);

        double GetWeekendPayRate(int accountID);
    }
}
