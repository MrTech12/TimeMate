﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicLayer.Logic;
using Model.DTO_s;
using DataAccessLayer.Interfaces;
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
                if (viewModel.AgendaName == "Bijbaan" || viewModel.AgendaName == "bijbaan")
                {
                    ModelState.AddModelError("", "Deze agendanaam mag niet gebruikt worden.");
                    return View(viewModel);
                }

                AgendaDTO agendaDTO = new AgendaDTO();
                agendaDTO.AgendaName = viewModel.AgendaName;
                agendaDTO.AgendaColor = viewModel.AgendaColor;
                agendaDTO.NotificationType = viewModel.NotificationType;

                accountDTO.AccountID = HttpContext.Session.GetInt32("accountID").Value;
                agenda = new Agenda(accountDTO, _agendaRepository);
                agenda.CreateAgenda(agendaDTO);
                return RedirectToAction("Index", "AgendaView");
            }
            else
            {
                return View(viewModel);
            }
        }

        [HttpPost]
        public IActionResult DeleteAgenda([FromBody] AgendaModel agendaModel)
        {
            accountDTO.AccountID = HttpContext.Session.GetInt32("accountID").Value;
            agenda = new Agenda(accountDTO, _agendaRepository);
            agenda.DeleteAgenda(agendaModel.AgendaID);
            return Ok();
        }
    }
}