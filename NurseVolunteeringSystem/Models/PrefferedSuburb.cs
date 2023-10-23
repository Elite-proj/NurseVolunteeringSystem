using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace NurseVolunteeringSystem.Models
{
    public class PrefferedSuburb
    {
        public int PrefferedSuburbID { get; set; }
        [Required(ErrorMessage ="Please select preferred suburb")]
        
        public int SuburbID { get; set; }
        public Suburb Suburb { get; set; }
        public int? NurseID { get; set; }
        public Nurse Nurse { get; set; }

        public string Status { get; set; }
    }
}
