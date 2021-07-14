using System;
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
    public class NormalAppointmentController : Controller
    {
        private readonly IAgendaRepository _agendaRepository;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly INormalAppointmentRepository _normalAppointmentRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private AgendaService agendaService;
        private NormalAppointmentService normalAppointmentService;
        private SessionService sessionService;
        private AccountDTO accountDTO = new AccountDTO();

        public NormalAppointmentController(IAgendaRepository agendaContainer, IAppointmentRepository appointmentContainer, INormalAppointmentRepository normalAppointmentContainer, IHttpContextAccessor httpContextAccessor)
        {
            _agendaRepository = agendaContainer;
            _appointmentRepository = appointmentContainer;
            _normalAppointmentRepository = normalAppointmentContainer;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public IActionResult Index()
        {
            sessionService = new SessionService(_httpContextAccessor);

            if (sessionService.CheckSessionValue())
            {
                NormalAppointmentViewModel viewModel = new NormalAppointmentViewModel();
                accountDTO.AccountID = HttpContext.Session.GetInt32("accountID").Value;

                agendaService = new AgendaService(accountDTO, _agendaRepository);
                ViewBag.agendaList = agendaService.RetrieveAgendas();

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
        public IActionResult Index(NormalAppointmentViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                AppointmentDTO appointmentDTO = new AppointmentDTO();
                appointmentDTO.AppointmentName = viewModel.AppointmentViewModel.AppointmentName;
                appointmentDTO.StartDate = viewModel.AppointmentViewModel.StartDate + viewModel.AppointmentViewModel.StartTime;
                appointmentDTO.EndDate = viewModel.AppointmentViewModel.EndDate + viewModel.AppointmentViewModel.EndTime;
                appointmentDTO.AgendaID = viewModel.AppointmentViewModel.AgendaID;

                if (viewModel.Description != null)
                {
                    appointmentDTO.DescriptionDTO.Description = viewModel.Description;
                }
                else
                {
                    appointmentDTO.DescriptionDTO.Description = null;
                }

                normalAppointmentService = new NormalAppointmentService(_appointmentRepository, _normalAppointmentRepository);
                normalAppointmentService.AddNormalAppointment(appointmentDTO);
                return RedirectToAction("Index", "AgendaView");
            }
            else
            {
                return View(viewModel);
            }  
        }
    }
}