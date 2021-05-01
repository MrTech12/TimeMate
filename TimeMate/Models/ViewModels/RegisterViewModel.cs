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
        [RegularExpression("[A-Za-z]+", ErrorMessage = "Gebruik letters voor de voornaam.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Dit veld is verplicht.")]
        [Display(Name = "Voor een geldig E-mailadres in.")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Voor een geldig E-mailadres in.")]
        [RegularExpression("^([0-9a-zA-Z-_]([-\\.\\w]*[0-9a-zA-Z-_])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$", ErrorMessage = "Voor een geldig E-mailadres in.")]
        public string Mail { get; set; }

        [Required(ErrorMessage = "Dit veld is verplicht.")]
        [Display(Name = "Voor een wachtwoord in.")]
        [DataType(DataType.Password)]
        [MinLength(9)]
        public string Password { get; set; }

        [Display(Name = "Kies het aantal bijbanen.")]
        [Range(0, 2)]
        public int JobAmount { get; set; }

        [Display(Name = "Voer het uurloon van bijbaan 1 in.")]
        [DisplayFormat(DataFormatString = "{0:0,0}")]
        [RegularExpression(@"\d{1,}(?:\,?\d)*%?", ErrorMessage = "Gebruik nummers met een komma.")]
        public string Job1HourlyWage { get; set; }

        [Display(Name = "Is de bijbaan doordeweeks of in het weekend?")]
        public string Job1DayType { get; set; }

        [Display(Name = "Voer het uurloon van bijbaan 2 in.")]
        [DisplayFormat(DataFormatString = "{0:0,0}")]
        [RegularExpression(@"\d{1,}(?:\,?\d)*%?", ErrorMessage = "Gebruik nummers met een komma.")]
        public string Job2HourlyWage { get; set; }

        [Display(Name = "Is de bijbaan doordeweeks of in het weekend?")]
        public string Job2DayType { get; set; }
    }
}
