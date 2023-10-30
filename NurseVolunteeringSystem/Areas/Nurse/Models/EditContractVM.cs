using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace NurseVolunteeringSystem.Areas.Nurse.Models
{
    public class EditContractVM
    {
        public int CareContractID { get; set; }
        
        public DateTime ContractDate { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string WoundDescription { get; set; }
        [Required(ErrorMessage ="Please enter care start date")]
        public DateTime? StartCareDate { get; set; }
        public DateTime? EndCareDate { get; set; }
        public string ContractStatus { get; set; }
        public string DeleteStatus { get; set; }

        public int SuburbID { get; set; }
       
        public int? PatientID { get; set; }
        
        public int? NurseID { get; set; }
       
    }
}
