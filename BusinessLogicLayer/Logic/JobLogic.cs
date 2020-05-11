using DataAccessLayer.DTO;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Logic
{
    public class JobLogic
    {
        private IJobContext _jobContext;
        private AccountDTO accountDTO;
        private string messageToUser;

        public JobLogic(AccountDTO accountDTOInput, IJobContext jobContextInput)
        {
            this.accountDTO = accountDTOInput;
            this._jobContext = jobContextInput;
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
