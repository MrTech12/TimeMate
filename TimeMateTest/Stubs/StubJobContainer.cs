using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DataAccessLayer.DTO;
using DataAccessLayer.Interfaces;

namespace TimeMateTest.Stubs
{
    class StubJobContainer : IJobContainer
    {
        public void AddPayDetails(AccountDTO accountDTO)
        {
            using (StreamWriter streamWriter = new StreamWriter(@"C:\tmp\addWorkPayDetails.txt"))
            {
                streamWriter.WriteLine(accountDTO.JobHourlyWage[0]);
                streamWriter.WriteLine(accountDTO.JobDayType[0]);
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
