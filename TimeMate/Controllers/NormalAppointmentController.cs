using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicLayer.Logic;
using DataAccessLayer.Contexts;
using DataAccessLayer.DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TimeMate.Models;

namespace TimeMate.Controllers
{
    public class NormalAppointmentController : Controller
    {
        AccountDTO accountDTO = new AccountDTO();
        Account account;
        Agenda agenda;

        [HttpGet]
        public IActionResult Index()
        {
            NAppointmentViewModel nAppointmentViewModel = new NAppointmentViewModel();
            account = new Account(accountDTO, new SQLAccountContext(), new SQLAgendaContext());
            nAppointmentViewModel.AppointmentViewModel.AgendaDTO = account.GetAgendaNames();
            return View(nAppointmentViewModel);
        }

        [HttpPost]
        public IActionResult Index(string json)
        {
            var newAgenda = JsonConvert.DeserializeObject<List<string>>(json);

            AppointmentDTO appointmentDTO = new AppointmentDTO();
            appointmentDTO.AppointmentName = newAgenda[0];
            appointmentDTO.StartDate = Convert.ToDateTime(newAgenda[1]) + TimeSpan.Parse(newAgenda[2]);
            appointmentDTO.EndDate = Convert.ToDateTime(newAgenda[3]) + TimeSpan.Parse(newAgenda[4]);
            appointmentDTO.AgendaName = newAgenda[5];
            appointmentDTO.AgendaID = Convert.ToInt32(newAgenda[6]);

            if (newAgenda[7] != null)
            {
                string newDescription = newAgenda[7].Replace("<span class=\"bolding\">", "<b>").Replace("</span>", "</b>")
                    .Replace("<span class=\"normal-text\">", "</b>");
                appointmentDTO.Description = newDescription;
            }
            else
            {
                appointmentDTO.Description = null;
            }

            agenda = new Agenda(accountDTO, new SQLAgendaContext(), new SQLAppointmentContext(), new SQLNormalAppointmentContext());

            agenda.CreateNAppointment(appointmentDTO);

            return Ok();
        }
    }
}