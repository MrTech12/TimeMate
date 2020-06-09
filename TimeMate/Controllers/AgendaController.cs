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

namespace TimeMate.Controllers
{
    public class AgendaController : Controller
    {
        private readonly IAgendaContainer _agendaContext;
        private readonly IAppointmentContainer _appointmentContext;
        private readonly INormalAppointmentContainer _normalAppointmentContext;
        private readonly IChecklistAppointmentContainer _checklistAppointmentContext;

        private AccountDTO accountDTO = new AccountDTO();
        private Agenda agenda;
        private Account account;

        public AgendaController(IAgendaContainer agendaContext, IAppointmentContainer appointmentContext, INormalAppointmentContainer normalAppointmentContext, IChecklistAppointmentContainer checklistAppointmentContext)
        {
            _agendaContext = agendaContext;
            _appointmentContext = appointmentContext;
            _normalAppointmentContext = normalAppointmentContext;
            _checklistAppointmentContext = checklistAppointmentContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("accountID").HasValue)
            {
                accountDTO.AccountID = HttpContext.Session.GetInt32("accountID").Value;

                agenda = new Agenda(accountDTO, _appointmentContext);
                List<AppointmentDTO> appointments = agenda.RetrieveAppointments();
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
            AgendaViewModel viewModel = new AgendaViewModel();
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult AddAgenda(AgendaViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                AgendaDTO agendaDTO = new AgendaDTO();
                agendaDTO.AgendaName = viewModel.AgendaName;
                agendaDTO.AgendaColor = viewModel.AgendaColor;
                agendaDTO.NotificationType = viewModel.NotificationType;

                account = new Account(accountDTO, _agendaContext);
                account.CreateAgenda(agendaDTO);
                return RedirectToAction("Index", "Agenda");
            }
            else
            {
                return View(viewModel);
            }
        }

        [HttpPost]
        public IActionResult DeleteAgenda(string json)
        {
            int agendaID = JsonConvert.DeserializeObject<int>(json);
            
            account = new Account(accountDTO, _agendaContext);
            account.DeleteAgenda(agendaID);
            return Ok();
        }

        [HttpGet]
        public IActionResult ChangeTaskStatus(string json)
        {
            int taskID = JsonConvert.DeserializeObject<int>(json);

            ChecklistAppointment checklistAppointment = new ChecklistAppointment(_checklistAppointmentContext);
            checklistAppointment.ChangeTaskStatus(taskID);
            return Ok();
        }

        [HttpGet]
        public IActionResult RetrieveAppointmentExtra(string json)
        {
            int appointmentID = JsonConvert.DeserializeObject<int>(json);

            NormalAppointment normalAppointment = new NormalAppointment(_normalAppointmentContext);

            string description = normalAppointment.RetrieveDescription(appointmentID);

            if (description == "")
            {
                ChecklistAppointment checklistAppointment = new ChecklistAppointment(_checklistAppointmentContext);
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