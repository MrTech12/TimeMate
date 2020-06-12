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

        public JobDTO RetrieveJobDetails(int accountID)
        {
            JobDTO jobDetails = new JobDTO();
            double workdayWage = _jobContainer.GetWorkdayPayWage(accountID);
            double weekendWage = _jobContainer.GetWeekendPayWage(accountID);

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
                jobDetails.WeeklyPay += Convert.ToDouble(workdays) * workdayWage;
                jobDetails.WeeklyPay += Convert.ToDouble(weekend) * weekendWage;
            }
            return jobDetails;
        }

        public string RetrieveWorkdayHours(int agendaID)
        {
            jobDTO = new JobDTO();

            DateTime monday = GetFirstDayOfWeek(DateTime.Now);
            List<DateTime> workdayDates = new List<DateTime>();
            workdayDates.Add(monday);
            workdayDates.Add(monday.AddDays(4));

            jobDTO = _appointmentContainer.GetWorkHours(agendaID, workdayDates);

            return CalculateWorkedHours(jobDTO);
        }

        public string RetrieveWeekendHours(int accountID)
        {
            jobDTO = new JobDTO();

            DateTime monday = GetFirstDayOfWeek(DateTime.Now);
            List<DateTime> weekendDates = new List<DateTime>();
            weekendDates.Add(monday.AddDays(5));
            weekendDates.Add(monday.AddDays(6));

            jobDTO = _appointmentContainer.GetWorkHours(accountID, weekendDates);

            return CalculateWorkedHours(jobDTO);
        }

        public string CalculateWorkedHours(JobDTO jobDTO)
        {
            double workedHours = 0;
            string roundedWorkedHours = null;
            for (int i = 0; i < jobDTO.StartDate.Count; i++)
            {
                workedHours += (jobDTO.EndDate[i] - jobDTO.StartDate[i]).TotalHours;
            }
            roundedWorkedHours = workedHours.ToString("N2");
            return roundedWorkedHours;
        }

        public DateTime GetFirstDayOfWeek(DateTime date)
        {
            var culture = System.Threading.Thread.CurrentThread.CurrentCulture;
            var diff = date.DayOfWeek - culture.DateTimeFormat.FirstDayOfWeek;
            if (diff < 0)
            {
                diff += 7;
            }
            return date.AddDays(-diff).Date;
        }
    }
}
