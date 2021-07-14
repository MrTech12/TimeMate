using Core.Entities;
using Core.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TimeMateTest.Stubs
{
    class StubJobRepository : IJobRepository
    {
        public void CreatePayDetails(Account account)
        {
            using (StreamWriter streamWriter = new StreamWriter(@"C:\tmp\addWorkPayDetails.txt"))
            {
                for (int i = 0; i < account.JobHourlyWage.Count; i++)
                {
                    streamWriter.WriteLine(account.JobHourlyWage[i]);
                    streamWriter.WriteLine(account.JobDayType[i]);
                }
            }
        }

        public double GetWorkdayPayWage(int accountID)
        {
            double payWage = 0;
            if (accountID == 15)
            {
                payWage = 12.50;
            }
            else if (accountID == 30)
            {
                payWage = 10;
            }
            return payWage;
        }

        public double GetWeekendPayWage(int accountID)
        {
            double payWage = 0;
            if (accountID == 25)
            {
                payWage = 15.30;
            }
            else if (accountID == 30)
            {
                payWage = 10;
            }
            return payWage;
        }
    }
}
