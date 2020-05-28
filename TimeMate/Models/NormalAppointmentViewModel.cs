using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TimeMate.Models
{
    public class NormalAppointmentViewModel
    {
        public NormalAppointmentViewModel()
        {
            this.AppointmentViewModel = new AppointmentViewModel();
        }

        public AppointmentViewModel AppointmentViewModel { get; set; }

        [Display(Name = "Voer optionele details hier in.")]
        [DataType(DataType.Html)]
        [HtmlAttributeName]
        public string Description { get; set; }
    }
}
