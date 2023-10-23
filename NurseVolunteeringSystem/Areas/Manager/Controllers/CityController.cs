using Microsoft.AspNetCore.Mvc;
using System.Linq;
using NurseVolunteeringSystem.Models;
using NurseVolunteeringSystem;

public class CityController : Controller
{
    private readonly AppDBContext _context;

    public CityController(AppDBContext context)
    {
        _context = context;
    }

    [Area("Manager")]
    public IActionResult Index()
    {
        var cities = _context.City.Where(c => c.Status == "Active").ToList();
        return View(cities);
    }

    [Area("Manager")]
    public IActionResult Create()
    {
        return View();
    }

    [Area("Manager")]
    [HttpPost]
    public IActionResult Create(City model)
    {
        if (ModelState.IsValid)
        {
            _context.City.Add(model);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(model);
    }

    [Area("Manager")]

    public IActionResult Edit(int id)
    {
        var city = _context.City.Find(id);
        if (city == null)
        {
            return NotFound();
        }
        return View(city);
    }

    [Area("Manager")]

    [HttpPost]
    public IActionResult Edit(City model)
    {
        if (ModelState.IsValid)
        {
            _context.City.Update(model);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(model);
    }

    [Area("Manager")]

    public IActionResult Details(int id)
    {
        var city = _context.City.Find(id);
        if (city == null)
        {
            return NotFound();
        }
        return View(city);
    }

    [Area("Manager")]
    public IActionResult Delete(int id)
    {
        var city = _context.City.Find(id);
        if (city == null)
        {
            return NotFound();
        }
        return View(city);
    }

    [Area("Manager")]
    [HttpPost, ActionName("Delete")]
    public IActionResult DeleteConfirmed(int id)
    {
        var city = _context.City.Find(id);
        if (city != null)
        {
            city.Status = "In-Active";
            _context.City.Update(city);
            _context.SaveChanges();
        }
        return RedirectToAction("Index");
    }
}
