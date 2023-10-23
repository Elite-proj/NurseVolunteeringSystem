using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NurseVolunteeringSystem.Models
{
    public class CareVisit
    {
        public int CareVisitID { get; set; }
        public DateTime VisitDate { get; set; }
        public DateTime ApproximateArriveTime { get; set; }
        public DateTime VisistArriveTime { get; set; }
        public DateTime DepartTime { get; set; }
        public string WoundProgress { get; set; }
        public string Notes { get; set; }
        public string Status { get; set; }

        public int CareContractID { get; set; }
        public CareContract CareContract { get; set; }
    }
}
