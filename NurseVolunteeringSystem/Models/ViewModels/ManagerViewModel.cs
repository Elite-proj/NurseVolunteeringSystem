using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace NurseVolunteeringSystem.Models.ViewModels
{
    public class ManagerViewModel
    {
        public int UserID { get; set; }
        [Required(ErrorMessage ="Please enter username")]
        public string Username { get; set; }

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
        public string UserType { get; set; }

    }
}
