using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TimeMate.Models
{
    public class AppointmentViewModel
    {
        [Required(ErrorMessage = "Dit veld is verplicht.")]
        [Display(Name = "Voor een afspraaknaam in.")]
        public string AppointmentName { get; set; }

        [Required(ErrorMessage = "Dit veld is verplicht.")]
        [Display(Name = "Kies een startdatum.")]
        [DataType(DataType.DateTime)]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Dit veld is verplicht.")]
        [Display(Name = "Kies een starttijd.")]
        [DataType(DataType.Time)]
        public TimeSpan StartTime { get; set; }

        [Required(ErrorMessage = "Dit veld is verplicht.")]
        [Display(Name = "Kies een einddatum.")]
        [DataType(DataType.DateTime)]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Dit veld is verplicht.")]
        [Display(Name = "Kies een eindtijd.")]
        [DataType(DataType.Time)]
        public TimeSpan EndTime { get; set; }

        [Required(ErrorMessage = "Dit veld is verplicht.")]
        [Display(Name = "Kies een agenda uit.")]
        public int AgendaID { get; set; }
    }
}
