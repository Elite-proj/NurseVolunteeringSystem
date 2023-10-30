using Microsoft.AspNetCore.Mvc;
using NurseVolunteeringSystem.Areas.Nurse.Models;
using NurseVolunteeringSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NurseVolunteeringSystem.Areas.Nurse.Controllers
{
    [Area("Nurse")]
    public class CareVisitController : Controller
    {
        private AppDBContext context { get; set; }

        public CareVisitController(AppDBContext ctx)
        {
            //this._IConfiguration = config;
            this.context = ctx;
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var visit = context.CareVisit.Find(id);

            EditCareVisitVM care = new EditCareVisitVM();

            care.CareVisitID = visit.CareVisitID;
            care.ApproximateArriveTime = visit.ApproximateArriveTime;
            care.CareContractID = visit.CareContractID;
            care.DepartTime = visit.DepartTime;
            care.Notes = visit.Notes;
            care.Status = visit.Status;
            care.VisistArriveTime = visit.VisistArriveTime;
            care.VisitDate = visit.VisitDate;
            care.WoundProgress = visit.WoundProgress;
           

            return View(care);
        }

        [HttpPost]
        public IActionResult Edit(EditCareVisitVM visit)
        {
            if(ModelState.IsValid)
            {
                CareVisit care = new CareVisit();

                care.CareVisitID = visit.CareVisitID;
                care.ApproximateArriveTime = visit.ApproximateArriveTime;
                care.CareContractID = visit.CareContractID;
                care.DepartTime = visit.DepartTime;
                care.Notes = visit.Notes;
                care.Status = visit.Status;
                care.VisistArriveTime = visit.VisistArriveTime;
                care.VisitDate = visit.VisitDate;
                care.WoundProgress = visit.WoundProgress;

                context.CareVisit.Update(care);
                context.SaveChanges();

                return RedirectToAction("ListAssignedContracts", "CareContract", new { area = "Nurse" });

            }
            else
            {
                return View(visit);
            }
        }

        public IActionResult Delete(int id)
        {
            var visit = context.CareVisit.Find(id);

            visit.Status = "In-Active";

            context.CareVisit.Update(visit);
            context.SaveChanges();

            return RedirectToAction("ListAssignedContracts", "CareContract", new { area = "Nurse" });
        }
    }
}
