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
    public class ChecklistAppointmentController : Controller
    {
        AccountDTO accountDTO = new AccountDTO();
        Account account;
        Agenda agenda;

        [HttpGet]
        public IActionResult Index()
        {
            CAppointmentViewModel cAppointmentViewModel = new CAppointmentViewModel();
            account = new Account(accountDTO, new SQLAccountContext(), new SQLAgendaContext());
            cAppointmentViewModel.AgendaName = account.GetAgendaNames();
            return View(cAppointmentViewModel);
        }

        [HttpPost]
        public IActionResult Index(CAppointmentViewModel viewModel)
        {
            AppointmentDTO appointmentDTO = new AppointmentDTO();
            appointmentDTO.AppointmentName = viewModel.Name;
            appointmentDTO.StartDate = viewModel.StartDate + viewModel.StartTime;
            appointmentDTO.EndDate = viewModel.EndDate + viewModel.EndTime;
            appointmentDTO.AgendaName = viewModel.AgendaName[0];
            appointmentDTO.ChecklistItemName.AddRange(viewModel.Task);
            //foreach (var item in viewModel.Task)
            //{
            //    appointmentDTO.ChecklistItemName.Add(item);
            //}
            agenda = new Agenda(accountDTO, new SQLAgendaContext(), new SQLNormalAppointmentContext(), new SQLChecklistAppointmentContext());

            agenda.CreateCAppointment(appointmentDTO, appointmentDTO.AgendaName);

            return RedirectToAction("Index", "Agenda", new { id = accountDTO.AccountID });
        }
    }
}