using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TimeMate.Models
{
    public class AgendaViewModel
    {
        [Required(ErrorMessage = "Dit veld is verplicht.")]
        [Display(Name = "Voor een naam voor de agenda in.")]
        public string AgendaName { get; set; }

        [Required(ErrorMessage = "Dit veld is verplicht.")]
        [Display(Name = "Kies de kleur van de agenda.")]
        public string AgendaColor { get; set; }

        [Required(ErrorMessage = "Dit veld is verplicht.")]
        [Display(Name = "Kies wat voor melding je te zien krijgt.")]
        public string NotificationType { get; set; }
    }
}
