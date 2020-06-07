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
        private readonly IAccountContext _accountContext;
        private readonly IAgendaContext _agendaContext;
        private readonly IAppointmentContext _appointmentContext;
        private readonly IChecklistAppointmentContext _checklistAppointmentContext;

        private AccountDTO accountDTO = new AccountDTO();
        private Account account;
        private Agenda agenda;

        public ChecklistAppointmentController(IAccountContext accountContext, IAgendaContext agendaContext, IAppointmentContext appointmentContext, IChecklistAppointmentContext checklistAppointmentContext)
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
            var newAppointment = JsonConvert.DeserializeObject<List<string>>(json);

            AppointmentDTO appointmentDTO = new AppointmentDTO();
            appointmentDTO.AppointmentName = newAppointment[0];
            appointmentDTO.StartDate = Convert.ToDateTime(newAppointment[1]) + TimeSpan.Parse(newAppointment[2]);
            appointmentDTO.EndDate = Convert.ToDateTime(newAppointment[3]) + TimeSpan.Parse(newAppointment[4]);
            appointmentDTO.AgendaName = newAppointment[5];
            appointmentDTO.AgendaID = Convert.ToInt32(newAppointment[6]);

            for (int i = 7; i < newAppointment.Count; i++)
            {
                if (newAppointment[i] != "")
                {
                    ChecklistDTO checklistDTO = new ChecklistDTO() { TaskName = newAppointment[i] };
                    appointmentDTO.ChecklistDTOs.Add(checklistDTO);
                }
            }

            agenda = new Agenda(accountDTO, _agendaContext, _appointmentContext, _checklistAppointmentContext);

            agenda.CreateChecklistAppointment(appointmentDTO);

            return Ok();
        }
    }
}