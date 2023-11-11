using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NurseVolunteeringSystem.Areas.Patient.Models;
using NurseVolunteeringSystem.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NurseVolunteeringSystem.Controllers
{
    [Area("Patient")]
    public class CareContractController : Controller
    {
        private readonly AppDBContext _context;
        
        public CareContractController(AppDBContext context)
        {
            _context = context;
        }
        
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetInt32("PatientID") == null)
            {
                return RedirectToAction("Account", "Login", new { area = "" });
            }

            int PatientID = int.Parse(HttpContext.Session.GetInt32("PatientID").ToString());
            var careContracts = await _context.CareContract.Where(c=> c.DeleteStatus=="Active" && c.PatientID==PatientID).Include(s => s.Suburb).OrderBy(o=>o.CareContractID).ToListAsync();
            return View(careContracts);
        }

        
        public IActionResult Create()
        {
            if (HttpContext.Session.GetInt32("PatientID") == null)
            {
                return RedirectToAction("Account", "Login", new { area = "" });
            }

            ViewBag.Suburbs = _context.Suburb.ToList();
            return View();
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddCareContractVM contractVM)
        {

            if (ModelState.IsValid)
            {
                CareContract careContract = new CareContract();

                careContract.AddressLine1 = contractVM.AddressLine1;
                careContract.AddressLine2 = contractVM.AddressLine2;
                careContract.SuburbID = contractVM.SuburbID;
                careContract.WoundDescription = contractVM.WoundDescription;

                careContract.ContractDate = DateTime.Today;
                careContract.DeleteStatus = "Active";
                careContract.ContractStatus = "N";
                careContract.PatientID = int.Parse(HttpContext.Session.GetInt32("PatientID").ToString());
                _context.CareContract.Add(careContract);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            foreach (var modelState in ModelState.Values)
            {
                foreach (var error in modelState.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.ErrorMessage);
                }
            }

            ViewBag.Suburbs = _context.Suburb.ToList();
            return View(contractVM);
       
        }

        
        public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Session.GetInt32("PatientID") == null)
            {
                return RedirectToAction("Account", "Login", new { area = "" });
            }

            if (id == null)
            {
                return NotFound();
            }

            var careContract = await _context.CareContract.FindAsync(id);

            AddCareContractVM contractVM = new AddCareContractVM();

            contractVM.ContractID = careContract.CareContractID;
            contractVM.AddressLine1 = careContract.AddressLine1;
            contractVM.AddressLine2 = careContract.AddressLine2;
            contractVM.SuburbID = careContract.SuburbID;
            contractVM.WoundDescription = careContract.WoundDescription;

            contractVM.ContractDate = careContract.ContractDate;
            contractVM.StartCareDate = careContract.StartCareDate;
            contractVM.EndCareDate = careContract.EndCareDate;
            contractVM.ContractStatus = careContract.ContractStatus;
            contractVM.DeleteStatus = careContract.DeleteStatus;
            contractVM.PatientID = careContract.PatientID;
            contractVM.NurseID = careContract.NurseID;

            if (careContract == null)
            {
                return NotFound();
            }

            ViewBag.Suburbs = _context.Suburb.ToList();
            return View(contractVM);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AddCareContractVM contractVM)
        {
            

            if (id != contractVM.ContractID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    CareContract careContract = new CareContract();
                    careContract.CareContractID = contractVM.ContractID;
                    careContract.AddressLine1 = contractVM.AddressLine1;
                    careContract.AddressLine2 = contractVM.AddressLine2;
                    careContract.SuburbID = contractVM.SuburbID;
                    careContract.WoundDescription = contractVM.WoundDescription;

                    careContract.ContractDate = contractVM.ContractDate;
                    careContract.StartCareDate = contractVM.StartCareDate;
                    careContract.EndCareDate = contractVM.EndCareDate;
                    careContract.ContractStatus = contractVM.ContractStatus;
                    careContract.DeleteStatus = contractVM.DeleteStatus;
                    careContract.PatientID = contractVM.PatientID;
                    careContract.NurseID = contractVM.NurseID;

                    _context.CareContract.Update(careContract);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CareContractExists(contractVM.ContractID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewBag.Suburbs = _context.Suburb.ToList();
                return View(contractVM);
            }

           
            
        }

        
        public async Task<IActionResult> Delete(int? id)
        {
            if (HttpContext.Session.GetInt32("PatientID") == null)
            {
                return RedirectToAction("Account", "Login", new { area = "" });
            }

            if (id == null)
            {
                return NotFound();
            }

            var careContract = await _context.CareContract
                .Include(c => c.Suburb)
                .FirstOrDefaultAsync(m => m.CareContractID == id);

            if (careContract == null)
            {
                return NotFound();
            }

            return View(careContract);
        }

       
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var careContract = await _context.CareContract.FindAsync(id);
            careContract.DeleteStatus = "Deleted";
            _context.CareContract.Update(careContract);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CareContractExists(int id)
        {
            return _context.CareContract.Any(e => e.CareContractID == id);
        }
    }
}
