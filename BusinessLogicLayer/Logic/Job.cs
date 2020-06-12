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
            double weekendPayWage = 0;
            double workdayPayWage = 0;

            workdayPayWage = _jobContainer.GetWorkdayPayRate(accountID);
            weekendPayWage = _jobContainer.GetWeekendPayRate(accountID);

            if (workdayPayWage != 0 && weekendPayWage == 0)
            {
                int agendaID = Convert.ToInt32(_agendaContainer.GetAgendaID("Bijbaan", accountID));
                jobDetails.WeeklyHours = RetrieveWorkdayHours(agendaID);
                jobDetails.WeeklyPay = Convert.ToDouble(jobDetails.WeeklyHours) * workdayPayWage;
            }
            else if (workdayPayWage == 0 && weekendPayWage != 0)
            {
                int agendaID = Convert.ToInt32(_agendaContainer.GetAgendaID("Bijbaan", accountID));
                jobDetails.WeeklyHours = RetrieveWeekendHours(agendaID);
                jobDetails.WeeklyPay = Convert.ToDouble(jobDetails.WeeklyHours) * weekendPayWage;
            }
            else if (workdayPayWage != 0 && weekendPayWage != 0)
            {
                string workdays = RetrieveWorkdayHours(accountID);
                string weekend = RetrieveWeekendHours(accountID);
                jobDetails.WeeklyHours = workdays + weekend;
                jobDetails.WeeklyPay = Convert.ToDouble(jobDetails.WeeklyHours) * weekendPayWage;
            }
            return jobDetails;
        }

        public string RetrieveWorkdayHours(int agendaID)
        {
            jobDTO = new JobDTO();

            DateTime monday = GetFirstDayOfWeek(DateTime.Now);
            DateTime friday = monday.AddDays(4);

            List<DateTime> weekDates = new List<DateTime>();
            weekDates.Add(monday);
            weekDates.Add(friday);

            jobDTO = _appointmentContainer.GetHoursForWorkdayJob(agendaID, weekDates);

            double workdayHours = 0;
            for (int i = 0; i < jobDTO.StartDate.Count; i++)
            {
                workdayHours += (jobDTO.EndDate[i] - jobDTO.StartDate[i]).TotalHours;
            }
            string workdayHours2 = workdayHours.ToString("N2");
            return workdayHours2;
        }

        public string RetrieveWeekendHours(int accountID)
        {
            jobDTO = new JobDTO();

            DateTime monday = GetFirstDayOfWeek(DateTime.Now);
            DateTime saturday = monday.AddDays(5);
            DateTime sunday = monday.AddDays(6);

            List<DateTime> weekendDates = new List<DateTime>();
            weekendDates.Add(saturday);
            weekendDates.Add(sunday);

            jobDTO = _appointmentContainer.GetHoursForWeekendJob(accountID, weekendDates);

            string weekendHours = null;
            for (int i = 0; i < jobDTO.StartDate.Count; i++)
            {
                weekendHours += (jobDTO.EndDate[i] - jobDTO.StartDate[i]).TotalHours.ToString("N2");
            }
            return weekendHours;
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
