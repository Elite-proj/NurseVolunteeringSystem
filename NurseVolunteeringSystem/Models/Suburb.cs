﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace NurseVolunteeringSystem.Models
{
    public class Suburb
    {
        public int SuburbID { get; set; }
        [Required(ErrorMessage ="Please enter suburb name")]
        public string SuburbName { get; set; }
        [Required(ErrorMessage ="Please enter postal code")]
        public string PostalCode { get; set; }
        public string Status { get; set; }
        [Required(ErrorMessage ="Please select city.")]
        public int CityID { get; set; }
        public City City { get; set; }

        
    }
}
