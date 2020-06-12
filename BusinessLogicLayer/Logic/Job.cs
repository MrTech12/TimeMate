using DataAccessLayer.DTO;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Logic
{
    public class Job
    {
        private IJobContainer _jobContext;
        private AccountDTO accountDTO;

        public Job(AccountDTO accountDTO, IJobContainer jobContext)
        {
            this.accountDTO = accountDTO;
            this._jobContext = jobContext;
        }

        public List<double> RetrievePayRate(int accountID)
        {
            List<double> payRate = new List<double>();
            return payRate;
        }

        public double CalculateWeeklyPay(List<double> payRate)
        {
            return 0.0;
        }
    }
}
