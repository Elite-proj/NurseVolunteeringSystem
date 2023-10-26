using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace NurseVolunteeringSystem.Models
{
    public class CareContract
    {
        public int CareContractID { get; set; }
        public DateTime? ContractDate { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string WoundDescription { get; set; }
        public DateTime? StartCareDate { get; set; }
        public DateTime? EndCareDate { get; set; }
        public string ContractStatus { get; set; }
        public string DeleteStatus { get; set; }

        public int SuburbID { get; set; }
        public Suburb Suburb { get; set; }
        public int? PatientID { get; set; }
        public Patient Patient { get; set; }
        public int? NurseID { get; set; }
        public Nurse Nurse { get; set;}
    }
}
