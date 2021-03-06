using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TimeMate.Models;
using BusinessLogicLayer.Logic;
using Microsoft.AspNetCore.Http;
using TimeMate.Services;
using System.Text.RegularExpressions;
using Core.Repositories;
using Core.DTOs;
using Core.Services;

namespace TimeMate.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IAgendaRepository _agendaRepository;
        private readonly IJobRepository _jobRepository;
        private readonly ISender _sender;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private AccountService accountService;
        private AgendaService agendaService;
        private JobService jobService;
        private SessionService sessionService;
        private AccountDTO accountDTO;

        public AccountController(IAccountRepository accountRepository, IAgendaRepository agendaRepository, IJobRepository jobRepository, ISender senderRepository, IHttpContextAccessor httpContextAccessor)
        {
            _accountRepository = accountRepository;
            _agendaRepository = agendaRepository;
            _jobRepository = jobRepository;
            _sender = senderRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public IActionResult Index()
        {
            sessionService = new SessionService(_httpContextAccessor);
            
            if (sessionService.CheckSessionValue())
            {
                return RedirectToAction("Index", "AgendaView");
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

                accountService = new AccountService(accountDTO, _accountRepository);
                string[] response = accountService.LoggingIn();

                if (response[1] == null)
                {
                    ModelState.AddModelError("", "Verkeerd mailadres en/of wachtwoord.");
                    return View(viewModel);
                }
                else
                {
                    HttpContext.Session.SetInt32("accountID", Convert.ToInt32(response[0]));
                    HttpContext.Session.SetString("firstName", (response[1]));
                    return RedirectToAction("Index", "AgendaView");
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
                return RedirectToAction("Index", "AgendaView");
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
            if (ModelState.IsValid) // TODO: find how to structure this differently. This is a very long method.
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

                if (viewModel.JobHourlyWage != null)
                {
                    for (int i = 0; i < viewModel.JobHourlyWage.Count; i++)
                    {
                        accountDTO.JobHourlyWage.Add(Convert.ToDouble(viewModel.JobHourlyWage[i]));
                        accountDTO.JobDayType.Add(viewModel.JobDayType[i]);
                    }
                }

                accountService = new AccountService(accountDTO, _accountRepository, _sender);
                string mailCheck = accountService.CheckExistingMail();

                if (mailCheck != null)
                {
                    ModelState.AddModelError("", mailCheck);
                    return View(viewModel);
                }
                else if (mailCheck == null)
                {
                    accountService.CreateAccount();
                }

                if(accountDTO.JobHourlyWage.Count != 0)
                {
                    agendaService = new AgendaService(accountService.AccountDTO, _agendaRepository);
                    AgendaDTO workAgendaDTO = new AgendaDTO() { AgendaName = viewModel.AgendaName, AgendaColor = viewModel.AgendaColor, IsWorkAgenda = true };
                    agendaService.AddAgenda(workAgendaDTO);

                    jobService = new JobService(_jobRepository);
                    jobService.AddPayDetails(accountDTO);
                }

                HttpContext.Session.SetInt32("accountID", Convert.ToInt32(accountService.AccountDTO.AccountID));
                HttpContext.Session.SetString("firstName", accountDTO.FirstName);
                return RedirectToAction("Index", "AgendaView");
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
                accountDTO = new AccountDTO() { AccountID = HttpContext.Session.GetInt32("accountID").Value };
                agendaService = new AgendaService(accountDTO, _agendaRepository);
                List<AgendaDTO> viewModel = agendaService.RetrieveAgendas();
                if (viewModel.Count == 0)
                {
                    return RedirectToAction("AddAgenda", "Agenda");
                }
                else
                {
                    return View(viewModel);
                }
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