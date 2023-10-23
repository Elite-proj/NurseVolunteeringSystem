using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NurseVolunteeringSystem.Models
{
    public class City
    {
        public int CityID { get; set; }

        [Required(ErrorMessage ="Please enter city name")]
        public string CityName { get; set; }

        [StringLength(3, MinimumLength = 2)]
        public string Abbreviation { get; set; }
        public string Status { get; set; }

    }
}
