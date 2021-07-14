using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class Job
    {
        public int AccountID { get; set; }

        public List<DateTime> StartDate { get; set; } = new List<DateTime>();

        public List<DateTime> EndDate { get; set; } = new List<DateTime>();

        public double WeeklyPay { get; set; }

        public double WeeklyHours { get; set; }
    }
}
