using System;
using System.Collections.Generic;
using System.Text;
using DataAccessLayer.DTO;
using DataAccessLayer.Interfaces;

namespace TimeMateTest.Stubs
{
    class StubJobContext : IJobContext
    {
        public double GetWeekendPay(AccountDTO accountDTO, JobDTO jobDTO)
        {
            throw new NotImplementedException();
        }

        public double GetWorkdayPay(AccountDTO accountDTO, JobDTO jobDTO)
        {
            throw new NotImplementedException();
        }
    }
}
