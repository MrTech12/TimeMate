using DataAccessLayer.DTO;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Logic
{
    public class Job
    {
        private IJobRepository _jobContainer;
        private IAgendaRepository _agendaContainer;
        private IAppointmentRepository _appointmentContainer;
        private JobDTO jobDTO;

        public Job(IJobRepository jobContainer, IAgendaRepository agendaContainer, IAppointmentRepository appointmentContainer)
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

            if (workdayWage == 0 && weekendWage == 0)
            {
                return jobDetails;
            }
            else
            {
                int agendaID = _agendaContainer.GetAgendaID("Bijbaan", accountID);
                if (workdayWage != 0 && weekendWage == 0)
                {
                    jobDetails.WeeklyHours = RetrieveWorkdayHours(agendaID);
                    jobDetails.WeeklyPay = jobDetails.WeeklyHours * workdayWage;
                }
                else if (workdayWage == 0 && weekendWage != 0)
                {
                    jobDetails.WeeklyHours = RetrieveWeekendHours(agendaID);
                    jobDetails.WeeklyPay = jobDetails.WeeklyHours * weekendWage;
                }
                else
                {
                    double workdays = RetrieveWorkdayHours(agendaID);
                    double weekend = RetrieveWeekendHours(agendaID);
                    jobDetails.WeeklyHours = workdays + weekend;

                    jobDetails.WeeklyPay += workdays * workdayWage;
                    jobDetails.WeeklyPay += weekend * weekendWage;
                }
            }
            return jobDetails;
        }

        public double RetrieveWorkdayHours(int agendaID)
        {
            jobDTO = new JobDTO();

            DateTime monday = GetFirstDayOfWeek(DateTime.Now);
            List<DateTime> workdayDates = new List<DateTime>();
            workdayDates.Add(monday);
            workdayDates.Add(monday.AddDays(5));

            jobDTO = _appointmentContainer.GetWorkHours(agendaID, workdayDates);

            return CalculateWorkedHours(jobDTO);
        }

        public double RetrieveWeekendHours(int accountID)
        {
            jobDTO = new JobDTO();

            DateTime monday = GetFirstDayOfWeek(DateTime.Now);
            List<DateTime> weekendDates = new List<DateTime>();
            weekendDates.Add(monday.AddDays(5));
            weekendDates.Add(monday.AddDays(7));

            jobDTO = _appointmentContainer.GetWorkHours(accountID, weekendDates);

            return CalculateWorkedHours(jobDTO);
        }

        public double CalculateWorkedHours(JobDTO jobDTO)
        {
            double workedHours = 0;

            for (int i = 0; i < jobDTO.StartDate.Count; i++)
            {
                workedHours += (jobDTO.EndDate[i] - jobDTO.StartDate[i]).TotalHours;
            }
            return workedHours;
        }

        public DateTime GetFirstDayOfWeek(DateTime date)
        {
            var culture = System.Threading.Thread.CurrentThread.CurrentCulture;
            var difference = date.DayOfWeek - culture.DateTimeFormat.FirstDayOfWeek;
            if (difference < 0)
            {
                difference += 7;
            }
            return date.AddDays(-difference).Date;
        }
    }
}
