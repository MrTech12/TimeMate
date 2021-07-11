using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DTOs
{
    public class JobDTO
    {
        private List<DateTime> startDate = new List<DateTime>();
        private List<DateTime> endDate = new List<DateTime>();
        private double weeklyPay;
        private double weeklyHours;

        public List<DateTime> StartDate { get { return this.startDate; } set { this.startDate = value; } }

        public List<DateTime> EndDate { get { return this.endDate; } set { this.endDate = value; } }

        public double WeeklyPay { get { return this.weeklyPay; } set { this.weeklyPay = value; } }

        public double WeeklyHours { get { return this.weeklyHours; } set { this.weeklyHours = value; } }
    }
}