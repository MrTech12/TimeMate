using DataAccessLayer.DTO;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Contexts
{
    public class SQLJobContext : IJobContext
    {
        public JobDTO GetRegisteredWeeklyHours(AccountDTO accountDTO)
        {
            throw new NotImplementedException();
        }

        public JobDTO GetWeeklyPay(AccountDTO accountDTO)
        {
            throw new NotImplementedException();
        }

        public JobDTO GetWeeklyWorkHours(AccountDTO accountDTO)
        {
            throw new NotImplementedException();
        }
    }
}
