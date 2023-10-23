using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace NurseVolunteeringSystem.Models
{
    public class ChronicCondition
    {
        public int ChronicConditionID { get; set; }
        [Required(ErrorMessage ="Please enter condition name")]
        public string ConditionName { get; set; }
        public string Status { get; set; }
    }
}
