using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.DTO
{
    public class JobDTO
    {
        private List<DateTime> startDate;
        private List<DateTime> endDate;
        private double workdayWage;
        private double weekendWage;

        public List<DateTime> StartDate { get { return this.startDate; } set { this.startDate = value; } }

        public List<DateTime> EndDate { get { return this.endDate; } set { this.endDate = value; } }

        public double WorkdayWage { get { return this.workdayWage; } set { this.workdayWage = value; } }

        public double WeekendWage { get { return this.weekendWage; } set { this.weekendWage = value; } }
    }
}
