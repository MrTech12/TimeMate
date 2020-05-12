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
        [HttpGet]
        public IActionResult Index()
        {
            NAppointmentViewModel nAppointmentViewModel = new NAppointmentViewModel();
            account = new Account(accountDTO, new SQLAccountContext(), new SQLAgendaContext());
            nAppointmentViewModel.AgendaName = account.GetAgendaNames();
            return View(nAppointmentViewModel);
        }

        [HttpPost]
        public IActionResult Index(NAppointmentViewModel nAppointmentViewModel)
        {
            return View();
        }
    }
}