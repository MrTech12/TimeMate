using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.DTO
{
    public class AccountDTO
    {
        private int accountID;
        private string firstName;
        private string mailAddress;
        private string password;
        private int jobCount;
        private double job1HourlyWage;
        private string job1DayType;
        private double job2HourlyWage;
        private string job2DayType;
        private double allocatedHours;

        public int AccountID { get { return this.accountID; } set { this.accountID = value; } }

        public string FirstName { get { return this.firstName; } set { this.firstName = value; } }

        public string MailAddress { get { return this.mailAddress; } set { this.mailAddress = value; } }

        public string Password { get { return this.password; } set { this.password = value; } }

        public int JobCount { get { return this.jobCount; } set { this.jobCount = value; } }

        public double Job1HourlyWage { get { return this.job1HourlyWage; } set { this.job1HourlyWage = value; } }

        public string Job1DayType { get { return this.job1DayType; } set { this.job1DayType = value; } }

        public double Job2HourlyWage { get { return this.job2HourlyWage; } set { this.job2HourlyWage = value; } }

        public string Job2DayType { get { return this.job2DayType; } set { this.job2DayType = value; } }

        public double AllocatedHours { get { return this.allocatedHours; } set { this.allocatedHours = value; } }
    }
}
