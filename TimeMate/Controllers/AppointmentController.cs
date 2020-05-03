using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TimeMate.Controllers
{
    abstract public class AppointmentController : Controller
    {
        public abstract void Greet();
    }
}