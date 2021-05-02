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
using TimeMate.Services;
using System.Text.RegularExpressions;
using BusinessLogicLayer;

namespace TimeMate.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountContainer;
        private readonly IAgendaRepository _agendaContainer;
        private readonly IJobRepository _jobContainer;
        private readonly ISender _senderContainer;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private Account account;
        private SessionService sessionService;
        private AccountDTO accountDTO;

        public AccountController(IAccountRepository accountContainer, IAgendaRepository agendaContainer, IJobRepository jobContainer, ISender senderContainer, IHttpContextAccessor httpContextAccessor)
        {
            _accountContainer = accountContainer;
            _agendaContainer = agendaContainer;
            _jobContainer = jobContainer;
            _senderContainer = senderContainer;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public IActionResult Index()
        {
            sessionService = new SessionService(_httpContextAccessor);
            
            if (sessionService.CheckSessionValue())
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
                string[] result = account.LoggingIn();

                if (result == null || result[1] == null)
                {
                    ModelState.AddModelError("", "Verkeerd mailadres en/of wachtwoord.");
                    return View(viewModel);
                }
                else
                {
                    HttpContext.Session.SetInt32("accountID", Convert.ToInt32(result[0]));
                    HttpContext.Session.SetString("firstName", (result[1]));
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
            sessionService = new SessionService(_httpContextAccessor);

            if (sessionService.CheckSessionValue())
            {
                return RedirectToAction("Index", "Agenda");
            }
            else
            {
                RegisterViewModel registerViewModel = new RegisterViewModel();
                return View(registerViewModel);
            }
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                string specialCharacterValidate = @"[~`!@#$%^&*()+=|\\{}':;.,<>/?[\]""_-]";

                if (!viewModel.Password.Any(char.IsUpper))
                {
                    ModelState.AddModelError("", "Het wachtwoord moet een hoofdletter bevatten.");
                    return View(viewModel);
                }
                else if (!Regex.IsMatch(viewModel.Password, specialCharacterValidate))
                {
                    ModelState.AddModelError("", "Het wachtwoord moet een speciale karakter bevatten.");
                    return View(viewModel);
                }
                else if (!viewModel.Password.Any(char.IsDigit))
                {
                    ModelState.AddModelError("", "Het wachtwoord moet een cijfer bevatten.");
                    return View(viewModel);
                }
                accountDTO = new AccountDTO();
                accountDTO.FirstName = viewModel.FirstName;
                accountDTO.Mail = viewModel.Mail;
                accountDTO.Password = viewModel.Password;
                accountDTO.JobCount = viewModel.JobAmount;

                if (viewModel.Job1HourlyWage != null)
                {
                    accountDTO.JobHourlyWage.Add(Convert.ToDouble(viewModel.Job1HourlyWage));
                    accountDTO.JobDayType.Add(viewModel.Job1DayType);
                }
                if (viewModel.Job2HourlyWage != null)
                {
                    accountDTO.JobHourlyWage.Add(Convert.ToDouble(viewModel.Job2HourlyWage));
                    accountDTO.JobDayType.Add(viewModel.Job2DayType);
                }

                account = new Account(accountDTO, _accountContainer, _agendaContainer, _jobContainer, _senderContainer);
                string[] result = account.NewAccountInputValidation();

                if (result[1] == null)
                {
                    ModelState.AddModelError("", result[0]);
                    return View(viewModel);
                }
                else
                {
                    HttpContext.Session.SetInt32("accountID", Convert.ToInt32(result[0]));
                    HttpContext.Session.SetString("firstName", (result[1]));
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
            sessionService = new SessionService(_httpContextAccessor);

            if (sessionService.CheckSessionValue())
            {
                accountDTO = new AccountDTO();
                accountDTO.AccountID = HttpContext.Session.GetInt32("accountID").Value;

                account = new Account(accountDTO, _agendaContainer);
                List<AgendaDTO> viewModel = account.RetrieveAgendas();
                return View(viewModel);
            }
            else
            {
                return RedirectToAction("Index", "Account");
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Account");
        }
    }
}