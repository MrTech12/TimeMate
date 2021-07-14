using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class Account
    {
        public int AccountID { get; set; }

        public string FirstName { get; set; }

        public string Mail { get; set; }

        public string Password { get; set; }

        public List<double> JobHourlyWage { get; set; }

        public List<string> JobDayType { get; set; }
    }
}
