using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NurseVolunteeringSystem.Areas.Nurse.Models;
using NurseVolunteeringSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NurseVolunteeringSystem.Areas.Nurse.Controllers
{
    [Area("Nurse")]
    public class CareContractController : Controller
    {
        private AppDBContext context { get; set; }

        public CareContractController(AppDBContext ctx)
        {
            //this._IConfiguration = config;
            this.context = ctx;
        }

        [HttpGet]
        public IActionResult ListContracts()
        {

            int id= int.Parse(HttpContext.Session.GetInt32("NurseID").ToString());

            var preferred = context.PrefferedSuburb.Where(p => p.NurseID == id && p.Status=="Active").ToList();

            List<CareContract> contracts = new List<CareContract>();

            
            foreach(var pref in preferred)
            {
                var getContract = context.CareContract.Where(c => c.SuburbID == pref.SuburbID && c.ContractStatus=="N").Include(s => s.Suburb);

                foreach(var cont in getContract)
                {
                    CareContract contract = new CareContract();

                    contract.CareContractID = cont.CareContractID;
                    contract.AddressLine1 = cont.AddressLine1;
                    contract.AddressLine2 = cont.AddressLine2;
                    contract.Suburb = cont.Suburb;
                    contract.ContractDate = cont.ContractDate;
                    contract.WoundDescription = cont.WoundDescription;
                    contract.ContractStatus = cont.ContractStatus;
                    contract.DeleteStatus = cont.DeleteStatus;
                    contract.EndCareDate = cont.EndCareDate;
                    contract.StartCareDate = cont.StartCareDate;
                    contract.NurseID = cont.NurseID;
                    contract.PatientID = cont.PatientID;
                    

                    contracts.Add(contract);
                }
            }

            

            return View(contracts);
        }

        [HttpGet]
        public IActionResult ListClosedContracts()
        {
            int id = int.Parse(HttpContext.Session.GetInt32("NurseID").ToString());

            var contracts = context.CareContract.Where(c => c.NurseID == id && c.ContractStatus=="C").Include(s => s.Suburb).OrderBy(o => o.CareContractID);

            return View(contracts);
        }

        public IActionResult ListClosedCareVisits(int id)
        {
            var careVisits = context.CareVisit.Where(c => c.CareContractID == id && c.Status == "Active").OrderBy(o => o.CareVisitID);

            return View(careVisits);
        }


        public IActionResult Choose(int id)
        {
            var contract = context.CareContract.Find(id);

            contract.NurseID= int.Parse(HttpContext.Session.GetInt32("NurseID").ToString());

            contract.ContractStatus = "A";

            context.CareContract.Update(contract);

            context.SaveChanges();

            return RedirectToAction("ListAssignedContracts", "CareContract", new { area = "Nurse" });
        }

        [HttpGet]
        public IActionResult ListAssignedContracts()
        {
            int id = int.Parse(HttpContext.Session.GetInt32("NurseID").ToString());

            var contracts = context.CareContract.Where(c => c.NurseID == id).Include(s => s.Suburb).OrderBy(o => o.CareContractID);

            return View(contracts);
        }

        [HttpGet]
        public IActionResult ListCareVisits(int id)
        {
            var careVisits = context.CareVisit.Where(c => c.CareContractID == id && c.Status=="Active").OrderBy(o => o.CareVisitID);

            return View(careVisits);
        }

        [HttpGet]
        public IActionResult AddCareVisit(int id)
        {
            ViewBag.ContractID = id;

            return View();
        }

        [HttpPost]
        public IActionResult AddCareVisit(CareVisit careVisit)
        {
            if(ModelState.IsValid)
            {
                careVisit.Status = "Active";
                context.CareVisit.Add(careVisit);

                context.SaveChanges();

                return RedirectToAction("ListAssignedContracts", "CareContract", new { area = "Nurse" });
            }
            else
            {
                return View(careVisit);
            }
        }


        [HttpGet]
        public IActionResult EditContract(int id)
        {
            var cont  = context.CareContract.Find(id);

            EditContractVM contract = new EditContractVM();

            contract.CareContractID = cont.CareContractID;
            contract.AddressLine1 = cont.AddressLine1;
            contract.AddressLine2 = cont.AddressLine2;
            contract.SuburbID = cont.SuburbID;
            contract.ContractDate = DateTime.Parse(cont.ContractDate.ToString());
            contract.WoundDescription = cont.WoundDescription;
            contract.ContractStatus = cont.ContractStatus;
            contract.DeleteStatus = cont.DeleteStatus;
            contract.EndCareDate = cont.EndCareDate;
            contract.StartCareDate = cont.StartCareDate;
            contract.NurseID = cont.NurseID;
            contract.PatientID = cont.PatientID;

            return View(contract);
        }

        [HttpPost]
        public IActionResult EditContract(EditContractVM cont)
        {
            CareContract contract = new CareContract();

            contract.CareContractID = cont.CareContractID;
            contract.AddressLine1 = cont.AddressLine1;
            contract.AddressLine2 = cont.AddressLine2;
            contract.SuburbID = cont.SuburbID;
            contract.ContractDate = cont.ContractDate;
            contract.WoundDescription = cont.WoundDescription;
            contract.ContractStatus = cont.ContractStatus;
            contract.DeleteStatus = cont.DeleteStatus;
            contract.EndCareDate = cont.EndCareDate;
            contract.StartCareDate = cont.StartCareDate;
            contract.NurseID = cont.NurseID;
            contract.PatientID = cont.PatientID;

            context.CareContract.Update(contract);
            context.SaveChanges();

            return RedirectToAction("ListAssignedContracts", "CareContract", new { area = "Nurse" });
        }

        
        public IActionResult CloseContract(int id)
        {
            var contract = context.CareContract.Find(id);

            contract.ContractStatus = "C";

            context.CareContract.Update(contract);
            context.SaveChanges();

            return RedirectToAction("ListAssignedContracts", "CareContract", new { area = "Nurse" });
        }
    }
}
