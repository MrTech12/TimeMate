using BusinessLogicLayer.Logic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeMate.Services;
using TimeMate.Models.BodyModels;
using Core.Repositories;
using Core.DTOs;

namespace TimeMate.Controllers
{
    public class AgendaViewController : Controller
    {
        private readonly IAgendaRepository _agendaRepository;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly INormalAppointmentRepository _normalAppointmentRepository;
        private readonly IChecklistAppointmentRepository _checklistAppointmentRepository;
        private readonly IJobRepository _jobRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private AgendaViewService agendaViewService;
        private JobService jobService;
        private SessionService sessionService;
        private AccountDTO accountDTO = new AccountDTO();

        public AgendaViewController(IAgendaRepository agendaRepository, IAppointmentRepository appointmentRepository, INormalAppointmentRepository normalAppointmentContainer, IChecklistAppointmentRepository checklistAppointmentRepository, IJobRepository jobRepository, IHttpContextAccessor httpContextAccessor)
        {
            _agendaRepository = agendaRepository;
            _appointmentRepository = appointmentRepository;
            _normalAppointmentRepository = normalAppointmentContainer;
            _checklistAppointmentRepository = checklistAppointmentRepository;
            _jobRepository = jobRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public IActionResult Index()
        {
            sessionService = new SessionService(_httpContextAccessor);

            if (sessionService.CheckSessionValue())
            {
                accountDTO.AccountID = HttpContext.Session.GetInt32("accountID").Value;

                agendaViewService = new AgendaViewService(accountDTO, _appointmentRepository);
                List<AppointmentDTO> appointments = agendaViewService.RetrieveAppointments();

                jobService = new JobService(_jobRepository, _agendaRepository, _appointmentRepository);
                JobDTO jobDTO = jobService.RetrieveJobDetails(accountDTO.AccountID);

                if (jobDTO.WeeklyPay != 0)
                {
                    ViewBag.pay = jobDTO.WeeklyPay.ToString("N2");
                    ViewBag.hours = jobDTO.WeeklyHours.ToString("N1");
                }
                return View(appointments);
            }
            else
            {
                return RedirectToAction("Index", "Account");
            }
        }

        [HttpGet]
        [Route("AgendaView/AppointmentExtra/{appointmentID}")]
        public IActionResult RetrieveAppointmentExtra(int appointmentID)
        {
            NormalAppointmentService normalAppointment = new NormalAppointmentService(_normalAppointmentRepository);
            string description = normalAppointment.RetrieveDescription(appointmentID);

            if (description == String.Empty)
            {
                ChecklistAppointmentService checklistAppointment = new ChecklistAppointmentService(_checklistAppointmentRepository);
                var tasks = checklistAppointment.RetrieveTasks(appointmentID);

                List<TaskBodyModel> taskBodyModel = new List<TaskBodyModel>();
                foreach (KeyValuePair<int, string> kvp in tasks)
                {
                    taskBodyModel.Add(new TaskBodyModel() { TaskID = kvp.Key, TaskName = kvp.Value });
                }

                return Json(taskBodyModel);
            }
            else
            {
                return Json(description);
            }
        }
    }
}
