using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace NurseVolunteeringSystem.Models.ViewModels
{
    public class UpdateUserPassword
    {
        public int UserID { get; set; }
        [Required(ErrorMessage ="Please enter old password")]
        [Compare("ConfirmOldPassword",ErrorMessage ="Old password is incorrect")]
        public string OldPassword { get; set; }
        [Required(ErrorMessage ="Old password incorrect")]
        public string ConfirmOldPassword { get; set; }
        [Required(ErrorMessage ="Please enter password")]
        [Compare("ConfirmPassword")]
        public string Password { get; set; }
        [Required(ErrorMessage ="Please confirm password")]
        [Display(Name ="Confirm password")]
        public string ConfirmPassword { get; set; }
    }
}
