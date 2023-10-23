using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace NurseVolunteeringSystem.Models
{
    public class Patient
    {
        [ForeignKey("User")]
        public int PatientID { get; set; }
        [Required(ErrorMessage ="Please enter date of birth")]
        public DateTime DateOfBirth { get; set; }
        [Required(ErrorMessage ="Please enter emergency contact person")]
        public string EmergencyContactPerson { get; set; }
        [Required(ErrorMessage = "Please enter emergency contact number")]
        public string EmergencyContactNumber { get; set; }

        public virtual User User { get; set; }

    }
}
