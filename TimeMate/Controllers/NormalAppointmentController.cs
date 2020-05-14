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
    public class NormalAppointmentController : Controller
    {
        AccountDTO accountDTO = new AccountDTO();
        Account account;
        Agenda agenda;

        [HttpGet]
        public IActionResult Index()
        {
            NAppointmentViewModel nAppointmentViewModel = new NAppointmentViewModel();
            account = new Account(accountDTO, new SQLAccountContext(), new SQLAgendaContext());
            nAppointmentViewModel.AgendaName = account.GetAgendaNames();
            return View(nAppointmentViewModel);
        }

        [HttpPost]
        public IActionResult Index(NAppointmentViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                AppointmentDTO appointmentDTO = new AppointmentDTO();
                appointmentDTO.AppointmentName = viewModel.Name;
                appointmentDTO.StartDate = viewModel.StartDate + viewModel.StartTime;
                appointmentDTO.EndDate = viewModel.EndDate + viewModel.EndTime;
                appointmentDTO.AgendaName = viewModel.AgendaName[0];
                appointmentDTO.Description = viewModel.Description;

                agenda = new Agenda(accountDTO, new SQLAgendaContext(), new SQLNormalAppointmentContext(), new SQLChecklistAppointmentContext());

                agenda.CreateNAppointment(appointmentDTO, appointmentDTO.AgendaName);

                return RedirectToAction("Index", "Agenda", new { id = accountDTO.AccountID });
            }
            else
            {
                return View(viewModel);
            }

        }
    }
}