using DataAccessLayer.DTO;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Logic
{
    public class Job
    {
        private IJobContainer _jobContainer;
        private IAgendaContainer _agendaContainer;
        private IAppointmentContainer _appointmentContainer;
        private JobDTO jobDTO;

        public Job(IJobContainer jobContainer, IAgendaContainer agendaContainer, IAppointmentContainer appointmentContainer)
        {
            this._jobContainer = jobContainer;
            this._agendaContainer = agendaContainer;
            this._appointmentContainer = appointmentContainer;
        }

        public JobDTO CalculateWeeklyPay(int accountID)
        {
            JobDTO jobDetails = new JobDTO();
            double workdayWage = _jobContainer.GetWorkdayPayRate(accountID);
            double weekendWage = _jobContainer.GetWeekendPayRate(accountID);

            if (workdayWage != 0 && weekendWage == 0)
            {
                int agendaID = Convert.ToInt32(_agendaContainer.GetAgendaID("Bijbaan", accountID));
                jobDetails.WeeklyHours = RetrieveWorkdayHours(agendaID);
                jobDetails.WeeklyPay = Convert.ToDouble(jobDetails.WeeklyHours) * workdayWage;
            }
            else if (workdayWage == 0 && weekendWage != 0)
            {
                int agendaID = Convert.ToInt32(_agendaContainer.GetAgendaID("Bijbaan", accountID));
                jobDetails.WeeklyHours = RetrieveWeekendHours(agendaID);
                jobDetails.WeeklyPay = Convert.ToDouble(jobDetails.WeeklyHours) * weekendWage;
            }
            else if (workdayWage != 0 && weekendWage != 0)
            {
                int agendaID = Convert.ToInt32(_agendaContainer.GetAgendaID("Bijbaan", accountID));
                string workdays = RetrieveWorkdayHours(agendaID);
                string weekend = RetrieveWeekendHours(agendaID);
                jobDetails.WeeklyHours = workdays + weekend;
                jobDetails.WeeklyPay = Convert.ToDouble(jobDetails.WeeklyHours) * weekendWage;
            }
            return jobDetails;
        }

        public string RetrieveWorkdayHours(int agendaID)
        {
            jobDTO = new JobDTO();

            DateTime monday = GetFirstDayOfWeek(DateTime.Now);
            List<DateTime> weekDates = new List<DateTime>();
            weekDates.Add(monday);
            weekDates.Add(monday.AddDays(4));

            jobDTO = _appointmentContainer.GetWorkHours(agendaID, weekDates);

            double workdayHours = CalculateWorkedHours();
            string test = workdayHours.ToString("N2");
            return test;
        }

        public string RetrieveWeekendHours(int accountID)
        {
            jobDTO = new JobDTO();

            DateTime monday = GetFirstDayOfWeek(DateTime.Now);
            List<DateTime> weekendDates = new List<DateTime>();
            weekendDates.Add(monday.AddDays(5));
            weekendDates.Add(monday.AddDays(6));

            jobDTO = _appointmentContainer.GetWorkHours(accountID, weekendDates);

            double workdayHours = CalculateWorkedHours();
            string test = workdayHours.ToString("N2");
            return test;
        }

        public double CalculateWorkedHours()
        {
            double workedHours = 0;
            for (int i = 0; i < jobDTO.StartDate.Count; i++)
            {
                workedHours += (jobDTO.EndDate[i] - jobDTO.StartDate[i]).TotalHours;
            }
            return workedHours;
        }

        public static DateTime GetFirstDayOfWeek(DateTime date)
        {
            var culture = System.Threading.Thread.CurrentThread.CurrentCulture;
            var diff = date.DayOfWeek - culture.DateTimeFormat.FirstDayOfWeek;
            if (diff < 0)
                diff += 7;
            return date.AddDays(-diff).Date;
        }
    }
}
