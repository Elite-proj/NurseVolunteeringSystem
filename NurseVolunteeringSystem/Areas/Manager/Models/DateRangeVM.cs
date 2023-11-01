using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace NurseVolunteeringSystem.Areas.Manager.Models
{
    public class DateRangeVM
    {
        [Required(ErrorMessage ="Please enter minimun date")]
        public DateTime MinDate { get; set; }
        [Required(ErrorMessage = "Please enter miximum date")]
        public DateTime MaxDate { get; set; }
        public int contractID { get; set; }
    }
}
