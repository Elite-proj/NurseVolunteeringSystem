using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NurseVolunteeringSystem.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NurseVolunteeringSystem.Controllers
{
    public class CareContractController : Controller
    {
        private readonly AppDBContext _context;

        public CareContractController(AppDBContext context)
        {
            _context = context;
        }
        [Area("Patient")]
        public async Task<IActionResult> Index()
        {
            var careContracts = await _context.CareContract.Include(c => c.Suburb).ToListAsync();
            return View(careContracts);
        }

        [Area("Patient")]
        public IActionResult Create()
        {
            ViewBag.Suburbs = _context.Suburb.ToList();
            return View();
        }
        [Area("Patient")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CareContract careContract)
        {
            if (ModelState.IsValid)
            {
                careContract.ContractDate = DateTime.Today;
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
            return View(careContract);
       
        }

        [Area("Patient")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var careContract = await _context.CareContract.FindAsync(id);
            if (careContract == null)
            {
                return NotFound();
            }

            ViewBag.Suburbs = _context.Suburb.ToList();
            return View(careContract);
        }

        [Area("Patient")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CareContract careContract)
        {
            if (id != careContract.CareContractID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(careContract);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CareContractExists(careContract.CareContractID))
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
            ViewBag.Suburbs = _context.Suburb.ToList();
            return View(careContract);
        }

        [Area("Patient")]
        public async Task<IActionResult> Delete(int? id)
        {
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

        [Area("Patient")]
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
