using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TimeMate.Models
{
    public class NAppointmentViewModel
    {
        public AppointmentViewModel AppointmentViewModel { get; set; }

        [Required(ErrorMessage = "Dit veld is verplicht.")]
        [Display(Name = "Kies een agenda uit.")]
        public List<string> AgendaName { get; set; }

        [Display(Name = "Voer optionele details hier in.")]
        [DataType(DataType.Html)]
        [HtmlAttributeName]
        public string Description { get; set; }
    }
}
