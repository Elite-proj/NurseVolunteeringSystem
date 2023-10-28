using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace NurseVolunteeringSystem.Areas.Patient.Models
{
    public class AddCareContractVM
    {

        public int ContractID { get; set; }
        public DateTime? ContractDate { get; set; }
        [Required(ErrorMessage ="Please enter address line 1")]
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        [Required(ErrorMessage = "Please enter address line 1")]
        public string WoundDescription { get; set; }
        public DateTime? StartCareDate { get; set; }
        public DateTime? EndCareDate { get; set; }
        public string ContractStatus { get; set; }
        public string DeleteStatus { get; set; }
        [Required(ErrorMessage = "Please enter address line 1")]
        public int SuburbID { get; set; }
       
        public int? PatientID { get; set; }
        
        public int? NurseID { get; set; }
        


    }
}
