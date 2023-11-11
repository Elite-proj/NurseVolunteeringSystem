using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NurseVolunteeringSystem.Models.ViewModels
{
    public class NurseViewModel
    {
        public int UserID { get; set; }
        [Required(ErrorMessage = "Please enter username")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Please enter first name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please enter surname")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Please enter email address")]
        [RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9]+)*\\.([a-z]{2,4})$", ErrorMessage = "Invalid format")]
        [Compare("ConfirmEmail")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please confirm email")]
        [RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9]+)*\\.([a-z]{2,4})$", ErrorMessage = "Invalid format")]
        [Display(Name = "Confirm email")]
        public string ConfirmEmail { get; set; }
        [Required(ErrorMessage = "Please enter password")]
        [Compare("ConfirmPassword")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Please confirm password")]
        [Display(Name = "Confirm password")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Please enter contacts")]
        public string ContactNo { get; set; }
        [Required(ErrorMessage ="Please select gender")]
        public int GenderID { get; set; }
        [Required(ErrorMessage = "Please enter ID Number")]
        [Range(1000000000000, 9999999999999, ErrorMessage = "ID Number must be 13 digits.")]
        public string IDNumber { get; set; }

        public string UserType { get; set; }

        public string GenderName { get; set; }
    }
}
