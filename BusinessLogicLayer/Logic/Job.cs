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
        private string messageToUser;

        public Job(AccountDTO accountDTO, IJobContainer jobContext)
        {
            this.accountDTO = accountDTO;
            this._jobContext = jobContext;
        }

        /// <summary>
        /// Retrieve the work hours that are logged into the database for business days.
        /// </summary>
        public double RetrieveWeeklyWorkHours()
        {
            return 0.0;
        }

        /// <summary>
        /// Retrieve the work hours that are logged into the database for the weekend.
        /// </summary>
        public double RetrieveWeeklyPay()
        {
            return 0.0;
        }
    }
}
