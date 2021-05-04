using BusinessLogicLayer.Logic;
using Model.DTO_s;
using DataAccessLayer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeMate.Services;

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

        private AgendaView agendaView;
        private Job job;
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

                agendaView = new AgendaView(accountDTO, _appointmentRepository);
                List<AppointmentDTO> appointments = agendaView.RetrieveAppointments();

                job = new Job(_jobRepository, _agendaRepository, _appointmentRepository);
                JobDTO jobDTO = job.RetrieveJobDetails(accountDTO.AccountID);

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
        public IActionResult AppointmentExtra(int appointmentID)
        {
            NormalAppointment normalAppointment = new NormalAppointment(_normalAppointmentRepository);
            string description = normalAppointment.RetrieveDescription(appointmentID);

            if (description == "")
            {
                ChecklistAppointment checklistAppointment = new ChecklistAppointment(_checklistAppointmentRepository);
                var tasks = checklistAppointment.RetrieveTasks(appointmentID);
                return Json(tasks);
            }
            else
            {
                return Json(description);
            }
        }
    }
}
