using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.DTO;
using Microsoft.AspNetCore.Mvc;

namespace TimeMate.Controllers
{
    public class AgendaController : Controller
    {
        AccountDTO accountDTO = new AccountDTO();

        public AgendaController(AccountDTO accountDTO)
        {
            this.accountDTO = accountDTO;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}