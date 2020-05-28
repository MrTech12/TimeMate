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
        private AccountDTO accountDTO = new AccountDTO();
        private Account account;
        private Agenda agenda;

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
            var newAppointment = JsonConvert.DeserializeObject<List<string>>(json);

            AppointmentDTO appointmentDTO = new AppointmentDTO();
            appointmentDTO.AppointmentName = newAppointment[0];
            appointmentDTO.StartDate = Convert.ToDateTime(newAppointment[1]) + TimeSpan.Parse(newAppointment[2]);
            appointmentDTO.EndDate = Convert.ToDateTime(newAppointment[3]) + TimeSpan.Parse(newAppointment[4]);
            appointmentDTO.AgendaName = newAppointment[5];
            appointmentDTO.AgendaID = Convert.ToInt32(newAppointment[6]);

            if (newAppointment[7] != null)
            {
                string newDescription = newAppointment[7].Replace("<span class=\"bolding\">", "<b>").Replace("</span>", "</b>")
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