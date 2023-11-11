using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NurseVolunteeringSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NurseVolunteeringSystem.Areas.Patient.Controllers
{
    [Area("Patient")]
    public class PatientConditionController : Controller
    {
        private readonly AppDBContext _context;

        public PatientConditionController(AppDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult List()
        {
            if (HttpContext.Session.GetInt32("PatientID") == null)
            {
                return RedirectToAction("Account", "Login", new { area = "" });
            }

            int PatientID = int.Parse(HttpContext.Session.GetInt32("PatientID").ToString());
            var conditions = _context.Patient_ChronicConditions.Where(p => p.PatientID == PatientID && p.Status=="Active").Include(c => c.ChronicCondition).OrderBy(o => o.PatientChronicConditionID);
            return View(conditions);
        }

        [HttpGet]
        public IActionResult Create()
        {
            if (HttpContext.Session.GetInt32("PatientID") == null)
            {
                return RedirectToAction("Account", "Login", new { area = "" });
            }

            ViewBag.Conditions = _context.ChronicCondition.OrderBy(o => o.ConditionName).ToList();

            return View();
        }

        [HttpPost]
        public IActionResult Create(PatientChronicCondition patientChronicCondition)
        {
            if(ModelState.IsValid)
            {
                patientChronicCondition.Status = "Active";
                patientChronicCondition.PatientID = int.Parse(HttpContext.Session.GetInt32("PatientID").ToString());
                _context.Patient_ChronicConditions.Add(patientChronicCondition);
                _context.SaveChanges();

                return RedirectToAction("List", "PatientCondition",new { area="Patient"});
            }
            else
            {
                ViewBag.Conditions = _context.ChronicCondition.OrderBy(o => o.ConditionName).ToList();

                return View(patientChronicCondition);
            }
        }

        [HttpGet]
        public IActionResult ConfirmDelete(int id)
        {
            if (HttpContext.Session.GetInt32("PatientID") == null)
            {
                return RedirectToAction("Account", "Login", new { area = "" });
            }

            var conditions = _context.Patient_ChronicConditions.Where(p => p.PatientChronicConditionID == id).Include(c => c.ChronicCondition);

            return View(conditions);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var condition = _context.Patient_ChronicConditions.Find(id);

            condition.Status = "In-Active";

            _context.Patient_ChronicConditions.Update(condition);
            _context.SaveChanges();

            return RedirectToAction("List", "PatientCondition", new { area = "Patient" });
        }
    }
}
