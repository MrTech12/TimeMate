using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TimeMate.Models;
using DataAccessLayer.DTO;
using BusinessLogicLayer.Logic;
using Microsoft.AspNetCore.Http;
using DataAccessLayer.Interfaces;

namespace TimeMate.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountContainer _accountContainer;
        private readonly IAgendaContainer _agendaContainer;
        private readonly ISenderContainer _senderContainer;

        private Account account;
        private AccountDTO accountDTO;

        public AccountController(IAccountContainer accountContainer, IAgendaContainer agendaContainer, ISenderContainer senderContainer)
        {
            _accountContainer = accountContainer;
            _agendaContainer = agendaContainer;
            _senderContainer = senderContainer;
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("accountID").HasValue)
            {
                return RedirectToAction("Index", "Agenda");
            }
            else
            {
                LogInViewModel viewModel = new LogInViewModel();
                return View(viewModel);
            }
        }

        [HttpPost]
        public IActionResult Index(LogInViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                accountDTO = new AccountDTO();
                accountDTO.Mail = viewModel.Mail;
                accountDTO.Password = viewModel.Password;

                account = new Account(accountDTO, _accountContainer);
                string result = account.LoggingIn();

                if (!result.All(char.IsDigit))
                {
                    ModelState.AddModelError("", result);
                    return View(viewModel);
                }
                else
                {
                    HttpContext.Session.SetInt32("accountID", Convert.ToInt32(result));
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
                accountDTO.Mail = viewModel.Mail;
                accountDTO.Password = viewModel.Password;
                accountDTO.JobCount = viewModel.JobAmount;

                if (viewModel.Job1HourlyWage != "0.00")
                {
                    accountDTO.JobHourlyWage.Add(Convert.ToDouble(viewModel.Job1HourlyWage));
                    accountDTO.JobDayType.Add(viewModel.Job1DayType);

                }
                else if (viewModel.Job2HourlyWage != "0.00")
                {
                    accountDTO.JobHourlyWage.Add(Convert.ToDouble(viewModel.Job2HourlyWage));
                    accountDTO.JobDayType.Add(viewModel.Job2DayType);
                }

                account = new Account(accountDTO, _accountContainer, _agendaContainer, _senderContainer);

                string result = account.NewAccountValidation();

                if (!result.All(char.IsDigit))
                {
                    ModelState.AddModelError("", result);
                    return View(viewModel);
                }
                else
                {
                    HttpContext.Session.SetInt32("accountID", Convert.ToInt32(result));
                    return RedirectToAction("Index", "Agenda");
                }
            }
            else
            {
                return View(viewModel);
            }
        }

        [HttpGet]
        public IActionResult AccountSettings()
        {
            accountDTO = new AccountDTO();
            accountDTO.AccountID = HttpContext.Session.GetInt32("accountID").Value;

            account = new Account(accountDTO, _agendaContainer);
            List<AgendaDTO> viewModel = account.RetrieveAgendas();
            return View(viewModel);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("accountID");
            return RedirectToAction("Index", "Account");
        }
    }
}