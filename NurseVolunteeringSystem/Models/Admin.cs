using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NurseVolunteeringSystem.Models
{
    public class Admin
    {
        [ForeignKey("User")]
        public int AdminID { get; set; }

        public virtual User User { get; set; }
    }
}
