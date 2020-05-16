using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicLayer.Logic;
using DataAccessLayer.Contexts;
using DataAccessLayer.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimeMate.Models;

namespace TimeMate.Controllers
{
    public class AgendaController : Controller
    {
        AccountDTO accountDTO = new AccountDTO();

        [HttpGet]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("accountID") != null)
            {
                var accountID = HttpContext.Session.GetInt32("accountID");
                accountDTO.AccountID = Convert.ToInt32(accountID);
                Agenda agendaLogic = new Agenda(accountDTO, new SQLAgendaContext());
                List<AppointmentDTO> appointmentModelForView = new List<AppointmentDTO>();

                appointmentModelForView = agendaLogic.RetrieveAppointments();

                return View(appointmentModelForView);
            }
            else
            {
                return RedirectToAction("Index", "Account");
            }
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
                Account accountLogic = new Account(accountDTO, new SQLAccountContext(), new SQLAgendaContext());
                accountLogic.CreateAgenda(agendaDTO);
                return RedirectToAction("Index", "Agenda");
            }
            else
            {
                return View(viewModel);
            }
        }
    }
}