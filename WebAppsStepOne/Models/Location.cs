using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebAppStepOne.Models
{
    public class Location
    {
        [Required]
        [Display(Name = "Location")]
        public string? Name { get; set; }
        public string? Region { get; set; }
        public string? Country { get; set; }

        [Display(Name ="Local Time")]
        public string? Time { get; set; }

        [Display(Name = "Temp. C")]
        public string? TempC { get; set; }

        [Display(Name = "Wind speed, kmp")]
        public string? Wind { get; set; }

        [Display(Name = "Wind Direction")]
        public string? WindDirection { get; set; }
        public string? Cloud { get; set; }
        public string? Visibility { get; set; }

    }
}
