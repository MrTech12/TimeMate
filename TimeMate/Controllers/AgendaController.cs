using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicLayer.Logic;
using DataAccessLayer.DTO;
using DataAccessLayer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TimeMate.Models;
using TimeMate.Services;

namespace TimeMate.Controllers
{
    public class AgendaController : Controller
    {
        private readonly IAgendaRepository _agendaContainer;
        private readonly IAppointmentRepository _appointmentContainer;
        private readonly INormalAppointmentRepository _normalAppointmentContainer;
        private readonly IChecklistAppointmentRepository _checklistAppointmentContainer;
        private readonly IJobRepository _jobContainer;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private Account account;
        private Agenda agenda;
        private Job job;
        private SessionService sessionService;
        private AccountDTO accountDTO = new AccountDTO();

        public AgendaController(IAgendaRepository agendaContainer, IAppointmentRepository appointmentContainer, INormalAppointmentRepository normalAppointmentContainer, IChecklistAppointmentRepository checklistAppointmentContainer, IJobRepository jobContainer, IHttpContextAccessor httpContextAccessor)
        {
            _agendaContainer = agendaContainer;
            _appointmentContainer = appointmentContainer;
            _normalAppointmentContainer = normalAppointmentContainer;
            _checklistAppointmentContainer = checklistAppointmentContainer;
            _jobContainer = jobContainer;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public IActionResult Index()
        {
            sessionService = new SessionService(_httpContextAccessor);

            if (sessionService.CheckSessionValue())
            {
                accountDTO.AccountID = HttpContext.Session.GetInt32("accountID").Value;

                agenda = new Agenda(accountDTO, _appointmentContainer);
                List<AppointmentDTO> appointments = agenda.RetrieveAppointments();

                job = new Job(_jobContainer, _agendaContainer, _appointmentContainer);
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
        public IActionResult AddAgenda()
        {
            sessionService = new SessionService(_httpContextAccessor);

            if (sessionService.CheckSessionValue())
            {
                AgendaViewModel viewModel = new AgendaViewModel();
                return View(viewModel);
            }
            else
            {
                return RedirectToAction("Index", "Account");
            }
        }

        [HttpPost]
        public IActionResult AddAgenda(AgendaViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                if (viewModel.AgendaName == "Bijbaan" || viewModel.AgendaName == "bijbaan")
                {
                    ModelState.AddModelError("", "Deze agendanaam mag niet gebruikt worden.");
                    return View(viewModel);
                }

                AgendaDTO agendaDTO = new AgendaDTO();
                agendaDTO.AgendaName = viewModel.AgendaName;
                agendaDTO.AgendaColor = viewModel.AgendaColor;
                agendaDTO.NotificationType = viewModel.NotificationType;

                accountDTO.AccountID = HttpContext.Session.GetInt32("accountID").Value;
                account = new Account(accountDTO, _agendaContainer);
                account.CreateAgenda(agendaDTO);
                return RedirectToAction("Index", "Agenda");
            }
            else
            {
                return View(viewModel);
            }
        }

        [HttpPost]
        public IActionResult DeleteAgenda([FromBody] AgendaModel agendaModel)
        {
            accountDTO.AccountID = HttpContext.Session.GetInt32("accountID").Value;
            account = new Account(accountDTO, _agendaContainer);
            account.DeleteAgenda(agendaModel.AgendaID);
            return Ok();
        }

        [HttpPatch]
        [Route("Agenda/ChangeTaskStatus/{taskID}")]
        public IActionResult ChangeTaskStatus(int taskID)
        {
            ChecklistAppointment checklistAppointment = new ChecklistAppointment(_checklistAppointmentContainer);
            checklistAppointment.ChangeTaskStatus(taskID);
            return Ok();
        }

        [HttpGet]
        [Route("Agenda/AppointmentExtra/{appointmentID}")]
        public IActionResult AppointmentExtra(int appointmentID)
        {
            NormalAppointment normalAppointment = new NormalAppointment(_normalAppointmentContainer);

            string description = normalAppointment.RetrieveDescription(appointmentID);

            if (description == "")
            {
                ChecklistAppointment checklistAppointment = new ChecklistAppointment(_checklistAppointmentContainer);
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