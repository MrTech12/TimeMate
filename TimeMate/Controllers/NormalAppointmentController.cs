using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicLayer.Logic;
using DataAccessLayer.DTO;
using DataAccessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TimeMate.Models;

namespace TimeMate.Controllers
{
    public class NormalAppointmentController : Controller
    {
        private readonly IAgendaContainer _agendaContainer;
        private readonly IAppointmentContainer _appointmentContainer;
        private readonly INormalAppointmentContainer _normalAppointmentContainer;

        private AccountDTO accountDTO = new AccountDTO();
        private Account account;
        private Agenda agenda;

        public NormalAppointmentController(IAgendaContainer agendaContainer, IAppointmentContainer appointmentContainer, INormalAppointmentContainer normalAppointmentContainer)
        {
            _agendaContainer = agendaContainer;
            _appointmentContainer = appointmentContainer;
            _normalAppointmentContainer = normalAppointmentContainer;
        }

        [HttpGet]
        public IActionResult Index()
        {
            NormalAppointmentViewModel viewModel = new NormalAppointmentViewModel();
            account = new Account(accountDTO, _agendaContainer);
            viewModel.AppointmentViewModel.AgendaDTO = account.RetrieveAgendas();
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Index(string json)
        {
            var appointment = JsonConvert.DeserializeObject<List<string>>(json);

            AppointmentDTO appointmentDTO = new AppointmentDTO();
            appointmentDTO.AppointmentName = appointment[0];
            appointmentDTO.StartDate = Convert.ToDateTime(appointment[1]) + TimeSpan.Parse(appointment[2]);
            appointmentDTO.EndDate = Convert.ToDateTime(appointment[3]) + TimeSpan.Parse(appointment[4]);
            appointmentDTO.AgendaName = appointment[5];
            appointmentDTO.AgendaID = Convert.ToInt32(appointment[6]);

            if (appointment[7] != null)
            {
                string description = appointment[7].Replace("<span class=\"bolding\">", "<b>").Replace("</span>", "</b>")
                    .Replace("<span class=\"normal-text\">", "</b>");
                appointmentDTO.DescriptionDTO.Description = description;
            }
            else
            {
                appointmentDTO.DescriptionDTO.Description = null;
            }

            agenda = new Agenda(accountDTO, _appointmentContainer, _normalAppointmentContainer);
            agenda.CreateNormalAppointment(appointmentDTO);
            return Ok();
        }
    }
}