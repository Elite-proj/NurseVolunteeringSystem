using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NurseVolunteeringSystem.Models.ViewModels
{
    public class UpdateManagerVM
    {
        public int UserID { get; set; }

        [Required(ErrorMessage = "Please enter email address")]
        [RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9]+)*\\.([a-z]{2,4})$", ErrorMessage = "Invalid format")]
        [Compare("ConfirmEmail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please confirm email")]
        [RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9]+)*\\.([a-z]{2,4})$", ErrorMessage = "Invalid format")]
        [Display(Name = "Confirm email")]
        public string ConfirmEmail { get; set; }
        [Required(ErrorMessage = "Please enter contact number")]
        public string ContactNo { get; set; }
        
    }
}
