﻿using System;
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
            if (HttpContext.Session.GetInt32("accountID") != null)
            {
                accountDTO.AccountID = HttpContext.Session.GetInt32("accountID").Value;

                agenda = new Agenda(accountDTO, _agendaContext, _appointmentContext);
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
            ChecklistDTO checklist = new ChecklistDTO();
            checklist.TaskID = JsonConvert.DeserializeObject<int>(json);

            AppointmentDTO appointmentDTO = new AppointmentDTO();
            ChecklistAppointment checklistAppointment = new ChecklistAppointment(appointmentDTO, _checklistAppointmentContext);
            checklistAppointment.ChangeTaskStatus(checklist.TaskID);
            return Ok();
        }

        [HttpGet]
        public IActionResult RetrieveAppointmentExtra(string json)
        {
            int appointmentID = JsonConvert.DeserializeObject<int>(json);

            AppointmentDTO appointmentDTO = new AppointmentDTO() {AppointmentID = appointmentID };
            NormalAppointment normalAppointment = new NormalAppointment(appointmentDTO, _normalAppointmentContext);
            ChecklistAppointment checklistAppointment = new ChecklistAppointment(appointmentDTO, _checklistAppointmentContext);

            string description = normalAppointment.RetrieveDescription(appointmentDTO);

            if (description == "")
            {
                var tasks = checklistAppointment.RetrieveTasks(appointmentDTO);
                return Json(tasks);
            }
            else
            {
                return Json(description);
            }
        }
    }
}