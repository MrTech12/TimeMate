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

        public List<string> JobHourlyWage { get; set; }

        public List<string> JobDayType { get; set; }

        [Display(Name = "Voor een naam voor de agenda in.")]
        public string AgendaName { get; set; }

        [Display(Name = "Kies de kleur van de agenda.")]
        public string AgendaColor { get; set; }
    }
}
