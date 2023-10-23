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

    }
}
