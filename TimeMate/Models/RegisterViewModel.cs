using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TimeMate.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Dit veld is verplicht.")]
        [Display(Name = "Voor uw voornaam in.")]
        [DataType(DataType.Text)]
        [RegularExpression("[A-Za-z]+", ErrorMessage = "Gebruik letters in plaats van nummers")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Dit veld is verplicht.")]
        [Display(Name = "Voor een geldige E-mailadres in.")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Voor een geldige E-mailadres in.")]
        public string Mail { get; set; }

        [Required(ErrorMessage = "Dit veld is verplicht.")]
        [Display(Name = "Voor een wachtwoord in.")]
        [DataType(DataType.Password)]
        [MinLength(9)]
        public string Password { get; set; }

        [Display(Name = "Kies het aantal bijbanen.")]
        [Range(0, 2)]
        public int JobAmount { get; set; }

        [Display(Name = "Voer de uurloon van bijbaan 1 in.")]
        [DisplayFormat(DataFormatString = "{0:0,0}")]
        [RegularExpression(@"\d{1,}(?:\,?\d)*%?", ErrorMessage = "Gebruik een nummer met een komma.")]
        public string job1HourlyWage { get; set; }

        [Display(Name = "Is de bijbaan doordeweeks of in het weekend?")]
        public string job1DayType { get; set; }

        [Display(Name = "Voer de uurloon van bijbaan 2 in.")]
        [DisplayFormat(DataFormatString = "{0:0,0}")]
        [RegularExpression(@"\d{1,}(?:\,?\d)*%?", ErrorMessage = "Gebruik een nummer met een komma.")]
        public string job2HourlyWage { get; set; }

        [Display(Name = "Is de bijbaan doordeweeks of in het weekend?")]
        public string job2DayType { get; set; }
    }
}
