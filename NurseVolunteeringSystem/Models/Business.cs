using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace NurseVolunteeringSystem.Models
{
    public class Business
    {
        public int BusinessID { get; set; }
        [Required(ErrorMessage ="Please enter organization name")]
        public string OrganizationName { get; set; }
        [Required(ErrorMessage ="Please enter NPO number")]
        public  string NPONumber { get; set; }
        [Required(ErrorMessage ="Please enter address line 1")]
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        [Required(ErrorMessage ="Please enter contacts")]
        public string ContactNo { get; set; }
        [Required(ErrorMessage ="Please enter email address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter operating hours")]
        public string OperatingHours { get; set; }
        [Required(ErrorMessage = "Please select Suburb")]
        public int SuburbID { get; set; }
        
        public Suburb Suburb { get; set; }
    }
}
