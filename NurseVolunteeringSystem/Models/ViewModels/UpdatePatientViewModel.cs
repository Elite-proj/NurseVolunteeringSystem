using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NurseVolunteeringSystem.Models.ViewModels
{
    public class UpdatePatientViewModel
    {
        public int PatientID { get; set; }
        [Required(ErrorMessage = "Please enter first name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please enter surname")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Please enter ID number")]
        public string IDNumber { get; set; }
        [Required(ErrorMessage = "Please enter Address line 1")]
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        [Required(ErrorMessage = "Please select suburb")]
        public int SuburbID { get; set; }
        [Required(ErrorMessage = "Please enter email address")]
        [RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9]+)*\\.([a-z]{2,4})$", ErrorMessage = "Invalid format")]
        [Compare("ConfirmEmail")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please confirm email")]
        [RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9]+)*\\.([a-z]{2,4})$", ErrorMessage = "Invalid format")]
        [Display(Name = "Confirm password")]
        public string ConfirmEmail { get; set; }
        
        [Required(ErrorMessage = "Please enter contacts")]
        public string ContactNo { get; set; }
        
        [Required(ErrorMessage = "Please select gender")]
        public int GenderID { get; set; }
        [Required(ErrorMessage = "Please select date of birth")]
        public DateTime DateOfBirth { get; set; }
        [Required(ErrorMessage = "Please enter emergency contact person")]
        public string EmergencyContactPerson { get; set; }
        [Required(ErrorMessage = "Please enter emergency contact number")]
        public string EmergencyContactNumber { get; set; }
    }
}
