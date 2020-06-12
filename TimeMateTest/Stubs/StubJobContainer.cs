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

        public double GetWeekendPayRate(int accountID)
        {
            throw new NotImplementedException();
        }

        public double GetWorkdayPayRate(int accountID)
        {
            throw new NotImplementedException();
        }
    }
}
