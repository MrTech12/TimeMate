using Model.DTO_s;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Interfaces
{
    public interface IJobRepository
    {
        void AddPayDetails(AccountDTO accountDTO);

        double GetWorkdayPayWage(int accountID);

        double GetWeekendPayWage(int accountID);
    }
}
