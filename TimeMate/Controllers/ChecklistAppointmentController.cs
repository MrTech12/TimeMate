using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicLayer.Logic;
using DataAccessLayer.DTO;
using DataAccessLayer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimeMate.Models;
using TimeMate.Services;

namespace TimeMate.Controllers
{
    public class ChecklistAppointmentController : Controller
    {
        private readonly IAgendaContainer _agendaContainer;
        private readonly IAppointmentContainer _appointmentContainer;
        private readonly IChecklistAppointmentContainer _checklistAppointmentContainer;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private AccountDTO accountDTO = new AccountDTO();
        private Account account;
        private Agenda agenda;
        private SessionService sessionService;
        bool sessionHasValue;

        public ChecklistAppointmentController(IAgendaContainer agendaContainer, IAppointmentContainer appointmentContainer, IChecklistAppointmentContainer checklistAppointmentContainer, IHttpContextAccessor httpContextAccessor)
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
            sessionHasValue = sessionService.CheckSessionValue();

            if (sessionHasValue)
            {
                ChecklistAppointmentViewModel viewModel = new ChecklistAppointmentViewModel();
                accountDTO.AccountID = HttpContext.Session.GetInt32("accountID").Value;

                account = new Account(accountDTO, _agendaContainer);
                ViewBag.agendaList = account.RetrieveAgendas();

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

                foreach (var item in viewModel.Task)
                {
                    if (item != null)
                    {
                        ChecklistDTO checklistDTO = new ChecklistDTO() { TaskName = item };
                        appointmentDTO.ChecklistDTOs.Add(checklistDTO);
                    }
                }

                agenda = new Agenda(_appointmentContainer, _checklistAppointmentContainer);
                agenda.CreateChecklistAppointment(appointmentDTO);
                return RedirectToAction("Index", "Agenda");
            }
            else
            {
                return View(viewModel);
            }
        }
    }
}