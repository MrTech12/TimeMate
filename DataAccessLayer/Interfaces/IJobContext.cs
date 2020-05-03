using DataAccessLayer.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Interfaces
{
    public interface IJobContext
    {
        JobDTO GetWeeklyWorkHours(AccountDTO accountDTO);

        JobDTO GetWeeklyPay(AccountDTO accountDTO);

        JobDTO GetRegisteredWeeklyHours(AccountDTO accountDTO);
    }
}
