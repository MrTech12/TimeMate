using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Repositories
{
    public interface IJobRepository
    {
        void CreatePayDetails(Account account);

        double GetWorkdayPayWage(int accountID);

        double GetWeekendPayWage(int accountID);
    }
}