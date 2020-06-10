using DataAccessLayer.DTO;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Containers
{
    public class SQLJobContext : IJobContainer
    {
        /// <summary>
        /// Get the pay for business days from the database.
        /// </summary>
        public double GetWorkdayPay(AccountDTO accountDTO, JobDTO jobDTO)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get the pay for the weekend from the database.
        /// </summary>
        public double GetWeekendPay(AccountDTO accountDTO, JobDTO jobDTO)
        {
            throw new NotImplementedException();
        }
    }
}
