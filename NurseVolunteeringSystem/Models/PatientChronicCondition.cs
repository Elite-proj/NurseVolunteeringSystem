using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace NurseVolunteeringSystem.Models
{
    public class PatientChronicCondition
    {
        public int PatientChronicConditionID { get; set; }
        [Required(ErrorMessage ="Please select chronic condition")]
        public int ChronicConditionID { get; set; }
        public ChronicCondition ChronicCondition { get; set; }
        public int PatientID { get; set; }
        public Patient Patient { get; set; }
        public string Status { get; set; }
    }
}
