using Core.DTOs;
using Core.Entities;
using Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Logic
{
    public class JobService
    {
        private IJobRepository _jobRepository;
        private IAgendaRepository _agendaRepository;
        private IAppointmentRepository _appointmentRepository;
        private JobDTO jobDTO;
        private Job job;

        public JobService(IJobRepository jobRepository, IAgendaRepository agendaRepository, IAppointmentRepository appointmentRepository)
        {
            _jobRepository = jobRepository;
            _agendaRepository = agendaRepository;
            _appointmentRepository = appointmentRepository;
        }

        public JobService(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        public JobDTO RetrieveJobDetails(int accountID)
        {
            JobDTO jobDetails = new JobDTO();
            double workdayPayWage = _jobRepository.GetWorkdayPayWage(accountID);
            double weekendPayWage = _jobRepository.GetWeekendPayWage(accountID);

            if (workdayPayWage == 0 && weekendPayWage == 0)
            {
                return jobDetails;
            }
            else
            {
                int agendaID = _agendaRepository.GetWorkAgendaID(accountID);
                if (workdayPayWage != 0 && weekendPayWage == 0)
                {
                    jobDetails.WeeklyHours = RetrieveWorkdayHours(agendaID);
                    jobDetails.WeeklyPay = jobDetails.WeeklyHours * workdayPayWage;
                }
                else if (workdayPayWage == 0 && weekendPayWage != 0)
                {
                    jobDetails.WeeklyHours = RetrieveWeekendHours(agendaID);
                    jobDetails.WeeklyPay = jobDetails.WeeklyHours * weekendPayWage;
                }
                else
                {
                    double workdayHours = RetrieveWorkdayHours(agendaID);
                    double weekendHours = RetrieveWeekendHours(agendaID);
                    jobDetails.WeeklyHours = workdayHours + weekendHours;

                    jobDetails.WeeklyPay += workdayHours * workdayPayWage;
                    jobDetails.WeeklyPay += weekendHours * weekendPayWage;
                }
            }
            return jobDetails;
        }

        public double RetrieveWorkdayHours(int agendaID)
        {
            DateTime monday = GetFirstDayOfWeek(DateTime.Now);
            List<DateTime> workdayDates = new List<DateTime>();
            workdayDates.Add(monday);
            workdayDates.Add(monday.AddDays(5));

            job = new Job();
            job = _appointmentRepository.GetWorkHours(agendaID, workdayDates);

            jobDTO = new JobDTO();
            jobDTO.StartDate = job.StartDate;
            jobDTO.EndDate = job.EndDate;

            return CalculateWorkedHours(jobDTO);
        }

        public double RetrieveWeekendHours(int agendaID)
        {
            DateTime monday = GetFirstDayOfWeek(DateTime.Now);
            List<DateTime> weekendDates = new List<DateTime>();
            weekendDates.Add(monday.AddDays(5));
            weekendDates.Add(monday.AddDays(7));

            job = new Job();
            job = _appointmentRepository.GetWorkHours(agendaID, weekendDates);

            jobDTO = new JobDTO();
            jobDTO.StartDate = job.StartDate;
            jobDTO.EndDate = job.EndDate;

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

        public void AddPayDetails(AccountDTO accountDTO)
        {
            Account account = new Account() { AccountID = accountDTO.AccountID };
            account.JobHourlyWage = accountDTO.JobHourlyWage;
            account.JobDayType = accountDTO.JobDayType;

            _jobRepository.CreatePayDetails(account);
        }
    }
}
