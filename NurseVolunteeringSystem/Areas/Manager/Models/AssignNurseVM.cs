using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace NurseVolunteeringSystem.Areas.Manager.Models
{
    public class AssignNurseVM
    {
        [Required(ErrorMessage ="Please select Nurse")]
        public int NurseID { get; set; }
        public int CareContractID { get; set; }
    }
}
