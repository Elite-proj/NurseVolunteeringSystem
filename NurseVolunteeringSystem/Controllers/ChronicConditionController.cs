using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using NurseVolunteeringSystem.Models;
using NurseVolunteeringSystem;

public class ChronicConditionController : Controller
{
 
    private readonly AppDBContext _context;

    public ChronicConditionController(AppDBContext context)
    {
        _context = context;
    }

    
  
    public IActionResult Index()
    {
        var chronicConditions = _context.ChronicCondition.Where(c => c.Status == "Active").ToList();
        return View(chronicConditions);
    }

   

    public IActionResult Create()
    {
        return View();
    }

    

    [HttpPost]
    public IActionResult Create(ChronicCondition model)
    {
        if (ModelState.IsValid)
        {
            _context.ChronicCondition.Add(model);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(model);
    }


    public IActionResult Edit(int id)
    {
        var condition = _context.ChronicCondition.Find(id);
        if (condition == null)
        {
            return NotFound();
        }
        return View(condition);
    }

    

    [HttpPost]
    public IActionResult Edit(ChronicCondition model)
    {
        if (ModelState.IsValid)
        {
            _context.ChronicCondition.Update(model);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(model);
    }


    public IActionResult Details(int id)
    {
        var condition = _context.ChronicCondition.Find(id);
        if (condition == null)
        {
            return NotFound();
        }
        return View(condition);
    }

    
    public IActionResult Delete(int id)
    {
        var condition = _context.ChronicCondition.Find(id);
        if (condition == null)
        {
            return NotFound();
        }
        return View(condition);
    }

    

    [HttpPost, ActionName("Delete")]
    public IActionResult DeleteConfirmed(int id)
    {
        var condition = _context.ChronicCondition.Find(id);
        if (condition != null)
        {
            condition.Status = "In-Active";
            _context.ChronicCondition.Update(condition);
            _context.SaveChanges();
        }
        return RedirectToAction("Index");
    }

    
}

