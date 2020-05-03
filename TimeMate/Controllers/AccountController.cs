using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TimeMate.Models;
using DataAccessLayer.DTO;
using DataAccessLayer.Interfaces;
using BusinessLogicLayer.Logic;
using DataAccessLayer.Contexts;

namespace TimeMate.Controllers
{
    public class AccountController : Controller
    {
        AccountLogic accountLogic;
        AccountDTO accountDTO;


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
                accountDTO = new AccountDTO();
                accountDTO.MailAddress = loginViewModel.Mail;
                accountDTO.Password = loginViewModel.Password;

                accountLogic = new AccountLogic(accountDTO, new SQLAccountContext(), new SQLAgendaContext());
                string result = accountLogic.UserLogsIn();

                if (result != null)
                {
                    accountDTO = new AccountDTO();
                    accountDTO.AccountID = accountLogic.GetActiveAccountID(accountDTO.MailAddress);
                    AgendaController agendaController = new AgendaController(accountDTO);
                    return RedirectToAction("Index", "Agenda");
                }
                else
                {
                    ModelState.AddModelError("", result);
                    return View(loginViewModel);
                }
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