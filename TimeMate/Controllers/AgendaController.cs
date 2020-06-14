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
        private readonly IAgendaContainer _agendaContainer;
        private readonly IAppointmentContainer _appointmentContainer;
        private readonly INormalAppointmentContainer _normalAppointmentContainer;
        private readonly IChecklistAppointmentContainer _checklistAppointmentContainer;
        private readonly IJobContainer _jobContainer;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private AccountDTO accountDTO = new AccountDTO();
        private Agenda agenda;
        private Account account;
        private Job job;
        private SessionService sessionService;
        bool sessionHasValue;

        public AgendaController(IAgendaContainer agendaContainer, IAppointmentContainer appointmentContainer, INormalAppointmentContainer normalAppointmentContainer, IChecklistAppointmentContainer checklistAppointmentContainer, IJobContainer jobContainer, IHttpContextAccessor httpContextAccessor)
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
            sessionHasValue = sessionService.CheckSessionValue();

            if (sessionHasValue)
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
            sessionHasValue = sessionService.CheckSessionValue();

            if (sessionHasValue)
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

        [HttpGet]
        public IActionResult DeleteAgenda(string json)
        {
            int agendaID = JsonConvert.DeserializeObject<int>(json);

            accountDTO.AccountID = HttpContext.Session.GetInt32("accountID").Value;
            account = new Account(accountDTO, _agendaContainer);
            account.DeleteAgenda(agendaID);
            return Ok();
        }

        [HttpGet]
        public IActionResult ChangeTaskStatus(string json)
        {
            int taskID = JsonConvert.DeserializeObject<int>(json);

            ChecklistAppointment checklistAppointment = new ChecklistAppointment(_checklistAppointmentContainer);
            checklistAppointment.ChangeTaskStatus(taskID);
            return Ok();
        }

        [HttpGet]
        public IActionResult RetrieveAppointmentExtra(string json)
        {
            int appointmentID = JsonConvert.DeserializeObject<int>(json);

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