using DataAccessLayer.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Interfaces
{
    public interface IJobContainer
    {
        void AddPayDetails(AccountDTO accountDTO);

        double GetWorkdayPay(AccountDTO accountDTO, JobDTO jobDTO);

        double GetWeekendPay(AccountDTO accountDTO, JobDTO jobDTO);
    }
}
