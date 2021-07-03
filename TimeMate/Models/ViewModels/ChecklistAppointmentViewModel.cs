using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TimeMate.Models
{
    public class ChecklistAppointmentViewModel
    {
        public ChecklistAppointmentViewModel()
        {
            this.AppointmentViewModel = new AppointmentViewModel();
        }

        public AppointmentViewModel AppointmentViewModel { get; set; }

        public List<string> Task { get; set; }
    }
}
