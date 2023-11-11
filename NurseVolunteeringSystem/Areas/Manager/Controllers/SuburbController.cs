using Microsoft.AspNetCore.Http;
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

        [Area("Manager")]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("Names") == null)
            {
                return RedirectToAction("Account", "Login", new { area = "" });
            }

            var suburbs = _context.Suburb
                .Where(s => s.Status == "Active")
                .Include(s => s.City)
                .ToList();

            ViewBag.Suburbs = suburbs;
            return View(suburbs);
        }

        [Area("Manager")]
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("Names") == null)
            {
                return RedirectToAction("Account", "Login", new { area = "" });
            }

            ViewBag.Cities = _context.City.ToList();
            return View();
        }
        [Area("Manager")]

        [HttpPost]
        public IActionResult Create(Suburb model)
        {
            if (HttpContext.Session.GetString("Names") == null)
            {
                return RedirectToAction("Account", "Login", new { area = "" });
            }

            if (ModelState.IsValid)
            {
                
                _context.Suburb.Add(model);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Cities = _context.City.ToList();
                return View(model);
            }

            
        }

        [Area("Manager")]
        public IActionResult Edit(int id)
        {
            if (HttpContext.Session.GetString("Names") == null)
            {
                return RedirectToAction("Account", "Login", new { area = "" });
            }

            var suburb = _context.Suburb.Include(s => s.City).FirstOrDefault(s => s.SuburbID == id);

            if (suburb == null)
            {
                return NotFound();
            }


            ViewBag.Cities = _context.City.ToList();
            return View(suburb);
        }

        [Area("Manager")]
        [HttpPost]
        public IActionResult Edit(Suburb model)
        {
            if (ModelState.IsValid)
            {
                

                _context.Suburb.Update(model);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Cities = _context.City.ToList();
                return View(model);
            }

           
        }

        [Area("Manager")]
        public IActionResult Details(int id)
        {
            if (HttpContext.Session.GetString("Names") == null)
            {
                return RedirectToAction("Account", "Login", new { area = "" });
            }

            var suburb = _context.Suburb.Include(s => s.City).FirstOrDefault(s => s.SuburbID == id);
            if (suburb == null)
            {
                return NotFound();
            }
            return View(suburb);
        }


        [Area("Manager")]
        public IActionResult Delete(int id)
        {
            if (HttpContext.Session.GetString("Names") == null)
            {
                return RedirectToAction("Account", "Login", new { area = "" });
            }

            var suburb = _context.Suburb.Include(s => s.City).FirstOrDefault(s => s.SuburbID == id);
            if (suburb == null)
            {
                return NotFound();
            }
            return View(suburb);
        }
        [Area("Manager")]
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
