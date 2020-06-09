using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicLayer.Logic;
using DataAccessLayer.Contexts;
using DataAccessLayer.DTO;
using DataAccessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TimeMate.Models;

namespace TimeMate.Controllers
{
    public class ChecklistAppointmentController : Controller
    {
        private AccountDTO accountDTO = new AccountDTO();
        private Account account;
        private Agenda agenda;

        private readonly IAccountContainer _accountContext;
        private readonly IAgendaContainer _agendaContext;
        private readonly IAppointmentContainer _appointmentContext;
        private readonly IChecklistAppointmentContainer _checklistAppointmentContext;

        public ChecklistAppointmentController(IAccountContainer accountContext, IAgendaContainer agendaContext, IAppointmentContainer appointmentContext, IChecklistAppointmentContainer checklistAppointmentContext)
        {
            _accountContext = accountContext;
            _agendaContext = agendaContext;
            _appointmentContext = appointmentContext;
            _checklistAppointmentContext = checklistAppointmentContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ChecklistAppointmentViewModel viewModel = new ChecklistAppointmentViewModel();
            account = new Account(accountDTO, _accountContext, _agendaContext);
            viewModel.AppointmentViewModel.AgendaDTO = account.RetrieveAgendas();
            return View(viewModel);
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

            agenda = new Agenda(accountDTO, _agendaContext, _appointmentContext, _checklistAppointmentContext);
            agenda.CreateChecklistAppointment(appointmentDTO);
            return Ok();
        }
    }
}