using Core.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Repositories
{
    public interface IJobRepository
    {
        void CreatePayDetails(AccountDTO accountDTO);

        double GetWorkdayPayWage(int accountID);

        double GetWeekendPayWage(int accountID);
    }
}