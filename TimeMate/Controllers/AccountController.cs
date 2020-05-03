using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TimeMate.Models;
using DataAccessLayer.DTO;
using DataAccessLayer.Interfaces;

namespace TimeMate.Controllers
{
    public class AccountController : Controller
    {
        private IAccountContext accountContext;
        private IAgendaContext agendaContext;
        private AccountDTO accountDTO;

        public AccountController(AccountDTO accountDTOInput, IAccountContext accountContextInput, IAgendaContext agendaContextInput)
        {
            this.accountDTO = accountDTOInput;
            this.accountContext = accountContextInput;
            this.agendaContext = agendaContextInput;
        }


        [HttpGet]
        public IActionResult Index()
        {
            LogInViewModel loginViewModel = new LogInViewModel();
            return View(loginViewModel);
        }

        [HttpPost]
        public IActionResult Index(LogInViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index", "Agenda");
            }
            else
            {
                return View(loginViewModel);
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            RegisterViewModel registerViewModel = new RegisterViewModel();
            return View(registerViewModel);
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                return null;
            }
            else
            {
                return View(registerViewModel);
            }
        }
    }
}