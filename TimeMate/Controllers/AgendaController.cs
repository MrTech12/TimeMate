using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicLayer.Logic;
using DataAccessLayer.Contexts;
using DataAccessLayer.DTO;
using Microsoft.AspNetCore.Mvc;

namespace TimeMate.Controllers
{
    public class AgendaController : Controller
    {
        AccountDTO accountDTO = new AccountDTO();

        [HttpGet]
        public IActionResult Index(int id)
        {
            accountDTO.AccountID = id;
            AgendaLogic agenda = new AgendaLogic(accountDTO, new SQLAgendaContext(new SQLDatabaseContext()));
            List<AppointmentDTO> appointmentModelForView = new List<AppointmentDTO>();

            appointmentModelForView = agenda.RetrieveAppointments();

            return View(appointmentModelForView);
        }
    }
}