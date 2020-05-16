using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TimeMate.Models
{
    public class CAppointmentViewModel
    {
        public CAppointmentViewModel()
        {
            this.AppointmentViewModel = new AppointmentViewModel();
        }

        public AppointmentViewModel AppointmentViewModel { get; set; }

        [Required(ErrorMessage = "Dit veld is verplicht.")]
        [Display(Name = "Kies een agenda uit.")]
        public List<string> AgendaName { get; set; }

        [Display(Name = "Voer een taak in.")]
        public List<string> Task { get; set; }
    }
}
