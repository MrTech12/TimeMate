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
                appointmentDTO.AppointmentName = viewModel.AppointmentViewModel.Name;
                appointmentDTO.StartDate = viewModel.AppointmentViewModel.StartDate + viewModel.AppointmentViewModel.StartTime;
                appointmentDTO.EndDate = viewModel.AppointmentViewModel.EndDate + viewModel.AppointmentViewModel.EndTime;
                appointmentDTO.AgendaName = viewModel.AgendaName[0];
                appointmentDTO.Description = viewModel.Description;

                agenda = new Agenda(accountDTO, new SQLAgendaContext(), new SQLAppointmentContext(), new SQLNormalAppointmentContext());

                agenda.CreateNAppointment(appointmentDTO, appointmentDTO.AgendaName);

                return RedirectToAction("Index", "Agenda");
            }
            else
            {
                return View(viewModel);
            }

        }
    }
}