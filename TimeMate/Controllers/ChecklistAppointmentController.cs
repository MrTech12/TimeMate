﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicLayer.Logic;
using Core.DTOs;
using Core.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimeMate.Models;
using TimeMate.Services;

namespace TimeMate.Controllers
{
    public class ChecklistAppointmentController : Controller
    {
        private readonly IAgendaRepository _agendaContainer;
        private readonly IAppointmentRepository _appointmentContainer;
        private readonly IChecklistAppointmentRepository _checklistAppointmentContainer;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private ChecklistAppointment checklistAppointment;
        private Agenda agenda;
        private SessionService sessionService;
        private AccountDTO accountDTO = new AccountDTO();

        public ChecklistAppointmentController(IAgendaRepository agendaContainer, IAppointmentRepository appointmentContainer, IChecklistAppointmentRepository checklistAppointmentContainer, IHttpContextAccessor httpContextAccessor)
        {
            _agendaContainer = agendaContainer;
            _appointmentContainer = appointmentContainer;
            _checklistAppointmentContainer = checklistAppointmentContainer;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public IActionResult Index()
        {
            sessionService = new SessionService(_httpContextAccessor);

            if (sessionService.CheckSessionValue())
            {
                ChecklistAppointmentViewModel viewModel = new ChecklistAppointmentViewModel();
                accountDTO.AccountID = HttpContext.Session.GetInt32("accountID").Value;

                agenda = new Agenda(accountDTO, _agendaContainer);
                ViewBag.agendaList = agenda.RetrieveAgendas();

                if (ViewBag.agendaList.Count == 0)
                {
                    return RedirectToAction("AddAgenda", "Agenda");
                }
                else
                {
                    return View(viewModel);
                }
            }
            else
            {
                return RedirectToAction("Index", "Account");
            }
        }

        [HttpPost]
        public IActionResult Index(ChecklistAppointmentViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                AppointmentDTO appointmentDTO = new AppointmentDTO();
                appointmentDTO.AppointmentName = viewModel.AppointmentViewModel.AppointmentName;
                appointmentDTO.StartDate = viewModel.AppointmentViewModel.StartDate + viewModel.AppointmentViewModel.StartTime;
                appointmentDTO.EndDate = viewModel.AppointmentViewModel.EndDate + viewModel.AppointmentViewModel.EndTime;
                appointmentDTO.AgendaID = viewModel.AppointmentViewModel.AgendaID;

                if (viewModel.Task != null)
                {
                    foreach (var item in viewModel.Task)
                    {
                        if (item != null)
                        {
                            TaskDTO taskDTO = new TaskDTO() { TaskName = item };
                            appointmentDTO.TaskList.Add(taskDTO);
                        }
                    }
                }

                checklistAppointment = new ChecklistAppointment(_appointmentContainer, _checklistAppointmentContainer);
                checklistAppointment.AddChecklistAppointment(appointmentDTO);
                return RedirectToAction("Index", "AgendaView");
            }
            else
            {
                return View(viewModel);
            }
        }

        [HttpPatch]
        [Route("ChecklistAppointment/TaskStatus/{taskID}")]
        public IActionResult ChangeTaskStatus(int taskID)
        {
            checklistAppointment = new ChecklistAppointment(_checklistAppointmentContainer);
            checklistAppointment.ChangeTaskStatus(taskID);
            return Ok();
        }
    }
}