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
    public class NormalAppointmentController : Controller
    {
        private readonly IAgendaContainer _agendaContainer;
        private readonly IAppointmentContainer _appointmentContainer;
        private readonly INormalAppointmentContainer _normalAppointmentContainer;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private AccountDTO accountDTO = new AccountDTO();
        private Account account;
        private Agenda agenda;
        private SessionService sessionService;
        bool sessionHasValue;

        public NormalAppointmentController(IAgendaContainer agendaContainer, IAppointmentContainer appointmentContainer, INormalAppointmentContainer normalAppointmentContainer, IHttpContextAccessor httpContextAccessor)
        {
            _agendaContainer = agendaContainer;
            _appointmentContainer = appointmentContainer;
            _normalAppointmentContainer = normalAppointmentContainer;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public IActionResult Index()
        {
            sessionService = new SessionService(_httpContextAccessor);
            sessionHasValue = sessionService.CheckSessionValue();

            if (sessionHasValue)
            {
                NormalAppointmentViewModel viewModel = new NormalAppointmentViewModel();
                account = new Account(accountDTO, _agendaContainer);
                ViewBag.agendaList = account.RetrieveAgendas();
                return View(viewModel);
            }
            else
            {
                return RedirectToAction("Index", "Account");
            }
        }

        [HttpPost]
        public IActionResult Index(NormalAppointmentViewModel viewModel)
        {
            AppointmentDTO appointmentDTO = new AppointmentDTO();
            appointmentDTO.AppointmentName = viewModel.AppointmentViewModel.AppointmentName;
            appointmentDTO.StartDate = viewModel.AppointmentViewModel.StartDate + viewModel.AppointmentViewModel.StartTime;
            appointmentDTO.EndDate = viewModel.AppointmentViewModel.EndDate + viewModel.AppointmentViewModel.EndTime;
            appointmentDTO.AgendaID = viewModel.AppointmentViewModel.AgendaID;

            if (viewModel.Description != null)
            {
                string description = viewModel.Description.Replace("<span class=\"bolding\">", "<b>").Replace("</span>", "</b>")
                    .Replace("<span class=\"normal-text\">", "</b>").Replace("<script>", "");
                appointmentDTO.DescriptionDTO.Description = description;
            }
            else
            {
                appointmentDTO.DescriptionDTO.Description = null;
            }

            agenda = new Agenda(_appointmentContainer, _normalAppointmentContainer);
            agenda.CreateNormalAppointment(appointmentDTO);
            return RedirectToAction("Index", "Agenda");
        }
    }
}