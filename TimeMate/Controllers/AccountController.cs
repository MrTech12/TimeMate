using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TimeMate.Models;
using DataAccessLayer.DTO;
using BusinessLogicLayer.Logic;
using DataAccessLayer.Contexts;
using Microsoft.AspNetCore.Http;

namespace TimeMate.Controllers
{
    public class AccountController : Controller
    {
        Account accountLogic;
        AccountDTO accountDTO;

        [HttpGet]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("accountID") != null)
            {
                return RedirectToAction("Index", "Agenda");
            }
            else
            {
                LogInViewModel loginViewModel = new LogInViewModel();
                return View(loginViewModel);
            }
        }

        [HttpPost]
        public IActionResult Index(LogInViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                accountDTO = new AccountDTO();
                accountDTO.MailAddress = viewModel.Mail;
                accountDTO.Password = viewModel.Password;

                accountLogic = new Account(accountDTO, new SQLAccountContext(), new SQLAgendaContext());
                string result = accountLogic.UserLogsIn();

                if (result != null)
                {
                    ModelState.AddModelError("", result);
                    return View(viewModel);
                }
                else
                {
                    accountDTO.AccountID = accountLogic.GetActiveAccountID(accountDTO.MailAddress);
                    HttpContext.Session.SetInt32("accountID", accountDTO.AccountID);
                    return RedirectToAction("Index", "Agenda");
                }
            }
            else
            {
                return View(viewModel);
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            RegisterViewModel registerViewModel = new RegisterViewModel();
            return View(registerViewModel);
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                accountDTO = new AccountDTO();
                accountDTO.FirstName = viewModel.FirstName;
                accountDTO.MailAddress = viewModel.Mail;
                accountDTO.Password = viewModel.Password;
                accountDTO.JobCount = viewModel.JobAmount;

                accountDTO.JobHourlyWage.Add(Convert.ToDouble(viewModel.job1Wage));
                accountDTO.JobDayType.Add(viewModel.job1DayType);
                accountDTO.JobHourlyWage.Add(Convert.ToDouble(viewModel.job2Wage));
                accountDTO.JobDayType.Add(viewModel.job2DayType);
                accountDTO.AllocatedHours = Convert.ToDouble(viewModel.AllocatedHours);

                accountLogic = new Account(accountDTO, new SQLAccountContext(), new SQLAgendaContext());

                string result = accountLogic.NewAccountValidation();

                if (result != null)
                {
                    ModelState.AddModelError("", result);
                    return View(viewModel);
                }
                else
                {
                    accountDTO.AccountID = accountLogic.GetActiveAccountID(accountDTO.MailAddress);
                    HttpContext.Session.SetInt32("accountID", accountDTO.AccountID);
                    return RedirectToAction("Index", "Agenda");
                }
            }
            else
            {
                return View(viewModel);
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("accountID");
            return RedirectToAction("Index", "Account");
        }
    }
}