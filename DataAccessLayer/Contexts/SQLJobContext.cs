using DataAccessLayer.DTO;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Contexts
{
    public class SQLJobContext : IJobContext
    {
        public double GetWorkdayPay(AccountDTO accountDTO, JobDTO jobDTO)
        {
            throw new NotImplementedException();
        }

        public double GetWeekendPay(AccountDTO accountDTO, JobDTO jobDTO)
        {
            throw new NotImplementedException();
        }

        public JobDTO GetRegisteredWeeklyHours(AccountDTO accountDTO)
        {
            throw new NotImplementedException();
        }
    }
}
