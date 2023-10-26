using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NurseVolunteeringSystem.Models;
using System.Linq;

namespace NurseVolunteeringSystem.Controllers
{
    public class SuburbController : Controller
    {
        private readonly AppDBContext _context;

        public SuburbController(AppDBContext context)
        {
            _context = context;
        }

        
        public IActionResult Index()
        {
            var suburbs = _context.Suburb
                .Where(s => s.Status == "Active")
                .Include(s => s.City)
                .ToList();

            ViewBag.Suburbs = suburbs;
            return View(suburbs);
        }

        
        public IActionResult Create()
        {
            ViewBag.Cities = _context.City.ToList();
            return View();
        }

        
        [HttpPost]
        public IActionResult Create(Suburb model)
        {
            if (ModelState.IsValid)
            {
                var existingCity = _context.City.FirstOrDefault(c => c.CityName == model.City.CityName);

                if (existingCity != null)
                {
                    model.City = existingCity; 
                }

                _context.Suburb.Add(model);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Cities = _context.City.ToList();
            return View(model);
        }

        
        public IActionResult Edit(int id)
        {
            var suburb = _context.Suburb.Include(s => s.City).FirstOrDefault(s => s.SuburbID == id);

            if (suburb == null)
            {
                return NotFound();
            }

            ViewBag.Cities = _context.City.ToList();
            return View(suburb);
        }

       
        [HttpPost]
        public IActionResult Edit(Suburb model)
        {
            if (ModelState.IsValid)
            {
                var existingCity = _context.City.FirstOrDefault(c => c.CityName == model.City.CityName);

                if (existingCity != null)
                {
                    model.City = existingCity; 
                }

                _context.Suburb.Update(model);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Cities = _context.City.ToList();
            return View(model);
        }

        
        public IActionResult Details(int id)
        {
            var suburb = _context.Suburb.Include(s => s.City).FirstOrDefault(s => s.SuburbID == id);
            if (suburb == null)
            {
                return NotFound();
            }
            return View(suburb);
        }

        

        public IActionResult Delete(int id)
        {
            var suburb = _context.Suburb.Include(s => s.City).FirstOrDefault(s => s.SuburbID == id);
            if (suburb == null)
            {
                return NotFound();
            }
            return View(suburb);
        }
        
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var suburb = _context.Suburb.Find(id);
            if (suburb != null)
            {
                suburb.Status = "In-Active";
                _context.Suburb.Update(suburb);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
