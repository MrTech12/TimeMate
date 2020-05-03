using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TimeMate.Models
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Voor uw voornaam in.")]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Mail { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(9)]
        public string Password { get; set; }

        [Range(0,2)]
        public int JobAmount { get; set; }

        public double job1Wage { get; set; }

        public string job1DayType { get; set; }

        public double job2Wage { get; set; }

        public string job2DayType { get; set; }
    }
}
