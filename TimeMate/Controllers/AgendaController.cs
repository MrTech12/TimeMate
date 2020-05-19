using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicLayer.Logic;
using DataAccessLayer.Contexts;
using DataAccessLayer.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TimeMate.Models;

namespace TimeMate.Controllers
{
    public class AgendaController : Controller
    {
        AccountDTO accountDTO = new AccountDTO();
        Agenda agenda;

        [HttpGet]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("accountID") != null)
            {
                var accountID = HttpContext.Session.GetInt32("accountID");
                accountDTO.AccountID = Convert.ToInt32(accountID);
                Agenda agendaLogic = new Agenda(accountDTO, new SQLAgendaContext());
                List<AppointmentDTO> appointmentModelForView = new List<AppointmentDTO>();

                appointmentModelForView = agendaLogic.RetrieveAppointments();

                return View(appointmentModelForView);
            }
            else
            {
                return RedirectToAction("Index", "Account");
            }
        }

        [HttpGet]
        public IActionResult AddAgenda()
        {
            AgendaViewModel agendaViewModel = new AgendaViewModel();
            return View(agendaViewModel);
        }

        [HttpPost]
        public IActionResult AddAgenda(AgendaViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                AgendaDTO agendaDTO = new AgendaDTO() { AgendaName = viewModel.Name, AgendaColor = viewModel.Color, Notification = viewModel.NotificationType };
                Account accountLogic = new Account(accountDTO, new SQLAccountContext(), new SQLAgendaContext());
                accountLogic.CreateAgenda(agendaDTO);
                return RedirectToAction("Index", "Agenda");
            }
            else
            {
                return View(viewModel);
            }
        }

        [HttpGet]
        public IActionResult RetrieveAppointmentDetails(string json)
        {
            var selectedAppointment = JsonConvert.DeserializeObject<List<string>>(json);
            AppointmentDTO appointmentDTO = new AppointmentDTO();
            appointmentDTO.AppointmentName = selectedAppointment[0];
            appointmentDTO.StartDate = Convert.ToDateTime(selectedAppointment[1]);
            appointmentDTO.EndDate = Convert.ToDateTime(selectedAppointment[2]);
            appointmentDTO.AgendaName = selectedAppointment[3];

            NormalAppointment normalAppointment = new NormalAppointment(appointmentDTO, new SQLNormalAppointmentContext());
            ChecklistAppointment checklistAppointment = new ChecklistAppointment(appointmentDTO, new SQLChecklistAppointmentContext());

            agenda = new Agenda(accountDTO, new SQLAgendaContext(), new SQLAppointmentContext());
            int agendaID = agenda.GetAgendaID(appointmentDTO.AgendaName);
            appointmentDTO.AppointmentID = agenda.GetAppointmentID(appointmentDTO, agendaID);
            string description = normalAppointment.RetrieveDescription(appointmentDTO.AppointmentID);

            if (description == "")
            {
                appointmentDTO = checklistAppointment.RetrieveTask(appointmentDTO.AppointmentID);

                if (appointmentDTO.ChecklistItemName != null)
                {
                    List<string> tasks = new List<string>();
                    for (int i = 0; i < appointmentDTO.ChecklistItemName.Count; i++)
                    {
                        tasks.Add(appointmentDTO.ChecklistItemName[i]);
                        tasks.Add(Convert.ToString(appointmentDTO.ChecklistItemChecked[i]));
                    }

                    return Json(tasks);
                }
                else
                {
                    return Json(null);
                }
            }
            else if (description != "")
            {
                return Json(description);
            }
            return View();
        }
    }
}