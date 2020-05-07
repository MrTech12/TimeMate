﻿using System;
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
        private List<double> jobHourlyWage = new List<double>();
        private List<string> jobDayType = new List<string>();
        private double allocatedHours;

        public int AccountID { get { return this.accountID; } set { this.accountID = value; } }

        public string FirstName { get { return this.firstName; } set { this.firstName = value; } }

        public string MailAddress { get { return this.mailAddress; } set { this.mailAddress = value; } }

        public string Password { get { return this.password; } set { this.password = value; } }

        public int JobCount { get { return this.jobCount; } set { this.jobCount = value; } }

        public List<double> JobHourlyWage { get { return this.jobHourlyWage; } set { this.jobHourlyWage = value; } }

        public List<string> JobDayType { get { return this.jobDayType; } set { this.jobDayType = value; } }

        public double AllocatedHours { get { return this.allocatedHours; } set { this.allocatedHours = value; } }
    }
}
