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
                account = new Account(accountDTO, _agendaContainer);
                viewModel.AppointmentViewModel.AgendaDTO = account.RetrieveAgendas();
                return View(viewModel);
            }
            else
            {
                return RedirectToAction("Index", "Account");
            }
        }

        [HttpPost]
        public IActionResult Index(string json)
        {
            var appointment = JsonConvert.DeserializeObject<List<string>>(json);

            AppointmentDTO appointmentDTO = new AppointmentDTO();
            appointmentDTO.AppointmentName = appointment[0];
            appointmentDTO.StartDate = Convert.ToDateTime(appointment[1]) + TimeSpan.Parse(appointment[2]);
            appointmentDTO.EndDate = Convert.ToDateTime(appointment[3]) + TimeSpan.Parse(appointment[4]);
            appointmentDTO.AgendaName = appointment[5];
            appointmentDTO.AgendaID = Convert.ToInt32(appointment[6]);

            for (int i = 7; i < appointment.Count; i++)
            {
                if (appointment[i] != "")
                {
                    ChecklistDTO checklistDTO = new ChecklistDTO() { TaskName = appointment[i] };
                    appointmentDTO.ChecklistDTOs.Add(checklistDTO);
                }
            }

            agenda = new Agenda(accountDTO, _appointmentContainer, _checklistAppointmentContainer);
            agenda.CreateChecklistAppointment(appointmentDTO);
            return Ok();
        }
    }
}