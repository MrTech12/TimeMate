using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicLayer.Logic;
using DataAccessLayer.Contexts;
using DataAccessLayer.DTO;
using Microsoft.AspNetCore.Mvc;
using TimeMate.Models;

namespace TimeMate.Controllers
{
    public class AgendaController : Controller
    {
        AccountDTO accountDTO = new AccountDTO();

        [HttpGet]
        public IActionResult Index(int id)
        {
            accountDTO.AccountID = id;
            AgendaLogic agendaLogic = new AgendaLogic(accountDTO, new SQLAgendaContext(new SQLDatabaseContext()));
            List<AppointmentDTO> appointmentModelForView = new List<AppointmentDTO>();

            appointmentModelForView = agendaLogic.RetrieveAppointments();

            return View(appointmentModelForView);
        }

        [HttpGet]
        public IActionResult AddAgenda()
        {
            AgendaViewModel agendaViewModel = new AgendaViewModel();
            return View(agendaViewModel);
        }

        [HttpPost]
        public IActionResult AddAgenda(AgendaViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                AgendaDTO agendaDTO = new AgendaDTO() { AgendaName = viewModel.Name, AgendaColor = viewModel.Color, Notification = viewModel.NotificationType };
                AccountLogic accountLogic = new AccountLogic(accountDTO, new SQLAccountContext(new SQLDatabaseContext()), new SQLAgendaContext(new SQLDatabaseContext()));
                accountLogic.CreateAgenda(agendaDTO);
                return RedirectToAction("Index", "Agenda", new { id = accountDTO.AccountID });
            }
            else
            {
                return View(viewModel);
            }
        }
    }
}