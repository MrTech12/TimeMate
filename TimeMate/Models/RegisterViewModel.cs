using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TimeMate.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Dit veld is verplicht")]
        [Display(Name = "Voor uw voornaam in.")]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Dit veld is verplicht")]
        [Display(Name = "Voor een geldige E-mailadres in.")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress (ErrorMessage = "Voor een geldige E-mailadres in.")]
        public string Mail { get; set; }

        [Required(ErrorMessage = "Dit veld is verplicht")]
        [Display(Name = "Voor een wachtwoord in.")]
        [DataType(DataType.Password)]
        [MinLength(9)]
        public string Password { get; set; }

        [Display(Name = "Kies het aantal bijbanen.")]
        [Range(0,2)]
        public int JobAmount { get; set; }

        [Display(Name = "Voer de uurloon van bijbaan 1 in.")]
        public double job1Wage { get; set; }

        [Display(Name = "Is de bijbaan doordeweeks of in het weekend?")]
        public string job1DayType { get; set; }

        [Display(Name = "Voer de uurloon van bijbaan 2 in.")]
        public double job2Wage { get; set; }

        [Display(Name = "Is de bijbaan doordeweeks of in het weekend?")]
        public string job2DayType { get; set; }

        [Display(Name = "Voer in hoeveel uur u beschikbaar wilt stellen voor uw bijbaan.")]
        public double AllocatedHours { get; set; }
    }
}
