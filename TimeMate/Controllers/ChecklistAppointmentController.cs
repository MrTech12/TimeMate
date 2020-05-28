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
    public class ChecklistAppointmentController : Controller
    {
        AccountDTO accountDTO = new AccountDTO();
        Account account;
        Agenda agenda;

        [HttpGet]
        public IActionResult Index()
        {
            CAppointmentViewModel cAppointmentViewModel = new CAppointmentViewModel();
            account = new Account(accountDTO, new SQLAccountContext(), new SQLAgendaContext());
            cAppointmentViewModel.AppointmentViewModel.AgendaDTO = account.GetAgendaNames();
            return View(cAppointmentViewModel);
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

            for (int i = 7; i < newAppointment.Count; i++)
            {
                if (newAppointment[i] != "")
                {
                    appointmentDTO.ChecklistItemName.Add(newAppointment[i]);
                }
            }

            agenda = new Agenda(accountDTO, new SQLAgendaContext(), new SQLAppointmentContext(), new SQLChecklistAppointmentContext());

            agenda.CreateCAppointment(appointmentDTO);

            return Ok();
        }
    }
}