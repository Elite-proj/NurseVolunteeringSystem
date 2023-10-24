using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace NurseVolunteeringSystem.Models
{
    public class User
    {
        public int UserID { get; set; }
        //[Required(ErrorMessage ="Please enter username.")]
        public string Username { get; set; }
        //[Required(ErrorMessage ="Please enter first name")]
        public string FirstName { get; set; }
        //[Required(ErrorMessage = "Please enter surname")]
        public string Surname { get; set; }
        //[Required(ErrorMessage = "Please enter ID number")]
        public string IDNumber { get; set; }
        //[Required(ErrorMessage = "Please enter Address line 1")]
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        //[Required(ErrorMessage = "Please enter email address")]
        //[RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9]+)*\\.([a-z]{2,4})$", ErrorMessage = "Invalid format")]
        //[Compare("ConfirmEmail")]
        public string Email { get; set; }
        //[Required(ErrorMessage = "Please confirm email")]
        //[RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9]+)*\\.([a-z]{2,4})$", ErrorMessage = "Invalid format")]
        [Display(Name ="Confirm password")]
        public string ConfirmEmail { get; set; }
        //[Required(ErrorMessage = "Please enter password")]
        [Compare("ConfirmPassword")]
        public string Password { get; set; }
        //[Required(ErrorMessage ="Please confirm password")]
        [Display(Name ="Confirm password")]
        public string ConfirmPassword { get; set; }
        //[Required(ErrorMessage = "Please enter contacts")]
        public string ContactNo { get; set; }
        public string UserType { get; set; }
        public string Status { get; set; }
        //[Required(ErrorMessage = "Please select gender")]
        public int GenderID { get; set; }
        public Gender Gender { get; set; }
        //[Required(ErrorMessage = "Please select suburb")]
        public int SuburbID { get; set; }
        public Suburb Suburb { get; set; }

        public virtual Patient Patient { get; set; }
        public virtual Nurse Nurse { get; set; }
        public virtual Admin Admin { get; set; }
        public virtual Manager Manager { get; set; }

    }
}
