using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NurseVolunteeringSystem.Areas.Nurse.Models
{
    public class EditCareVisitVM
    {
        public int CareVisitID { get; set; }
        public DateTime VisitDate { get; set; }
        public DateTime ApproximateArriveTime { get; set; }
        [Required(ErrorMessage = "Please enter arrive time")]
        public DateTime VisistArriveTime { get; set; }
        public DateTime DepartTime { get; set; }
        public string WoundProgress { get; set; }
        public string Notes { get; set; }
        public string Status { get; set; }

        public int CareContractID { get; set; }
        
    }
}
