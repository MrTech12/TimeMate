using System;
using System.Collections.Generic;
using System.Text;

namespace Model.DTO_s
{
    public class AccountDTO
    {
        private int accountID;
        private string firstName;
        private string mail;
        private string password;
        private List<double> jobHourlyWage = new List<double>();
        private List<string> jobDayType = new List<string>();


        public int AccountID { get { return this.accountID; } set { this.accountID = value; } }

        public string FirstName { get { return this.firstName; } set { this.firstName = value; } }

        public string Mail { get { return this.mail; } set { this.mail = value; } }

        public string Password { get { return this.password; } set { this.password = value; } }

        public List<double> JobHourlyWage { get { return this.jobHourlyWage; } set { this.jobHourlyWage = value; } }

        public List<string> JobDayType { get { return this.jobDayType; } set { this.jobDayType = value; } }
    }
}
