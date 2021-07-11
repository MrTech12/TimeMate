using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicLayer.Logic;
using Core.DTOs;
using Core.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TimeMate.Models;
using TimeMate.Services;

namespace TimeMate.Controllers
{
    public class AgendaController : Controller
    {
        private readonly IAgendaRepository _agendaRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private Agenda agenda;
        private SessionService sessionService;
        private AccountDTO accountDTO = new AccountDTO();

        public AgendaController(IAgendaRepository agendaContainer, IHttpContextAccessor httpContextAccessor)
        {
            _agendaRepository = agendaContainer;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public IActionResult AddAgenda()
        {
            sessionService = new SessionService(_httpContextAccessor);

            if (sessionService.CheckSessionValue())
            {
                AgendaViewModel viewModel = new AgendaViewModel();
                return View(viewModel);
            }
            else
            {
                return RedirectToAction("Index", "Account");
            }
        }

        [HttpPost]
        public IActionResult AddAgenda(AgendaViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                AgendaDTO agendaDTO = new AgendaDTO();
                agendaDTO.AgendaName = viewModel.AgendaName;
                agendaDTO.AgendaColor = viewModel.AgendaColor;
                agendaDTO.IsWorkAgenda = false;

                accountDTO.AccountID = HttpContext.Session.GetInt32("accountID").Value;
                agenda = new Agenda(accountDTO, _agendaRepository);
                agenda.AddAgenda(agendaDTO);
                return RedirectToAction("Index", "AgendaView");
            }
            else
            {
                return View(viewModel);
            }
        }

        [HttpDelete]
        public IActionResult DeleteAgenda([FromBody] AgendaBodyModel bodyModel)
        {
            accountDTO.AccountID = HttpContext.Session.GetInt32("accountID").Value;
            agenda = new Agenda(accountDTO, _agendaRepository);
            agenda.DeleteAgenda(bodyModel.AgendaID);
            return Ok();
        }
    }
}