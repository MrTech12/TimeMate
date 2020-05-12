using DataAccessLayer.DTO;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Logic
{
    public class Job
    {
        private IJobContext _jobContext;
        private AccountDTO accountDTO;
        private string messageToUser;

        public Job(AccountDTO accountDTO, IJobContext jobContext)
        {
            this.accountDTO = accountDTO;
            this._jobContext = jobContext;
        }

        public double ShowWeeklyWorkHours()
        {

            return 0.0;
        }

        public double ShowWeeklyPay()
        {
            return 0.0;
        }

        public string GetAdvice()
        {
            return null;
        }
    }
}
