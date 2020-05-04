using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TimeMate.Models
{
    public class LogInViewModel
    {
        [Required(ErrorMessage = "Dit veld is verplicht")]
        [Display(Name = "Voor een E-mailadres in")]
        [DataType(DataType.EmailAddress)]
        public string Mail { get; set; }

        [Required(ErrorMessage = "Dit veld is verplicht")]
        [Display(Name = "Voor een wachtwoord in")]
        [DataType(DataType.Password)]
        [MinLength(9)]
        public string Password { get; set; }
    }
}
