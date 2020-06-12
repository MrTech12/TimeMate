using BusinessLogicLayer.Logic;
using DataAccessLayer.DTO;
using System;
using System.Collections.Generic;
using TimeMateTest.Stubs;
using Xunit;

namespace TimeMateTest.BLL
{
    public class JobTest
    {
        private Job job;
        private AccountDTO accountDTO = new AccountDTO();

        //[Fact]
        //public void GetWeeklyPayTest()
        //{
        //    accountDTO.AccountID = 0;
        //    List<double> result = new List<double>();

        //    result = job.RetrievePayRate(accountDTO.AccountID);

        //    Assert.Equal(1.2, result[0]);
        //    Assert.True(result.Count == 1);
        //}

        //[Fact]
        //public void GetWeeklyWorkHoursTest()
        //{
        //    accountDTO.AccountID = 0;
        //    List<double> result = new List<double>();
        //    result.Add(23.3);
        //    double output;

        //    output = job.CalculateWeeklyPay(result);

        //    Assert.Equal(1.2, output);
        //}
    }
}
