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

                accountLogic = new AccountLogic(accountDTO, new SQLAccountContext(new SQLDatabaseContext()), new SQLAgendaContext(new SQLDatabaseContext()));
                string result = accountLogic.UserLogsIn();

                if (result != null)
                {
                    ModelState.AddModelError("", result);
                    return View(loginViewModel);
                }
                else
                {
                    accountDTO.AccountID = accountLogic.GetActiveAccountID(accountDTO.MailAddress);
                    return RedirectToAction("Index", "Agenda", new {id = accountDTO.AccountID});
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
                accountDTO = new AccountDTO();
                accountDTO.FirstName = registerViewModel.FirstName;
                accountDTO.MailAddress = registerViewModel.Mail;
                accountDTO.Password = registerViewModel.Password;
                accountDTO.JobCount = registerViewModel.JobAmount;

                accountDTO.JobHourlyWage.Add(Convert.ToDouble(registerViewModel.job1Wage));
                accountDTO.JobDayType.Add(registerViewModel.job1DayType);
                accountDTO.JobHourlyWage.Add(Convert.ToDouble(registerViewModel.job2Wage));
                accountDTO.JobDayType.Add(registerViewModel.job2DayType);
                accountDTO.AllocatedHours = Convert.ToDouble(registerViewModel.AllocatedHours);

                accountLogic = new AccountLogic(accountDTO, new SQLAccountContext(new SQLDatabaseContext()), new SQLAgendaContext(new SQLDatabaseContext()));

                string result = accountLogic.NewAccountValidation();

                if (result != null)
                {
                    ModelState.AddModelError("", result);
                    return View(registerViewModel);
                }
                else
                {
                    accountDTO.AccountID = accountLogic.GetActiveAccountID(accountDTO.MailAddress);
                    return RedirectToAction("Index", "Agenda", new { id = accountDTO.AccountID });
                }
            }
            else
            {
                return View(registerViewModel);
            }
        }
    }
}